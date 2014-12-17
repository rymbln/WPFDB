using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Common
{
    public class DefaultManager
    {
        private static DefaultManager instance;
        //  private Conference defaultConference;

        public static DefaultManager Instance
        {
            get { return instance ?? (instance = new DefaultManager()); }
        }



        public string MailServer
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_SERVER");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_SERVER";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_SERVER");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_SERVER");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_SERVER";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_SERVER";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string AbstractFilePath
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("ABSTRACT_FILE_PATH");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "ABSTRACT_FILE_PATH";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("ABSTRACT_FILE_PATH");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("ABSTRACT_FILE_PATH");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "ABSTRACT_FILE_PATH";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "ABSTRACT_FILE_PATH";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }


        public string MailPort
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_PORT");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_PORT";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_PORT");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_PORT");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_PORT";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_PORT";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string MailLogin
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_LOGIN");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_LOGIN";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_LOGIN");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_LOGIN");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_LOGIN";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_LOGIN";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string MailPassword
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_PASSWORD");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_PASSWORD";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_PASSWORD");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_PASSWORD");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_PASSWORD";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_PASSWORD";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string MailHeaderAbstract
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_HEADER_ABSTRACT");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_HEADER_ABSTRACT";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_HEADER_ABSTRACT");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_HEADER_ABSTRACT");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_HEADER_ABSTRACT";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_HEADER_ABSTRACT";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string MailMessagePosterSession
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_POSTER_SESSION");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_POSTER_SESSION";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_POSTER_SESSION");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_POSTER_SESSION");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_POSTER_SESSION";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_MESSAGE_POSTER_SESSION";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }


        public string MailHeaderPoster
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_HEADER_POSTER");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_HEADER_POSTER";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_HEADER_POSTER");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_HEADER_POSTER");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_HEADER_POSTER";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_HEADER_POSTER";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }


        public string MailMessageWork
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_WORK");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_WORK";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_WORK");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_WORK");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_WORK";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_MESSAGE_WORK";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string MailMessagePositive
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_POSITIVE");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_POSITIVE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_POSITIVE");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_POSITIVE");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_POSITIVE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_MESSAGE_POSITIVE";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public string MailMessageNegative
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_NEGATIVE");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_NEGATIVE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_NEGATIVE");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_NEGATIVE");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_NEGATIVE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_MESSAGE_NEGATIVE";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }


        public string MailMessageNegativeSecond
        {
            get
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_NEGATIVE_SECOND");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_NEGATIVE_SECOND";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "---";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueDecimal = 0;
                    newProp.ValueInt = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_NEGATIVE_SECOND");
                return def.ValueString;
            }
            set
            {
                var def = DataManager.Instance.GetPropertyValue("MAIL_MESSAGE_NEGATIVE_SECOND");
                if (def == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "MAIL_MESSAGE_NEGATIVE_SECOND";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = value;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 1;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    def.Name = "MAIL_MESSAGE_NEGATIVE_SECOND";
                    def.ValueGuid = def.Id;
                    def.ValueString = value;
                    def.ValueDate = DateTime.Now;
                    def.ValueInt = 1;
                    def.ValueDecimal = 1;
                    def.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public bool ConferenceMode
        {
            get
            {
                var defConf = DataManager.Instance.GetPropertyValue("CONFERENCE_MODE");
                if (defConf == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "CONFERENCE_MODE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "False";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 0;
                    newProp.ValueDecimal = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                defConf = DataManager.Instance.GetPropertyValue("CONFERENCE_MODE");
                return defConf.ValueLogic;
            }
            set
            {
                var defConf = DataManager.Instance.GetPropertyValue("CONFERENCE_MODE");
                if (defConf == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "CONFERENCE_MODE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "False";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 0;
                    newProp.ValueDecimal = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    if (value)
                    {
                        defConf.ValueGuid = defConf.Id;
                        defConf.ValueString = "True";
                        defConf.ValueDate = DateTime.Now;
                        defConf.ValueInt = 1;
                        defConf.ValueDecimal = 1;
                        defConf.ValueLogic = true;
                    }
                    else
                    {
                        defConf.ValueGuid = defConf.Id;
                        defConf.ValueString = "False";
                        defConf.ValueDate = DateTime.Now;
                        defConf.ValueInt = 0;
                        defConf.ValueDecimal = 0;
                        defConf.ValueLogic = false;
                    }

                    DataManager.Instance.Save();
                }
            }
        }

        public bool RegistrationMode
        {
            get
            {
                var defConf = DataManager.Instance.GetPropertyValue("REGISTRATION_MODE");
                if (defConf == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "REGISTRATION_MODE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "False";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 0;
                    newProp.ValueDecimal = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                defConf = DataManager.Instance.GetPropertyValue("REGISTRATION_MODE");
                return defConf.ValueLogic;
            }
            set
            {
                var defConf = DataManager.Instance.GetPropertyValue("REGISTRATION_MODE");
                if (defConf == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "REGISTRATION_MODE";
                    newProp.ValueGuid = newProp.Id;
                    newProp.ValueString = "False";
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = 0;
                    newProp.ValueDecimal = 0;
                    newProp.ValueLogic = false;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    if (value)
                    {
                        defConf.ValueGuid = defConf.Id;
                        defConf.ValueString = "True";
                        defConf.ValueDate = DateTime.Now;
                        defConf.ValueInt = 1;
                        defConf.ValueDecimal = 1;
                        defConf.ValueLogic = true;
                    }
                    else
                    {
                        defConf.ValueGuid = defConf.Id;
                        defConf.ValueString = "False";
                        defConf.ValueDate = DateTime.Now;
                        defConf.ValueInt = 0;
                        defConf.ValueDecimal = 0;
                        defConf.ValueLogic = false;
                    }

                    DataManager.Instance.Save();
                }
            }
        }

        public Conference DefaultConference
        {
            get
            {
                var defConf = DataManager.Instance.GetPropertyValue("ACTIVE_CONFERENCE");
                if (defConf == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "ACTIVE_CONFERENCE";
                    var conf = DataManager.Instance.GetConferenceBySourceId(0);
                    newProp.ValueGuid = conf.Id;
                    newProp.ValueString = conf.Name;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = conf.SourceId;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                defConf = DataManager.Instance.GetPropertyValue("ACTIVE_CONFERENCE");
                var obj = DataManager.Instance.GetConferenceById(defConf.ValueGuid);
                if (obj == null)
                {
                    obj = DataManager.Instance.GetConferenceByName(defConf.ValueString);
                }
                return obj;
            }
            set
            {
                var defConf = DataManager.Instance.GetPropertyValue("ACTIVE_CONFERENCE");
                if (defConf == null)
                {
                    var newProp = DataManager.Instance.CreateObject<Propertie>();
                    newProp.Id = GuidComb.Generate();
                    newProp.Name = "ACTIVE_CONFERENCE";
                    newProp.ValueGuid = value.Id;
                    newProp.ValueString = value.Name;
                    newProp.ValueDate = DateTime.Now;
                    newProp.ValueInt = value.SourceId;
                    newProp.ValueDecimal = 1;
                    newProp.ValueLogic = true;
                    DataManager.Instance.AddPropertie(newProp);
                }
                else
                {
                    defConf.ValueGuid = value.Id;
                    defConf.ValueString = value.Name;
                    defConf.ValueDate = DateTime.Now;
                    defConf.ValueInt = value.SourceId;
                    defConf.ValueDecimal = 1;
                    defConf.ValueLogic = true;
                    DataManager.Instance.Save();
                }
            }
        }

        public Sex DefaultSex
        {
            get
            {
                var list = DataManager.Instance.GetAllSexes();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public Speciality DefaultSpeciality
        {
            get
            {
                var list = DataManager.Instance.GetAllSpecialities();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public ScienceDegree DefaultScienceDegree
        {
            get
            {
                var list = DataManager.Instance.GetAllScienceDegrees();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public ScienceStatus DefaultScienceStatus
        {
            get
            {
                var list = DataManager.Instance.GetAllScienceStatuses();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public Rank DefaultRank
        {
            get
            {
                var list = DataManager.Instance.GetAllRanks();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public Company DefaultCompany
        {
            get
            {
                var list = DataManager.Instance.GetAllCompanies();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public OrderStatus DefaultOrderStatus
        {
            get
            {
                var list = DataManager.Instance.GetAllOrderStatuses();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }
        public PaymentType DefaultPaymentType
        {
            get
            {
                var list = DataManager.Instance.GetAllPaymentTypes();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public ContactType DefaultContactType
        {
            get
            {
                var list = DataManager.Instance.GetAllContactTypes();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }


        public PersonConferences_Detail DefaultPersonConferenceDetail(Guid personConferenceId)
        {

            if (personConferenceId == null)
            {
                throw new ArgumentNullException("personConferenceId");
            }
            var pcd = DataManager.Instance.CreateObject<PersonConferences_Detail>();
            pcd.PersonConferenceId = personConferenceId;
            pcd.IsAutoreg = false;
            pcd.IsAbstract = false;
            pcd.IsArrive = false;
            pcd.IsBadge = false;
            pcd.IsNeedBadge = true;
            pcd.Rank = DefaultRank;
            pcd.Company = DefaultCompany;
            return pcd;
        }

        public PersonConferences_Payment DefaultPersonConferencePayment(Guid personConferenceId)
        {
            if (personConferenceId == null)
            {
                throw new ArgumentNullException("personConferenceId");
            }
            var pcp = DataManager.Instance.CreateObject<PersonConferences_Payment>();
            pcp.PersonConferenceId = personConferenceId;
            pcp.Company = DefaultCompany;
            pcp.IsComplect = false;
            pcp.Money = 0;
            pcp.OrderNumber = 0;
            pcp.OrderStatus = DefaultOrderStatus;
            pcp.PaymentType = DefaultPaymentType;
            pcp.PaymentDate = DateTime.Now;
            pcp.PaymentDocument = "---";
            return pcp;
        }

        private Iacmac DefaultIacmac(Guid personId)
        {
            if (personId == null)
            {
                throw new ArgumentNullException("personId");
            }
            var iacmac = DataManager.Instance.CreateObject<Iacmac>();
            iacmac.PersonId = personId;
            iacmac.Code = "-";
            iacmac.IsMember = false;
            iacmac.IsCardCreate = false;
            iacmac.IsCardSent = false;
            iacmac.IsForm = false;
            return iacmac;
        }




        public Person DefaultPerson
        {
            get
            {
                var p = DataManager.Instance.CreateObject<Person>();
                p.Id = GuidComb.Generate();
              //  p.BirthDate = Convert.ToDateTime("01/01/1900");
                p.FirstName = "---";
                p.SecondName = "---";
                p.ThirdName = "---";
                p.Sex = DefaultSex;
                p.Speciality = DefaultSpeciality;
                p.ScienceDegree = DefaultScienceDegree;
                p.ScienceStatus = DefaultScienceStatus;
                p.Iacmac = DefaultIacmac(p.Id);
                DataManager.Instance.AddPerson(p);
                DataManager.Instance.Save();

          //      var pc = DataManager.Instance.CreateObject<PersonConference>();
           //     pc.PersonConferenceId = GuidComb.Generate();
              //  pc.PersonId = p.Id;
               // pc.Conference = DefaultConference;

             //   pc.PersonConferences_Payment = DefaultPersonConferencePayment(pc.PersonConferenceId);
             //   pc.PersonConferences_Detail = DefaultPersonConferenceDetail(pc.PersonConferenceId);

                DataManager.Instance.AddPersonConference(p, DefaultConference);
                DataManager.Instance.Save();
                return p;
            }
        }




        public Email DefaultEmail
        {
            get
            {
                var e = DataManager.Instance.CreateObject<Email>();
                e.Id = GuidComb.Generate();
                e.Name = "---";
                e.ContactType = DefaultContactType;
                return e;
            }
        }

        public Address DefaultAddress
        {
            get
            {
                var a = DataManager.Instance.CreateObject<Address>();
                a.Id = GuidComb.Generate();
                a.ZipCode = "000000";
                a.CountryName = "---";
                a.RegionName = "---";
                a.CityName = "---";
                a.StreetHouseName = "---";
                a.ContactType = DefaultContactType;
                return a;
            }
        }

        public Phone DefaultPhone
        {
            get
            {
                var p = DataManager.Instance.CreateObject<Phone>();
                p.Id = GuidComb.Generate();
                p.Number = "000000";
                p.ContactType = DefaultContactType;

                return p;
            }
        }

        public Abstract DefaultAbstract
        {
            get
            {
                var abs = DataManager.Instance.CreateObject<Abstract>();
                abs.Id = GuidComb.Generate();
                abs.Name = "---";
                abs.OtherAuthors = "---";
                abs.Text = "---";
                abs.Link = "---";
                return abs;
            }
        }



        public User DefaultResponsiblePerson
        {
            get
            {
                return DataManager.Instance.GetUsers().FirstOrDefault();
            }
        }

        public AbstractStatus DefaultAbstractStatus
        {
            get { return DataManager.Instance.GetAllAbstractStatuses().FirstOrDefault(o => o.Code == "-"); }
        }

        public AbstractWork DefaultAbstractWork
        {
            get
            {
                var obj = DataManager.Instance.CreateObject<AbstractWork>();
                obj.Id = GuidComb.Generate();
                obj.AbstractStatus = DefaultAbstractStatus;
                obj.Reviewer = DefaultResponsiblePerson;
                obj.IsSentByEmail = false;
                obj.DateWork = DateTime.Now;
                return obj;
            }
        }


        public string GetMailServer()
        {
            return "smtp.rambler.ru";
        }

        public string GetMailLogin()
        {
            return "614614@rambler.ru";
        }

        public string GetMailPassword()
        {
            return "windofchange";
        }

        public string GetMailFrom()
        {
            return "rymbln@gmail.com";
        }

        public int GetMailPort()
        {
            return 465;
        }

        public Badge DefaultBadgeElement
        {
            get
            {
                var obj = DataManager.Instance.CreateObject<Badge>();
                obj.Id = GuidComb.Generate();
                obj.PositionX1 = 0;
                obj.PositionX2 = 0;
                obj.PositionX3 = 0;
                obj.PositionX4 = 0;
                obj.PositionY1 = 0;
                obj.PositionY2 = 0;
                obj.PositionY3= 0;
                obj.PositionY4 = 0;
                obj.Width = 100;
                obj.Height = 50;
                obj.Value = "---";
                obj.BackgroundColor = "#FFFFFF";
                
                obj.Font = "Default";
                obj.FontColor = "#FFFFFF";
                obj.FontSize = 10;
                obj.ForegroundColor = "#FFFFFF";
                obj.Name = "Name";
                return obj;
            }
        }
    }
}
