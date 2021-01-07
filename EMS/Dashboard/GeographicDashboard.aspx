<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="GeographicDashboard.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.GeographicDashboard" %>

<%@ Register Assembly="Microsoft.Live.ServerControls.VE" Namespace="Microsoft.Live.ServerControls.VE"
    TagPrefix="ve" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script src="../AppCode/JavaScriptCore/jquery-1.5.2.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>

    <script src="../AppCode/JavaScriptCore/markerclusterer_packed.js" type="text/javascript"></script>

    <script type="text/javascript">
        var styles = [[{
            url: '../image/people35.png',
            height: 35,
            width: 35,
            opt_anchor: [16, 0],
            opt_textColor: '#FF00FF',
            opt_textSize: 10
        },
          {
              url: '../images/people45.png',
              height: 45,
              width: 45,
              opt_anchor: [24, 0],
              opt_textColor: '#FF0000',
              opt_textSize: 10
          },
          {
              url: '../images/people55.png',
              height: 55,
              width: 55,
              opt_anchor: [32, 0]
}],
          [{
              url: '../images/conv30.png',
              height: 27,
              width: 30,
              anchor: [3, 0],
              textColor: '#FF00FF',
              opt_textSize: 10
          },
          {
              url: '../images/conv40.png',
              height: 36,
              width: 40,
              opt_anchor: [6, 0],
              opt_textColor: '#FF0000',
              opt_textSize: 10
          },
          {
              url: '../images/conv50.png',
              width: 50,
              height: 45,
              opt_anchor: [8, 0]
}],
          [{
              url: '../images/heart30.png',
              height: 26,
              width: 30,
              opt_anchor: [4, 0],
              opt_textColor: '#FF00FF',
              opt_textSize: 10
          },
          {
              url: '../images/heart40.png',
              height: 35,
              width: 40,
              opt_anchor: [8, 0],
              opt_textColor: '#FF0000',
              opt_textSize: 10
          },
          {
              url: '../images/heart50.png',
              width: 50,
              height: 44,
              opt_anchor: [12, 0]
}]];
    </script>
       
    <script type="text/javascript">

        //INICIALIZACION
        //Event onload
        if (_BrowserName == _IEXPLORER) {   //IE and Opera
            window.attachEvent('onload', InitMap);
            window.attachEvent('onunload', GUnload);
        }
        else {   //FireFox
            document.addEventListener('DOMContentLoaded', InitMap, false);
            document.addEventListener('onunload', GUnload, false);
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
        //Variables para la visualizacion (filtros de la pagina)
        var _ChkProcess;
        var _ChkMeasurement;
        var _ChkFacility;
        var _ChkDevice;
        //Variables para el click derecho sobre el pushpin..
        var _Hide;
        var _CurrentShape = null;
        var _IsPanning = false;
        var geocoder = null;
        var _Icon = null;

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
        function InitMap() {
            SetFwBounds();
            //La altura del Content (ya calculada)
            var _contentHeight = document.getElementById('divContent').style.height.replace('px', '');
            var _contentWidth = document.getElementById('divContent').style.width.replace('px', '');


            var _myMap = document.getElementById('myMap');
            var _myFilterMap = document.getElementById('FilterMap');


            //Ver esto, ya que en IE7 no estaba funcionando, entonces agrege esto fijo.
            //           if (_contentHeight == '') {
            //                _contentHeight = 458;
            //                _contentWidth = 1200;
            //            }

            _myMap.style.height = (_contentHeight - 2) + 'px';
            _myMap.style.width = (_contentWidth - 2) + 'px';
            _myFilterMap.style.left = (_contentWidth - 364) + 'px';
            _myFilterMap.style.bottom = (_contentHeight - 24) + 'px';


            GetMap();
        }
        function InitializeHandlers() {
            //Agrega Eventos...
            _Map.AttachEvent("onmousedown", OnMouseDown);
            _Map.AttachEvent("onmousemove", OnMouseMove);
            _Map.AttachEvent("onmouseup", OnMouseUp);
            _Map.AttachEvent("onmouseover", OnMouseOver);
            _Map.AttachEvent("onclick", OnClick);
            _Map.AttachEvent("onstartpan", OnStartPan);
            _Map.AttachEvent("onstartzoom", OnStartZoom);
        }
        function GetFilterEntity() {
            //Carga los valores de visualizacion de filtros.
            //Ahora solo queda el process
            _ChkProcess = true;
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

            //Carga los valores de visualizacion de filtros.
            GetFilterEntity();

            //Contruye y carga el mapa.
            _Map = new VEMap('myMap');
            _Map.SetDashboardSize(VEDashboardSize.Small);
            _Map.LoadMap();
            _Map.SetScaleBarDistanceUnit(VEDistanceUnit.Kilometers);

            //Agrega los eventos sobre el mapa.
            InitializeHandlers();

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
                //Carga los valores de visualizacion de filtros.
                GetFilterEntity();

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

                geocoder = new GClientGeocoder();

                //Finalmente agrega todos los puntos que corresponden como cluster y el resto en forma normal.
                //var markerCluster = new MarkerClusterer(_Map, _Markers, { maxZoom: _zoom, gridSize: _size, styles: styles[_style] });
                var mcOptions = {gridSize: 50, maxZoom: 15};

                _MarkerCluster = new MarkerClusterer(_Map, _Markers, mcOptions);

                //Oculta el reloj.
                $get('FWMasterGlobalUpdateProgress').style.display = 'none';
            }
        }

    </script>

    <script type="text/javascript">
        function GetIcon(processIcon) {
            _Icon = new GIcon();
            _Icon.image = "../Skins/Images/Map/IconMap" + processIcon + ".png";
            _Icon.shadow = "../Skins/Images/Map/IconMapShadow.png";
            _Icon.iconSize = new GSize(29, 34);
            _Icon.shadowSize = new GSize(44, 34);
            _Icon.iconAnchor = new GPoint(9, 34);
            _Icon.infoWindowAnchor = new GPoint(9, 2);
            _Icon.infoShadowAnchor = new GPoint(18, 25); 
            
//            _Icon.iconSize = new GSize(29, 34);
//            _Icon.shadowSize = new GSize(48, 34);
//            _Icon.iconAnchor = new GPoint(37, 59);
//            _Icon.infoWindowAnchor = new GPoint(31, 8);
        }
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

            //Si el meanScale es cero (0) le ponemos algo al zoom porque sino sale muy expandido!
            if (meanScale == 0) {
                _ZoomLevel = 2;
            }
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
        /////////////////EVENTOS DEL MAPA///////////////////////////
        //////////////////////////////////////////////////
        function OnMouseDown(e) {
            if (e.leftMouseButton)
                _IsPanning = true;
        }
        //////////////////////////////////////////////////
        function OnMouseMove(e) {
            if (_IsPanning)
                CustomHideInfoBox(_CurrentShape);
        }
        //////////////////////////////////////////////////
        function OnMouseUp(e) {
            _IsPanning = false;
        }
        //////////////////////////////////////////////////
        function OnMouseOver(e) {
            return true;
        }
        //////////////////////////////////////////////////
        function OnStartPan(e) {
            if (e.leftMouseButton) {
                // We don't want to handle this here, because a simple mouse click on a shape,
                // could be interpreted as a "start pan" action.
                return;
            }

            CustomHideInfoBox(_CurrentShape);
        }
        //////////////////////////////////////////////////
        function OnStartZoom(e) {
            CustomHideInfoBox(_CurrentShape);
        }
        ////////////////////////////////////////////
        function OnClick(e) {
            //Si es click derecho, hago algo...sino oculto
            if (e.rightMouseButton) {
                if (e.elementID) {
                    var clickedShape = _Map.GetShapeByID(e.elementID);

                    if (clickedShape) {
                        if (window.ero.isVisible() && (clickedShape == _CurrentShape)) {
                            // We clicked on the shape which has an infobox open.
                            CustomHideInfoBox(_CurrentShape);
                        }
                        else if (window.ero.isVisible()) {
                            // We clicked on a different shape. Close the infobox first,
                            // then show it for the new shape.
                            CustomHideInfoBox(_CurrentShape);
                            CustomShowInfoBox(clickedShape);
                            ShowLoading();
                        }
                        else {
                            CustomShowInfoBox(clickedShape);
                            ShowLoading();
                        }
                    }
                }
            }
            else {
                CustomHideInfoBox(_CurrentShape);
            }
        }
    </script>

    <script type="text/javascript">
        //Retorna si se debe mostrar o no este shape
        function GetShowObject(shapeObject) {
            //Para cada nombre de objeto, verifica si el check esta activo o no...
            switch (shapeObject.toString().toLowerCase()) {
                case _PROCESS_OBJECT_NAME:
                    if (_ChkProcess)
                    { return true; }
                    break;
                case _MEASUREMENT_OBJECT_NAME:
                    if (_ChkMeasurement)
                    { return true; }
                    break;
                case _FACILITY_OBJECT_NAME:
                    if (_ChkFacility)
                    { return true; }
                    break;
                case _DEVICE_OBJECT_NAME:
                    if (_ChkDevice)
                    { return true; }
                    break;
            }
            return false;
        }
        //Setea los puntos
        function SetPoint(points) {
            var _arrayVELatLong = new Array();
            var _lat;
            var _lon;
            var _shapeType;
            var _shapeIdEntity;
            var _shapeObject;
            var _iconName;            
            var _record = points.toString().split(";");
            var _ownerName;
            
            //Ahora en el primer campo viene el nombre de la organizacion
            _ownerName = _record[0].split("|")[0]; //El primer campo siempre viene el nombre de la organizacion (Owner)
            
            _shapeObject = _record[0].split("|")[1]; //El primer campo siempre viene el Objeto (ClassName)
            _iconName = _record[0].split("|")[2]; //El segundo campo siempre viene el nombre del icono.
            _shapeIdEntity = _record[0].split("|")[3]; //El segundo campo siempre viene el Id del Objeto (IdEntity)
            _shapeType = _record[0].split("|")[4];  //El tercer campo siempre viene el tipo (Poligono, punto, linea)
            for (var i = 0; i < _record.length - 1; i = i + 1) {
                _ownerName = _record[i].split("|")[0]; //El primer campo siempre viene el nombre de la organizacion (Owner)
                _shapeObject = _record[i].split("|")[1]; //El primer campo siempre viene el Objeto (ClassName)
                _iconName = _record[i].split("|")[2]; //El segundo campo siempre viene el nombre del icono.
                _shapeIdEntity = _record[i].split("|")[3]; //El segundo campo siempre viene el Id del Objeto (IdEntity)
                _shapeType = _record[i].split("|")[4]; //El tercer campo siempre viene el tipo (Poligono, punto, linea)

                //ACA DEBERIA FILTRAR POR EL TIPO DE PROCESS (POR AHORA LA CLASIFICACION) QUE NOS LOS MARCA EL ICONO
                //"ProcessTerritorial"
                //"ProcessSectorial"
                //"ProcessOrganizacional"
                //"ProcessPersonal"
                //"ProcessProductive"
                //"Process"
                if (ShowFilterProcessType(_iconName)) {
                    //Si no esta marcado como check de visibilidad, entonces no carga los puntos...
                    if (GetShowObject(_shapeObject)) {
                        //                        //Aca vemos si hay un filtro por nombre...
                        //                        var _searchOwnerName = document.getElementById('ownerName').value;
                        //                        if (_searchOwnerName != '') {

                        //                            if (_ownerName.toLowerCase().indexOf(_searchOwnerName.toLowerCase()) != -1) {
                        //                                //Lo encontro...
                        //                                //3° campo latitud
                        //                                //4° campo Longitud
                        //                                //Pero se pueden repetir n veces.
                        //                                var _latlonPoints = _record[i].split("|");
                        //                                //Inicializa el array, para cada registro.
                        //                                _arrayVELatLong = new Array(0);
                        //                                //Empieza del 5 porque se saltea el type, objeto, el icono y el nombre de la organizacion, que ya lo tengo.
                        //                                for (var x = 5; x < _latlonPoints.length - 1; x = x + 2) {
                        //                                    _lat = parseFloat(_latlonPoints[x]); //saca el campo de latitud.
                        //                                    _lon = parseFloat(_latlonPoints[x + 1]); //saca el campo de longitud.
                        //                                    //Se guarda los minimos y maximos de Lat y Long.
                        //                                    //Obtiene el mayor y menor latitude
                        //                                    _MaxLatitude = GetMax(_lat, _MaxLatitude);
                        //                                    _MinLatitude = GetMin(_lat, _MinLatitude);
                        //                                    //Obtiene el mayor y menor longitude
                        //                                    _MaxLongitude = GetMax(_lon, _MaxLongitude);
                        //                                    _MinLongitude = GetMin(_lon, _MinLongitude);

                        //                                    //Agrega el punto en el array
                        //                                    _arrayVELatLong.push(GetLatLongPoint(_lat, _lon));
                        //                                    //_arrayVELatLong.push(new VELatLong(_lat, _lon));
                        //                                }
                        //                                //Ahora arma el Shape con el type y el array de puntos.
                        //                                AddPoint(_shapeType, _shapeIdEntity, _shapeObject, _iconName, _arrayVELatLong);
                        //                            }
                        //                        }
                        //                        else {

                        //3° campo latitud
                        //4° campo Longitud
                        //Pero se pueden repetir n veces.
                        var _latlonPoints = _record[i].split("|");
                        //Inicializa el array, para cada registro.
                        _arrayVELatLong = new Array(0);
                        //Empieza del 5 porque se saltea el type, objeto, el icono y el nombre de la organizacion, que ya lo tengo.
                        for (var x = 5; x < _latlonPoints.length - 1; x = x + 2) {
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
                        AddPoint(_shapeType, _shapeIdEntity, _shapeObject, _iconName, _arrayVELatLong);
                        //                        }
                    }
                }
            }
        }
        function ShowFilterProcessType(iconName) {
            //"ProcessTerritorial"
            //"ProcessSectorial"
            //"ProcessOrganizacional"
            //"ProcessPersonal"
            //"ProcessProductive"
            //"Process"
            switch (iconName) {
                case "ProcessTerritorial":
                    if (document.getElementById('ctl00_ContentMain_chkTerritorial').checked) {
                        return true;
                    }
                    break;
                case "ProcessSectorial":
                    if (document.getElementById('ctl00_ContentMain_chkSectorial').checked) {
                        return true;
                    }
                    break;
                case "ProcessOrganizacional":
                    if (document.getElementById('ctl00_ContentMain_chkOrganizational').checked) {
                        return true;
                    }
                    break;
                case "ProcessPersonal":
                    if (document.getElementById('ctl00_ContentMain_chkPersonal').checked) {
                        return true;
                    }
                    break;
                case "ProcessProductive":
                    if (document.getElementById('ctl00_ContentMain_chkProductive').checked) {
                        return true;
                    }
                    break;
                case "Process":
                    return true;
                    break;
            }

            return false;
        }
        
        function SetAttributeAndAddShape(shapeType, idEntity, entityObject, iconName, pointLatLong) {

            var _toolTip = "<iframe id='iframeTooltip' src='../Dashboard/GeographicTooltips.aspx?IdEntity=" + idEntity.toString().replace("&", "|")
                        + "&EntityObject=" + entityObject
                        + "&' scrolling='no' frameborder='0' class='iframeTooltip' allowtransparency='true'>"
                        + "</iframe>";

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

                _Shape.SetDescription(_toolTip);

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
                    //var marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()));
                    GetIcon(iconName);
                    if (pointLatLong.length == 1) {
                        //var marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()), _Icon);
                        var marker = new google.maps.Marker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()), _Icon);
                    }
                    else {
                        //var marker = new GMarker(SetCenterOfPolygon(pointLatLong), _Icon);
                        var marker = new google.maps.Marker(SetCenterOfPolygon(pointLatLong), _Icon);
                    }
                    //Se guarda en el array de puntos para armar el cluster...
                    _Markers.push(marker);

                    //Si es click derecho, hago algo...sino oculto
                    GEvent.addListener(marker, "click", function() {
                        ShowLoading();
                        marker.openInfoWindowHtml(_toolTip);
                    });
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
        function AddPoint(shapeType, idEntity, entityObject, iconName, pointLatLong) {
            //Construye el _Shape
            GetShape(shapeType, pointLatLong);
            //Setea los atributos para el shape y lo agrega al layer.
            SetAttributeAndAddShape(shapeType, idEntity, entityObject, iconName, pointLatLong);

            //Analiza al Poligono.
            //Si es un Pushpin, no setea el line
            if (shapeType == _POLYGON_SHAPE_TYPE) {
                //Construye el punto en el centro del poligono.
                //_Shape = new VEShape(VEShapeType.Pushpin, SetCenterOfPolygon(pointLatLong));
                GetShape(_PUSHPIN_SHAPE_TYPE, SetCenterOfPolygon(pointLatLong));
                //SetAttributeAndAddShape(_PUSHPIN_SHAPE_TYPE, idEntity, entityObject, SetCenterOfPolygon(pointLatLong));
                SetAttributeAndAddShape(_PUSHPIN_SHAPE_TYPE, idEntity, entityObject, iconName, pointLatLong);
            }
        }
//        function showAddress() {
//            var address = document.getElementById("address").value;
//            if (geocoder) {
//                geocoder.getLatLng(address,
//                         function(point) {
//                             if (!point) {
//                                 alert(address + " not found");
//                             } else {
//                                 //clearMap();
//                                 _Map.setCenter(point, 13);
//                             }
//                         }
//                       );
//            }
//        }
        function showPopUpSearch(eventArgs) {
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
//            StopEvent(eventArgs);
            //Busca el popup
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            //muestra el popup
            modalPopupBehavior.show();
        }
        function showPopUpFilter(eventArgs) {
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            //            StopEvent(eventArgs);
            //Busca el popup
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorFilter');
            //muestra el popup
            modalPopupBehavior.show();
        }
        function showSearchOwner() {
            //Limpiamos los maximos y minimos
            _MaxLatitude = 0;
            _MinLatitude = 0;
            _MaxLongitude = 0;
            _MinLongitude = 0;
          
            InitMap();
            //Busca el popup
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            //muestra el popup
            modalPopupBehavior.hide();
        }
        function FilterProcessType() {
            InitMap();

            //Busca el popup
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorFilter');
            //muestra el popup
            modalPopupBehavior.hide();
        }
    </script>

    <input id="inputPoints" type="hidden" value="<% =_PointsLatLong%>" />
    <input id="MapType" type="hidden" value="<% =_MapType%>" />
    <div id='myMap'>
    </div>
    <div id='FilterMap'>
        <asp:Button ID="btnShowSearch" runat="server" Text="" OnClientClick="javascript:showPopUpSearch(this); return false;" CssClass="btnShowFilter" />
        <asp:Button ID="btnShowFilter" runat="server" Text="" OnClientClick="javascript:showPopUpFilter(this); return false;" CssClass="btnShowFilter" />
    </div>
    <!--Abro Popup de Search -->
    <asp:Panel ID="pnlPopUpSearch" runat="server">
        <asp:Button ID="btnHidden" runat="server" Visible="False" CausesValidation="False" />
        <ajaxToolkit:ConfirmButtonExtender ID="cbeSearch" runat="server" TargetControlID="btnHidden"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpeSearch" Enabled="False"
            ConfirmText="" />
        <ajaxToolkit:ModalPopupExtender ID="mpeSearch" runat="server" TargetControlID="btnHidden"
            BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmSearch"
            CancelControlID="btnClose" BackgroundCssClass="ModalPopUp"
            DynamicServicePath="" Enabled="True" />
        <div>
            <asp:Panel ID="pnlConfirmSearch" runat="server" Style="display: none" CssClass="pnlBgSearch">
                <asp:Label ID="lblTitleSearch" runat="server" Text="Search" CssClass="lblTitleSearch"></asp:Label>
                <asp:Label ID="lblSubTitleSearch" runat="server" Text="Search by Name"
                    CssClass="lblSubTitleSearch"></asp:Label>
                <input type="text" name="ownerName" runat="server" id="ownerName" value="" class="txtSearch" />
                <asp:Button ID="btnSearchName" runat="server" Text="Search" CssClass="btnSearchApply" />
                <asp:Button ID="btnClose" runat="server" Text="x" CssClass="btnSearchClose" />
            </asp:Panel>
        </div>
    </asp:Panel>
    <!-- Fin de Popup de Search -->

    <!--Abro Popup de Filter -->
    <asp:Panel ID="pnlPopUpFilter" runat="server">
        <asp:Button ID="btnHiddenFilter" runat="server" Visible="False" CausesValidation="False" />
        <ajaxToolkit:ConfirmButtonExtender ID="cbeFilter" runat="server" TargetControlID="btnHiddenFilter"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpeFilter" Enabled="False"
            ConfirmText="" />
        <ajaxToolkit:ModalPopupExtender ID="mpeFilter" runat="server" TargetControlID="btnHiddenFilter"
            BehaviorID="programmaticModalPopupBehaviorFilter" PopupControlID="pnlConfirmFilter"
            CancelControlID="btnCloseFilter" BackgroundCssClass="ModalPopUp"
            DynamicServicePath="" Enabled="True" />
        <div>
            <asp:Panel ID="pnlConfirmFilter" runat="server" Style="display: none" CssClass="pnlBgFilter">
                <asp:Label ID="lblTitleFilter" runat="server" Text="Filter" CssClass="lblTitleFilter"></asp:Label>
                <asp:Label ID="lblSubTitleFilter" runat="server" Text="Filter for Process Type"
                    CssClass="lblSubTitleFilter"></asp:Label>
                <br />
                <div id="divFilter" runat="server" class="Check">
                    <asp:CheckBox ID="chkTerritorial" runat="server" Text="<%$ Resources:CommonListManage, ProcessTypeTerritorial %>" Checked="true" CssClass="Check" />
                    <asp:CheckBox ID="chkSectorial" runat="server" Text="<%$ Resources:CommonListManage, ProcessTypeSectorial %>" Checked="true" CssClass="Check"/>
                    <asp:CheckBox ID="chkOrganizational" runat="server" Text="<%$ Resources:CommonListManage, ProcessTypeOrganizational %>" Checked="true" CssClass="Check"/>
                    <asp:CheckBox ID="chkPersonal" runat="server" Text="<%$ Resources:CommonListManage, ProcessTypePersonnel %>" Checked="true" CssClass="Check" />
                    <asp:CheckBox ID="chkProductive" runat="server" Text="<%$ Resources:CommonListManage, ProcessTypeProductive %>" Checked="true" CssClass="Check"/>
                </div>
                <asp:Button ID="btnFilter" runat="server" Text="Apply" OnClientClick="FilterProcessType();" CausesValidation="false" CssClass="btnFilterApply" />
                <asp:Button ID="btnCloseFilter" runat="server" Text="x" CssClass="btnFilterClose" />
            </asp:Panel>
        </div>
    </asp:Panel>
    <!-- Fin de Popup de Filter -->
</asp:Content>
