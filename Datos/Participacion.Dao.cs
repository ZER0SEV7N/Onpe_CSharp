using Onpe.Database;
using Onpe.Models;
using Microsoft.Extensions.Configuration;
namespace Onpe.Datos
{
    public class daoParticipacion
    {
        private readonly ConexionDB db;

        //Constructor para inicializar la conexion a la base de datos
        public daoParticipacion(IConfiguration configuration)
        {
            db =  new ConexionDB(configuration, "CadenaSQLAzure");
        }

        //Metodo para obtener la participacion a nivel Nacional (Departamento = 1 / 25)
        public List<Participacion> getNacional(int inicio = 1, int fin = 25)
        {
            db.Setencia($"exec usp_getVotos {inicio}, {fin}");
            return new Participacion().getList(db.getRegistros());
        }

        //Metodo para obtener la participacion de un departamento en especifico (Provincia = 1 / 195)
        public List<Participacion> getPorDepartamento(string Departamento)
        {
            db.Setencia($"exec usp_getVotosDepartamento '{Departamento}'");
            return new Participacion().getList(db.getRegistros());
        }

        //Metodo para obtener la participacion de una provincia en especifico
        public List<Participacion> getPorProvincia(string Provincia)
        {
            db.Setencia($"exec usp_getVotosProvincia '{Provincia}'");
            return new Participacion().getList(db.getRegistros());
        }

        //Metodo para obtener la participacion de un distrito en especifico
        public List<Participacion> getPorDistrito(string Distrito)
        {
            //Consulta para obtener la participacion de un distrito en especifico, se le pasa el nombre del distrito como parametro
            string query = $@"SELECT TRIM(LV.RazonSocial) AS 'DPD', 
                                     SUM(GV.TotalVotantes) AS 'TV',
                                     CONCAT(CAST((SUM(GV.TotalVotantes) * 100.0 / SUM(GV.ElectoresHabiles)) AS DECIMAL(8,3)), '%') AS 'PTV',
                                     SUM(GV.ElectoresHabiles) - SUM(GV.TotalVotantes) 'TA',
                                     CONCAT(CAST(((SUM(GV.ElectoresHabiles) - SUM(GV.TotalVotantes)) * 100.0 / SUM(GV.ElectoresHabiles)) AS decimal(8,3)), ' %') 'PTA',
                                     SUM(GV.ElectoresHabiles) 'EH'   
                                FROM GrupoVotacion GV
                                INNER JOIN LocalVotacion LV ON GV.idLocalVotacion = LV.idLocalVotacion
                                INNER JOIN Distrito DI ON LV.idDistrito = DI.idDistrito
                                WHERE DI.Detalle = '{Distrito}' 
                                GROUP BY LV.RazonSocial";
            //Utilizando procedimiento almacenado
            //db.Setencia($"exec usp_getVotosDistrito '{Distrito}'");
            //Ejecutando la consulta
            db.Setencia(query);
            return new Participacion().getList(db.getRegistros());
        }
    }
}
