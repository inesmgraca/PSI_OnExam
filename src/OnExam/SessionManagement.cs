using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.ExamManagement;

namespace OnExam
{
    public static class SessionManagement
    {
        public static ExamDetails DetailsExam { get; set; }

        public static int SessionID { get; set; }

        public static int SessionExits { get; set; }

        public static bool TakeExam(int examID, string examName, string examOwner)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("ExamSearch", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", examOwner);
                cmd.Parameters.AddWithValue("@ExamID", examID);
                cmd.Parameters.AddWithValue("@ExamName", examName);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    DetailsExam = new ExamDetails();

                    while (dr.Read())
                    {
                        DetailsExam.ExamID = (int)dr["ExamID"];
                        DetailsExam.ExamName = dr["ExamName"].ToString();
                        DetailsExam.Duration = (int)dr["Duration"];
                        DetailsExam.isRandom = (bool)dr["isRandom"];
                        DetailsExam.State = (State)((int)dr["State"]);
                        DetailsExam.ExamOwner = dr["Username"].ToString();

                        if (DetailsExam.State != State.Active)
                        {
                            MessageBox.Show(ResourceManager.GetString("examNotActive"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    if (DetailsExam.isRandom)
                    {
                        cmd = new SqlCommand("QuestionsOpenRandom", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ExamID", DetailsExam.ExamID);

                        dr.Close();
                        dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        cmd = new SqlCommand("QuestionsOpen", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ExamID", DetailsExam.ExamID);

                        dr.Close();
                        dr = cmd.ExecuteReader();
                    }

                    if (dr.HasRows)
                    {
                        DetailsExam.Questions = new List<ExamQuestion>();

                        while (dr.Read())
                        {
                            var question = new ExamQuestion()
                            {
                                QuestionID = (int)dr["QuestionID"],
                                Type = (QuestionType)((int)dr["Type"]),
                                Question = dr["Question"].ToString()
                            };

                            DetailsExam.Questions.Add(question);
                        }

                        foreach (var question in DetailsExam.Questions)
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
                                        QuestionDetailsID = (int)dr["QuestionDetailsID"],
                                        Text = dr["Text"].ToString()
                                    };

                                    question.QuestionDetails.Add(questionDetails);
                                }
                            }
                        }

                        return true;
                    }
                }
                else
                    MessageBox.Show(ResourceManager.GetString("invalidExam"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool SessionAdd(string name, string info)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("SessionAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamID", DetailsExam.ExamID);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Info", info);

                var sessionID = Convert.ToInt32(cmd.ExecuteScalar());

                if (sessionID != 0)
                {
                    SessionID = sessionID;
                    SessionExits = 0;
                    return true;
                }
                else
                    MessageBox.Show(ResourceManager.GetString("invalidExam"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool SessionClose(List<ExamAnswer> examAnswers)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                var success = true;

                foreach (var examAnswer in examAnswers)
                {
                    if (examAnswer.Type != QuestionType.Checkbox)
                    {
                        var cmd = new SqlCommand("AnswerAdd", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SessionID", SessionID);
                        cmd.Parameters.AddWithValue("@QuestionID", examAnswer.QuestionID);

                        if (examAnswer.Type == QuestionType.RadioButton)
                            cmd.Parameters.AddWithValue("@QuestionDetailsID", examAnswer.AnswerRdb);
                        else
                            cmd.Parameters.AddWithValue("@Text", examAnswer.AnswerText);

                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            success = false;
                            break;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < examAnswer.AnswerChk.Count; i++)
                        {
                            var cmd = new SqlCommand("AnswerAdd", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@SessionID", SessionID);
                            cmd.Parameters.AddWithValue("@QuestionID", examAnswer.QuestionID);
                            cmd.Parameters.AddWithValue("@QuestionDetailsID", examAnswer.AnswerChk[i]);

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                success = false;
                                break;
                            }
                        }
                    }
                }

                if (SessionExits != 0)
                {
                    var cmd = new SqlCommand("SessionUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SessionID", SessionID);
                    cmd.Parameters.AddWithValue("@SessionExits", SessionExits);

                    if (cmd.ExecuteNonQuery() != 1)
                        success = false;
                }


                if (success)
                    return true;
                else
                    MessageBox.Show(ResourceManager.GetString("sessionSaveError"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static DataSet SessionsView(int examID)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("SessionsView", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamID", examID);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var ds = new DataSet();
                    var dataTable = new DataTable("Sessions");
                    ds.Tables.Add(dataTable);
                    ds.Load(dr, LoadOption.PreserveChanges, ds.Tables["Sessions"]);
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

        public static List<ExamQuestion> SessionQuestionsOpen()
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("SessionQuestionsOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionID", SessionID);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var questions = new List<ExamQuestion>();

                    while (dr.Read())
                    {
                        var question = new ExamQuestion()
                        {
                            QuestionID = (int)dr["QuestionID"],
                            Type = (QuestionType)((int)dr["Type"]),
                            Question = dr["Question"].ToString()
                        };

                        questions.Add(question);
                    }

                    foreach (var question in questions)
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
                                    QuestionDetailsID = (int)dr["QuestionDetailsID"],
                                    isRight = (bool)dr["isRight"],
                                    Text = dr["Text"].ToString()
                                };

                                question.QuestionDetails.Add(questionDetails);
                            }
                        }
                    }

                    return questions;
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

        public static List<ExamAnswer> SessionAnswersOpen(List<ExamQuestion> questions)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("SessionOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionID", SessionID);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    var answers = new List<ExamAnswer>();

                    foreach (var question in questions)
                    {
                        cmd = new SqlCommand("SessionAnswersOpen", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SessionID", SessionID);
                        cmd.Parameters.AddWithValue("@QuestionID", question.QuestionID);

                        dr.Close();
                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            var answer = new ExamAnswer
                            {
                                QuestionID = question.QuestionID,
                                Type = question.Type
                            };

                            if (question.Type == QuestionType.Checkbox)
                                answer.AnswerChk = new List<int>();

                            while (dr.Read())
                            {
                                if (question.Type == QuestionType.Text)
                                    answer.AnswerText = dr["Text"].ToString();
                                else if (question.Type == QuestionType.RadioButton)
                                    answer.AnswerRdb = (int)dr["QuestionDetailsID"];
                                else
                                    answer.AnswerChk.Add((int)dr["QuestionDetailsID"]);

                                answers.Add(answer);
                            }
                        }
                    }

                    return answers;
                }
                else
                    MessageBox.Show(ResourceManager.GetString("examNotSubmitted"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }

    public class ExamAnswer
    {
        public int QuestionID { get; set; }
        public QuestionType Type { get; set; }
        public string AnswerText { get; set; }
        public int AnswerRdb { get; set; }
        public List<int> AnswerChk { get; set; }
    }
}
