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
        public static string UserLoggedIn { get; set; }

        public static string UserName { get; set; }

        public static string UserEmail { get; set; }

        public static bool CheckUsername(string name)
        {
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
                    return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return false;
        }

        public static bool UserLogin(string name, string pass)
        {
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
                        return true;
                    }
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("invalidLogin"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return false;
        }

        public static bool UserSignUp(string nome, string email, string username, string password)
        {
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
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorEmail"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("cantSignUp"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return false;
        }

        public static void UserProfile()
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
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool UserUpdate(string nome, string email, string username)
        {
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
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorEmail"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        return true;
                    else
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("cantUpdate"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return false;
        }

        public static bool UserUpdatePass(string password)
        {
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
                    return true;
                else
                    MessageBox.Show("Não foi possível atualizar!", Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage") + ex.Message, Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return false;
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
