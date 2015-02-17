using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using WPFDB.Model;
using WPFDB.View;

namespace WPFDB.Common
{
    public class ImportManager
    {
        private static ImportManager instance;
        private static SqlConnection conn;
        private static int personsCount;
        private static int iacmacCount;
        private static int personConferenceCount;


        public string ImportAll(System.ComponentModel.BackgroundWorker backgroundWorker)
        {
            var res = 0;
            try
            {
                ImportAllKnowalls(backgroundWorker);
                ImportPersons();
                ImportPersonConferences();
                res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return "persons " + personsCount + "|| iacmac " + iacmacCount + "|| personConferenceCount " + personConferenceCount;
        }

        public int ImportAllKnowalls(System.ComponentModel.BackgroundWorker backgroundWorker)
        {
            var res = 0;
            try
            {
                ImportUsers();
                ImportSexes();
                ImportScienceDegrees();
                ImportScienceStatuses();
                ImportSpecialities();
                ImportRanks();
                ImportPaymentTypes();
                ImportConferenceTypes();
                ImportCompanies();
                ImportAbstractStatuses();
               // ImportOrderStatuses();
                MessageBox.Show("Imported");
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
        public int ImportAbstractsWorks()
        {
            var res = 0;
            try
            {
                var sql = "SELECT " +
                          "[ABSTRACT_STATE_ID]" +//0
                          ",[ABSTRACT_ID]" +//1
                          ",[ABSTRACT_RESPONSIBLE_PERSON_ID]" +//2
                          ",[ABSTRACT_STATUS_ID]" +//3
                          ",[IS_SEND_STATE_BY_EMAIL] " +//4
                          ",[DATE_ADD]" +//5
                          "FROM [Conference].[dbo].[EXP_ABSTRACT_WORKS]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    AbstractWork obj;

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<AbstractWork>();
                        obj.Id = GuidComb.Generate();
                        obj.AbstractStatusId = DataManager.Instance.GetAbstractStatusBySourceId(rdr.GetInt32(3)).Id;
                        obj.AbstractId = DataManager.Instance.GetAbstractBySourceId(rdr.GetInt32(1)).Id;
                        obj.ReviewerId = DataManager.Instance.GetUserBySourceId(rdr.GetInt32(2)).Id;
                        obj.IsSentByEmail = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt16(4));
                        obj.DateWork = rdr.GetDateTime(5);
                        obj.SourceId = rdr.GetInt32(0);

                        DataManager.Instance.AddAbstractWork(obj);

                        res++;

                        // personConferenceCount++;
                    }
                }
                rdr.Close();
                rdr = null;
                // res = 1;
            }
            catch (Exception ex)
            {

                LogManager.Write(ex);
            }

            return res;

        }
        public int ImportAbstracts()
        {
            var res = 0;
            Abstract obj;
            try
            {
                var sql = "SELECT " +
                          "[ABSTRACT_ID]" +//0
                          ",[PERSON_CONFERENCE_TYPE_ID]" +//1
                          ",[OTHER_AUTHORS]" +//2
                          ",[ABSTRACT_NAME]" +//3
                          ",[ABSTRACT_TEXT]" +//4
                          ",[ABSTRACT_TEXT_LINK] " +//5
                          "FROM [Conference].[dbo].[EXP_ABSTRACTS]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                  

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Abstract>();
                        obj.Id = GuidComb.Generate();
                        obj.PersonConferenceId = DataManager.Instance.GetPersonConferenceBySourceID(rdr.GetInt32(1)).PersonConferenceId;
                        obj.OtherAuthors = rdr.GetString(2);
                        obj.Name = rdr.GetString(3);
                        obj.Text = rdr.GetString(4);
                        obj.Link = rdr.GetString(5);
                        obj.SourceId = rdr.GetInt32(0);

                        DataManager.Instance.AddAbstract(obj);

                        res++;

                        // personConferenceCount++;
                    }
                }
                rdr.Close();
                rdr = null;
                // res = 1;
            }
            catch (Exception ex)
            {

                LogManager.Write(ex);
            }

            return res;

        }

        public int ImportPersonConferenceMoney()
        {
            var res = 0;
            PersonConferences_Payment obj;
            try
            {
                var sql = "SELECT " +
                          "[PERSON_CONFERENCE_MONEY_ID]" +//0
                          ",[PERSON_CONFERENCE_TYPE_ID]" +//1
                          ",[PAYMENT_TYPE_ID]" +//2
                          ",[COMPANY_ID]" +//3
                          ",[PAYMENT_DOCUMENT]" +//4
                          ",[PAYMENT_DATE]" +//5
                          ",[SUMMA]" +//6
                          ",[DATE_ADD]" +//7
                          ",[DATE_UPDATE]" +//8
                          ",[SUSER]" +//9
                          ",[SOURCE_ID]" +//10
                          ",[IS_COMPLECT]" +//11
                          ",[ORDER_NUMBER]" +//12
                          ",[ORDER_STATUS] " +//13
                          "FROM [Conference].[dbo].[EXP_PERSON_CONFERENCE_MONEY]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<PersonConferences_Payment>();
                        obj.PersonConferenceId = DataManager.Instance.GetPersonConferenceBySourceID(rdr.GetInt32(1)).PersonConferenceId;
                        obj.PaymentTypeId = DataManager.Instance.GetPaymentTypeBySourceId(rdr.GetInt32(2)).Id;
                        obj.CompanyId = DataManager.Instance.GetCompanyBySourceId(rdr.GetInt32(3)).Id;
                        obj.PaymentDocument = rdr.GetString(4);
                        obj.PaymentDate = rdr.GetDateTime(5);
                        obj.Money = rdr.GetDecimal(6);
                        obj.IsComplect = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt16(11));
                        obj.OrderNumber = rdr.GetInt32(12);
                        obj.OrderStatusId = DataManager.Instance.GetOrderStatusBySourceId(rdr.GetInt32(13)).Id;
                        obj.SourceId = rdr.GetInt32(0);

                        DataManager.Instance.AddPersonConferenceMoney(obj);

                        res++;

                        // personConferenceCount++;
                    }
                }
                rdr.Close();
                rdr = null;
                // res = 1;
            }
            catch (Exception ex)
            {

                LogManager.Write(ex);
            }

            return res;

        }

        public int ImportPersonConferenceDetail()
        {
            var res = 0;
            //        personConferenceDetailCount = 0;
            try
            {
                var sql = "SELECT " +
                          "[PERSON_CONFERENCE_DETAIL_ID]" +//0
                          ",[PERSON_CONFERENCE_TYPE_ID]" +//1
                          ",ISNULL([RANK_ID],0)" +//2
                          ",ISNULL([COMPANY_ID],0)" +//3
                          ",ISNULL([IS_BADGE],0)" +//4
                          ",ISNULL([IS_ARRIVE],0)" +//5
                          ",[DATE_ARRIVE]" +//6
                          ",ISNULL([IS_COMPLECT],0)" +//7
                          ",ISNULL([IS_ADDITIONAL_MATERIAL],0)" +//8
                          ",ISNULL([IS_ABSTRACT],0)" +//9
                          ",ISNULL([IS_AUTOREG],0)" +//10
                          ",ISNULL([IS_NEED_BADGE],0) " +//11
                          "FROM [Conference].[dbo].[EXP_PERSON_CONFERENCE_DETAIL]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<PersonConferences_Detail>();

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<PersonConferences_Detail>();
                        obj.PersonConferenceId = DataManager.Instance.GetPersonConferenceBySourceID(rdr.GetInt32(1)).PersonConferenceId;
                        obj.RankId = DataManager.Instance.GetRankBySourceId(rdr.GetInt32(2)).Id;
                        obj.CompanyId = DataManager.Instance.GetCompanyBySourceId(rdr.GetInt32(3)).Id;
                        obj.IsBadge = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt16(4));
                        obj.IsArrive = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt16(5));
                        obj.DateArrive = rdr.GetDateTime(6);
                        obj.IsAbstract = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt16(9));
                        obj.IsAutoreg = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt32(10));
                        obj.IsNeedBadge = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetInt32(11));
                        obj.SourceId = rdr.GetInt32(0);

                        DataManager.Instance.AddPersonConferenceDetail(obj);

                        res++;

                        // personConferenceCount++;
                    }
                }
                rdr.Close();
                rdr = null;
                // res = 1;
            }
            catch (Exception ex)
            {

                LogManager.Write(ex);
            }

            return res;

        }

        public int ImportPersonConferences()
        {
            var res = 0;
            personConferenceCount = 0;
            try
            {
                var sql = "SELECT " +
                          "[PERSON_CONFERENCE_TYPE_ID]," +
                          "[PERSON_ID]," +
                          "[CONFERENCE_TYPE_ID]," +
                          "[DATE_ADD]," +
                          "[DATE_UPDATE]," +
                          "[SUSER]" +
                          " FROM [Conference].[dbo].[EXP_PERSON_CONFERENCE]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<PersonConference>();

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<PersonConference>();
                        obj.PersonConferenceId = GuidComb.Generate();
                        obj.PersonId = DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)).Id;
                        obj.ConferenceId = DataManager.Instance.GetConferenceBySourceId(rdr.GetInt32(2)).Id;
                        obj.SourceId = rdr.GetInt32(0);

                        DataManager.Instance.AddPersonConference(obj);



                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                // res = 1;
            }
            catch (Exception ex)
            {

                LogManager.Write(ex);
                MessageBox.Show(res.ToString());
            }

            return res;


        }
        public int ImportPersons()
        {
            personsCount = 0;
            var res = 0;
            //try
            //{
            var sql =
         "SELECT " +
         "[PERSON_ID]," + //0
         "[LOGIN]," + //1
         "[PASSWORD]," +//2
         "ISNULL([FIRST_NAME],N'-')," +//3
         "ISNULL([SECOND_NAME],N'-')," +//4
         "ISNULL(SURNAME,N'-')," +//5
         "ISNULL([SEX_CODE],0)," +//6
         "ISNULL([WORK_PLACE],N'-')," +//7
         "ISNULL([POST],N'-')," +//8
         "ISNULL(SPECIALITY_ID,0)," +//9
         "ISNULL([SCIENCE_DEGREE_ID],0)," +//10
         "ISNULL([SCIENCE_STATUS_ID],0)," +//11
         "[IS_IACMAC_MEMBER]," +//12
         "[IACMAC_CARD_NUMBER]," +//13
         "[DATE_ADD]," +//14
         "[DATE_UPDATE]," +//15
         "[SUSER]," +//16
         "[SOURCE_ID]" +//17
        ",ISNULL([IACMAC_ID],0)" +//18
        ",ISNULL([IS_MEMBER],0)" +//19
        ",ISNULL([IACMAC_NUMBER],0)" +//20
        ",ISNULL([IACMAC_CODE],N'-')" +//21
        ",ISNULL([IS_CARD_CREATE],0)" +//22
        ",ISNULL([IS_CARD_SENT],0)" +//23
        ",ISNULL([IS_FORM],0)" +//24
        ",ISNULL([DATE_REGISTRATION], N'01.01.1970')" +//25
         " FROM [Conference].[dbo].[EXP_PATIENT_IACMAC]";
            var cmd = new SqlCommand(sql, conn);
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }
            //         cmd.CommandTimeout = 120;
            var rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                var obj = DataManager.Instance.CreateObject<Person>();

                while (rdr.Read())
                {
                    obj = DataManager.Instance.CreateObject<Person>();
                    obj.Id = GuidComb.Generate();
                    obj.SourceId = rdr.GetInt32(0);
                    obj.FirstName = rdr.GetString(3);
                    obj.SecondName = rdr.GetString(4);
                    obj.ThirdName = rdr.GetString(5);
                    obj.Sex = DataManager.Instance.GetSexBySourceId((rdr.GetByte(6)));
                    obj.WorkPlace = rdr.GetString(7);
                    obj.Post = rdr.GetString(8);
                    obj.Speciality = DataManager.Instance.GetSpecialityBySourceId(rdr.GetInt32(9));
                    obj.ScienceDegree = DataManager.Instance.GetScienceDegreeBySourceID(rdr.GetInt32(10));
                    obj.ScienceStatus = DataManager.Instance.GetScienceStatusBySourceId(rdr.GetInt32(11));

                    DataManager.Instance.AddPerson(obj);
                    if (rdr.GetInt32(18) > 0)
                    {
                        var iacmac = DataManager.Instance.CreateObject<Iacmac>();

                        iacmac = DataManager.Instance.CreateObject<Iacmac>();
                        iacmac.PersonId = obj.Id;
                        iacmac.Code = rdr.GetString(21);
                        iacmac.DateRegistration = rdr.GetDateTime(25);
                        iacmac.IsCardCreate = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetByte(22));
                        iacmac.IsCardSent = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetByte(23));
                        iacmac.IsForm = DataManager.Instance.GetBoolLogicBySourceId((rdr.GetByte(24)));
                        iacmac.IsMember = DataManager.Instance.GetBoolLogicBySourceId(rdr.GetByte(19));
                        iacmac.Number = rdr.GetInt32(20);

                        DataManager.Instance.AddIacmacToPerson(obj, iacmac);
                    }
                    personsCount++;
                }
            }
            rdr.Close();
            rdr = null;
            // res = 1;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " || " + ex.InnerException + "||" + ex.Source + "||" + ex.StackTrace);
            //    res = 0;
            //}
            return personsCount;
        }


        public int ImportUsers()
        {
            var res = 0;
            try
            {
                var sql =
                   "SELECT [ABSTRACT_RESPONSIBLE_PERSON_ID]" +
                   ",[ABSTRACT_RESPONSIBLE_PERSON_NAME]" +
                   ",[ABSTRACT_RESPONSIBLE_PERSON_EMAIL]" +
                   ",[DATE_ADD],[DATE_UPDATE]" +
                   ",[SUSER],[SOURCE_ID] " +
                   "FROM [Conference].[dbo].[abstract_responsible_person]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    User obj;
                    obj = DataManager.Instance.CreateObject<User>();
                    obj.Id = GuidComb.Generate();
                    obj.Password = "---";
                    obj.Email = "---";
                    obj.Name = "-";
                    obj.FullName = "---";
                    obj.SourceId = 0;
                    obj.Role += "---";
                    DataManager.Instance.AddUser(obj);
                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<User>();
                        obj.Id = GuidComb.Generate();
                        obj.Password = "111";
                        obj.Email = rdr.GetString(2);
                        obj.Name = rdr.GetString(1);
                        obj.FullName = rdr.GetString(1);
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
                    Speciality obj;
                    obj = DataManager.Instance.CreateObject<Speciality>();
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
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                // res++;
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
                    "SELECT [ADDRESS_TYPE_ID]" +
                    ",[ADDRESS_TYPE_ENG]" +
                    ",[ADDRESS_TYPE_RUS]" +
                    ",[DATE_ADD]" +
                    ",[DATE_UPDATE]" +
                    ",[SUSER]" +
                    ",[SOURCE_ID] FROM [Conference].[dbo].[address_type]";
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
                    //obj.Id = GuidComb.Generate();
                    //obj.Code = "-";
                    //obj.Name = "-";
                    //obj.SourceId = 0;
                    //DataManager.Instance.AddContactType(obj);

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
                    ScienceStatus obj;
                    obj = DataManager.Instance.CreateObject<ScienceStatus>();
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
                    ScienceDegree obj;
                    obj = DataManager.Instance.CreateObject<ScienceDegree>();
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
                    Rank obj;
                    //obj = DataManager.Instance.CreateObject<Rank>();
                    //obj.Id = GuidComb.Generate();
                    //obj.Code = "-";
                    //obj.Name = "-";
                    //obj.SourceId = 0;
                    //DataManager.Instance.AddRank(obj);
                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Rank>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddRank(obj);
                        res++;

                    }
                }
                rdr.Close();
                rdr = null;
                // res = 1;
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
                    PaymentType obj;
                    //obj = DataManager.Instance.CreateObject<PaymentType>();
                    //obj.Id = GuidComb.Generate();
                    //obj.Code = "-";
                    //obj.Name = "-";
                    //obj.SourceId = 0;
                    //DataManager.Instance.AddPaymentType(obj);
                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<PaymentType>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddPaymentType(obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //   res = 1;
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
                    Company obj;

                    //obj = DataManager.Instance.CreateObject<Company>();
                    //obj.Id = GuidComb.Generate();
                    //obj.Code = "-";
                    //obj.Name = "-";
                    //obj.SourceId = 0;
                    //DataManager.Instance.AddCompany(obj);
                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Company>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddCompany(obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //     res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }

        public int ImportAdresses()
        {
            var res = 0;
            try
            {
                var sql =
                    "SELECT " +
                    "[ADDRESS_ID]," +//0
                    "[PERSON_ID]," +//1
                    "[ADDRESS_TYPE_ID]," +//2
                    "[COUNTRY_ID]," +//3
                    "[REGION_ID]," +//4
                    "[CITY_ID]," +//5
                    "[CITY_NAME]," +//6
                    "[ZIP]," +//7
                    "[STREET_HOUSE_FLAT]," +//8
                    "[COUNTRY_RUS]," +//9
                    "[REGION_RUS]," +//10
                    "[CITY_RUS] " +//11
                    "FROM [Conference].[dbo].[EXP_ADDRESS]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<Address>();

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Address>();
                        obj.Id = GuidComb.Generate();
                        obj.SourceId = rdr.GetInt32(0);
                        obj.ContactTypeId = DataManager.Instance.GetContactTypeBySourceId(rdr.GetInt32(2)).Id;
                        obj.ZipCode = rdr.GetString(7);
                        obj.StreetHouseName = rdr.GetString(8);
                        obj.CountryName = rdr.GetString(9);
                        obj.RegionName = rdr.GetString(10);
                        obj.CityName = rdr.GetString(11);
                        obj.PersonId = DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)).Id;


                        DataManager.Instance.AddAddressToPerson(DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)), obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //     res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }
        public int ImportPhones()
        {
            var res = 0;
            Phone obj;
            try
            {
                var sql =
                    "SELECT " +
                    "[PHONE_ID]" +//0
                    ",[PERSON_ID]" +//1
                    ",[PHONE_TYPE_ID]" +//2
                    ",[PHONE_CODE]" +//3
                    ",[PHONE_NUMBER] " +//4
                    "FROM [Conference].[dbo].[EXP_PHONES]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    obj = DataManager.Instance.CreateObject<Phone>();

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Phone>();
                        obj.Id = GuidComb.Generate();
                        obj.SourceId = rdr.GetInt32(0);
                        obj.ContactTypeId = DataManager.Instance.GetContactTypeBySourceId(rdr.GetInt32(2)).Id;
                        obj.Number = "(" + rdr.GetString(3) + ") " + rdr.GetString(4);
                        obj.PersonId = DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)).Id;


                        DataManager.Instance.AddPhoneToPerson(DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)), obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //     res = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " || " + ex.InnerException);
                res = 0;
            }
            return res;
        }
        public int ImportEmails()
        {
            var res = 0;
            try
            {
                var sql =
                    "SELECT " +
                    "[EMAIL_ID]" +//0
                    ",[PERSON_ID]" +//1
                    ",[EMAIL] " +//2
                    "FROM [Conference].[dbo].[EXP_EMAILS]";
                var cmd = new SqlCommand(sql, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.CommandTimeout = 120;
                var rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var obj = DataManager.Instance.CreateObject<Email>();

                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Email>();
                        obj.Id = GuidComb.Generate();
                        obj.SourceId = rdr.GetInt32(0);
                        obj.ContactTypeId = DataManager.Instance.GetContactTypeBySourceId(0).Id;
                        obj.Name = rdr.GetString(2);
                        obj.PersonId = DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)).Id;


                        DataManager.Instance.AddEmailToPerson(DataManager.Instance.GetPersonBySourceId(rdr.GetInt32(1)), obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //     res = 1;
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
                    AbstractStatus obj;
                    //obj = DataManager.Instance.CreateObject<AbstractStatus>();
                    //obj.Id = GuidComb.Generate();
                    //obj.Code = "-";
                    //obj.Name = "-";
                    //obj.SourceId = 0;
                    //DataManager.Instance.AddAbstractStatus(obj);
                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<AbstractStatus>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddAbstractStatus(obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //     res = 1;
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
                    Conference obj;
                    //obj = DataManager.Instance.CreateObject<Conference>();
                    //obj.Id = GuidComb.Generate();
                    //obj.Code = "-";
                    //obj.Name = "-";
                    //obj.SourceId = 0;
                    //DataManager.Instance.AddConference(obj);
                    while (rdr.Read())
                    {
                        obj = DataManager.Instance.CreateObject<Conference>();
                        obj.Id = GuidComb.Generate();
                        obj.Code = "";
                        obj.Name = rdr.GetString(2);
                        obj.SourceId = rdr.GetInt32(0);
                        DataManager.Instance.AddConference(obj);
                        res++;
                    }
                }
                rdr.Close();
                rdr = null;
                //   res = 1;
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
