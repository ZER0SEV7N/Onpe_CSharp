namespace Onpe.Models
{
    //Clase modelo DTO para representar la participacion ciudadana
    public class Participacion
    {
        //Datos DPD de la ubicacion por Zona = Departamento, Provincia, Distrito
        public string DPD { get; set; }

        //TV = Total de Votantes
        public int TV { get; set; }

        //PTV = Porcentaje de Total de Votantes
        public string PTV { get; set; }

        //TA = Total de ausentes
        public int TA { get; set; }

        //PTA = Porcentaje de Total de Ausentes
        public string PTA { get; set; }

        //EH = Electores Habiles
        public int EH { get; set; }

        //Constructor vacio para la clase modelo
        public Participacion() { }

        public Participacion(string[] aRegistro)
        {
            if (aRegistro == null) return;
            DPD = aRegistro[0];
            TV = int.Parse(aRegistro[1]);
            PTV = aRegistro[2];
            TA = int.Parse(aRegistro[3]);
            PTA = aRegistro[4];
            EH = int.Parse(aRegistro[5]);
        }

        //Metodo para procesar la lista de registros
        public List<Participacion> getList(string[][] mRegistros)
        {
            if (mRegistros == null) return null;

            List<Participacion> lstParticipacion = new List<Participacion>();
            foreach (string[] aRegistro in mRegistros)
                lstParticipacion.Add(new Participacion(aRegistro));
            return lstParticipacion;
        }
    }
}
