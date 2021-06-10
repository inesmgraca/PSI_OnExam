using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static OnExam.Properties.Resources;

namespace OnExam
{
    public static class UserManagement
    {
        public static string UserLoggedIn { get; set; }

        public static string UserName { get; set; }

        public static string UserEmail { get; set; }

        public static bool UserSearch(string name)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("UserSearch", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", name);

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                    return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var cmd = new SqlCommand("UserSearch", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", name);

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
                    MessageBox.Show(ResourceManager.GetString("invalidLogin"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool UserSignUp(string name, string email, string username, string password)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("select Email from Users where Email = @Email;", conn);
                cmd.Parameters.AddWithValue("@Email", email);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show(ResourceManager.GetString("errorEmail"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new SqlCommand("UserAdd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pass = HashWithSalt(password);

                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", pass.Hash);
                    cmd.Parameters.AddWithValue("@Salt", pass.Salt);

                    dr.Close();

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        UserLoggedIn = username;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(ResourceManager.GetString("cantSignUp"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var cmd = new SqlCommand("UserOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);

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
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool UserUpdate(string name, string email, string username)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("select Email from Users where Email = @Email and Username != @Username;", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                    MessageBox.Show(ResourceManager.GetString("errorEmail"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    cmd = new SqlCommand("UserUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@UsernameOld", UserLoggedIn);
                    cmd.Parameters.AddWithValue("@UsernameNew", username);

                    dr.Close();

                    if (cmd.ExecuteNonQuery() == 1)
                        return true;
                    else
                        MessageBox.Show(ResourceManager.GetString("cantUpdate"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var cmd = new SqlCommand("UserUpdatePass", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var pass = HashWithSalt(password);

                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);
                cmd.Parameters.AddWithValue("@Password", pass.Hash);
                cmd.Parameters.AddWithValue("@Salt", pass.Salt);

                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    MessageBox.Show(ResourceManager.GetString("cantUpdate"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("errorDB"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceManager.GetString("errorMessage") + ex.Message, ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
