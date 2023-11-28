using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2Registro.Clases
{
    public class Equipo
        {
            public int EquipoID { get; set; }
            public string TipoEquipo { get; set; }
            public string Modelo { get; set; }
            public int UsuarioID { get; set; }

            public Equipo() { }

            public Equipo(int equipoID, string tipoEquipo, string modelo, int usuarioID)
            {
                EquipoID = equipoID;
                TipoEquipo = tipoEquipo;
                Modelo = modelo;
                UsuarioID = usuarioID;
            }

            public Equipo(string tipoEquipo, string modelo, int usuarioID)
            {
                TipoEquipo = tipoEquipo;
                Modelo = modelo;
                UsuarioID = usuarioID;
            }

            public int AgregarEquipo()
            {
                int retorno = 0;

                using (SqlConnection Conn = DBConn.ObtenerConexion())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("IngresarEquipo", Conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new SqlParameter("@TipoEquipo", TipoEquipo));
                        cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                        cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));

                        retorno = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        retorno = -1;
                        Console.WriteLine("Error al agregar equipo: " + ex.Message);
                    }
                }

                return retorno;
            }

            public int BorrarEquipo()
            {
                int retorno = 0;

                using (SqlConnection Conn = DBConn.ObtenerConexion())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("BorrarEquipo", Conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new SqlParameter("@EquipoID", EquipoID));

                        retorno = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        retorno = -1;
                        Console.WriteLine("Error al borrar equipo: " + ex.Message);
                    }
                }

                return retorno;
            }

            public int ModificarEquipo()
            {
                int retorno = 0;

                using (SqlConnection Conn = DBConn.ObtenerConexion())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("ActualizarEquipo", Conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new SqlParameter("@EquipoID", EquipoID));
                        cmd.Parameters.Add(new SqlParameter("@TipoEquipo", TipoEquipo));
                        cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                        cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));

                        retorno = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        retorno = -1;
                        Console.WriteLine("Error al modificar equipo: " + ex.Message);
                    }
                }

                return retorno;
            }

            public static List<Equipo> ConsultarEquipos()
            {
                List<Equipo> equipos = new List<Equipo>();

                using (SqlConnection Conn = DBConn.ObtenerConexion())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("ConsultarEquipo", Conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Equipo equipo = new Equipo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                                equipos.Add(equipo);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al consultar equipos: " + ex.Message);
                    }
                }

                return equipos;
            }

            public static List<Equipo> ConsultarEquipoPorID(int id)
            {
                List<Equipo> equipos = new List<Equipo>();

                using (SqlConnection Conn = DBConn.ObtenerConexion())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("ConsultarEquipoFiltro", Conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new SqlParameter("@EquipoID", id));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Equipo equipo = new Equipo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                                equipos.Add(equipo);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al consultar equipo por ID: " + ex.Message);
                    }
                }

                return equipos;
            }
        }
    }