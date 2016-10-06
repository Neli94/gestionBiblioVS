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
        private const string conexionString= ConfigurationManager.ConnectionStrings["GESTLIBRERIA"].ConnectionString;
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
            throw new NotImplementedException();
        }

        public Usuario getById(Guid codigo)
        {
            Usuario usuario=null;
            const string SQL = "";
            using (SqlConnection conexion = new System.Data.SqlClient.SqlConnection())
            {
                SqlCommand command = conexion.CreateCommand();
            }
            return usuario;
        }

        public Usuario update(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}