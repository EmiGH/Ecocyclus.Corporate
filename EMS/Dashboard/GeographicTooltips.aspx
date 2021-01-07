<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/EMSPopUp.Master"
    CodeBehind="GeographicTooltips.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.GeographicTooltips" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPopUp" runat="server">
    <link href="../Skins/GeographicTooltips.css" type="text/css" rel="stylesheet" />
    <link href="../Skins/RadControls/TabStrip.Map.css" type="text/css" rel="stylesheet" />
    <div id="GeoToolTipGlobalUpdateProgress" class="Loading" style="display: none">
    </div>
    <asp:Panel ID="pnlIcon" runat="server">
    </asp:Panel>
    <asp:Label ID="lblTitle" runat="server" CssClass="lblTitleTooltips"></asp:Label>
    <asp:Label ID="lblSubTitle" runat="server" CssClass="lblSubTitleTooltips"></asp:Label>
    <rad:RadTabStrip ID="rtsToolTipInformation" runat="server" MultiPageID="rmpGeoToolTip"
        EnableEmbeddedSkins="false" Skin="Map" CausesValidation="false" SelectedIndex="0"
        Orientation="HorizontalTop" Align="Right">
        <Tabs>
            <%--Main Data--%>
            <rad:RadTab Text="Main Data">
            </rad:RadTab>
            <%--Photo--%>
            <rad:RadTab Text="Photo">
            </rad:RadTab>
            <%--Chart GEI--%>
            <rad:RadTab Text="Chart GEI">
            </rad:RadTab>
            <%--Chart Local Pollutants--%>
            <rad:RadTab Text="Chart Local Pollutants">
            </rad:RadTab>
            <%--Extended Property--%>
            <rad:RadTab Text="Extended Property">
            </rad:RadTab>
            <%--Forum--%>
            <rad:RadTab Text="Forum">
            </rad:RadTab>
            <%--Statistics--%>
            <rad:RadTab Text="Statistics">
            </rad:RadTab>
            <%--Report--%>
            <rad:RadTab Text="Report">
            </rad:RadTab>
            <%--Measurement--%>
            <rad:RadTab Text="Measurement">
            </rad:RadTab>
            <%--Sector--%>
            <rad:RadTab Text="Sector">
            </rad:RadTab>
            <%--Process Asociated--%>
            <rad:RadTab Text="Process Asociated">
            </rad:RadTab>
        </Tabs>
    </rad:RadTabStrip>
    <rad:RadMultiPage ID="rmpGeoToolTip" runat="server" SelectedIndex="0">
        <rad:RadPageView ID="rpvMainData" runat="server" Selected="true">
            <asp:Panel ID="pnlMainData" runat="server" CssClass="pnlMainData">
            </asp:Panel>
            <asp:Image ID="imgMainPhoto" runat="server" CssClass="imgMainPhoto" />

            <a id="btnFollow" runat="server" href="https://twitter.com/ghree_CABA" class="twitter-follow-button" data-show-count="false" data-lang="es"></a>
            <script src="//platform.twitter.com/widgets.js" type="text/javascript"></script>

            <a id="btnTweet" runat="server" href="https://twitter.com/share" class="twitter-share-button" data-count="none" data-lang="es">Tweet</a>
            <script type="text/javascript" src="//platform.twitter.com/widgets.js"></script>

            <div id="fb-root"></div>
            <script>(function(d, s, id) {
                    var js, fjs = d.getElementsByTagName(s)[0];
                    if (d.getElementById(id)) { return; }
                    js = d.createElement(s); js.id = id;
                    js.src = "//connect.facebook.net/es_LA/all.js#xfbml=1";
                    fjs.parentNode.insertBefore(js, fjs);
                } (document, 'script', 'facebook-jssdk'));</script>

            <div id="btnFBLike" runat="server" class="fb-like" data-href="http://www.facebook.com/pages/Nanu-Design/114850425215521" data-send="true" data-width="450" data-show-faces="true"></div>

        </rad:RadPageView>
        <rad:RadPageView ID="rpvPhotoGalleryAndChart" runat="server" Selected="false">
            <asp:UpdatePanel ID="upCounter" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_ImagePosition" runat="server" Value="0" />
                    <asp:Panel ID="pnlGraphic" runat="server" CssClass="pnlPhotoGallery">
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upChart" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <DCWC:Chart ID="chtMeasurement" runat="server" Visible="false" BackColor="#ffffff"
                        BackGradientEndColor="226, 239, 247" BorderLineColor="26, 59, 105" BorderLineStyle="Solid"
                        BorderLineWidth="0" Palette="None" Width="400px" Height="300px" ImageType="Jpeg"
                        Style="position: absolute">
                        <Legends>
                            <DCWC:Legend Enabled="true" DockInsideChartArea="true" DockToChartArea="NotSet" LegendStyle="Table"
                                BackColor="White" BorderColor="26, 59, 105" Font="Verdana, 7" Name="Default"
                                ShadowOffset="0">
                            </DCWC:Legend>
                        </Legends>
                        <UI>
                            <ContextMenu Enabled="True">
                            </ContextMenu>
                            <Toolbar BorderColor="26, 59, 105" Placement="InsideChart">
                                <BorderSkin PageColor="60, 60, 60" SkinStyle="Emboss" />
                            </Toolbar>
                        </UI>
                        <Titles>
                            <DCWC:Title Color="60, 60, 60" Font="Verdana, 7" Name="Title1">
                            </DCWC:Title>
                        </Titles>
                        <Series>
                            <DCWC:Series Name="Data" ChartArea="AreaData" MarkerStyle="Circle" MarkerSize="5">
                                <SmartLabels Enabled="True" />
                            </DCWC:Series>
                            <%--<DCWC:Series Name="Average" ChartArea="AreaData">
                            </DCWC:Series>--%>
                        </Series>
                        <ChartAreas>
                            <DCWC:ChartArea BackColor="White" BorderColor="26, 59, 105" BorderStyle="Solid" Name="AreaData"
                                BackGradientEndColor="213, 241, 255" BackGradientType="DiagonalRight">
                                <AxisY LabelsAutoFit="False">
                                    <MajorGrid LineColor="Silver" />
                                    <MinorGrid LineColor="Silver" />
                                    <LabelStyle Font="Verdana, 6.75pt" />
                                </AxisY>
                                <AxisX LabelsAutoFit="False">
                                    <MajorGrid LineColor="Silver" />
                                    <MinorGrid LineColor="Silver" />
                                    <LabelStyle Font="Verdana, 7pt" />
                                </AxisX>
                                <AxisX2>
                                    <MajorGrid LineColor="Silver" />
                                    <MinorGrid LineColor="Silver" />
                                </AxisX2>
                                <AxisY2>
                                    <MajorGrid LineColor="Silver" />
                                    <MinorGrid LineColor="Silver" />
                                </AxisY2>
                                <Area3DStyle WallWidth="0" />
                            </DCWC:ChartArea>
                        </ChartAreas>
                        <BorderSkin FrameBackColor="51, 102, 153" FrameBackGradientEndColor="CornflowerBlue"
                            PageColor="AliceBlue" />
                    </DCWC:Chart>
                </ContentTemplate>
            </asp:UpdatePanel>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvChartGEI" runat="server" Selected="false">
            <asp:UpdatePanel runat="server" ID="upChartBarActivityByScope1AndFacility" UpdateMode="Always">
                <ContentTemplate>
                    <div style="width: 490px; height: 220px; border: solid 0px #ccc; background-color: #fafafa;
                        margin-top: 5px; float: left; margin-right: 5px;">
                        <rad:RadChart Width="490px" Height="200px" ID="chartBarActivityByScope1AndFacility"
                            runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                            AutoTextWrap="false" OnItemDataBound="chartBarActivityByScope1AndFacility_ItemDataBound"
                            OnDataBound="charts_DataBound">
                            <Appearance FillStyle-FillType="Solid" FillStyle-MainColor="#ffffff">
                                <Border Color="Transparent" />
                                <FillStyle MainColor="Transparent" SecondColor="Transparent" GammaCorrection="false"
                                    FillType="Solid">
                                </FillStyle>
                            </Appearance>
                            <ChartTitle>
                                <Appearance Border-Color="Transparent" FillStyle-MainColor="Transparent">
                                    <FillStyle MainColor="Transparent">
                                    </FillStyle>
                                    <Border Color="Transparent" />
                                </Appearance>
                                <TextBlock Appearance-TextProperties-Color="#4a5678">
                                    <Appearance TextProperties-Color="74, 86, 120" TextProperties-Font="Arial, 12px">
                                    </Appearance>
                                </TextBlock>
                            </ChartTitle>
                            <Legend Visible="false">
                                <Appearance>
                                    <ItemTextAppearance TextProperties-Color="Black">
                                    </ItemTextAppearance>
                                    <ItemMarkerAppearance Figure="Square">
                                    </ItemMarkerAppearance>
                                    <Border Color="#cccccc" />
                                </Appearance>
                            </Legend>
                            <Series>
                                <rad:ChartSeries Name="Series 1">
                                    <Appearance LegendDisplayMode="ItemLabels" TextAppearance-TextProperties-Font="Verdana, 10px">
                                        <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                        </FillStyle>
                                        <LabelAppearance>
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                        </TextAppearance>
                                        <Border Color="69, 115, 167" />
                                    </Appearance>
                                </rad:ChartSeries>
                            </Series>
                            <PlotArea>
                                <XAxis DataLabelsColumn="Activity">
                                    <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                        <MajorGridLines Color="134, 134, 134" Width="0" />
                                        <LabelAppearance>
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel>
                                        <TextBlock>
                                            <Appearance TextProperties-Color="Black">
                                            </Appearance>
                                        </TextBlock>
                                    </AxisLabel>
                                    <Items>
                                        <rad:ChartAxisItem>
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="1">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="2">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="3">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="4">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="5">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="6">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="7">
                                        </rad:ChartAxisItem>
                                    </Items>
                                </XAxis>
                                <YAxis>
                                    <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" MinorTick-Color="134, 134, 134">
                                        <MajorGridLines Color="134, 134, 134" />
                                        <MinorGridLines Color="134, 134, 134" />
                                        <TextAppearance TextProperties-Color="Black">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel>
                                        <TextBlock>
                                            <Appearance TextProperties-Color="Black">
                                            </Appearance>
                                        </TextBlock>
                                    </AxisLabel>
                                </YAxis>
                                <Appearance>
                                    <FillStyle FillType="Solid" MainColor="">
                                    </FillStyle>
                                </Appearance>
                            </PlotArea>
                        </rad:RadChart>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvChartLocalPollutants" runat="server" Selected="false">
            <asp:UpdatePanel runat="server" ID="upChartBarActivityByScope1AndFacilityLC" UpdateMode="Always">
                <ContentTemplate>
                    <div style="width: 490px; height: 220px; border: solid 0px #ccc; background-color: #fafafa;
                        margin-top: 5px; float: left; margin-right: 5px;">
                        <rad:RadChart Width="490px" Height="220px" ID="chartBarActivityByScope1AndFacilityLC"
                            runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                            AutoTextWrap="false" OnItemDataBound="chartBarActivityByScope1AndFacilityLC_ItemDataBound"
                            OnDataBound="charts_DataBound">
                            <Appearance FillStyle-FillType="Solid" FillStyle-MainColor="#ffffff">
                                <Border Color="Transparent" />
                                <FillStyle MainColor="Transparent" SecondColor="Transparent" GammaCorrection="false"
                                    FillType="Solid">
                                </FillStyle>
                            </Appearance>
                            <ChartTitle>
                                <Appearance Border-Color="Transparent" FillStyle-MainColor="Transparent">
                                    <FillStyle MainColor="Transparent">
                                    </FillStyle>
                                    <Border Color="Transparent" />
                                </Appearance>
                                <TextBlock Appearance-TextProperties-Color="#4a5678">
                                    <Appearance TextProperties-Color="74, 86, 120" TextProperties-Font="Arial, 12px">
                                    </Appearance>
                                </TextBlock>
                            </ChartTitle>
                            <Legend Visible="false">
                                <Appearance>
                                    <ItemTextAppearance TextProperties-Color="Black" TextProperties-Font="Verndana, 9px">
                                    </ItemTextAppearance>
                                    <ItemMarkerAppearance Figure="Square" Dimensions-Height="3px" Dimensions-Width="3px">
                                    </ItemMarkerAppearance>
                                    <Border Color="#cccccc" />
                                </Appearance>
                            </Legend>
                            <Series>
                                <rad:ChartSeries Name="Series 1">
                                    <Appearance LegendDisplayMode="ItemLabels" TextAppearance-TextProperties-Font="Verdana, 10px">
                                        <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                        </FillStyle>
                                        <LabelAppearance>
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                        </TextAppearance>
                                        <Border Color="69, 115, 167" />
                                    </Appearance>
                                </rad:ChartSeries>
                            </Series>
                            <PlotArea>
                                <XAxis DataLabelsColumn="Activity">
                                    <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                        <MajorGridLines Color="134, 134, 134" Width="0" />
                                        <LabelAppearance>
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel>
                                        <TextBlock>
                                            <Appearance TextProperties-Color="Black">
                                            </Appearance>
                                        </TextBlock>
                                    </AxisLabel>
                                    <Items>
                                        <rad:ChartAxisItem>
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="1">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="2">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="3">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="4">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="5">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="6">
                                        </rad:ChartAxisItem>
                                        <rad:ChartAxisItem Value="7">
                                        </rad:ChartAxisItem>
                                    </Items>
                                </XAxis>
                                <YAxis>
                                    <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" MinorTick-Color="134, 134, 134">
                                        <MajorGridLines Color="134, 134, 134" />
                                        <MinorGridLines Color="134, 134, 134" />
                                        <TextAppearance TextProperties-Color="Black">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel>
                                        <TextBlock>
                                            <Appearance TextProperties-Color="Black">
                                            </Appearance>
                                        </TextBlock>
                                    </AxisLabel>
                                </YAxis>
                                <Appearance>
                                    <FillStyle FillType="Solid" MainColor="">
                                    </FillStyle>
                                </Appearance>
                            </PlotArea>
                        </rad:RadChart>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvMoreInfo" runat="server" Selected="false">
            <asp:UpdatePanel ID="upExtendedInfo" runat="server" UpdateMode="Always">
                <ContentTemplate>
                  <div class="divPatchHeaderTableScroll"></div>
                    <asp:Panel ID="pnlExtendedInfo" runat="server" CssClass="pnlExtendedInfo">
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </rad:RadPageView>
    </rad:RadMultiPage>

    <script type="text/javascript">
        //Event onload
        //window.attachEvent('onload', HideUProgress);
        if (_BrowserName == _IEXPLORER)
        {   //IE and Opera                                                                          
            window.attachEvent('onload', HideUProgress);
        }
        else
        {   //FireFox
            document.addEventListener('DOMContentLoaded', HideUProgress, false);
        }

        function HideUProgress() {
            //Oculta el reloj al terminar de cargar el iFrame.
            parent.document.getElementById("FWMasterGlobalUpdateProgress").style.display = 'none';
        }
        function ShowUProgress() {
            //Oculta el reloj al terminar de cargar el iFrame.
            parent.document.getElementById("FWMasterGlobalUpdateProgress").style.display = 'block';
        }
        function NavigateToContent(sender, e) {
            var _strRequestParam;

            //Mostrar el reloj.
            ShowUProgress();
            _strRequestParam = "PkCompost=" + sender.attributes["PkCompost"].value; //El PK, viene separado en pipe |,  para que no falle el tranfer a esta pagina.
            _strRequestParam += "&EntityName=" + sender.attributes["EntityName"].value
            _strRequestParam += "&EntityNameGrid=" + sender.attributes["EntityNameGrid"].value;
            _strRequestParam += "&EntityNameContextInfo=" + sender.attributes["EntityNameContextInfo"].value;
            _strRequestParam += "&EntityNameContextElement=" + sender.attributes["EntityNameContextElement"].value;

            if (sender.attributes["ProcessName"] != undefined) {
                _strRequestParam += "&Text=" + sender.attributes["ProcessName"].value;
            }
            else {
                if (sender.attributes["EntityName"].value != "KeyIndicator") {
                    _strRequestParam += "&Text=" + sender.innerHTML;
                }
                else {
                    _strRequestParam += "&Text=" + sender.attributes["Text"].value;
                }
            }
            _strRequestParam += "&";
            
            //Pagina de transicion entre el link del tooltip del mapa y el Viewer.
            window.open('../Dashboard/GeographicTooltipNavigate.aspx?' + _strRequestParam, '_parent');

            parent.document.getElementById("iframeTooltip").style.width = "0px";
            parent.document.getElementById("iframeTooltip").style.height = "0px";
            if (_BrowserName == _IEXPLORER) {
                parent.document.getElementById("iframeTooltip").setActive(false);
            }
            this.close();

            StopEvent(e);     //window.event.returnValue = false;
        }
        function NavigateToContentReport(sender, e, idEntity) {
            var _strRequestParam;

            //Mostrar el reloj.
            ShowUProgress();
            _strRequestParam = "IsReport=true&IdEntity=" + idEntity + "&";

            //Pagina de transicion entre el link del tooltip del mapa y el Viewer.
            window.open('../Dashboard/GeographicTooltipNavigate.aspx?' + _strRequestParam, '_parent');

            parent.document.getElementById("iframeTooltip").style.width = "0px";
            parent.document.getElementById("iframeTooltip").style.height = "0px";
            if (_BrowserName == _IEXPLORER) {
                parent.document.getElementById("iframeTooltip").setActive(false);
            }
            this.close();

            StopEvent(e);
        }
        function NavigateToContentLayers(sender, e) {
            var _strRequestParam;

            //Mostrar el reloj.
            ShowUProgress();
            var _layerView = sender.attributes["LayerView"].value

            _strRequestParam = "LayerView=" + _layerView;
            _strRequestParam += "&PkCompost=" + sender.attributes["PkCompost"].value; //El PK, viene separado en pipe |,  para que no falle el tranfer a esta pagina.
            _strRequestParam += "&EntityName=" + sender.attributes["EntityName"].value
            _strRequestParam += "&EntityNameGrid=" + sender.attributes["EntityNameGrid"].value;
            _strRequestParam += "&EntityNameContextInfo=" + sender.attributes["EntityNameContextInfo"].value;
            _strRequestParam += "&EntityNameContextElement=" + sender.attributes["EntityNameContextElement"].value;
            _strRequestParam += "&NavigateType=" + sender.attributes["NavigateType"].value;
            _strRequestParam += "&";
            
            //Pagina de transicion entre el link del tooltip del mapa y el Viewer.
            window.open('../Dashboard/GeographicTooltipNavigate.aspx?' + _strRequestParam, '_parent');

            parent.document.getElementById("iframeTooltip").style.width = "0px";
            parent.document.getElementById("iframeTooltip").style.height = "0px";
            if (_BrowserName == _IEXPLORER) {
                parent.document.getElementById("iframeTooltip").setActive(false);
            }
            this.close();

            StopEvent(e);
        }                
    </script>

</asp:Content>
