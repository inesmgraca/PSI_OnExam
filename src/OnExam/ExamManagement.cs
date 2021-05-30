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
        private static string _error;
        public static string error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = Properties.Resources.ResourceManager.GetString("error");
            }
        }

        private static string _errorDB;
        public static string errorDB
        {
            get
            {
                return _errorDB;
            }
            set
            {
                _errorDB = Properties.Resources.ResourceManager.GetString("errorDB");
            }
        }

        private static string _errorMessage;
        public static string errorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = Properties.Resources.ResourceManager.GetString("errorMessage");
            }
        }

        public static DataSet ExamsView()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

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
            }
            catch (SqlException ex)
            {
                MessageBox.Show(errorMessage + ex.Message, errorDB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorMessage + ex.Message, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return null;
        }


    }
}
