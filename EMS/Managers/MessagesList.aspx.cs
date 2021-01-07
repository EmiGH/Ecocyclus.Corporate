using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.CT.Entities;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.Managers
{
    public partial class MessagesList : BasePage
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
        #endregion

        #region PageLoad & Init
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                LoadData();
                PostReplyLinkBottom.Click += new EventHandler(PostReplyLink_Click);
                PostReplyLinkUp.Click += new EventHandler(PostReplyLink_Click);
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.Messages;
            }
            //Setea el Sub Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleList;
            }
        #endregion

        #region Private Methods
            private void LoadData()
            {
                Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processGroupProcess = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess);
                //Condesus.EMS.Business.CT.Entities.Category _category = ((Condesus.EMS.Business.CT.Entities.ActiveForum)_processGroupProcess.Forum(_IdForum)).Category(_IdCategory);
                ActiveTopic _activeTopic = (ActiveTopic)((Condesus.EMS.Business.CT.Entities.ActiveForum)_processGroupProcess.Forum(_IdForum)).Topic(_IdTopic);

                TopicTitle.Text = _activeTopic.Title;
                PostReplyLinkBottom.Text = Resources.Common.PostReply;

                int _count = 0;
                foreach (Message _message in _activeTopic.Messages.Values)
                {
                    MessageList.Controls.Add(BuildRepeaterItem(_count, _message));
                    _count++;
                }
            }
            private RepeaterItem BuildRepeaterItem(int countMessage, Message message)
            {
                RepeaterItem _ri = new RepeaterItem(countMessage, ListItemType.Item);
                //Al contador le sumamos 1 para que no arranque de 0. y en el repeater si se necesita como indice 0.
                _ri.Controls.Add(BuildContent(message, countMessage + 1));
                return _ri;
            }
            private Table BuildTable(String id, String cssClass)
            {
                Table _container = new Table();
                _container.ID = id;
                _container.CssClass = cssClass;
                return _container;
            }
            private TableCell BuildTableCell(String cssClass)
            {
                TableCell _td = new TableCell();
                _td.CssClass = cssClass;
                return _td;
            }
            private Table BuildContent(Message message, Int64 countMessage)
            {
                Table _tbl = new Table();
                _tbl.CellSpacing = 0;
                _tbl.CellPadding = 0;
                  _tbl.CssClass = "TableForum";
                


                TableRow _tr = new TableRow();

                TableCell _tdColUserBox = BuildTableCell("ColUserBox");
                _tdColUserBox.CssClass = "tdColUserBox";
                TableCell _tdColPosted = BuildTableCell("ColPosted");
                _tdColPosted.CssClass = "tdColPosted";

                _tdColUserBox.Controls.Add(BuildUserBox(message));
                _tdColPosted.Controls.Add(BuildPosted(message, countMessage));                

                _tr.Controls.Add(_tdColUserBox);
                _tr.Controls.Add(_tdColPosted);
                _tbl.Controls.Add(_tr);               


                TableRow _tr1 = new TableRow();

                TableCell _tdColMessageFooter = BuildTableCell("ColMessageFooter");
                _tdColMessageFooter.CssClass = "tdColMessageFooter";
                TableCell _tdColBody = BuildTableCell("ColBody");
                _tdColBody.CssClass = "tdColBody";

                _tdColMessageFooter.Controls.Add(BuildMessageFooter(message));
                _tdColBody.Controls.Add(BuildBody(message));

                _tr1.Controls.Add(_tdColMessageFooter);
                _tr1.Controls.Add(_tdColBody);                
                _tbl.Controls.Add(_tr1);                


                TableRow _tr2 = new TableRow();

                TableCell _tdColBack = BuildTableCell("ColBack");
                _tdColBack.CssClass = "tdColBack";
                TableCell _tdColBlank = new TableCell();
                _tdColBlank.CssClass = "tdColBlank";

                _tdColBack.Controls.Add(BuilBack());

                _tr2.Controls.Add(_tdColBack);
                _tr2.Controls.Add(_tdColBlank);
                _tbl.Controls.Add(_tr2);


                TableRow _tr3 = new TableRow();

                TableCell _tdColEnd = new TableCell();
                _tdColEnd.CssClass = "tdColEnd";
                _tdColEnd.ColumnSpan = 2;

                _tr3.Controls.Add(_tdColEnd);
                _tbl.Controls.Add(_tr3);

                return _tbl;


            }
            private Table BuildUserBox(Message message)
            {
                Table _container = BuildTable("UserBox", "UserBox");

                TableRow _tr = new TableRow();

                TableCell _tdName = BuildTableCell("Name");
                _tdName.CssClass = "tdName";

                //Agrego el row, y crea uno nuevo
                _container.Controls.Add(_tr);
                _tr = new TableRow();
                //Label _lblName = new Label();
                //_lblName.ID = "lblName";
                //_lblName.Text = Resources.Common.UserName;  // "Name: ";
                Label _lblNameValue = new Label();
                _lblNameValue.CssClass = "lblNameValue";
                _lblNameValue.ID = "lblNameValue";
                _lblNameValue.Text = message.Person.FullName;
                //_tdName.Controls.Add(_lblName);
                _tdName.Controls.Add(_lblNameValue);
                _tr.Controls.Add(_tdName);

                 //Agrego el ultimo row
                _container.Controls.Add(_tr);

                return _container;
            }
            private Table BuildPosted(Message message, Int64 countMessage)
            {
                Table _container = BuildTable("MessageHeader", "MessageHeader");
                _container.CssClass = "container";
                              
                TableRow _tr = new TableRow();

                TableCell _tdPosted = BuildTableCell("Posted");
                _tdPosted.CssClass = "tdPosted";
                TableCell _tdButtons = BuildTableCell("Buttons");
                _tdButtons.CssClass = "tdButtons";

                Label _lblNumberPosted = new Label();
                _lblNumberPosted.ID = "lblNumberPosted";
                _lblNumberPosted.CssClass = "lblNumberPosted";
                _lblNumberPosted.Text = "#";
                Label _lblNumberPostedValue = new Label();
                _lblNumberPostedValue.CssClass = "lblNumberPostedValue";
                _lblNumberPostedValue.ID = "lblNumberPostedValue";
                _lblNumberPostedValue.Text = countMessage.ToString();
                _tdPosted.Controls.Add(_lblNumberPosted);
                _tdPosted.Controls.Add(_lblNumberPostedValue);
                _tr.Controls.Add(_tdPosted);

                Label _lblPosted = new Label();
                _lblPosted.CssClass = "lblPosted";
                _lblPosted.ID = "lblPosted";
                _lblPosted.Text = " " + Resources.Common.NumberPosted;
                Label _lblPostedValue = new Label();
                _lblPostedValue.CssClass = "lblPostedValue";
                _lblPostedValue.ID = "lblPostedValue";
                _lblPostedValue.Text = message.PostedDate.ToLongDateString() + " " + message.PostedDate.ToLongTimeString();
                _tdPosted.Controls.Add(_lblPosted);
                _tdPosted.Controls.Add(_lblPostedValue);
                _tr.Controls.Add(_tdPosted);

                LinkButton _btnReply = new LinkButton();
                _btnReply.CssClass = "btnReply";
                _btnReply.ID = "btnReply";
                _btnReply.Text = Resources.Common.MessageReply; // "Reply";
                _btnReply.Attributes.Add("IdMessage", message.IdMessage.ToString());
                _btnReply.Attributes.Add("IdParentMessage", message.IdMessage.ToString());
                _btnReply.Click += new EventHandler(btnReply_Click);
                _tdButtons.Controls.Add(_btnReply);
                _tr.Controls.Add(_tdButtons);

                //Si el usuario que posteo originalemente este mensaje es el mismo que esta logeado, le dejo la posibilidad de editar
                if (message.Person.IdPerson == EMSLibrary.User.Person.IdPerson)
                {
                    LinkButton _btnEdit = new LinkButton();
                    _btnEdit.CssClass = "btnEdit";
                    _btnEdit.ID = "btnEdit";
                    _btnEdit.Text = Resources.Common.MessageEdit;       // "Edit";
                    _btnEdit.Attributes.Add("IdMessage", message.IdMessage.ToString());
                    _btnEdit.Attributes.Add("IdParentMessage", message.IdMessage.ToString());
                    _btnEdit.Click += new EventHandler(btnEdit_Click);
                    _tdButtons.Controls.Add(_btnEdit);
                    _tr.Controls.Add(_tdButtons);
                }
                _container.Controls.Add(_tr);

                return _container;
            }
            private Table BuildMessageFooter(Message message)
            {
                Table _container = BuildTable("MessageFooter", "MessageFooter");

                TableRow _tr = new TableRow();
                TableCell _tdImage = BuildTableCell("Avatar");
                TableCell _tdJoined = BuildTableCell("Joined");
                TableCell _tdPosts = BuildTableCell("Posts");

                Image _image = new Image();
                _image.Width = Unit.Pixel(80);
                _image.Height = Unit.Pixel(80);
                _image.ID = "Avatar";
                Int64 _idResource = -1;
                Int64 _idResourceFile = -1;
                try
                {
                    _idResource = message.Person.Pictures.ElementAt(0).Value.IdResource;
                    _idResourceFile = message.Person.Pictures.ElementAt(0).Value.IdResourceFile;
                    _image.ImageUrl = "~/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=" + _idResource.ToString() + "&IdResourceFile=" + _idResourceFile.ToString();
                    //_image.ImageUrl = "~/Skins/Images/Avatar.png";
                }
                catch
                {
                    _image.ImageUrl = "~/Skins/Images/Avatar.png";
                }

                _tdImage.Controls.Add(_image);
                _tr.Controls.Add(_tdImage);

                //Agrego el row, y crea uno nuevo
                _container.Controls.Add(_tr);
                _tr = new TableRow();
                Label _lblJoined = new Label();
                _lblJoined.CssClass = "lblJoined";
                _lblJoined.ID = "lblJoined";
                _lblJoined.Text = Resources.Common.Joined;  // "Joined: ";
                Label _lblJoinedValue = new Label();
                _lblJoinedValue.ID = "lblJoinedValue";
                _lblJoinedValue.Text = ((Condesus.EMS.Business.DS.Entities.PersonwithUser)message.Person).Posts.First().StartDate.ToShortDateString();
                _tdJoined.Controls.Add(_lblJoined);
                _tdJoined.Controls.Add(_lblJoinedValue);
                _tr.Controls.Add(_tdJoined);

                //Agrego el row, y crea uno nuevo
                _container.Controls.Add(_tr);
                _tr = new TableRow();
                Label _lblPosts = new Label();
                _lblPosts.CssClass = "lblPosts";
                _lblPosts.ID = "lblPosts";
                _lblPosts.Text = Resources.Common.PostsCount;   // "Posts: ";
                Label _lblPostsValue = new Label();
                _lblPostsValue.ID = "lblPostsValue";
                _lblPostsValue.Text = message.Person.MessagesPost.Count.ToString();
                _tdPosts.Controls.Add(_lblPosts);
                _tdPosts.Controls.Add(_lblPostsValue);
                _tr.Controls.Add(_tdPosts);


                _container.Controls.Add(_tr);

                return _container;
            }
            private Table BuildBody(Message message)
            {
                Table _container = BuildTable("Body", "Body");
                _container.CssClass = "container";

                TableRow _tr = new TableRow();

                TableCell _tdMessage = BuildTableCell("Message");

                Label _lblMessage = new Label();
                _lblMessage.CssClass = "lblMessage";
                _lblMessage.ID = "lblMessage";
                _lblMessage.Text = message.Text;
                _tdMessage.Controls.Add(_lblMessage);
                _tr.Controls.Add(_tdMessage);

                _container.Controls.Add(_tr);

                return _container;
            }
            private Table BuilBack()
            {
                Table _container = BuildTable("Back", "Back");
                _container.CssClass = "container";

                TableRow _tr = new TableRow();
                TableCell _tdBackTop = BuildTableCell("BackTop");

                LinkButton _lnkBtnBackTop = new LinkButton();
                _lnkBtnBackTop.CssClass = "lnkBtnBackTop";
                _lnkBtnBackTop.ID = "lnkBtnBackTop";
                _lnkBtnBackTop.Text = Resources.Common.BackToTop;   // "Back to Top";
                //_lnkBtnBackTop.Style.Add("color", "#307DB3");
                //_lnkBtnBackTop.Style.Add("font-family", "Verdana");
                //_lnkBtnBackTop.Style.Add("font-size", "10px");
                _lnkBtnBackTop.OnClientClick = "javascript:NavigateBackToTop(this, event);";
                _tdBackTop.Controls.Add(_lnkBtnBackTop);
                _tr.Controls.Add(_tdBackTop);


                _container.Controls.Add(_tr);

                return _container;
            
            }




            private void PostMessage(Int64 idParentMessage, Int64 idMessage, String actionTitleDecorator)
            {
                try
                {
                    base.NavigatorAddTransferVar("IdProcess", _IdProcess);
                    base.NavigatorAddTransferVar("IdForum", _IdForum);
                    base.NavigatorAddTransferVar("IdCategory", _IdCategory);
                    base.NavigatorAddTransferVar("IdTopic", _IdTopic);
                    base.NavigatorAddTransferVar("IdParentMessage", idParentMessage);
                    base.NavigatorAddTransferVar("IdMessage", idMessage);

                    String _pkValues = "IdMessage=" + idMessage.ToString()
                            + "& IdParentMessage=" + idParentMessage.ToString()
                            + "& IdTopic=" + _IdTopic.ToString()
                            + "& IdCategory=" + _IdCategory.ToString()
                            + "& IdForum=" + _IdForum.ToString()
                            + "& IdProcess=" + _IdProcess.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.CT.Message);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    //Navigate("~/AdministrationTools/CollaborationTools/MessageEditor.aspx", Resources.CommonListManage.Message);
                    NavigateEntity("~/AdministrationTools/CollaborationTools/MessageEditor.aspx", Common.ConstantsEntitiesName.CT.Message, TopicTitle.Text, String.Concat(" [", actionTitleDecorator, "]"), NavigateMenuAction.Edit);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }

            }
        #endregion

        #region Page Event
            protected void PostReplyLink_Click(object sender, EventArgs e)
            {
                LinkButton _btn = (LinkButton)sender;
                //Obtiene los atributos del LinkButton
                Int64 _idParentMessage = 0; //Esto es un mensaje para el topic, no una respuesta.
                Int64 _idMessage = 0;   // Convert.ToInt64(_btn.Attributes["IdMessage"]);

                PostMessage(_idParentMessage, _idMessage, _btn.Text);
            }
            protected void btnReply_Click(object sender, EventArgs e)
            {
                LinkButton _btn = (LinkButton)sender;
                //Obtiene los atributos del LinkButton
                Int64 _idParentMessage = Convert.ToInt64(_btn.Attributes["IdParentMessage"]);  //Una respuesta a un mensaje.
                Int64 _idMessage = 0;   // Convert.ToInt64(_btn.Attributes["IdMessage"]);

                PostMessage(_idParentMessage, _idMessage, _btn.Text);
            }
            protected void btnEdit_Click(object sender, EventArgs e)
            {
                LinkButton _btn = (LinkButton)sender;
                //Obtiene los atributos del LinkButton
                Int64 _idParentMessage = 0;
                Int64 _idMessage = Convert.ToInt64(_btn.Attributes["IdMessage"]); //Para poder editar.

                PostMessage(_idParentMessage, _idMessage, _btn.Text);
            }
        #endregion
    }
}

//MessageList.DataSource = EMSLibrary.User.CollaborationTools.Configuration.Forum(_idForum).Category(_idCategory).Topic(_idTopic).Messages().Values;
//MessageList.DataBind();
//foreach (RepeaterItem _repeaterItem in MessageList.Items)
//{
//    // if condition to add HeaderTemplate Dynamically only Once
//    if (_repeaterItem.ItemIndex == 0)
//    {
//        RepeaterItem headerItem = new RepeaterItem(_repeaterItem.ItemIndex, ListItemType.Header);
//        HtmlGenericControl hTag = new HtmlGenericControl("h4");
//        hTag.InnerHtml = "Message";
//        _repeaterItem.Controls.Add(hTag);
//    }
//    // Add ItemTemplate DataItems Dynamically
//    RepeaterItem repeaterItem = new RepeaterItem(_repeaterItem.ItemIndex, ListItemType.Item);
//    Label lbl = new Label();
//    lbl.Text = string.Format("{0} {1} <br />", myDataSet.Tables[0].Rows[_repeaterItem.ItemIndex]["FirstName"], myDataSet.Tables[0].Rows[repeatItem.ItemIndex]["LastName"]);
//    _repeaterItem.Controls.Add(lbl);
//    // Add SeparatorTemplate Dynamically
//    repeaterItem = new RepeaterItem(_repeaterItem.ItemIndex, ListItemType.Separator);
//    LiteralControl ltrlHR = new LiteralControl();
//    ltrlHR.Text = "<hr />";
//    _repeaterItem.Controls.Add(ltrlHR);
//    MessageList.Controls.Add(BuildRepeaterItem());
//}