using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace EjemploWebForm
{ 
    public partial class index : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            cargaDatos();
        }

        private void cargaDatos()
        {
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIA"].ConnectionString;
                string SQL = "SELECT id as codigoUsuario, nombre as nombreUsuario FROM usuario WHERE borrado=0";
                SqlConnection conn = new SqlConnection(cadenaConexion);
                conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter dAdapter = new SqlDataAdapter(SQL, conn);
                dAdapter.Fill(ds);
                dt = ds.Tables[0];

                grdv_Usuarios.DataSource = dt;
                grdv_Usuarios.DataBind();
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.Error.Write(ex.Message);
            }
        }

        protected void grdv_Usuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string comand = e.CommandName;
            int index = Convert.ToInt32(e.CommandArgument);
            string codigo = grdv_Usuarios.DataKeys[index].Value.ToString();
            int id = Int32.Parse(codigo);

            switch (comand)
            {
                case "editUsuario":
                    {
                        lblIdUsuario.Text = codigo;
                        SqlConnection conn = null;
                        SqlDataReader rdr = null;

                        try
                        {
                            string SQL = "getUsuarioByID";
                            string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIA"].ConnectionString;
                            conn = new SqlConnection(cadenaConexion);
                            SqlCommand cmd = new SqlCommand(SQL, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@pUsuarioId", id);
                            conn.Open();
                            rdr = cmd.ExecuteReader();
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    txtNombreUsuario.Text = rdr["nombreUsuario"].ToString();

                                    txtApellidos.Text = rdr["apellidosUsuario"].ToString();
                                    txtfNacimiento.Text = rdr["fNacimientoUsuario"].ToString();
                                    txtPassword.Text = rdr["passwordUsuario"].ToString();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se han encontrado registros");
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.Error.Write(ex.Message);
                        }
                        finally
                        {
                            rdr.Close();
                            conn.Close();
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script>");
                        sb.Append("$('#editModal').modal('show')");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ConfirmarEdit", sb.ToString(), false);
                        cargaDatos();
                    }
                    break;

                case "deleteUsuario":
                    {
                        txtIdUsuario.Text = codigo;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script>");
                        sb.Append("$('#deleteConfirm').modal('show')");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ConfirmarBorrado", sb.ToString(), false);
                        cargaDatos();
                    }
                    break;
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            string codigo = lblIdUsuario.Text;
            string nombre = txtNombreUsuario.Text;
            string apellidos = txtApellidos.Text;
            string fNacimiento = txtfNacimiento.Text;
            string password = txtPassword.Text;
            string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;
            int cod;
            string mensaje = "El registro se ha creado correctamente";
            string SQL = "INSERT INTO usuario(nombre, apellidos, password, fNacimiento) VALUES('" + nombre + "', '" + apellidos + "','" + password + "','" + fNacimiento + "')";
            if (Int32.TryParse(codigo, out cod) && cod > -1)
            {
                SQL = "UPDATE usuario SET nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',fNacimiento = '" + fNacimiento + "' WHERE id =" + codigo;
                mensaje = "La actualización se ha producido con éxito";
            }

            SqlConnection conn = null;
            System.Text.StringBuilder sb = null;
            try
            {
                conn = new SqlConnection(cadenaConexion);
                conn.Open();
                SqlCommand sqlcmm = new SqlCommand();
                sqlcmm.Connection = conn;
                sqlcmm.CommandText = SQL;
                //  sqlcmm.CommandType = CommandType.Text;
                //sqlcmm.CommandType = CommandType.StoredProcedure;
                sqlcmm.ExecuteNonQuery();
                cargaDatos();
                sb = new System.Text.StringBuilder();
                sb.Append(@"<script>");
                sb.Append("$('#editModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OcultarCreate", sb.ToString(), false);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                mensaje = "La operación no se ha podido realizar consulte con el administrador del sistema";
            }
            finally
            {
                conn.Close();
            }

            cargaDatos();
           
            sb.Append(@"<script>");
            sb.Append("$('#editModal').modal('hide')");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OcultarCreate", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIA"].ConnectionString;

            string codigo = txtIdUsuario.Text;
            string SQL = "UPDATE usuario SET borrado=1";
            try
            {
                conn = new SqlConnection(cadenaConexion);
                conn.Open();
                SqlCommand sqlcmm = new SqlCommand();
                sqlcmm.Connection = conn;
                sqlcmm.CommandText = SQL;
                sqlcmm.CommandType = CommandType.Text;
                // sqlcmm.CommandType = CommandType.StoredProcedure;
                sqlcmm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            cargaDatos();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script>");
            sb.Append("$('#deleteConfirm').modal('hide')");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OcultarCreate", sb.ToString(), false);
        }

        protected void btncrearUsuario_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string codigo = lblIdUsuario.Text;
            string nombre = txtNombreUsuario.Text;
            string apellidos = txtApellidos.Text;
            string fNacimiento = txtfNacimiento.Text;
            string password = txtPassword.Text;
            int borrado = 0;
            sb.Append(@"<script>");
            sb.Append("$('#crearEditarModal').text('Crear Usuario');");
            sb.Append("$('#editModal').modal('show')");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MostrarCreate", sb.ToString(), false);
        }
    }
}