using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public static class TakeExamManagement
    {
        public static ExamDetails ExamDetails { get; set; }

        public static string ExamOwner { get; set; }

        public static int AvaliadoID { get; set; }

        public static bool TakeExam(int examID, string examName, string examOwner)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("TakeExam", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@Username", examOwner);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ExamID", examID);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ExamName", examName);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    ExamDetails = new ExamDetails();

                    while (dr.Read())
                    {
                        int.TryParse(dr["State"].ToString(), out int state);
                        ExamDetails.State = (State)state;

                        if (ExamDetails.State != State.Active)
                            return false;

                        ExamOwner = dr["Username"].ToString();

                        int.TryParse(dr["ExamID"].ToString(), out int ID);
                        ExamDetails.ExamID = ID;

                        ExamDetails.ExamName = dr["ExamName"].ToString();

                        int.TryParse(dr["Duration"].ToString(), out int duration);
                        ExamDetails.Duration = duration;

                        if (dr["isRandom"].ToString().Equals("False"))
                            ExamDetails.isRandom = false;
                        else
                            ExamDetails.isRandom = true;
                    }

                    cmd = new SqlCommand("TakeExamQuestions", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    param = new SqlParameter("@ExamID", ExamDetails.ExamID);
                    cmd.Parameters.Add(param);

                    dr.Close();
                    dr = cmd.ExecuteReader();

                    ExamDetails.Questions = new List<ExamQuestion>();

                    while (dr.Read())
                    {
                        int.TryParse(dr["PerguntaID"].ToString(), out int ID);
                        int.TryParse(dr["Tipo"].ToString(), out int tipo);

                        var question = new ExamQuestion()
                        {
                            QuestionID = ID,
                            Type = (QuestionType)tipo,
                            Question = dr["Enunciado"].ToString()
                        };

                        ExamDetails.Questions.Add(question);
                    }

                    foreach (var question in ExamDetails.Questions)
                    {
                        if (question.Type != QuestionType.Text)
                        {
                            question.QuestionDetails = new List<ExamQuestionDetails>();

                            cmd = new SqlCommand("TakeExamQuestionDetails", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("@PerguntaID", question.QuestionID);
                            cmd.Parameters.Add(param);

                            dr.Close();
                            dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                int.TryParse(dr["DetalhesPerguntaID"].ToString(), out int ID);

                                var questionDetails = new ExamQuestionDetails
                                {
                                    QuestionDetailsID = ID,
                                    Text = dr["Espaco1"].ToString()
                                };

                                question.QuestionDetails.Add(questionDetails);
                            }
                        }
                    }

                    return true;
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("invalidExam"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool SessionAdd(string name, string info)
        {
            var connString = ConfigurationManager.ConnectionStrings["OnExamDB"].ConnectionString;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                var cmd = new SqlCommand("AddSession", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@ExamID", ExamDetails.ExamID);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Name", name);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Info", info);
                cmd.Parameters.Add(param);

                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int.TryParse(dr["AvaliadoID"].ToString(), out int ID);
                        AvaliadoID = ID;
                    }

                    return true;
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("invalidExam"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}
