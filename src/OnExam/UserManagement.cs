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
                    if (PassCompare(dr["Password"].ToString(), pass))
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
                
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "select Email from Users where Email = @email;";
                cmd.Parameters.AddWithValue("@email", email);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show("Já existe um utilizador com este email!", "Email incorreto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new SqlCommand("AddUser");
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@Nome", nome);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Email", email);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Username", username);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Password", HashWithSalt(password));
                    cmd.Parameters.Add(param);

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

        public static string HashWithSalt(string password)
        {
            // medium.com/@mehanix/lets-talk-security-salted-password-hashing-in-c-5460be5c3aae

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

            // converter o array para string
            return Convert.ToString(hashSalt);
        }

        public static bool PassCompare(string passOriginal, string passGiven)
        {
            // medium.com/@mehanix/lets-talk-security-salted-password-hashing-in-c-5460be5c3aae

            // salt(16) + hash(20) = 36
            var salt = new byte[16];
            var passGiv = new byte[36];

            // atribuir salt da password original
            var passOrigin = Encoding.ASCII.GetBytes(passOriginal);
            Array.Copy(passOrigin, 0, salt, 0, 16);

            // hashing da password dada
            var hashing = new Rfc2898DeriveBytes(passGiven, salt, 10000);

            var hash = hashing.GetBytes(20);

            // pôr hash e salt no array de bytes hashSalt
            Array.Copy(salt, 0, passGiv, 0, 16);
            Array.Copy(hash, 0, passGiv, 16, 20);

            // comparar passwords original e dada
            if (passOrigin.Equals(passGiv))
                return true;
            else
                return false;
        }
    }
}
