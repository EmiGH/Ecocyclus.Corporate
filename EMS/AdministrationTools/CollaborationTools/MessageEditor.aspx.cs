using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.CT.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.CollaborationTools
{
    public partial class MessageEditor : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private Int64 _IdForum
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdForum") ? base.NavigatorGetTransferVar<Int64>("IdForum") : Convert.ToInt64(GetPKfromNavigator("IdForum"));
                }
            }
            private Int64 _IdCategory
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdCategory") ? base.NavigatorGetTransferVar<Int64>("IdCategory") : Convert.ToInt64(GetPKfromNavigator("IdCategory"));
                }
            }
            private Int64 _IdTopic
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdTopic") ? base.NavigatorGetTransferVar<Int64>("IdTopic") : Convert.ToInt64(GetPKfromNavigator("IdTopic"));
                }
            }
            private Int64 _IdParentMessage
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentMessage") ? base.NavigatorGetTransferVar<Int64>("IdParentMessage") : Convert.ToInt64(GetPKfromNavigator("IdParentMessage"));
                }
            }
            private Int64 _IdMessage
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdMessage") ? base.NavigatorGetTransferVar<Int64>("IdMessage") : Convert.ToInt64(GetPKfromNavigator("IdMessage"));
                }
            }
            private Message _Entity = null;
            private Message Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            //Construye el foro
                            ActiveForum _activeForum = ((ActiveForum)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum));
                            //Construye la categoria
                            //Category _category = _activeForum.Category(_IdCategory);
                            //Construye el Topic
                            ActiveTopic _activeTopic = (ActiveTopic)_activeForum.Topic(_IdTopic);
                            //Finalmente retorna el mensaje
                            return _activeTopic.Message(_IdMessage);
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
        #endregion

        #region Load & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                
                RemoveToolsFromRadEditor();

                if (!Page.IsPostBack)
                {
                    LoadMessage();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.MessageForum;
            }
            //Setea el Sub Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = (Entity != null) ? Entity.Topic.Title : Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void RemoveToolsFromRadEditor()
            {
                reMessage.EnsureToolsFileLoaded();
                reMessage.Tools[0].Tools.RemoveAt(1);           //Saca el SpellCheck
                reMessage.Tools[1].Tools.RemoveAt(0);           //Saca los dialogos de inserts image.
                reMessage.Tools[1].Tools.RemoveAt(0);           //Saca los dialogos de inserts document.
                reMessage.Tools[1].Tools.RemoveAt(0);           //Saca los dialogos de inserts flash.
                reMessage.Tools[1].Tools.RemoveAt(0);           //Saca los dialogos de inserts media.
                reMessage.Tools[1].Tools.RemoveAt(0);           //Saca los dialogos de inserts template.
                reMessage.Tools[1].Tools.RemoveAt(0);           //Saca el separador antes del link
                reMessage.Tools[5].Tools.RemoveAt(2);           //Saca la configuracion de Style CSS.
                reMessage.Tools[6].Tools.RemoveAt(4);           //Saca los inserts image map editor.
            }
            private void LoadMessage()
            {
                if (_IdParentMessage == 0)
                {
                    //Puede ser un Edit del mensaje
                    if (Entity != null)
                    {
                        //Como es un edit, entonces agrego el mensaje original para que se visualice pero sin editar.
                        String _content = "<span style=\"background-color: #b8cce4;\"><fieldset><legend>" + Resources.Common.forumEditing  + ":</legend>"
                            + Entity.Text
                            + "</fieldset> \r\n\r\n</span>";

                        redtOriginalMessage.Visible = true;
                        redtOriginalMessage.Content = _content;
                    }
                }
                else
                {
                    //Puede ser una respuesta a un mensaje...
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processGroupProcess = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess);
                    //Category _category = ((ActiveForum)_processGroupProcess.Forum(_IdForum)).Category(_IdCategory);
                    ActiveTopic _activeTopic = (ActiveTopic)((ActiveForum)_processGroupProcess.Forum(_IdForum)).Topic(_IdTopic);
                    Message _message = _activeTopic.Message(_IdParentMessage);

                    if (_message != null)
                    {
                        String _content = "<span style=\"background-color: #b8cce4;\"><fieldset><legend>" + _message.Person.FullName + " " + Resources.Common.forumWrote + ":</legend>"
                            + _message.Text
                            + "</fieldset> \r\n\r\n</span>";

                        redtOriginalMessage.Visible = true;
                        redtOriginalMessage.Content = _content;
                    }
                }
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        //Alta
                        ActiveForum _activeForum = ((ActiveForum)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum));
                        //Category _category = _activeForum.Category(_IdCategory);
                        ActiveTopic _activeTopic = (ActiveTopic)_activeForum.Topic(_IdTopic);
                        NormalMessage _parentMessage = (NormalMessage)_activeTopic.Message(_IdParentMessage);

                        if (_parentMessage == null)
                        {
                            //Si no tiene parent, quiere decir que es un mensaje comentario
                            _activeTopic.MessageAdd(reMessage.Content);
                        }
                        else
                        {
                            //Al ser una respuesta, citamos al original...que ya tenemos...
                            String _content = redtOriginalMessage.Content
                                + "<br /><br />"
                                + reMessage.Content;

                            //Quiere decir que es una respuesta
                            _activeTopic.MessageAdd(_content);
                        }
                    }
                    else
                    {
                        String _content = Entity.Text
                        + "\r\n\r\n"
                        + "<br /><br />"
                        + Resources.Common.forumEdit + " " + Entity.Person.FullName + " - " + DateTime.Now.ToString();

                        //Modificacion
                        //    Entity = EMSLibrary.User.CollaborationTools.Configuration.Forum(_IdForum).Category(_IdCategory).Topic(_IdTopic).MessagesModify(Entity.IdMessage, reMessage.Content);
                    }

                    base.NavigateBack();

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion

    }
}
