using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2Registro.Clases
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        public Usuario() { }

        public Usuario(int id, string nom, string correo, string tel)
        {
            UsuarioID = id;
            Nombre = nom;
            CorreoElectronico = correo;
            Telefono = tel;
        }

        public Usuario(string nom, string correo, string tel)
        {
            Nombre = nom;
            CorreoElectronico = correo;
            Telefono = tel;
        }

        public int Agregar()
        {
            int retorno = 0;

            using (SqlConnection Conn = DBConn.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("IngresarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error al agregar usuario: " + ex.Message);
                }
            }

            return retorno;
        }

        public int Borrar()
        {
            int retorno = 0;

            using (SqlConnection Conn = DBConn.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BorrarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error al borrar usuario: " + ex.Message);
                }
            }

            return retorno;
        }

        public int Modificar()
        {
            int retorno = 0;

            using (SqlConnection Conn = DBConn.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ActualizarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error al modificar usuario: " + ex.Message);
                }
            }

            return retorno;
        }

        public static List<Usuario> ConsultaFiltro(int id)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            List<Usuario> Usuarios = new List<Usuario>();
            try
            {

                using (Conn = DBConn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ConsultarUsuarioFiltro", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", id));
                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario1 = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            Usuarios.Add(usuario1);
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return Usuarios;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return Usuarios;
        }

        public static List<Usuario> ObtenerUsuarios()
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            List<Usuario> Usuarios = new List<Usuario>();
            try
            {

                using (Conn = DBConn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ConsultarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario1 = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            Usuarios.Add(usuario1);
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return Usuarios;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return Usuarios;
        }
    }
}