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
                string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;
                string SQL = "SELECT * FROM usuarios";
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
            //int codigo = 
            switch (comand)
            {
                case "editUsuario":
                    //hemos pulsado el boton de editar;
                    /*
                     {
                     lblIdUsuario.Text=codigo;
                     System.Text.StringBuilder sb= new System.Text.StringBuilder();
                     sb.Append(@"<script>");
                     sb.Append("$('#editModal').modal('show')");
                     sb.Append(@"</script>");
                     }
                     */
                    break;
                case "deleteUsuario":
                    string cadenaConexion= ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;
                    string codigo = grdv_Usuarios.DataKeys[index].Value.ToString();
                    txtIdUsuario.Text = codigo;
                    string SQL = "DELETE FROM usuarios WHERE codigoUsuario=" + codigo;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script>");
                    sb.Append("$('#deleteConfirm').modal('show')");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ConfirmarBorrado", sb.ToString(), false);
                    SqlConnection conn = null;
                    try
                    {
                        conn = new SqlConnection(cadenaConexion);
                        conn.Open();
                        SqlCommand sqlcmm = new SqlCommand(SQL);
                        sqlcmm.Connection = conn;
                        sqlcmm.CommandText = SQL;
                        sqlcmm.CommandType = CommandType.Text;
                        //sqlcmm.CommandType = CommandType.StoredProcedure;
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

                    break;
            }
        }

        private void cancelDelete()
        {

        }

        private  void deleteUsuario()
        {

        }
    }
}