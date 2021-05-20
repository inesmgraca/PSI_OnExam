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

        public static bool CheckUsername(string name)
        {
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
                var conn = new SqlConnection(connString);

                conn.Open();

                var cmd = new SqlCommand("SearchUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Name", name);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                    return true;

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

            return false;
        }

        public static bool UserLogin(string name, string pass)
        {
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
                var conn = new SqlConnection(connString);

                conn.Open();

                var cmd = new SqlCommand("SearchUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Name", name);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var passOriginal = "";
                    var saltOriginal = "";

                    while (dr.Read())
                    {
                        passOriginal = dr["Password"].ToString();
                        saltOriginal = dr["Salt"].ToString();
                    }

                    if (PassCompare(passOriginal, saltOriginal, pass))
                        return true;
                }
                else
                    MessageBox.Show("As informações de login estão incorretas!", "Login incorreto!", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

            return false;
        }

        public static bool UserSignUp(string nome, string email, string username, string password)
        {
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
                var conn = new SqlConnection(connString);

                conn.Open();

                var cmd = new SqlCommand("select Email from Users where Email = @email;", conn);
                cmd.Parameters.AddWithValue("@email", email);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show("Já existe um utilizador com este email!", "Email incorreto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        return true;
                    else
                        MessageBox.Show("Não foi possível realizar o registo!", "Erro ao registar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
