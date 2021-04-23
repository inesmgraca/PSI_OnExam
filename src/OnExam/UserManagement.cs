using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnExam
{
    public static class UserManagement
    {
        public static bool UserLogin(string name, string pass)
        {
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
                var conn = new SqlConnection(connString);

                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "select * from Users where (Email=@name or Username=@name) and Password=@pass;";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@pass", pass);

                var dr = cmd.ExecuteReader();

                conn.Close();
                conn.Dispose();

                if (dr.HasRows)
                    return true;
                else
                    MessageBox.Show("As informações de login estão incorretas!", "Login incorreto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Mensagem de erro: " + ex.Message, "Erro de Base de dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensagem de erro: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
