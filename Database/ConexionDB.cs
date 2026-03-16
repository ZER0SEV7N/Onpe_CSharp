using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Onpe.Database
{
    //Clase para manejar la conexion a la base de datos
    public class ConexionDB
    {

        //Definir la cadena de conexion
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;

        //Constructor para inicializar la conexion a la base de datos
        public ConexionDB(IConfiguration configuration, string bd)
        {
            cn = new SqlConnection(configuration.GetConnectionString(bd));
            cmd = new SqlCommand("", cn);
            da = new SqlDataAdapter(cmd);
        }

        //Metodo para las consultas
        internal void Setencia(string SQL)
        {
            cmd.CommandText = SQL;
            cmd.Parameters.Clear();
        }

        //Metodo para obtener un dataTable
        internal DataTable getDataTable()
        {
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Metodo para obtener un registro
        internal String[] getRegistro()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;
            return System.Array.ConvertAll(dt.Rows[0].ItemArray, x => x?.ToString()?.Trim() ?? "");
        }

        //Metodo para obtener varios registros
        internal string[][]? getRegistros()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;
            int i = 0;
            string[][] mRegistros = new string[dt.Rows.Count][];
            foreach (DataRow dr in dt.Rows)
                mRegistros[i++] = System.Array.ConvertAll(dr.ItemArray, x => x?.ToString()?.Trim() ?? "");
            return mRegistros;
        }
    }
}
