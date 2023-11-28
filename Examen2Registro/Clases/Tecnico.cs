using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2Registro.Clases
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public Tecnico() { }

        public Tecnico(int tecnicoID, string nombre, string especialidad)
        {
            TecnicoID = tecnicoID;
            Nombre = nombre;
            Especialidad = especialidad;
        }

        public Tecnico(string nombre, string especialidad)
        {
            Nombre = nombre;
            Especialidad = especialidad;
        }

        public int AgregarTec()
        {
            int retorno = 0;

            using (SqlConnection Conn = DBConn.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("IngresarTecnico", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error al agregar técnico: " + ex.Message);
                }
            }

            return retorno;
        }

        public int BorrarTec()
        {
            int retorno = 0;

            using (SqlConnection Conn = DBConn.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BorrarTecnico", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoID));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error al borrar técnico: " + ex.Message);
                }
            }

            return retorno;
        }

        public int ModificarTec()
        {
            int retorno = 0;

            using (SqlConnection Conn = DBConn.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ActualizarTecnico", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoID));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error al modificar técnico: " + ex.Message);
                }
            }

            return retorno;
        }

        public static List<Tecnico> ConsultaFiltro(int id)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            List<Tecnico> Tecnicos = new List<Tecnico>();
            try
            {

                using (Conn = DBConn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ConsultarTecnicoFiltro", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", id));
                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tecnico Tecnico1 = new Tecnico(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            Tecnicos.Add(Tecnico1);
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return Tecnicos;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return Tecnicos;
        }

        public static List<Tecnico> ObtenerTecnicos()
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            List<Tecnico> Tecnicos = new List<Tecnico>();
            try
            {

                using (Conn = DBConn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ConsultarTecnicos", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tecnico Tecnico1 = new Tecnico(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            Tecnicos.Add(Tecnico1);
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return Tecnicos;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return Tecnicos;
        }
    }
}
