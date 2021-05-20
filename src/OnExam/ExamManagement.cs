using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OnExam.UserManagement;

namespace OnExam
{
    public static class ExamManagement
    {
        public static DataSet ExamsView()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            conn.Open();

            try
            {
                var cmd = new SqlCommand("ExamsView", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", UserLoggedIn);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var ds = new DataSet();
                    var dataTable = new DataTable("Results");
                    ds.Tables.Add(dataTable);
                    ds.Load(dr, LoadOption.PreserveChanges, ds.Tables["Results"]);
                    return ds;
                }

                conn.Close();
                conn.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Mensagem de erro: " + ex.Message, "Erro de Base de dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensagem de erro: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }


    }
}
