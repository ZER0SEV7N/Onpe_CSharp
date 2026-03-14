namespace Onpe.Models
{
    //Clase modelo DTO para representar al grupo de votacion del procedimiento almacenado.
    public class GrupoVotacion
    {
        //Datos de ubicacion
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string LocalVotacion { get; set; }
        public string Direccion { get; set; }

        //Datos de las actas
        public int idLocalVotacion { get; set; }
        public string idGrupoVotacion { get; set; }
        public string nCopia { get; set; }
        public int idEstadoActa { get; set; }

        //Resultados Numericos
        public int ElectoresHabiles { get; set; }
        public int TotalVotantes { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int VotosBlancos { get; set; }
        public int VotosNulos { get; set; }
        public int VotosImpugnados { get; set; }
        public bool Valido { get; set; }

        //Constructor vacio para la clase modelo
        public GrupoVotacion() { }

        //Constructor para la clase modelo con parametros
        public GrupoVotacion(string[] aRegistro)
        {
            //Validar las actas
            Valido = aRegistro != null;
            if (!Valido) return;

            //Asignar los valores
            Departamento = aRegistro[0];
            Provincia = aRegistro[1];
            Distrito = aRegistro[2];
            LocalVotacion = aRegistro[3];
            Direccion = aRegistro[4];
            idLocalVotacion = int.Parse(aRegistro[5]);
            idGrupoVotacion = aRegistro[6];
            nCopia = aRegistro[7];
            idEstadoActa = int.Parse(aRegistro[8]);
            ElectoresHabiles = int.Parse(aRegistro[9]);
            TotalVotantes = int.Parse(aRegistro[10]);
            P1 = int.Parse(aRegistro[11]);
            P2 = int.Parse(aRegistro[12]);
            VotosBlancos = int.Parse(aRegistro[13]);
            VotosNulos = int.Parse(aRegistro[14]);
            VotosImpugnados = int.Parse(aRegistro[15]);
        }
    }
}
