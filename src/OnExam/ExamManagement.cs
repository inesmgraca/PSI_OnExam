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
        public enum QuestionType
        {
            Text,
            RadioButton,
            Checkbox
        }

        public enum State
        {
            Inactive,
            Active,
            Closed
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

            return null;
        }

        public static int ExamAdd(int duration, bool isRandom, State state)
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

                param = new SqlParameter("@State", state);
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
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("errorMessage"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var examDetails = new ExamDetails();

                while (dr.Read())
                {
                    examDetails.ExamID = examID;
                    examDetails.ExamName = dr["ExamName"].ToString();
                    int.TryParse(dr["Duration"].ToString(), out int duration);
                    examDetails.Duration = duration;

                    if (dr["isRandom"].ToString().Equals("False"))
                        examDetails.isRandom = false;
                    else
                        examDetails.isRandom = true;

                    int.TryParse(dr["State"].ToString(), out int state);
                    examDetails.State = (State)state;
                }

                cmd = new SqlCommand("OpenQuestions", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                param = new SqlParameter("@ExamID", examDetails.ExamID);
                cmd.Parameters.Add(param);

                dr.Close();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    examDetails.Questions = new List<ExamQuestion>();

                    while (dr.Read())
                    {
                        int.TryParse(dr["PerguntaID"].ToString(), out int perguntaID);
                        int.TryParse(dr["Tipo"].ToString(), out int tipo);

                        var question = new ExamQuestion()
                        {
                            QuestionID = perguntaID,
                            Type = (QuestionType)tipo,
                            Question = dr["Enunciado"].ToString(),
                            Notes = dr["Notas"].ToString()
                        };

                        examDetails.Questions.Add(question);
                    }

                    foreach (var question in examDetails.Questions)
                    {
                        if (question.Type != QuestionType.Text)
                        {
                            question.QuestionDetails = new List<ExamQuestionDetails>();

                            cmd = new SqlCommand("OpenQuestionDetails", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("@PerguntaID", question.QuestionID);
                            cmd.Parameters.Add(param);

                            dr.Close();
                            dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                var questionDetails = new ExamQuestionDetails
                                {
                                    Text = dr["Espaco1"].ToString()
                                };

                                if (dr["isRight"].ToString().Equals("False"))
                                    questionDetails.isRight = false;
                                else
                                    questionDetails.isRight = true;

                                question.QuestionDetails.Add(questionDetails);
                            }
                        }
                    }
                }

                return examDetails;
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

            return null;
        }

        public static bool ExamDelete(int examID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("select State from Exams where ExamID = @ExamID;", conn);
                cmd.Parameters.AddWithValue("@ExamID", examID);

                var dr = cmd.ExecuteReader();
                var state = State.Inactive;

                while (dr.Read())
                {
                    int.TryParse(dr["State"].ToString(), out int i);
                    state = (State)i;
                }

                if (state == State.Inactive)
                {
                    cmd = new SqlCommand("select PerguntaID from Perguntas where ExamID = @ExamID;", conn);
                    cmd.Parameters.AddWithValue("@ExamID", examID);

                    dr.Close();
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var perguntaIDs = new List<int>();

                        while (dr.Read())
                        {
                            int.TryParse(dr["PerguntaID"].ToString(), out int ID);
                            perguntaIDs.Add(ID);
                        }

                        dr.Close();

                        for (int i = 0; i < perguntaIDs.Count; i++)
                        {
                            cmd = new SqlCommand("DeleteQuestion", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            var parameter = new SqlParameter("@PerguntaID", perguntaIDs[i]);
                            cmd.Parameters.Add(parameter);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd = new SqlCommand("DeleteExam", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@ExamID", examID);
                    cmd.Parameters.Add(param);

                    if (cmd.ExecuteNonQuery() == 1)
                        return true;
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("examAlreadyDone"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool ExamUpdateQuestion(ExamQuestion examQuestion)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("UpdateQuestion", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@PerguntaID", examQuestion.QuestionID);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Enunciado", examQuestion.Question);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Notas", examQuestion.Notes);
                cmd.Parameters.Add(param);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    if (examQuestion.Type != QuestionType.Text)
                    {
                        cmd = new SqlCommand("select DetalhesPerguntaID from DetalhesPergunta where PerguntaID = @PerguntaID;", conn);
                        cmd.Parameters.AddWithValue("@PerguntaID", examQuestion.QuestionID);

                        var dr = cmd.ExecuteReader();
                        var detalhesPerguntaIDs = new List<int>();

                        while (dr.Read())
                        {
                            int.TryParse(dr["DetalhesPerguntaID"].ToString(), out var ID);
                            detalhesPerguntaIDs.Add(ID);
                        }

                        var num = 0;

                        if (examQuestion.QuestionDetails.Count > detalhesPerguntaIDs.Count)
                            num = detalhesPerguntaIDs.Count;
                        else
                            num = examQuestion.QuestionDetails.Count;

                        for (int i = 0; i < num; i++)
                        {
                            cmd = new SqlCommand("UpdateQuestionDetails", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("@DetalhesPerguntaID", detalhesPerguntaIDs[i]);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@isRight", examQuestion.QuestionDetails[i].isRight);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@Espaco1", examQuestion.QuestionDetails[i].Text);
                            cmd.Parameters.Add(param);

                            dr.Close();

                            if (cmd.ExecuteNonQuery() != 1)
                                break;
                        }

                        if (examQuestion.QuestionDetails.Count > detalhesPerguntaIDs.Count)
                        {
                            for (int i = num; i < examQuestion.QuestionDetails.Count; i++)
                            {
                                cmd = new SqlCommand("AddQuestionDetails", conn);
                                cmd.CommandType = CommandType.StoredProcedure;

                                param = new SqlParameter("@PerguntaID", examQuestion.QuestionID);
                                cmd.Parameters.Add(param);

                                param = new SqlParameter("@isRight", examQuestion.QuestionDetails[i].isRight);
                                cmd.Parameters.Add(param);

                                param = new SqlParameter("@Espaco1", examQuestion.QuestionDetails[i].Text);
                                cmd.Parameters.Add(param);

                                dr.Close();

                                if (cmd.ExecuteNonQuery() != 1)
                                    break;
                            }
                        }
                        else if (examQuestion.QuestionDetails.Count < detalhesPerguntaIDs.Count)
                        {
                            for (int i = num; i < detalhesPerguntaIDs.Count; i++)
                            {
                                cmd = new SqlCommand("DeleteQuestionDetails", conn);
                                cmd.CommandType = CommandType.StoredProcedure;

                                param = new SqlParameter("@DetalhesPerguntaID", detalhesPerguntaIDs[i]);
                                cmd.Parameters.Add(param);

                                dr.Close();

                                if (cmd.ExecuteNonQuery() != 1)
                                    break;
                            }
                        }
                    }

                    return true;
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

        public static bool ExamUpdate(int examID, ExamDetails exam)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("select ExamID from Exams e join Users u on u.UserID = e.UserID where ExamID != @ExamID and e.Name = @ExamName and Username = @Username", conn);

                cmd.Parameters.AddWithValue("@ExamID", examID);
                cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    cmd = new SqlCommand("UpdateExam", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@ExamID", examID);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@ExamName", exam.ExamName);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Duration", exam.Duration);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@isRandom", exam.isRandom);
                    cmd.Parameters.Add(param);

                    dr.Close();

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        for (int i = 0; i < exam.Questions.Count; i++)
                        {
                            cmd = new SqlCommand("AddQuestion", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("@ExamID", examID);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@Tipo", (int)exam.Questions[i].Type);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@Enunciado", exam.Questions[i].Question);
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("@Notas", exam.Questions[i].Notes);
                            cmd.Parameters.Add(param);

                            dr.Close();
                            dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                var PerguntaID = 0;

                                while (dr.Read())
                                    int.TryParse(dr["PerguntaID"].ToString(), out PerguntaID);

                                if (exam.Questions[i].Type != QuestionType.Text)
                                {
                                    for (int j = 0; j < exam.Questions[i].QuestionDetails.Count; j++)
                                    {
                                        cmd = new SqlCommand("AddQuestionDetails", conn);
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        param = new SqlParameter("@PerguntaID", PerguntaID);
                                        cmd.Parameters.Add(param);

                                        param = new SqlParameter("@isRight", exam.Questions[i].QuestionDetails[j].isRight);
                                        cmd.Parameters.Add(param);

                                        param = new SqlParameter("@Espaco1", exam.Questions[i].QuestionDetails[j].Text);
                                        cmd.Parameters.Add(param);

                                        dr.Close();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    return true;
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("invalidName"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool ExamUpdateState(int examID, State state)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("UpdateExamState", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@ExamID", examID);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@State", (int)state);
                cmd.Parameters.Add(param);

                if (cmd.ExecuteNonQuery() == 1)
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

        public static bool ExamDeleteQuestion(int perguntaID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("DeleteQuestion", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@PerguntaID", perguntaID);
                cmd.Parameters.Add(param);

                if (cmd.ExecuteNonQuery() == 1)
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
    }

    public class ExamDetails
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public int Duration { get; set; }
        public bool isRandom { get; set; }
        public ExamManagement.State State { get; set; }
        public List<ExamQuestion> Questions { get; set; }
    }

    public class ExamQuestion
    {
        public int QuestionID { get; set; }
        public ExamManagement.QuestionType Type { get; set; }
        public string Question { get; set; }
        public string Notes { get; set; }
        public List<ExamQuestionDetails> QuestionDetails { get; set; }
    }

    public class ExamQuestionDetails
    {
        public int QuestionDetailsID { get; set; }
        public bool isRight { get; set; }
        public string Text { get; set; }
    }
}
