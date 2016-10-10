using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EjemploWebForm.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace EjemploWebForm.DAL
{
    public class UsuarioRepositoryImp : UsuarioRepository
    {
        private string ConexionString= ConfigurationManager.ConnectionStrings["GESTLIBRERIA"].ConnectionString;
        public int create(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void delete(Guid codigo)
        {
            throw new NotImplementedException();
        }

        public IList<Usuario> getAll()
        {
            IList<Usuario> usuarios = null;
            const string SQL = "getAllUsuarios";
            using(SqlConnection conexion= new SqlConnection(ConexionString))
            {
                SqlCommand cmd = new SqlCommand(SQL, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conexion.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows){
                        usuarios=new List<Usuario>();
                        Usuario usuario = null;
                        while (reader.Read())
                        {
                            usuario = parseUsuario(reader);
                            usuarios.Add(usuario);
                        }

                    }
                }
            }

            return usuarios;
        }

        public Usuario getById(Guid codigo)
        {
            Usuario usuario=null;
            const string SQL = "getUsuarioById";
            using (SqlConnection conexion = new System.Data.SqlClient.SqlConnection())
            {
                SqlCommand command = new SqlCommand(SQL, conexion);
                command.CommandText = SQL;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", codigo);
                command.Connection = conexion;
                conexion.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usuario = parseUsuario(reader);
                        }
                    }
                }
            }
            return usuario;
        }

        private Usuario parseUsuario(SqlDataReader reader)
        {
            Usuario usuario = new Models.Usuario();
            usuario.CodigoUsuario = new Guid(reader["id"].ToString());
            usuario.Nombre = reader["nombre"].ToString();
            usuario.Apellidos = reader["apellidos"].ToString();
            usuario.Email = reader["email"].ToString();
            usuario.NickName = reader["userId"].ToString();
            usuario.Password = reader["password"].ToString();
            usuario.FechaNacimiento = Convert.ToDateTime(reader["fNacimiento"].ToString());

            return usuario;
        }

        public Usuario update(Usuario usuario)
        {
            Usuario u = null;
            const string SQL = "";
            using (SqlConnection conexion = new System.Data.SqlClient.SqlConnection())
            {
                SqlCommand command = conexion.CreateCommand();
            }

            return u;
        }
    }
}