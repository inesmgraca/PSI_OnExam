using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        public enum QuestionType
        {
            Text,
            RadioButton,
            Checkbox
        }

        public static DataSet ExamsView()
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
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

        public static int ExamAdd(int duration, bool isRandom)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("AddExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", UserLoggedIn);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Duration", duration);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@isRandom", isRandom);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var ExamID = 0;

                    while (dr.Read())
                        int.TryParse(dr["ExamID"].ToString(), out ExamID);

                    return ExamID;
                }
                else
                {
                    MessageBox.Show(errorMessage, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return 0;
        }

        public static ExamDetails ExamOpen(int examID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("OpenExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", UserLoggedIn);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ExamID", examID);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var examDetails = new ExamDetails();

                    while (dr.Read())
                    {
                        examDetails.ExamName = dr["ExamName"].ToString();
                        examDetails.Duration = dr["Duration"].ToString();
                        var isRandom = dr["isRandom"].ToString();

                        if (isRandom == "False")
                            examDetails.isRandom = false;
                        else
                            examDetails.isRandom = true;

                        return examDetails;
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

            return null;
        }

        public static List<ExamQuestion> ExamOpenQuestions(int examID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("OpenQuestions", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", UserLoggedIn);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ExamID", examID);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var questions = new List<ExamQuestion>();

                    while (dr.Read())
                    {
                        int.TryParse(dr["PerguntaID"].ToString(), out int perguntaID);
                        int.TryParse(dr["Tipo"].ToString(), out int tipo);
                        var question = new ExamQuestion()
                        {
                            PerguntaID = perguntaID,
                            Tipo = (QuestionType)tipo,
                            Enunciado = dr["Enunciado"].ToString(),
                            Notas = dr["Notas"].ToString(),
                            DetalhesPergunta = new List<ExamQuestionDetails>()
                        };

                        if (question.Tipo != QuestionType.Text)
                        {
                            var questionDetails = new List<ExamQuestionDetails>();
                            questionDetails = ExamOpenQuestionDetails(perguntaID);
                            question.DetalhesPergunta.AddRange(questionDetails);
                        }

                        questions.Add(question);
                    }

                    return questions;
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

        public static List<ExamQuestionDetails> ExamOpenQuestionDetails(int perguntaID)
        {
            var question = new List<ExamQuestionDetails>();
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("OpenQuestionDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@PerguntaID", perguntaID);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var questionDetails = new ExamQuestionDetails();

                        questionDetails.Espaco1 = dr["Espaco1"].ToString();
                        var isRight = dr["isRight"].ToString();

                        if (isRight == "False")
                            questionDetails.isRight = false;
                        else
                            questionDetails.isRight = true;

                        question.Add(questionDetails);
                    }
                }

                return question;
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

        public static bool ExamUpdateQuestion(ExamQuestion examQuestion)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("UpdateQuestion", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@PerguntaID", examQuestion.PerguntaID);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Enunciado", examQuestion.Enunciado);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Notas", examQuestion.Notas);
                cmd.Parameters.Add(param);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    if (examQuestion.Tipo != QuestionType.Text)
                    {
                        cmd = new SqlCommand("select DetalhesPerguntaID from DetalhesPergunta where PerguntaID = @PerguntaID;", conn);
                        cmd.Parameters.AddWithValue("@PerguntaID", examQuestion.PerguntaID);

                        var dr = cmd.ExecuteReader();
                        var detalhesPerguntaIDs = new List<int>();

                        while (dr.Read())
                        {
                            int.TryParse(dr["num"].ToString(), out var ID);
                            detalhesPerguntaIDs.Add(ID);
                        }

                        var num = 0;

                        if (examQuestion.DetalhesPergunta.Count > detalhesPerguntaIDs.Count)
                            num = detalhesPerguntaIDs.Count;
                        else
                            num = examQuestion.DetalhesPergunta.Count;

                        for (int i = 0; i < num; i++)
                        {
                            cmd = new SqlCommand("UpdateQuestionDetails", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("@DetalhesPerguntaID", detalhesPerguntaIDs[i]);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@isRight", examQuestion.DetalhesPergunta[i].isRight);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@Espaco1", examQuestion.DetalhesPergunta[i].Espaco1);
                            cmd.Parameters.Add(param);

                            dr.Close();

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                result = false;
                                break;
                            }
                        }

                        if (examQuestion.DetalhesPergunta.Count > detalhesPerguntaIDs.Count)
                        {
                            for (int i = num - 1; i < examQuestion.DetalhesPergunta.Count; i++)
                            {
                                cmd = new SqlCommand("AddQuestionDetails", conn);
                                cmd.CommandType = CommandType.StoredProcedure;

                                param = new SqlParameter("@PerguntaID", examQuestion.PerguntaID);
                                cmd.Parameters.Add(param);

                                param = new SqlParameter("@isRight", examQuestion.DetalhesPergunta[i].isRight);
                                cmd.Parameters.Add(param);

                                param = new SqlParameter("@Espaco1", examQuestion.DetalhesPergunta[i].Espaco1);
                                cmd.Parameters.Add(param);

                                dr.Close();
                            }
                        }
                        else if (examQuestion.DetalhesPergunta.Count < detalhesPerguntaIDs.Count)
                        {
                            for (int i = num - 1; i < detalhesPerguntaIDs.Count; i++)
                            {
                                cmd = new SqlCommand("DeleteQuestionDetails", conn);
                                cmd.CommandType = CommandType.StoredProcedure;

                                param = new SqlParameter("@DetalhesPerguntaID", detalhesPerguntaIDs[i]);

                                dr.Close();
                            }
                        }
                    }

                    result = true;
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

        public static bool ExamUpdateExam(int examID, ExamDetails exam)
        {
            var result = false;
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("UpdateExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@ExamID", examID);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ExamName", exam.ExamName);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Duration", exam.Duration);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@isRandom", exam.isRandom);
                cmd.Parameters.Add(param);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    for (int i = 0; i < exam.Perguntas.Count; i++)
                    {
                        cmd = new SqlCommand("AddQuestion", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        param = new SqlParameter("@ExamID", examID);
                        cmd.Parameters.Add(param);

                        param = new SqlParameter("@Tipo", (int)exam.Perguntas[i].Tipo);
                        cmd.Parameters.Add(param);

                        param = new SqlParameter("@Enunciado", exam.Perguntas[i].Enunciado);
                        cmd.Parameters.Add(param);

                        param = new SqlParameter("@Notas", exam.Perguntas[i].Notas);
                        cmd.Parameters.Add(param);

                        var dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            var PerguntaID = 0;

                            while (dr.Read())
                                int.TryParse(dr["PerguntaID"].ToString(), out PerguntaID);

                            if (exam.Perguntas[i].Tipo != QuestionType.Text)
                            {
                                for (int j = 0; j < exam.Perguntas[i].DetalhesPergunta.Count; j++)
                                {
                                    cmd = new SqlCommand("AddQuestionDetails", conn);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    param = new SqlParameter("@PerguntaID", PerguntaID);
                                    cmd.Parameters.Add(param);

                                    param = new SqlParameter("@isRight", exam.Perguntas[i].DetalhesPergunta[j].isRight);
                                    cmd.Parameters.Add(param);

                                    param = new SqlParameter("@Espaco1", exam.Perguntas[i].DetalhesPergunta[j].Espaco1);
                                    cmd.Parameters.Add(param);

                                    dr.Close();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    result = true;
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
    }

    public class ExamDetails
    {
        public string ExamName { get; set; }
        public string Duration { get; set; }
        public bool isRandom { get; set; }
        public List<ExamQuestion> Perguntas { get; set; }
    }

    public class ExamQuestion
    {
        public int PerguntaID { get; set; }
        public ExamManagement.QuestionType Tipo { get; set; }
        public string Enunciado { get; set; }
        public string Notas { get; set; }
        public List<ExamQuestionDetails> DetalhesPergunta { get; set; }
    }

    public class ExamQuestionDetails
    {
        public bool isRight { get; set; }
        public string Espaco1 { get; set; }
    }
}
