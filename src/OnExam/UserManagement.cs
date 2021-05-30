using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace OnExam
{
    public static class UserManagement
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

        public static string UserLoggedIn { get; set; }
        public static string UserName { get; set; }
        public static string UserEmail { get; set; }

        public static bool CheckUsername(string name)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("SearchUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Name", name);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                    result = true;
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

            return result;
        }

        public static bool UserLogin(string name, string pass)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("SearchUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Name", name);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var username = "";
                    var passOriginal = "";
                    var saltOriginal = "";

                    while (dr.Read())
                    {
                        username = dr["Username"].ToString();
                        passOriginal = dr["Password"].ToString();
                        saltOriginal = dr["Salt"].ToString();
                    }

                    if (PassCompare(passOriginal, saltOriginal, pass))
                    {
                        UserLoggedIn = username;
                        result = true;
                    }
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("invalidLogin"), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return result;
        }

        public static bool UserSignUp(string nome, string email, string username, string password)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("select Email from Users where Email = @email;", conn);
                cmd.Parameters.AddWithValue("@email", email);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorEmail"), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new SqlCommand("AddUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@Name", nome);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Email", email);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Username", username);
                    cmd.Parameters.Add(param);

                    var pass = HashWithSalt(password);

                    param = new SqlParameter("@Password", pass.Hash);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Salt", pass.Salt);
                    cmd.Parameters.Add(param);

                    dr.Close();

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        UserLoggedIn = username;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("cantSignUp"), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

            return result;
        }

        public static void UserGetProfile()
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("GetUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", UserLoggedIn);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    UserName = dr["Name"].ToString();
                    UserEmail = dr["Email"].ToString();
                }

                conn.Close();
                conn.Dispose();
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
        }

        public static bool UserUpdateProfile(string nome, string email, string username)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("select Email from Users where Email = @email and Username != @username;", conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", UserLoggedIn);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorEmail"), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new SqlCommand("UpdateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@Name", nome);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Email", email);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@UsernameOld", UserLoggedIn);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@UsernameNew", username);
                    cmd.Parameters.Add(param);

                    dr.Close();

                    if (cmd.ExecuteNonQuery() == 1)
                        result = true;
                    else
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("cantUpdate"), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return result;
        }

        public static bool UserUpdatePass(string password)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("UpdateUserPass", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", UserLoggedIn);
                cmd.Parameters.Add(param);

                var pass = HashWithSalt(password);

                param = new SqlParameter("@Password", pass.Hash);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Salt", pass.Salt);
                cmd.Parameters.Add(param);

                if (cmd.ExecuteNonQuery() == 1)
                    result = true;
                else
                    MessageBox.Show("Não foi possível atualizar!", error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return result;
        }

        public static HashSalt HashWithSalt(string password)
        {
            var salt = new byte[16];
            var pass = Encoding.UTF8.GetBytes(password);
            new RNGCryptoServiceProvider().GetBytes(salt);

            var passSalt = new List<byte>();
            passSalt.AddRange(pass);
            passSalt.AddRange(salt);

            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(passSalt.ToArray());

            return new HashSalt(Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool PassCompare(string passOrig, string saltOrig, string password)
        {
            var salt = Convert.FromBase64String(saltOrig);
            var pass = Encoding.UTF8.GetBytes(password);

            var passSalt = new List<byte>();
            passSalt.AddRange(pass);
            passSalt.AddRange(salt);

            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(passSalt.ToArray());

            if (passOrig.Equals(Convert.ToBase64String(hash)))
                return true;
            else
                return false;
        }
    }

    public class HashSalt
    {
        public string Salt { get; }
        public string Hash { get; set; }

        public HashSalt(string salt, string hash)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}
