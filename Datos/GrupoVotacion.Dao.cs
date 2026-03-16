using Onpe.Database;
using Onpe.Models;
using Microsoft.Extensions.Configuration;
namespace Onpe.Datos
{
    //Clase DAO para el manejo de los datos del grupo de votacion.
    public class daoGrupoVotacion
    {
        private readonly ConexionDB db;

        public daoGrupoVotacion(IConfiguration configuration)
        {
            db = new ConexionDB(configuration, "CadenaSQLProfesor");
        }

        //Metodo para obtener un acta de votacion por su id
        public GrupoVotacion getActa(string idGrupoVotacion)
        {
            db.Setencia($"exec usp_getGrupoVotacion '{idGrupoVotacion}'");
            string[] registro = db.getRegistro();

            //Si el registro es nulo, devolvemos null
            if (registro == null) return null;

            return new GrupoVotacion(registro);
        }

        //Metodos para los combos en cascada (UBIGEO)

        //Metodo para obtener todos los departamentos
        public List<object> getDepartamentos(int inicio, int fin)
        {
            string query = $"select * from Departamento where idDepartamento BETWEEN {inicio} and {fin}";

            //db.Setencia($"exec usp_getDepartamentos {inicio}, {fin}");
            db.Setencia(query);
            var lista = new List<object>();
            var registros = db.getRegistros();
            if (registros != null)
                foreach (var a in registros) lista.Add(new { id = a[0], text = a[1].Trim() });
            return lista;
        }

        //Metodo para obtener las provincias de un departamento
        public List<object> getProvincias(int idProvincia)
        {
            db.Setencia($"exec usp_getProvincias {idProvincia}");
            var lista = new List<object>();
            var registros = db.getRegistros();
            if (registros != null)
                foreach (var a in registros) lista.Add(new { id = a[0], text = a[1].Trim() });
            return lista;
        }

        //Metodo para obtener los distritos de una provincia
        public List<object> getDistritos(int idProvincia)
        {
            db.Setencia($"exec usp_getDistritos {idProvincia}");
            var lista = new List<object>();
            var registros = db.getRegistros();
            if (registros != null)
                foreach (var a in registros) lista.Add(new { id = a[0], text = a[1].Trim() });
            return lista;
        }

        //Metodo para obtener los locales de votacion
        public List<object> getLocales(int idDistrito)
        {
            db.Setencia($"exec usp_getLocalesVotacion {idDistrito}");
            var lista = new List<object>();
            var registros = db.getRegistros();
            if (registros != null)
                foreach (var a in registros) lista.Add(new { id = a[0], text = a[1].Trim() });
            return lista;
        }

        //Metodo para obtener las mesas de votacion
        public List<object> getGrupoVotacion(int idLocal)
        {
            db.Setencia($"exec usp_getGruposVotacion {idLocal}");
            var lista = new List<object>();
            var registros = db.getRegistros();
            if (registros != null)
                foreach (var a in registros) lista.Add(new { id = a[0].Trim(), text = a[0].Trim() });
            return lista;
        }
    }
}
