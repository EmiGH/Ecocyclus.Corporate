<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="MessagesList.aspx.cs" Inherits="Condesus.EMS.WebUI.Managers.MessagesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
        <table cellspacing='0' cellpadding='0' width='100%'>
            <tr>
                <td align="left">
                </td>
                <td align="right">
                    <asp:LinkButton ID="PostReplyLinkUp" runat="server" CssClass="LinkButton" Text="Post Reply"
                        ToolTip="Post Reply" />
                    <%--<asp:LinkButton ID="NewTopic1" runat="server" />
                <asp:LinkButton ID="DeleteTopic1" runat="server" />
                <asp:LinkButton ID="LockTopic1" runat="server" />
                <asp:LinkButton ID="UnlockTopic1" runat="server" />
                <asp:LinkButton ID="MoveTopic1" runat="server" />--%>
                </td>
            </tr>
        </table>
        <table class="Forum" cellspacing="1" cellpadding="0" width="100%" border="0">
            <tr>
                <td style="padding: 0px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="Header">
                        <tr>
                            <td>
                                <asp:Label ID="TopicTitle" runat="server" />
                            </td>
                            <%--<td align="right">
                        <asp:HyperLink ID="MyTest" runat="server"></asp:HyperLink>
                        <asp:PlaceHolder runat="server" ID="ViewOptions">&middot;
                            <asp:HyperLink ID="View" runat="server"></asp:HyperLink>
                        </asp:PlaceHolder>
                    </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr class="header2">
        <td colspan="2" align="right" class="header2links">
            <asp:LinkButton ID="PrevTopic" runat="server" ></asp:LinkButton>
            &middot;
            <asp:LinkButton ID="NextTopic" runat="server" ></asp:LinkButton>
            <div id="Div1" runat="server" visible="false">
                <asp:LinkButton ID="TrackTopic" runat="server" ></asp:LinkButton>
                &middot;
                <asp:LinkButton ID="EmailTopic" runat="server" ></asp:LinkButton>
                &middot;
                <asp:LinkButton ID="PrintTopic" runat="server" ></asp:LinkButton>
                &middot;
                <asp:HyperLink ID="RssTopic" runat="server"></asp:HyperLink>
            </div>
        </td>
    </tr>--%>
            <tr>
                <td>
                    <asp:Repeater ID="MessageList" runat="server">
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <table cellspacing='0' cellpadding='0' width='100%'>
            <tr>
                <td align="left">
                </td>
                <td align="right">
                    <asp:LinkButton ID="PostReplyLinkBottom" runat="server" CssClass="LinkButton" Text="Post Reply"
                        ToolTip="Post Reply" />
                </td>
            </tr>
        </table>

    <script type="text/javascript">
        function NavigateBackToTop(btnBack, e) {
            document.getElementById('divContent').scrollTop = 0;
            StopEvent(e);     //window.event.returnValue = false;
        }
    </script>

</asp:Content>
<%--<asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr class="postheader">
	            <td width="140px" id="NameCell" runat="server">
		            <a name=""/><!-- MessageID -->
		            <b><asp:hyperlink id="UserName" runat="server"></asp:hyperlink></b>
	            </td>
	            <td width="80%">
		        <table cellspacing="0" cellpadding="0" width="100%">
		            <tr>
			            <td class="postheader">
				            <asp:Label ID="lblPosted" runat="server" Text="Posted:" />
				            <asp:Label ID="lblPostedValue" runat="server" /> <!-- fecha de posteo -->
			            </td>
			            <td class="postheader" align="right">
				            <asp:hyperlink runat="server" id="Attach"/>
				            <asp:hyperlink runat="server" id="Edit"/>
				            <asp:linkbutton runat="server" id="Delete"/>
				            <asp:hyperlink runat="server" id="Quote"/>
			            </td>
		            </tr>
		        </table>
	            </td>
            </tr>
            <tr class="">
	            <td valign="top" height="100" class="UserBox" >
	                <!-- Avatar -->
                    <asp:Image ID="Avatar" runat="server" />
                    <br />
                    <!-- Name -->
                    <asp:Label ID="lblName" runat="server" Text="Posted:" />
				    <asp:Label ID="lblNameValue" runat="server" /> 
				    <br />
				    <!-- fecha de joined -->
				    <asp:Label ID="lblJoined" runat="server" Text="Joined:" />
				    <asp:Label ID="lblJoinedValue" runat="server" /> 
				    <br />
				    <!-- contador de posteos -->
				    <asp:Label ID="lblPosts" runat="server" Text="Posts:" />
				    <asp:Label ID="lblPostsValue" runat="server" /> 
				    <br />
	            </td>
	            <td valign="top" class="message">	   
	            <div class="postdiv">
	                <!-- Message -->
				    <asp:Label ID="lblMessageValue" runat="server" /> 
		        </div>
	        </td>
            </tr>
            <tr class="postfooter">
	            <td class="small" >
		            
	            </td>
	            <td class="postfooter">
		            <table border="0" cellpadding="0" cellspacing="0" width="100%">
		            <tr>
			            <td>
			            <asp:hyperlink runat='server' id='Pm'/>
			            <asp:hyperlink runat='server' id='Email'/>
			            <asp:hyperlink runat='server' id='Home'/>
			            <asp:hyperlink runat='server' id='Blog'/>
			            <asp:hyperlink runat='server' id='Msn'/>
			            <asp:hyperlink runat='server' id='Yim'/>
			            <asp:hyperlink runat='server' id='Aim'/>
			            <asp:hyperlink runat='server' id='Icq'/>
			            </td>
			            <td align="right" id="AdminInfo" runat="server">&nbsp;</td>
		            </tr>
		            </table>
	        </td>
        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        </AlternatingItemTemplate>
    </asp:Repeater>
    --%>