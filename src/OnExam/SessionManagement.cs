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

        public static int ExamExits { get; set; }

        public static bool TakeExam(int examID, string examName, string examOwner)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("TakeExam", conn);
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
                            return false;
                    }

                    cmd = new SqlCommand("TakeExamQuestions", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamID", DetailsExam.ExamID);

                    dr.Close();
                    dr = cmd.ExecuteReader();

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

                            cmd = new SqlCommand("TakeExamQuestionDetails", conn);
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

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                        SessionID = (int)dr["SessionID"];

                    ExamExits = 0;

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

                if (ExamExits != 0)
                {
                    var cmd = new SqlCommand("SessionUpdate", conn);

                    cmd.Parameters.AddWithValue("@ExamExits", ExamExits);

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
