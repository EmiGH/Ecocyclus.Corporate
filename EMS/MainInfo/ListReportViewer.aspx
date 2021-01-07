<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ListReportViewer.aspx.cs" Inherits="Condesus.EMS.WebUI.MainInfo.ListReportViewer" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="radCustom" Namespace="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script type="text/javascript" src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>

    <script src="../AppCode/JavaScriptCore/markerclusterer_packed.js" type="text/javascript"></script>

    <script type="text/javascript">
    
        if (_BrowserName == _IEXPLORER)
        {   //IE and Opera
            window.attachEvent('onload', Initimg);
        }
        else
        {   //FireFox
            document.addEventListener('DOMContentLoaded', Initimg, false);
        }
        function Initimg() {
            //La altura del Content (ya calculada)
            var _tdimgHeight = document.getElementById('tdPicture').clientHeight;  //document.getElementById('tdPicture').style.height.replace('px', '');
            var _img = document.getElementById('ctl00_ContentMain_imgShowSlide');

            //Si no hay imagen...no hace nada
            if (_img != null) {
                var _imgHeigth = _img.clientHeight; //Le resto la altura para que quede centrado?...
                _img.style.marginTop = (parseInt(_tdimgHeight - _imgHeigth, 10) / 2) + 'px';
            }
        }
         
    
        //Global Const/Variable
        //CONSTANT
        var _EARTHRADIUS = 6371;
        var _POLYGON_SHAPE_TYPE = "Polygon";
        var _POLYLINE_SHAPE_TYPE = "Polyline";
        var _PUSHPIN_SHAPE_TYPE = "Pushpin";
        var _PROCESS_OBJECT_NAME = "processgroupprocess";
        var _MEASUREMENT_OBJECT_NAME = "measurement";
        var _FACILITY_OBJECT_NAME = "facility";
        var _DEVICE_OBJECT_NAME = "measurementdevice";
        var _CALCULATE_OBJECT_NAME = "calculate";
        var _MAP_TYPE_GOOGLE = "googlemap";
        var _MAP_TYPE_VIRTUAL_EARTH = "virtualearth";

        //VARIABLES
        var _Points = null;
        var _Map = null;
        var _Shape = null;
        var _Layers = null;
        var _MapType = null;
        //Array de GMarker para armar el cluster
        var _Markers = new Array(0);
        var _MarkerCluster = null;
        //our bounding rectangle components, These parmeters can be calculated if we have an array of coordinates
        var _MaxLatitude = 0;
        var _MinLatitude = 0;
        var _MaxLongitude = 0;
        var _MinLongitude = 0;
        var _CenterLatitudeLongitude = 0;
        var _ZoomLevel = 10;
        //Variables para el click derecho sobre el pushpin..
        var _Hide;
        var _CurrentShape = null;
        var _IsPanning = false;
    </script>

    <script type="text/javascript">
        //INICIALIZACION
        //Event onload
//        window.attachEvent('onload', GetMap);
//        window.attachEvent('onunload', GUnload);
        if (_BrowserName == _IEXPLORER) {   //IE and Opera
            window.attachEvent('onload', GetMap);
            window.attachEvent('onunload', GUnload);
        }
        else {   //FireFox
            document.addEventListener('DOMContentLoaded', GetMap, false);
            document.addEventListener('onunload', GUnload, false);
        }
        

        //Declarative Function...
        function InitializeGlobalVars() {
            _Points = null;
            _MaxLatitude = 0;
            _MinLatitude = 0;
            _MaxLongitude = 0;
            _MinLongitude = 0;
            _CenterLatitudeLongitude = 0;
            _ZoomLevel = 1;
        }
        function GetMap() {
            _MapType = document.getElementById('MapType').value;

            //Funcion que verifica que Mapa debe cargar, Google o VE.
            switch (_MapType.toLowerCase()) {
                case _MAP_TYPE_GOOGLE:
                    GetGoogleMap();
                    break;

                case _MAP_TYPE_VIRTUAL_EARTH:
                    GetVirtualEarth();
                    break;
            }
        }
        function GetVirtualEarth() {
            //Inicializa las variables globales para la recarga por filtros...
            InitializeGlobalVars();
            //Muestra el reloj.
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
            //Carga los puntos pasados desde el CodeBehind.
            _Points = document.getElementById('inputPoints').value;

            //Contruye y carga el mapa.
            _Map = new VEMap('myMap');
            _Map.SetDashboardSize(VEDashboardSize.Small);
            _Map.LoadMap();
            _Map.SetScaleBarDistanceUnit(VEDistanceUnit.Kilometers);

            //Contruye el layer
            _Layers = new VEShapeLayer();
            //Carga los puntos en el mapa y guarda el min-max de lat-long.
            SetPoint(_Points);
            //Obtiene el centro del mapa y el zoom.
            CalculateCenterAndZoom();
            //Ahora setea el centro y el zoom
            _Map.SetCenterAndZoom(_CenterLatitudeLongitude, _ZoomLevel);
            //Agrega el layer al mapa.
            _Map.AddShapeLayer(_Layers);
            //Configura el layer como Cluster.
            _Layers.SetClusteringConfiguration(VEClusteringType.Grid);
            //Oculta el reloj.
            $get('FWMasterGlobalUpdateProgress').style.display = 'none';
        }
        function GetGoogleMap() {
            var _zoom = 20;
            var _size = -1;
            var _style = 0;

            if (GBrowserIsCompatible()) {
                _Map = new GMap2(document.getElementById("myMap"));

                _Map.setMapType(G_HYBRID_MAP);
                _Map.addControl(new GMapTypeControl());
                _Map.addControl(new GLargeMapControl());    //GSmallMapControl());
                //Carga los puntos pasados desde el CodeBehind.
                _Points = document.getElementById('inputPoints').value;
                _Map.clearOverlays();
                _Markers = new Array(0);
                if (_MarkerCluster != null) {
                    _MarkerCluster.clearMarkers();
                }

                //Obtiene y setea todos los puntos
                SetPoint(_Points);
                //Calcula el centro del mapa y zoom.
                CalculateCenterAndZoom();
                //Ahora setea el centro y el zoom
                //_Map.setCenter( new GLatLng(_CenterLatitudeLongitude.lat(), _CenterLatitudeLongitude.lng()), _ZoomLevel);
                _Map.setCenter(GetLatLongPoint(_CenterLatitudeLongitude.lat(), _CenterLatitudeLongitude.lng()), _ZoomLevel);

                _zoom = _zoom == -1 ? null : _zoom;
                _size = _size == -1 ? null : _size;
                _style = _style == "-1" ? null : parseInt(_style, 10);

                //Finalmente agrega todos los puntos que corresponden como cluster y el resto en forma normal.
                //var markerCluster = new MarkerClusterer(_Map, _Markers, { maxZoom: _zoom, gridSize: _size, styles: styles[_style] });
                var markerCluster = new MarkerClusterer(_Map, _Markers);
                //Oculta el reloj.
                $get('FWMasterGlobalUpdateProgress').style.display = 'none';
            }
        }
    </script>

    <script type="text/javascript">
        //FUNCIONES GENERICAS
        function GetLatLongPoint(latitude, longitude) {

            switch (_MapType.toLowerCase()) {
                case _MAP_TYPE_GOOGLE:
                    return new GLatLng(latitude, longitude);
                    break;

                case _MAP_TYPE_VIRTUAL_EARTH:
                    return new VELatLong(latitude, longitude);
                    break;
            }
        }
        function CalculateCenterAndZoom() {
            //default zoom scales in km/pixel from http://msdn2.microsoft.com/en-us/library/aa940990.aspx
            var defaultScales = new Array(78.27152, 39.13576, 19.56788, 9.78394, 4.89197, 2.44598, 1.22299, 0.61150, 0.30575, 0.15287, 0.07644, 0.03822, 0.01911, 0.00955, 0.00478, 0.00239, 0.00119, 0.0006, 0.0003);

            //calculate center coordinates
            var centerLat = (_MaxLatitude + _MinLatitude) / 2;
            var centerLong = (_MaxLongitude + _MinLongitude) / 2;
            var centerPoint = GetLatLongPoint(centerLat, centerLong);   //new VELatLong(centerLat, centerLong);

            //want to calculate the distance in km along the centers latitude between the two longitudes
            var meanDistanceX = GetDistance(centerLat, _MinLongitude, centerLat, _MaxLongitude);

            //want to calculate the distance in km along the centers longitude between the two latitudes
            var meanDistanceY = GetDistance(_MaxLatitude, centerLong, _MinLatitude, centerLong) * 2;

            //dimensions of the _Map - need to remove px or percentage and convert to int
            var mapWidth = parseFloat(document.getElementById('myMap').style.width);
            var mapHeight = parseFloat(document.getElementById('myMap').style.height);

            //calculates the x and y scales
            var meanScaleValueX = meanDistanceX / mapWidth;
            var meanScaleValueY = meanDistanceY / mapHeight;

            var meanScale;

            //gets the largest scale value to work with
            if (meanScaleValueX > meanScaleValueY)
                meanScale = meanScaleValueX;
            else
                meanScale = meanScaleValueY;

            //intialize zoom level variable
            var zoom = 1;
            _CenterLatitudeLongitude = centerPoint;

            //calculate zoom level
            //for (var i = 1; i < 19; i++) {
            //El array es zero base...
            for (var i = 0; i < 19; i++) {
                if (meanScale >= defaultScales[i]) {
                    zoom = i;
                    _ZoomLevel = zoom;
                    break;
                }
            }
        }
        //this function calculates the distance between two coordinates
        function GetDistance(lat1, lon1, lat2, lon2) {
            var factor = Math.PI / 180;
            var dLat = (lat2 - lat1) * factor;
            var dLon = (lon2 - lon1) * factor;
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(lat1 * factor) * Math.cos(lat2 * factor) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = _EARTHRADIUS * c;

            return d;
        }
        function SetCenterOfPolygon(pointsOfPolygon) {
            var _minLat = 0;
            var _maxLat = 0;
            var _minLong = 0;
            var _maxLong = 0;

            //Recorre todos los puntos del poligono y busca el mayor-menor para lat-long.
            for (var x = 0; x < pointsOfPolygon.length - 1; x = x + 1) {
                if (_MapType.toLowerCase() == _MAP_TYPE_VIRTUAL_EARTH) {
                    //Obtiene el mayor y menor latitude
                    _maxLat = GetMax(pointsOfPolygon[x].Latitude, _maxLat);
                    _minLat = GetMin(pointsOfPolygon[x].Latitude, _minLat);
                    //Obtiene el mayor y menor longitude
                    _maxLong = GetMax(pointsOfPolygon[x].Longitude, _maxLong);
                    _minLong = GetMin(pointsOfPolygon[x].Longitude, _minLong);
                }
                else {
                    //Obtiene el mayor y menor latitude
                    _maxLat = GetMax(pointsOfPolygon[x].lat(), _maxLat);
                    _minLat = GetMin(pointsOfPolygon[x].lat(), _minLat);
                    //Obtiene el mayor y menor longitude
                    _maxLong = GetMax(pointsOfPolygon[x].lng(), _maxLong);
                    _minLong = GetMin(pointsOfPolygon[x].lng(), _minLong);
                }
            }
            //calculate center coordinates
            var centerLat = (_maxLat + _minLat) / 2;
            var centerLong = (_maxLong + _minLong) / 2;

            return GetLatLongPoint(centerLat, centerLong);  //new VELatLong(centerLat, centerLong);
        }
        function GetMin(value, currentMin) {
            //Si el current es 0, quiere decir que el valor ya es el mayor.
            if (currentMin == 0) {
                return value;
            }
            //Ahora si el valor es menor al current, entonces es el menor...
            if (value < currentMin) {
                return value;
            }
            //Sino el current sigue siendo el menor.
            return currentMin;
        }
        function GetMax(value, currentMax) {
            //Si el current es 0, quiere decir que el valor ya es el mayor.
            if (currentMax == 0) {
                return value;
            }
            //Ahora si el valor es mayor al current, entonces es el mayor...
            if (value > currentMax) {
                return value;
            }
            //Sino el current sigue siendo el mayor.
            return currentMax;
        }

        function ShowLoading() {
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
        }
        //////////////////////////////////////////////////
        function DisableEroHide() {
            _Hide = window.ero.hide;
            window.ero.hide = function(a) {
                return;
            }
        }
        //////////////////////////////////////////////////
        function EnableEroHide() {
            window.ero.hide = _Hide;
        }
        //////////////////////////////////////////////////
        function CustomShowInfoBox(shape) {
            if (!shape)
                return;

            DisableEroHide();
            _Map.ShowInfoBox(shape);
            _CurrentShape = shape;
        }
        //////////////////////////////////////////////////
        function CustomHideInfoBox(shape) {
            if (!shape)
                return;

            // Before actually calling the hide method, we need to restore the ero hide function,
            EnableEroHide();
            _Map.HideInfoBox(shape);
            _CurrentShape = null;
        }
    </script>

    <script type="text/javascript">
        //Setea los puntos
        function SetPoint(points) {
            var _arrayVELatLong = new Array();
            var _lat;
            var _lon;
            var _shapeType;
            var _shapeIdEntity;
            var _shapeObject;
            var _record = points.toString().split(";");

            _shapeObject = _record[0].split("|")[0]; //El primer campo siempre viene el Objeto (ClassName)
            _shapeIdEntity = _record[0].split("|")[1]; //El segundo campo siempre viene el Id del Objeto (IdEntity)
            _shapeType = _record[0].split("|")[2];  //El tercer campo siempre viene el tipo (Poligono, punto, linea)
            for (var i = 0; i < _record.length - 1; i = i + 1) {
                _shapeObject = _record[i].split("|")[0]; //El primer campo siempre viene el Objeto (ClassName)
                _shapeIdEntity = _record[i].split("|")[1]; //El segundo campo siempre viene el Id del Objeto (IdEntity)
                _shapeType = _record[i].split("|")[2]; //El tercer campo siempre viene el tipo (Poligono, punto, linea)
                //Si no esta marcado como check de visibilidad, entonces no carga los puntos...
                //3° campo latitud
                //4° campo Longitud
                //Pero se pueden repetir n veces.
                var _latlonPoints = _record[i].split("|");
                //Inicializa el array, para cada registro.
                _arrayVELatLong = new Array(0);
                //Empieza del 1 porque se saltea el type y el objeto, que ya lo tengo.
                for (var x = 3; x < _latlonPoints.length - 1; x = x + 2) {
                    _lat = parseFloat(_latlonPoints[x]); //saca el campo de latitud.
                    _lon = parseFloat(_latlonPoints[x + 1]); //saca el campo de longitud.
                    //Se guarda los minimos y maximos de Lat y Long.
                    //Obtiene el mayor y menor latitude
                    _MaxLatitude = GetMax(_lat, _MaxLatitude);
                    _MinLatitude = GetMin(_lat, _MinLatitude);
                    //Obtiene el mayor y menor longitude
                    _MaxLongitude = GetMax(_lon, _MaxLongitude);
                    _MinLongitude = GetMin(_lon, _MinLongitude);

                    //Agrega el punto en el array
                    _arrayVELatLong.push(GetLatLongPoint(_lat, _lon));
                    //_arrayVELatLong.push(new VELatLong(_lat, _lon));
                }
                //Ahora arma el Shape con el type y el array de puntos.
                AddPoint(_shapeType, _shapeIdEntity, _shapeObject, _arrayVELatLong);
            }
        }
        function SetAttributeAndAddShape(shapeType, idEntity, entityObject, pointLatLong) {

            //            var _toolTip = "<iframe id='iframeTooltip' src='../Dashboard/GeographicTooltips.aspx?IdEntity=" + idEntity.toString().replace("&", "|")
            //                        + "&EntityObject=" + entityObject
            //                        + "&' scrolling='no' frameborder='0' class='iframeTooltip' allowtransparency='true'>"
            //                        + "</iframe>";

            if (_MapType.toLowerCase() == _MAP_TYPE_VIRTUAL_EARTH) {
                //Set the info box
                _Map.ClearInfoBoxStyles();
                //Set the line Color
                var lineColor = new VEColor(71, 171, 0, 1);
                _Shape.SetLineColor(lineColor);
                //Si es un Pushpin, no setea el line
                if (shapeType != _PUSHPIN_SHAPE_TYPE) {
                    //Set the line width
                    _Shape.SetLineWidth(3);
                }
                //Set the fill color
                var fillColor = new VEColor(142, 191, 0, 0.3);
                _Shape.SetFillColor(fillColor);

                //                _Shape.SetDescription(_toolTip);

                //Si es un Pushpin, no setea el line
                if ((shapeType == _POLYLINE_SHAPE_TYPE) || (shapeType == _POLYGON_SHAPE_TYPE)) {
                    _Shape.HideIcon();
                }
                // Add the pushpins to the _Map
                _Layers.AddShape(_Shape);
                //_Map.SetInfoBoxStyles();
            }
            else {
                if (shapeType != "Pushpin") {
                    //Si es puspin No se usa, ya que lo carga como cluster....con esto MarkerClusterer()
                    _Map.addOverlay(_Shape);
                }
                else {
                    var marker;
                    //var marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()));
                    if (pointLatLong.length == 1) {
                        marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()));
                    }
                    else {
                        marker = new GMarker(SetCenterOfPolygon(pointLatLong));
                    }
                    //Se guarda en el array de puntos para armar el cluster...
                    _Markers.push(marker);

                    //                    //Si es click derecho, hago algo...sino oculto
                    //                    GEvent.addListener(marker, "click", function() {
                    //                        ShowLoading();
                    //                        marker.openInfoWindowHtml(_toolTip);
                    //                    });
                    //return marker;
                }
            }
        }
        function GetShape(shapeType, pointLatLong) {
            //Verifica que tipo de mapa se esta usando y construye el SHAPE.
            switch (_MapType.toLowerCase()) {
                case _MAP_TYPE_GOOGLE:
                    //En base al type construye una polilinea, punto o poligono
                    switch (shapeType) {
                        case "Pushpin":
                            //_Shape = createInfoMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()), infobox);
                            break;
                        case "Polygon":
                            _Shape = new GPolygon(pointLatLong, "#669933", 5, 0.7, "#996633", 0.4);
                            break;
                        case "Polyline":
                            _Shape = new GPolyline(pointLatLong, "#669933", 5, 0.7, "#996633", 0.4);
                            break;
                    }
                    break;

                case _MAP_TYPE_VIRTUAL_EARTH:
                    //En base al type construye una polilinea, punto o poligono
                    switch (shapeType) {
                        case _PUSHPIN_SHAPE_TYPE:
                            _Shape = new VEShape(VEShapeType.Pushpin, pointLatLong);
                            break;
                        case _POLYGON_SHAPE_TYPE:
                            _Shape = new VEShape(VEShapeType.Polygon, pointLatLong);
                            break;
                        case _POLYLINE_SHAPE_TYPE:
                            _Shape = new VEShape(VEShapeType.Polyline, pointLatLong);
                            break;
                    }
                    break;
            }
        }
        function AddPoint(shapeType, idEntity, entityObject, pointLatLong) {
            //Construye el _Shape
            GetShape(shapeType, pointLatLong);
            //Setea los atributos para el shape y lo agrega al layer.
            SetAttributeAndAddShape(shapeType, idEntity, entityObject, pointLatLong);

            //Analiza al Poligono.
            //Si es un Pushpin, no setea el line
            if (shapeType == _POLYGON_SHAPE_TYPE) {
                //Construye el punto en el centro del poligono.
                //_Shape = new VEShape(VEShapeType.Pushpin, SetCenterOfPolygon(pointLatLong));
                GetShape(_PUSHPIN_SHAPE_TYPE, SetCenterOfPolygon(pointLatLong));
                //SetAttributeAndAddShape(_PUSHPIN_SHAPE_TYPE, idEntity, entityObject, SetCenterOfPolygon(pointLatLong));
                SetAttributeAndAddShape(_PUSHPIN_SHAPE_TYPE, idEntity, entityObject, pointLatLong);
            }
        }
    </script>

    <style type="text/css">
        .Loading
        {
            background-repeat: no-repeat;
            background-position: center center;
            -ms-background-position-x: center;
            -ms-background-position-y: center;
            background-color: #FFFFFF;
            height: 100%;
            width: 100%;
            filter: alpha(opacity=70);
            -ms-filter: alpha(opacity=70);
            -moz-opacity: .70;
            opacity: .70;
            position: fixed;
            visibility: visible;
            z-index: 999999;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            margin: 0px;
            overflow: visible;
        }
    </style>
    <input id="inputPoints" type="hidden" value="<% =_PointsLatLong%>" />
    <input id="MapType" type="hidden" value="<% =_MapType%>" />
    <asp:UpdatePanel ID="uPanelGraphicModes" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <div style="padding: 10px 10px 0 10px;">
            <table border="0" cellpadding="0" cellspacing="0" class="contentListViewReportTable">
                <tr>
                    <td class="Toolbar">
                        <rad:RadTabStrip ID="rtsGraphicModes" runat="server" MultiPageID="rmpGraphicModes"
                            EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false" Width="20px"
                            CausesValidation="false" SelectedIndex="0" Orientation="VerticalLeft">
                            <Tabs>
                                <rad:RadTab CssClass="contentListViewReportImages" SelectedCssClass="contentListViewReportImagesOpen">
                                </rad:RadTab>
                                <rad:RadTab CssClass="contentListViewReportMap" SelectedCssClass="contentListViewReportMapOpen">
                                </rad:RadTab>
                                <rad:RadTab CssClass="contentListViewReportChart" SelectedCssClass="contentListViewReportChartOpen">
                                </rad:RadTab>
                            </Tabs>
                        </rad:RadTabStrip>
                    </td>
                    <td class="Picture" id="tdPicture">
                        <rad:RadMultiPage ID="rmpGraphicModes" runat="server" SelectedIndex="0">
                            <rad:RadPageView ID="rpvPictures" runat="server" Selected="true">
                                <asp:UpdatePanel ID="upCounter" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <asp:Image ID="imgShowSlide" runat="server" CssClass="imgShowSlide" />
                                        <asp:HiddenField ID="hdn_ImagePosition" runat="server" Value="0" />
                                        <br />
                                        <asp:ImageButton ID="btnPrevPicture" CommandArgument="-1" runat="server" CssClass="Back" />
                                        <asp:ImageButton ID="btnNextPicture" CommandArgument="1" runat="server" CssClass="Next" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </rad:RadPageView>
                            <rad:RadPageView ID="rpvMap" runat="server" Selected="false">
                                <div id='myMap' style="width: 480px; height: 380px;">
                                </div>
                            </rad:RadPageView>
                            <rad:RadPageView ID="rpvChart" runat="server" Selected="false">
                                <DCWC:Chart ID="chtMeasurement" runat="server" Visible="false" BackColor="#ffffff"
                                    BackGradientEndColor="226, 239, 247" BorderLineColor="26, 59, 105" BorderLineStyle="Solid"
                                    BorderLineWidth="0" Palette="None" Width="480px" Height="380px" ImageType="Jpeg">
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
                            </rad:RadPageView>
                        </rad:RadMultiPage>
                    </td>
                    <td class="Space2">
                        &nbsp;
                    </td>
                    <td class="MainData">
                        <%--Va la grilla de viewer para el MainData--%>
                        <asp:Label ID="lblTitleMainData" runat="server" Text="Main Data" CssClass="lblTitleMainData" />
                        <asp:Panel ID="pnlListViewerMainData" runat="server">
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--Va la Tabla que contrendra a las grillas de viewer para el Related Data--%>
            <asp:UpdatePanel ID="upListViewerRelatedData" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="divContentTabStrip">
                        <asp:Label ID="lblSubTitle" runat="server" Text="Related Data" CssClass="lblSubTitle" />
                        <asp:Panel ID="pnlTabStrip" runat="server" CssClass="pnlTabStrip">
                        </asp:Panel>
                    </div>
                    <asp:Panel ID="pnlListViewerRelatedData" runat="server">
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </ContentTemplate>
        <Triggers>
            <%--El div que contiene al Mapa, no debe estar dentro del  Upanel...--%>
            <asp:PostBackTrigger ControlID="rtsGraphicModes" />
        </Triggers>
    </asp:UpdatePanel>
    <!--Abro RadMenu Context-->
    <asp:UpdatePanel ID="upMenuSelection" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <rad:RadContextMenu ID="rmnSelection" Skin="EMS" EnableEmbeddedSkins="false" runat="server"
                CollapseDelay="0" ExpandDelay="0" Style="left: 0px; top: 0px">
            </rad:RadContextMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--Abro RadMenu Context-->
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <asp:Button ID="btnHidden" runat="server" Visible="False" CausesValidation="False" />
    <ajaxToolkit:ConfirmButtonExtender ID="cbelbDelete" runat="server" TargetControlID="btnHidden"
        OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDelete" Enabled="False"
        ConfirmText="" />
    <ajaxToolkit:ModalPopupExtender ID="mpelbDelete" runat="server" TargetControlID="btnHidden"
        BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmDelete"
        CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" DynamicServicePath=""
        Enabled="True" />
    <div class="contentpopup">
        <asp:Panel ID="pnlConfirmDelete" runat="server" Style="display: none" meta:resourcekey="pnlConfirmDeleteResource1">
            <span>
                <asp:Literal ID="liMsgConfirmDelete" runat="server" Text="<%$ Resources:Common, msgConfirmDelete %>" /></span>
            <asp:Button ID="btnOkDelete" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnOk %>"
                CausesValidation="False" BorderStyle="None" meta:resourcekey="btnOkResource1" />
            <asp:Button ID="btnCancel" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnCancel %>"
                BorderStyle="None" meta:resourcekey="btnCancelResource1" />
        </asp:Panel>
    </div>
    <asp:UpdateProgress runat="server" ID="uProgressGraphic" AssociatedUpdatePanelID="uPanelGraphicModes">
        <ProgressTemplate>
            <div class="Loading">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script type="text/javascript">

        function GetPopupInfo(node, img) {
            //debugger;
            var text = "<div style='vertical-align: middle; height:25px;'><span style='color:#307DB3;font-family: Verdana; font-size: 11px;'>" + node._attributes.getAttribute('TitleNode') + "</span></div>";
            text += "<table border='0' cellpadding='0' cellspacing='0'>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Start Date</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('StartDate') + "</span></td></tr>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>End Date</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('EndDate') + "</span></td></tr>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Duration</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Duration') + "</span></td></tr>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Status</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('State') + "</span></td></tr>";

            if (node._attributes.getAttribute('ProcessType') == "ProcessTaskMeasurement" || node._attributes.getAttribute('ProcessType') == "ProcessTaskMeasurementDataRecovery"
            || node._attributes.getAttribute('ProcessType') == "ProcessTaskCalibration" || node._attributes.getAttribute('ProcessType') == "ProcessTaskOperation") {
                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Execution Status</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('ExecutionStatus') + "</span></td></tr>";
            }
//            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Completed</span></td>";
//            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Completed') + " %</span></td></tr>";

//            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Result</span></td>";
//            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Result') + "</span></td></tr>";

            //Los Custom Attributes "especiales"
            if (node._attributes.getAttribute('ProcessType') == "ProcessTaskMeasurement" || node._attributes.getAttribute('ProcessType') == "ProcessTaskMeasurementDataRecovery") {
                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Frequency</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Frequency') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Indicator</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Indicator') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Measurement Unit</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('MeasurementUnit') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Measurement</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Measurement') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Out Of Range</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('OutOfRange') + "</span></td></tr>";
            }
            if (node._attributes.getAttribute('ProcessType') == "ProcessTaskCalibration") {
                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Device</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('MeasurementDevice') + "</span></td></tr>";
            }

            text += "</table>";

            return text;
        }
    
    </script>

</asp:Content>
