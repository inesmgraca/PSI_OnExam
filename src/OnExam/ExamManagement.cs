using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
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

                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var ds = new DataSet();
                    var dataTable = new DataTable("Exams");
                    ds.Tables.Add(dataTable);
                    ds.Load(dr, LoadOption.PreserveChanges, ds.Tables["Exams"]);
                    return ds;
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

            return null;
        }

        public static int ExamAdd(int duration, bool isRandom, State state)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("ExamAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);
                cmd.Parameters.AddWithValue("@Duration", duration);
                cmd.Parameters.AddWithValue("@isRandom", isRandom);
                cmd.Parameters.AddWithValue("@State", state);

                var examID = (int)cmd.ExecuteScalar();

                if (examID != 0)
                    return examID;
                else
                    MessageBox.Show(ResourceManager.GetString("errorMessage"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return 0;
        }

        public static ExamDetails ExamOpen(int examID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("ExamOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);
                cmd.Parameters.AddWithValue("@ExamID", examID);

                var dr = cmd.ExecuteReader();

                var examDetails = new ExamDetails();

                while (dr.Read())
                {
                    examDetails.ExamID = examID;
                    examDetails.ExamName = dr["ExamName"].ToString();
                    examDetails.Duration = (int)dr["Duration"];
                    examDetails.isRandom = (bool)dr["isRandom"];
                    examDetails.State = (State)((int)dr["State"]);
                }

                cmd = new SqlCommand("QuestionsOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamID", examDetails.ExamID);

                dr.Close();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    examDetails.Questions = new List<ExamQuestion>();

                    while (dr.Read())
                    {
                        var question = new ExamQuestion()
                        {
                            QuestionID = (int)dr["QuestionID"],
                            Type = (QuestionType)((int)dr["Type"]),
                            Question = dr["Question"].ToString(),
                            Notes = dr["Notes"].ToString()
                        };

                        examDetails.Questions.Add(question);
                    }

                    foreach (var question in examDetails.Questions)
                    {
                        if (question.Type != QuestionType.Text)
                        {
                            question.QuestionDetails = new List<ExamQuestionDetails>();

                            cmd = new SqlCommand("QuestionDetailsOpen", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@QuestionID", question.QuestionID);

                            dr.Close();
                            dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                var questionDetails = new ExamQuestionDetails
                                {
                                    isRight = (bool)dr["isRight"],
                                    Text = dr["Text"].ToString()
                                };

                                question.QuestionDetails.Add(questionDetails);
                            }
                        }
                    }
                }

                return examDetails;
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

            return null;
        }

        public static bool ExamDelete(int examID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("ExamOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@ExamID", examID);
                cmd.Parameters.AddWithValue("Username", UserLoggedIn);

                var dr = cmd.ExecuteReader();
                var state = State.Inactive;

                while (dr.Read())
                    state = (State)((int)dr["State"]);

                if (state == State.Inactive)
                {
                    cmd = new SqlCommand("QuestionsOpen", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@ExamID", examID);

                    dr.Close();
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var questionsID = new List<int>();

                        while (dr.Read())
                            questionsID.Add((int)dr["QuestionID"]);

                        dr.Close();

                        for (int i = 0; i < questionsID.Count; i++)
                        {
                            cmd = new SqlCommand("QuestionDelete", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@QuestionID", questionsID[i]);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd = new SqlCommand("ExamDelete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamID", examID);

                    if (cmd.ExecuteNonQuery() == 1)
                        return true;
                }
                else
                    MessageBox.Show(ResourceManager.GetString("examAlreadyDone"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool ExamUpdateQuestion(ExamQuestion examQuestion)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("QuestionUpdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QuestionID", examQuestion.QuestionID);
                cmd.Parameters.AddWithValue("@Question", examQuestion.Question);
                cmd.Parameters.AddWithValue("@Notes", examQuestion.Notes);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    if (examQuestion.Type != QuestionType.Text)
                    {
                        cmd = new SqlCommand("QuestionDetailsOpen", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@QuestionID", examQuestion.QuestionID);

                        var dr = cmd.ExecuteReader();
                        var questionDetailsID = new List<int>();

                        while (dr.Read())
                            questionDetailsID.Add((int)dr["QuestionDetailsID"]);

                        var num = 0;

                        if (examQuestion.QuestionDetails.Count > questionDetailsID.Count)
                            num = questionDetailsID.Count;
                        else
                            num = examQuestion.QuestionDetails.Count;

                        for (int i = 0; i < num; i++)
                        {
                            cmd = new SqlCommand("QuestionDetailsUpdate", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@QuestionDetailsID", questionDetailsID[i]);
                            cmd.Parameters.AddWithValue("@isRight", examQuestion.QuestionDetails[i].isRight);
                            cmd.Parameters.AddWithValue("@Text", examQuestion.QuestionDetails[i].Text);

                            dr.Close();

                            if (cmd.ExecuteNonQuery() != 1)
                                break;
                        }

                        if (examQuestion.QuestionDetails.Count > questionDetailsID.Count)
                        {
                            for (int i = num; i < examQuestion.QuestionDetails.Count; i++)
                            {
                                cmd = new SqlCommand("QuestionDetailsAdd", conn);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@QuestionID", examQuestion.QuestionID);
                                cmd.Parameters.AddWithValue("@isRight", examQuestion.QuestionDetails[i].isRight);
                                cmd.Parameters.AddWithValue("@Text", examQuestion.QuestionDetails[i].Text);

                                dr.Close();

                                if (cmd.ExecuteNonQuery() != 1)
                                    break;
                            }
                        }
                        else if (examQuestion.QuestionDetails.Count < questionDetailsID.Count)
                        {
                            for (int i = num; i < questionDetailsID.Count; i++)
                            {
                                cmd = new SqlCommand("QuestionDetailsDelete", conn);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@QuestionDetailsID", questionDetailsID[i]);

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

        public static bool ExamUpdate(int examID, ExamDetails exam)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("ExamNameSearch", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamID", examID);
                cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                cmd.Parameters.AddWithValue("@Username", UserLoggedIn);

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    cmd = new SqlCommand("ExamUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamID", examID);
                    cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                    cmd.Parameters.AddWithValue("@Duration", exam.Duration);
                    cmd.Parameters.AddWithValue("@isRandom", exam.isRandom);

                    dr.Close();

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        for (int i = 0; i < exam.Questions.Count; i++)
                        {
                            cmd = new SqlCommand("QuestionAdd", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ExamID", examID);
                            cmd.Parameters.AddWithValue("@Type", (int)exam.Questions[i].Type);
                            cmd.Parameters.AddWithValue("@Question", exam.Questions[i].Question);
                            cmd.Parameters.AddWithValue("@Notes", exam.Questions[i].Notes);

                            var questionID = (int)cmd.ExecuteScalar();

                            if (questionID != 0)
                            {
                                while (dr.Read())
                                    int.TryParse(dr["QuestionID"].ToString(), out questionID);

                                if (exam.Questions[i].Type != QuestionType.Text)
                                {
                                    for (int j = 0; j < exam.Questions[i].QuestionDetails.Count; j++)
                                    {
                                        cmd = new SqlCommand("QuestionDetailsAdd", conn);
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@QuestionID", questionID);
                                        cmd.Parameters.AddWithValue("@isRight", exam.Questions[i].QuestionDetails[j].isRight);
                                        cmd.Parameters.AddWithValue("@Text", exam.Questions[i].QuestionDetails[j].Text);

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
                    MessageBox.Show(ResourceManager.GetString("invalidName"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool ExamUpdateState(int examID, State state)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("ExamUpdateState", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamID", examID);
                cmd.Parameters.AddWithValue("@State", (int)state);

                if (cmd.ExecuteNonQuery() == 1)
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

        public static bool ExamDeleteQuestion(int perguntaID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("QuestionDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QuestionID", perguntaID);

                if (cmd.ExecuteNonQuery() == 1)
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
    }

    public class ExamDetails
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public int Duration { get; set; }
        public bool isRandom { get; set; }
        public ExamManagement.State State { get; set; }
        public List<ExamQuestion> Questions { get; set; }
        public string ExamOwner { get; set; }
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
