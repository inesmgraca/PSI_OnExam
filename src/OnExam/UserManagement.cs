using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
                    var passOriginal = new byte[36];

                    while (dr.Read())
                        passOriginal = (byte[])dr["Password"];

                    //var passw = dr["Password"].ToString();
                    //var encoding = Encoding.GetEncoding(passw);
                    //var bytes = encoding.GetBytes(passw);

                    if (PassCompare(passOriginal, pass))
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

        public static bool UserRegister(string nome, string email, string username, string password)
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

                    var param = new SqlParameter("@Nome", nome);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Email", email);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Username", username);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Password", HashWithSalt(password));
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

        public static byte[] HashWithSalt(string password)
        {
            // salt(16) + hash(20) = 36
            var salt = new byte[16];
            var hashSalt = new byte[36];

            // gerar salt para hash, RNG gera números aleatórios
            new RNGCryptoServiceProvider().GetBytes(salt);

            // 10000 - nº de vezes que é feito o hashing
            var hashing = new Rfc2898DeriveBytes(password, salt, 10000);

            var hash = hashing.GetBytes(20);

            // pôr hash e salt no array de bytes hashSalt
            Array.Copy(salt, 0, hashSalt, 0, 16);
            Array.Copy(hash, 0, hashSalt, 16, 20);

            return hashSalt;
        }

        public static bool PassCompare(byte[] passOriginal, string passGiven)
        {
            // salt(16) + hash(20) = 36
            var salt = new byte[16];
            var passGiv = new byte[36];

            // atribuir salt da password original
            Array.Copy(passOriginal, 0, salt, 0, 16);

            // hashing da password dada
            var hashing = new Rfc2898DeriveBytes(passGiven, salt, 10000);

            var hash = hashing.GetBytes(20);

            // pôr hash e salt no array de bytes hashSalt
            Array.Copy(salt, 0, passGiv, 0, 16);
            Array.Copy(hash, 0, passGiv, 16, 20);

            // comparar passwords original e dada
            if (passOriginal.Equals(passGiv))
                return true;
            else
                return false;
        }
    }
}
