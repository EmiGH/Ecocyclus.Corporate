using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Condesus.EMS.WebUI
{
    public class Global : System.Web.HttpApplication
    {
        #region Private Properties
        public static String _ServerPathMediaFiles;

        private static Condesus.EMS.Business.DS.Entities.Language m_DefaultLanguage;
        private static Dictionary<String, Condesus.EMS.Business.DS.Entities.Language> _Languages;

        #endregion

        #region Public Properties

        public static Condesus.EMS.Business.DS.Entities.Language DefaultLanguage
        {
            get { return m_DefaultLanguage; }
        }
        public static Dictionary<String, Condesus.EMS.Business.DS.Entities.Language> Languages
        {
            get
            {
                if (_Languages != null)
                {
                    return _Languages;
                }
                return Condesus.EMS.Business.DS.DirectoryServices.LanguagesOptions();
            }
        }

        #endregion

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["Navigator"] = new Common.Navigator();

        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //Cargo las opciones de idioma de la solución
            _Languages = Condesus.EMS.Business.DS.DirectoryServices.LanguagesOptions();
            _ServerPathMediaFiles = Server.MapPath("~/ManagementTools/KnowledgeCollaboration/");

            foreach (Condesus.EMS.Business.DS.Entities.Language oLanguage in _Languages.Values)
            {
                if (oLanguage.IsDefault)
                {
                    //Dejo en una variable la opción de idioma por defecto
                    m_DefaultLanguage = oLanguage;
                    break;
                }
            }
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception objError = Server.GetLastError().GetBaseException();
            String Message = "\n\nURL:\n http://localhost/" + Request.Path
                                   + "\n\nMESSAGE:\n " + Server.GetLastError().Message
                                   + "\n\nSTACK TRACE:\n" + Server.GetLastError().StackTrace;

            // Create event Log if it does not exist
            String LogName = "Application";
            if (!EventLog.SourceExists(LogName))
            {
                EventLog.CreateEventSource(LogName, LogName);
            }

            // Insert into event log
            EventLog Log = new EventLog();
            Log.Source = LogName;
            Log.WriteEntry(Message, EventLogEntryType.Error);

            if (System.Configuration.ConfigurationManager.AppSettings["customErrorAutomaticLogging"].ToLower() == "on")
            {
                WriteErrorToLog(objError);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["customErrorAutomaticEmail"].ToLower() == "on")
            {
                EmailError(objError, System.Configuration.ConfigurationManager.AppSettings["customErrorEmailAddressTo"], System.Configuration.ConfigurationManager.AppSettings["customErrorEmailAddressFrom"]);

            }
            Server.ClearError();
            Context.Items.Add("Error", objError);
            Server.Transfer("/ErrorLogged.aspx");
        }
        protected Boolean WriteErrorToLog(Exception objError)
        {
            try
            {
                String strError = objError.ToString();
                String strSource = "Condesus EMS";
                String strEventLog = "Condesus EMS Log";

                // Create the event log if it does not exist
                if (!System.Diagnostics.EventLog.SourceExists(strEventLog)) System.Diagnostics.EventLog.CreateEventSource(strSource, strEventLog);
                // Write to the event log
                System.Diagnostics.EventLog objEventLog = new System.Diagnostics.EventLog();
                objEventLog.Source = strSource;
                objEventLog.WriteEntry(strError, System.Diagnostics.EventLogEntryType.Error);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected Boolean EmailError(Exception Error, String To, String From)
        {
            try
            {
                String strError = Error.ToString();
                String strErrorPath = HttpContext.Current.Request.FilePath;

                System.Text.StringBuilder sbMessage = new System.Text.StringBuilder();
                sbMessage.Append("\nError Occured: ");
                sbMessage.Append(DateTime.Now.ToString());
                sbMessage.Append("\nFile:");
                sbMessage.Append(strErrorPath);
                sbMessage.Append("\nError: \n");
                sbMessage.Append(strError);

                System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
                sc.Host = "localhost";
                sc.Port = 25;
                System.Net.Mail.MailMessage oMailMessage = new System.Net.Mail.MailMessage();
                oMailMessage.To.Add(To);
                System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(From, "EMS Error Notification");
                oMailMessage.From = fromAddress;
                oMailMessage.Subject = "EMS Error Notification";
                oMailMessage.Body = sbMessage.ToString();
                sc.Send(oMailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected void Application_End(object sender, EventArgs e)
        {

        }
        protected void Session_OnEnd(object sender, EventArgs e)
        {
            //Delete de todo lo del path;
            //_ServerPathMediaFiles
            //Session.SessionID;

            System.IO.FileInfo[] _myFilesMediaToDelete;
            String _fileNamePrefix = Session.SessionID.ToString() + "_";
            _myFilesMediaToDelete = new DirectoryInfo(_ServerPathMediaFiles).GetFiles(_fileNamePrefix + "*.*");

            for (int i = 0; i < _myFilesMediaToDelete.Length; i++)
            {
                _myFilesMediaToDelete[i].Delete();
            }
        }

        //Este evento permite la compresion de las paginas en cada request...(siempre y cuando el cliente lo soporte)
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        HttpApplication app = (HttpApplication)sender;

        //        string acceptEncoding = app.Request.Headers["Accept-Encoding"];
        //        Stream prevUncompressedStream = app.Response.Filter;

        //        if (acceptEncoding == null || acceptEncoding.Length == 0)
        //        {
        //            return;
        //        }

        //        acceptEncoding = acceptEncoding.ToLower();

        //        //Saco a la pagina de viewer del medio, para que no me comprima los archivos a mostrar.
        //        switch (app.Context.Request.FilePath)
        //        {
        //            case "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx":
        //            case "/AdministrationTools/ProcessesFramework/ShowFileAudit.aspx":
        //            case "/AdministrationTools/ProcessesFramework/ShowFileAttach.aspx":
        //                break;

        //            //Todas las demas se comprimen...
        //            default:
        //                if (app.Context.Request.FilePath.EndsWith(".aspx") || app.Context.Request.FilePath.EndsWith(".ashx"))
        //                {
        //                    if (acceptEncoding.Contains("gzip"))
        //                    { // gzip
        //                        app.Response.Filter = new GZipStream(prevUncompressedStream, CompressionMode.Compress);
        //                        app.Response.AppendHeader("Content-Encoding", "gzip");
        //                    }
        //                    else if (acceptEncoding.Contains("deflate"))
        //                    { // defalte
        //                        app.Response.Filter = new DeflateStream(prevUncompressedStream, CompressionMode.Compress);
        //                        app.Response.AppendHeader("Content-Encoding", "deflate");
        //                    }
        //                }
        //                break;
        //        }    
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        
    }
}