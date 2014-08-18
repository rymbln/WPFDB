using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDB.Model;

namespace WPFDB.Common
{
    public class ImportManager
    {
        private static ImportManager instance;
        private static SqlConnection conn;

        public int ImportAll(System.ComponentModel.BackgroundWorker backgroundWorker)
        {
            var res = 0;
            try
            {
                ImportAllKnowalls(backgroundWorker);
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportAllKnowalls(System.ComponentModel.BackgroundWorker backgroundWorker)
        {
            var res = 0;
            try
            {
                ImportUsers();
                if (backgroundWorker != null)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        // Возврат без какой-либо дополнительной работы
                        return 0;
                    }

                    if (backgroundWorker.WorkerReportsProgress)
                    {
                        //float progress = ((float)(i + 1)) / list.Length * 100;
                        backgroundWorker.ReportProgress(10);
                        //(int)Math.Round(progress));
                    }
                }
                ImportSexes();
                if (backgroundWorker != null)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        // Возврат без какой-либо дополнительной работы
                        return 0;
                    }

                    if (backgroundWorker.WorkerReportsProgress)
                    {
                        //float progress = ((float)(i + 1)) / list.Length * 100;
                        backgroundWorker.ReportProgress(20);
                        //(int)Math.Round(progress));
                    }
                }
                ImportScienceDegrees();
                if (backgroundWorker != null)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        // Возврат без какой-либо дополнительной работы
                        return 0;
                    }

                    if (backgroundWorker.WorkerReportsProgress)
                    {
                        //float progress = ((float)(i + 1)) / list.Length * 100;
                        backgroundWorker.ReportProgress(30);
                        //(int)Math.Round(progress));
                    }
                }
                ImportScienceStatuses();
                if (backgroundWorker != null)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        // Возврат без какой-либо дополнительной работы
                        return 0;
                    }

                    if (backgroundWorker.WorkerReportsProgress)
                    {
                        //float progress = ((float)(i + 1)) / list.Length * 100;
                        backgroundWorker.ReportProgress(40);
                        //(int)Math.Round(progress));
                    }
                }
                ImportSpecialities();
                ImportRanks();

                ImportOrderStatuses();
                ImportPaymentTypes();

                ImportConferenceTypes();

                ImportCompanies();

                ImportAbstractStatuses();

                if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
                {
                    backgroundWorker.ReportProgress(100);
                }

                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public static ImportManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImportManager();
                    string connStr = @"Server=.\SQL2008; User Id=sa; password=asuseee;";
                    conn = new SqlConnection(connStr);
                }
                return instance;
            }
        }



        public int ImportUsers()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [ABSTRACT_RESPONSIBLE_PERSON_ID],[ABSTRACT_RESPONSIBLE_PERSON_NAME],[ABSTRACT_RESPONSIBLE_PERSON_EMAIL],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[abstract_responsible_person]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<User>();

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<User>();
                        obj.Id = GuidComb.Generate();
                        obj.Password = "111";
                        obj.Email = rdr.GetString(2);
                        obj.Name = rdr.GetString(1);
                        obj.SourceId = rdr.GetInt32(0);
                        obj.Role += "Reviewer";
                        DataManager.Instance.AddUser(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportSpecialities()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [SPECIALITY_ID],[SPECIALITY_ENG],[SPECIALITY_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[speciality]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<Speciality>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddSpeciality(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Speciality>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddSpeciality(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportContactTypes()
        {
            var res = 0;
            try
            {
                var sql =
                    "SELECT [ADDRESS_TYPE_ID],[ADDRESS_TYPE_ENG],[ADDRESS_TYPE_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[address_type]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<ContactType>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddContactType(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<ContactType>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddContactType(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }
        public int ImportSexes()
        {
            var res = 0;
            try
            {
                var obj = new Sex();

                obj = DataManager.Instance.CreateObject<Sex>();
                obj.Id = GuidComb.Generate();
                obj.Code = "-";
                obj.Name = "-";
                obj.SourceId = 0;
                DataManager.Instance.AddSex(obj);
                obj = DataManager.Instance.CreateObject<Sex>();
                obj.Id = GuidComb.Generate();
                obj.Code = "";
                obj.Name = "Мужской";
                obj.SourceId = 1;
                DataManager.Instance.AddSex(obj);
                obj = DataManager.Instance.CreateObject<Sex>();
                obj.Id = GuidComb.Generate();
                obj.Code = "";
                obj.Name = "Женский";
                obj.SourceId = 2;
                DataManager.Instance.AddSex(obj);
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportScienceStatuses()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [SCIENCE_STATUS_ID],[SCIENCE_STATUS_ENG],[SCIENCE_STATUS_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[science_status]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<ScienceStatus>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddScienceStatus(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<ScienceStatus>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddScienceStatus(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportScienceDegrees()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [SCIENCE_DEGREE_ID],[SCIENCE_DEGREE_ENG],[SCIENCE_DEGREE_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[science_degree]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<ScienceDegree>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddScienceDegree(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<ScienceDegree>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddScienceDegree(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportRanks()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [RANK_ID],[RANK_ENG],[RANK_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[rank]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<Rank>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddRank(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Rank>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddRank(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }
        public int ImportPaymentTypes()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [PAYMENT_TYPE_ID],[PAYMENT_TYPE_ENG],[PAYMENT_TYPE_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[payment_type]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<PaymentType>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddPaymentType(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<PaymentType>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddPaymentType(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportOrderStatuses()
        {
            var res = 0;
            try
            {
                var obj = new OrderStatus();

                obj = DataManager.Instance.CreateObject<OrderStatus>();
                obj.Id = GuidComb.Generate();
                obj.Code = "-";
                obj.Name = "Нет";
                obj.SourceId = 0;
                DataManager.Instance.AddOrderStatus(obj);
                obj = DataManager.Instance.CreateObject<OrderStatus>();
                obj.Id = GuidComb.Generate();
                obj.Code = "";
                obj.Name = "Оплачен";
                obj.SourceId = 1;
                DataManager.Instance.AddOrderStatus(obj);
                obj = DataManager.Instance.CreateObject<OrderStatus>();
                obj.Id = GuidComb.Generate();
                obj.Code = "";
                obj.Name = "Возврат";
                obj.SourceId = 2;
                DataManager.Instance.AddOrderStatus(obj);

                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportCompanies()
        {
            var res = 0;
            try
            {
                var sql =
                    "SELECT [COMPANY_ID],[COMPANY_ENG],[COMPANY_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[company]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<Company>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddCompany(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Company>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddCompany(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportAbstractStatuses()
        {
            var res = 0;
            try
            {
                var sql =
                    "SELECT [ABSTRACT_STATUS_ID],[ABSTRACT_STATUS_ENG],[ABSTRACT_STATUS_RUS],[DATE_ADD],[DATE_UPDATE],[SUSER],[SOURCE_ID] FROM [Conference].[dbo].[abstract_status]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<AbstractStatus>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddAbstractStatus(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<AbstractStatus>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddAbstractStatus(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportConferenceTypes()
        {
            var res = 0;
            try
            {
                var sql =
                    "SELECT [CONFERENCE_TYPE_ID],"
                    + "[CONFERENCE_TYPE_DESCRIPTION_ENG]"
                    + ",[CONFERENCE_TYPE_DESCRIPTION_RUS]"
                    + ",[DATE_ADD]"
                    + ",[DATE_UPDATE]"
                    + ",[SUSER]"
                    + ",[SOURCE_ID]"
                    + "  FROM [Conference].[dbo].[conference_type]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<Conference>();
                    obj.Id = GuidComb.Generate();
                    obj.Code = "-";
                    obj.Name = "-";
                    obj.SourceId = 0;
                    DataManager.Instance.AddConference(obj);

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Conference>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddConference(obj);

                    }
                }
                rdr.Close();
                rdr = null;
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;

        }

    }
}
