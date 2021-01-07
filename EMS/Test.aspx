<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">  
<html style="height:100%">   
    <head>  
        <title>FullWindow</title>  
    </head>  
    <body style="margin:0px;height:100%;overflow:hidden;">   
        <form id="Form1" method="post" runat="server" style="height:100%" >  
            <rad:radsplitter id="MainSplitter" runat="server" height="100%"  width="100%">   
                <rad:radpane id="LeftPane" runat="server" Width="100px" >  
                    sadfsadf
                </rad:radpane>  
                  <rad:radsplitbar id="RadSplitBar1" runat="server" />  
  
                <rad:radpane id="MainPane" runat="server" >  
                  sdafsdaf
                </rad:radpane>  
            </rad:radsplitter>  
        </form>  
    </body>  
</html>  

