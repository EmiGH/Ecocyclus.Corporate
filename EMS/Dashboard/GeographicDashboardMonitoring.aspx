<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="GeographicDashboardMonitoring.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.GeographicDashboardMonitoring" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <%--<script type="text/javascript" src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>--%>

    <%--    <script src="../AppCode/JavaScriptCore/markerclusterer_packed.js" type="text/javascript"></script>--%>

    <script src="//maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false" type="text/javascript"></script>
        
    <script type="text/javascript">
        var styles = [[{
            url: '../image/people35.png',
            height: 35,
            width: 35,
            opt_anchor: [16, 0],
            opt_textColor: '#FF00FF'
        },
          {
              url: '../images/people45.png',
              height: 45,
              width: 45,
              opt_anchor: [24, 0],
              opt_textColor: '#FF0000'
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
              textColor: '#FF00FF'
          },
          {
              url: '../images/conv40.png',
              height: 36,
              width: 40,
              opt_anchor: [6, 0],
              opt_textColor: '#FF0000'
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
              opt_textColor: '#FF00FF'
          },
          {
              url: '../images/heart40.png',
              height: 35,
              width: 40,
              opt_anchor: [8, 0],
              opt_textColor: '#FF0000'
          },
          {
              url: '../images/heart50.png',
              width: 50,
              height: 44,
              opt_anchor: [12, 0]
}]];
    </script>

    <script type="text/javascript">
        //debugger;
        //INICIALIZACION
        //Event onload
        if (_BrowserName == _IEXPLORER) {   //IE and Opera
            window.attachEvent('onload', InitMap);
            //window.attachEvent('onunload', GUnload);
        }
        else {   //FireFox
            document.addEventListener('DOMContentLoaded', InitMap, false);
            //document.addEventListener('onunload', GUnload, false);
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
        //var _MAP_TYPE_VIRTUAL_EARTH = "virtualearth";

        //VARIABLES
        var _Points = null;
        var _Map = null;
        var _Shape = null;
        var _Layers = null;
        var _MapType = null;
        //Array de GMarker para armar el cluster
        //var _Markers = new Array(0);
        //var _MarkerCluster = null;
        //our bounding rectangle components, These parmeters can be calculated if we have an array of coordinates
        var _MaxLatitude = 0;
        var _MinLatitude = 0;
        var _MaxLongitude = 0;
        var _MinLongitude = 0;
        var _CenterLatitudeLongitude = 0;
        var _ZoomLevel = 17;
        //Variables para la visualizacion (filtros de la pagina)
        var _ChkProcess;
        var _ChkMeasurement;
        var _ChkFacility;
        var _ChkDevice;
        //Variables para el click derecho sobre el pushpin..
        var _Hide;
        var _CurrentShape = null;
        var _IsPanning = false;
        var _Icon = null;
        //Esto lo agregamos ahora para poder filtrar los Process....
        var _strProcessSelected = '';
        var _Limits;
        var _FullArrayLatLong = new Array();

        //Declarative Function...
        function InitializeGlobalVars() {
            _Points = null;
            _MaxLatitude = 0;
            _MinLatitude = 0;
            _MaxLongitude = 0;
            _MinLongitude = 0;
            _CenterLatitudeLongitude = 0;
            _ZoomLevel = 17;
        }
        function InitMap() {
            //debugger;
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
            _myFilterMap.style.left = (_contentWidth - 514) + 'px';
            _myFilterMap.style.bottom = (_contentHeight - 24) + 'px';
            //debugger;
            //Solo al ser la primera vez, debemos cargar la variable con los process seleccionado
            if (_strProcessSelected == '') {
                SetProcessSelected();
            }
            GetMap();
        }
//        function InitializeHandlers() {
//            //Agrega Eventos...
//            _Map.AttachEvent("onmousedown", OnMouseDown);
//            _Map.AttachEvent("onmousemove", OnMouseMove);
//            _Map.AttachEvent("onmouseup", OnMouseUp);
//            _Map.AttachEvent("onmouseover", OnMouseOver);
//            _Map.AttachEvent("onclick", OnClick);
//            _Map.AttachEvent("onstartpan", OnStartPan);
//            _Map.AttachEvent("onstartzoom", OnStartZoom);
//        }
        function GetFilterEntity() {
            //Carga los valores de visualizacion de filtros.
            //Ahora solo queda el process
            //_ChkProcess = true;
            _ChkFacility = true;
        }
        function GetMap() {
            //debugger;
            _MapType = document.getElementById('MapType').value;

            //Funcion que verifica que Mapa debe cargar, Google o VE.
            switch (_MapType.toLowerCase()) {
                case _MAP_TYPE_GOOGLE:
                    GetGoogleMap();
                    break;

                //                case _MAP_TYPE_VIRTUAL_EARTH: 
                //                    GetVirtualEarth(); 
                //                    break; 
            }
        }
//        function GetVirtualEarth() {
//            //Inicializa las variables globales para la recarga por filtros...
//            InitializeGlobalVars();
//            //Muestra el reloj.
//            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
//            //Carga los puntos pasados desde el CodeBehind.
//            _Points = document.getElementById('inputPoints').value;

//            //Carga los valores de visualizacion de filtros.
//            GetFilterEntity();

//            //Contruye y carga el mapa.
//            _Map = new VEMap('myMap');
//            _Map.SetDashboardSize(VEDashboardSize.Small);
//            _Map.LoadMap();
//            _Map.SetScaleBarDistanceUnit(VEDistanceUnit.Kilometers);

//            //Agrega los eventos sobre el mapa.
//            InitializeHandlers();

//            //Contruye el layer
//            _Layers = new VEShapeLayer();
//            //Carga los puntos en el mapa y guarda el min-max de lat-long.
//            SetPoint(_Points);
//            //Obtiene el centro del mapa y el zoom.
//            CalculateCenterAndZoom();
//            //Ahora setea el centro y el zoom
//            _Map.SetCenterAndZoom(_CenterLatitudeLongitude, _ZoomLevel);
//            //Agrega el layer al mapa.
//            _Map.AddShapeLayer(_Layers);
//            //Configura el layer como Cluster.
//            _Layers.SetClusteringConfiguration(VEClusteringType.Grid);
//            //Oculta el reloj.
//            $get('FWMasterGlobalUpdateProgress').style.display = 'none';
//        }
        function GetGoogleMap() {
            var _zoom = 20;
            var _size = -1;
            var _style = 0;

            //debugger;
            
            //Carga los puntos pasados desde el CodeBehind.
            _Points = document.getElementById('inputPoints').value;
            //                _Map.clearOverlays();
//            _Markers = new Array(0);
//            if (_MarkerCluster != null) {
//                _MarkerCluster.clearMarkers();
//            }

            if (_Points != '') {
                //Carga los valores de visualizacion de filtros.
                GetFilterEntity();

                //                    //Obtiene y setea todos los puntos
                //                    SetPoint(_Points);
                //                    //Calcula el centro del mapa y zoom.
                //CalculateCenterAndZoom();

                //Ahora setea el centro y el zoom
                //_Map.setCenter( new GLatLng(_CenterLatitudeLongitude.lat(), _CenterLatitudeLongitude.lng()), _ZoomLevel);
                //_Map.setCenter(GetLatLongPoint(_CenterLatitudeLongitude.lat(), _CenterLatitudeLongitude.lng()), _ZoomLevel);
                //debugger;
                _Map = new google.maps.Map(document.getElementById("myMap"), {
                    //center: new google.maps.LatLng(_CenterLatitudeLongitude.lat(), _CenterLatitudeLongitude.lng()),
                    //zoom: 5,   //_ZoomLevel,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

                //Obtiene y setea todos los puntos
                SetPoint(_Points);

            }
            else {
                //_Map.setCenter(new GLatLng(0, 0), 1);

                _Map = new google.maps.Map(document.getElementById("myMap"), {
                    center: new google.maps.LatLng(0, 0),
                    zoom: 1,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

            }
            //debugger;

//            _Map.fitBounds(_Limits);            
//            var listener = google.maps.event.addListener(_Map, "idle", function() {
//                _Map.setZoom(5);
//                google.maps.event.removeListener(listener);
//            });
            _zoom = _zoom == -1 ? null : _zoom;
            _size = _size == -1 ? null : _size;
            _style = _style == "-1" ? null : parseInt(_style, 10);

            geocoder = new google.maps.Geocoder();      //new GClientGeocoder();



            //  Make an array of the LatLng's of the markers you want to show
            //var LatLngList = new Array(new google.maps.LatLng(52.537, -2.061), new google.maps.LatLng(52.564, -2.017));
            //  Create a new viewpoint bound
            var bounds = new google.maps.LatLngBounds();
            //  Go through each...
            for (var i = 0, LtLgLen = _FullArrayLatLong.length; i < LtLgLen; i++) {
                var ltlgpoint = new google.maps.LatLng(_FullArrayLatLong[i].lat(), _FullArrayLatLong[i].lng());
                //  And increase the bounds to take this point
                bounds.extend(ltlgpoint);
            }
            //  Fit these bounds to the map
            _Map.fitBounds(bounds);

            
            //Finalmente agrega todos los puntos que corresponden como cluster y el resto en forma normal.
            //var markerCluster = new MarkerClusterer(_Map, _Markers, { maxZoom: _zoom, gridSize: _size, styles: styles[_style] });
            //Ruben Agrega ESto (mejor saca esto)
            //_MarkerCluster = new MarkerClusterer(_Map, _Markers);

            //Oculta el reloj.
            $get('FWMasterGlobalUpdateProgress').style.display = 'none';
            //}
        }

    </script>

    <script type="text/javascript">
//        function GetIcon(facilityType) {
////            _Icon = new GIcon();
////            _Icon.image = "../Skins/Images/Map/IconMap" + facilityType + ".png";
////            _Icon.shadow = "../Skins/Images/Map/IconMapShadow.png";
////            _Icon.iconSize = new GSize(29, 34);
////            _Icon.shadowSize = new GSize(44, 34);
////            _Icon.iconAnchor = new GPoint(9, 34);
////            _Icon.infoWindowAnchor = new GPoint(9, 2);
////            _Icon.infoShadowAnchor = new GPoint(18, 25);


//            var image = "../Skins/Images/Map/IconMap" + facilityType + ".png";
//            var shadow = "../Skins/Images/Map/IconMapShadow.png";
//            
//            //var myLatLng = new google.maps.LatLng(-33.890542, 151.274856);
//            var beachMarker = new google.maps.Marker({
//                //position: myLatLng,
//                map: _Map,
//                icon: image,
//                shadow: shadow
//            });
//        }


        //FUNCIONES GENERICAS
//        function GetLatLongPoint(latitude, longitude) {

//            switch (_MapType.toLowerCase()) {
//                case _MAP_TYPE_GOOGLE:
//                    //return new GLatLng(latitude, longitude);
//                    return new google.maps.LatLng(latitude, longitude);
//                    break;

//                case _MAP_TYPE_VIRTUAL_EARTH:
//                    return new VELatLong(latitude, longitude);
//                    break;
//            }
//        }
        function CalculateCenterAndZoom() {
            //debugger;
            //default zoom scales in km/pixel from http://msdn2.microsoft.com/en-us/library/aa940990.aspx
            var defaultScales = new Array(78.27152, 39.13576, 19.56788, 9.78394, 4.89197, 2.44598, 1.22299, 0.61150, 0.30575, 0.15287, 0.07644, 0.03822, 0.01911, 0.00955, 0.00478, 0.00239, 0.00119, 0.0006, 0.0003);

            if (_MaxLatitude == 0) {
                if (document.getElementById('inputPoints').value != "") {
                    //Pushpin;-36|-60;
                    //Como solo hay un punto, el maximo y minimo es el mismo!
                    _MaxLatitude = parseFloat(document.getElementById('inputPoints').value.split(';')[0].split('|')[5]);
                    _MinLatitude = parseFloat(document.getElementById('inputPoints').value.split(';')[0].split('|')[6]);

                    _MaxLongitude = parseFloat(document.getElementById('inputPoints').value.split(';')[0].split('|')[5]);
                    _MinLongitude = parseFloat(document.getElementById('inputPoints').value.split(';')[0].split('|')[6]);


//                    _MaxLatitude = parseFloat(document.getElementById('inputPoints').value.split(';')[1].split('|')[0]);
//                    _MinLatitude = parseFloat(document.getElementById('inputPoints').value.split(';')[1].split('|')[0]);

//                    _MaxLongitude = parseFloat(document.getElementById('inputPoints').value.split(';')[1].split('|')[1]);
//                    _MinLongitude = parseFloat(document.getElementById('inputPoints').value.split(';')[1].split('|')[1]);
                }
            }

            //calculate center coordinates
            var centerLat = (_MaxLatitude + _MinLatitude) / 2;
            var centerLong = (_MaxLongitude + _MinLongitude) / 2;
            var centerPoint = new google.maps.LatLng(centerLat, centerLong); //GetLatLongPoint(centerLat, centerLong);   //new VELatLong(centerLat, centerLong);

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
//                if (_MapType.toLowerCase() == _MAP_TYPE_VIRTUAL_EARTH) {
//                    //Obtiene el mayor y menor latitude
//                    _maxLat = GetMax(pointsOfPolygon[x].Latitude, _maxLat);
//                    _minLat = GetMin(pointsOfPolygon[x].Latitude, _minLat);
//                    //Obtiene el mayor y menor longitude
//                    _maxLong = GetMax(pointsOfPolygon[x].Longitude, _maxLong);
//                    _minLong = GetMin(pointsOfPolygon[x].Longitude, _minLong);
//                }
//                else {
                    //Obtiene el mayor y menor latitude
                    _maxLat = GetMax(pointsOfPolygon[x].lat(), _maxLat);
                    _minLat = GetMin(pointsOfPolygon[x].lat(), _minLat);
                    //Obtiene el mayor y menor longitude
                    _maxLong = GetMax(pointsOfPolygon[x].lng(), _maxLong);
                    _minLong = GetMin(pointsOfPolygon[x].lng(), _minLong);
//                }
            }
            //calculate center coordinates
            var centerLat = (_maxLat + _minLat) / 2;
            var centerLong = (_maxLong + _minLong) / 2;

            return new google.maps.LatLng(centerLat, centerLong);   // GetLatLongPoint(centerLat, centerLong);  //new VELatLong(centerLat, centerLong);
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
            //_Map.ShowInfoBox(shape);
            _CurrentShape = shape;
        }
        //////////////////////////////////////////////////
        function CustomHideInfoBox(shape) {
            if (!shape)
                return;

            // Before actually calling the hide method, we need to restore the ero hide function,
            EnableEroHide();
            //_Map.HideInfoBox(shape);
            _CurrentShape = null;
        }
    </script>

<%--    <script type="text/javascript">
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
--%>
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
            //debugger;
            var _arrayVELatLong = new Array();
            var _lat;
            var _lon;
            var _shapeType;
            var _shapeIdEntity;
            var _shapeObject;
            var _iconName;
            var _record = points.toString().split(";");
            var _idProcess;

            _FullArrayLatLong = new Array(0);
            
            _idProcess = _record[0].split("|")[0]; //El primer campo siempre viene el Objeto (_idProcess)
            _shapeObject = _record[0].split("|")[1]; //El segundo campo siempre viene el Objeto (ClassName)
            _iconName = _record[0].split("|")[2]; //El tercero campo siempre viene el nombre del icono.
            _shapeIdEntity = _record[0].split("|")[3]; //El cuarto campo siempre viene el Id del Objeto (IdEntity)
            _shapeType = _record[0].split("|")[4];  //El quinto campo siempre viene el tipo (Poligono, punto, linea)
            for (var i = 0; i < _record.length - 1; i = i + 1) {
                _idProcess = _record[i].split("|")[0]; //El primer campo siempre viene el Objeto (_idProcess)
                //Verificamos que el Process que tengo para cargar este seleccionado en el filtro!
                if (_strProcessSelected.indexOf('-' + _idProcess + '-') != -1) {
                    _shapeObject = _record[i].split("|")[1]; //El segundo campo siempre viene el Objeto (ClassName)
                    _iconName = _record[i].split("|")[2]; //El tercer campo siempre viene el nombre del icono.
                    _shapeIdEntity = _record[i].split("|")[3]; //El cuarto campo siempre viene el Id del Objeto (IdEntity)
                    _shapeType = _record[i].split("|")[4]; //El quinto campo siempre viene el tipo (Poligono, punto, linea)
                    //Si no esta marcado como check de visibilidad, entonces no carga los puntos...
                    if (GetShowObject(_shapeObject)) {
                        //3° campo latitud
                        //4° campo Longitud
                        //Pero se pueden repetir n veces.
                        var _latlonPoints = _record[i].split("|");
                        //Inicializa el array, para cada registro.
                        _arrayVELatLong = new Array(0);
                        //Empieza del 5 porque se saltea el type, objeto y el icono y ahora el idprocess, que ya lo tengo.
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
                            var _loc = new google.maps.LatLng(_lat, _lon);
                            _arrayVELatLong.push(_loc);
                            _FullArrayLatLong.push(_loc);
                            //_arrayVELatLong.push(GetLatLongPoint(_lat, _lon));

                        }
                        //Ahora arma el Shape con el type y el array de puntos.
                        AddPoint(_idProcess, _shapeType, _shapeIdEntity, _shapeObject, _iconName, _arrayVELatLong);
                    }
                }
            }
        }
        function SetAttributeAndAddShape(idProcess, shapeType, idEntity, entityObject, iconName, pointLatLong) {
            //var _idProcess = document.getElementById('IdProcess').value;
            var _toolTip = "<iframe id='iframeTooltip' src='../Dashboard/GeographicTooltips.aspx?IdEntity=" + idEntity.toString().replace("&", "|")
                        + "&EntityObject=" + entityObject
                        + "&IdProcess=" + idProcess
                        + "&IsDashBoardMonitoring=true" 
                        + "&' scrolling='no' frameborder='0' class='iframeTooltip' allowtransparency='true'>"
                        + "</iframe>";


            _Limits = new google.maps.LatLngBounds();
            var marker;
            var image = new google.maps.MarkerImage('../Skins/Images/Map/IconMap' + iconName + '.png');
            var shadow = new google.maps.MarkerImage('../Skins/Images/Map/IconMapShadow.png');

//            if (_MapType.toLowerCase() == _MAP_TYPE_VIRTUAL_EARTH) {
//                //Set the info box
//                _Map.ClearInfoBoxStyles();
//                //Set the line Color
//                var lineColor = new VEColor(71, 171, 0, 1);
//                _Shape.SetLineColor(lineColor);
//                //Si es un Pushpin, no setea el line
//                if (shapeType != _PUSHPIN_SHAPE_TYPE) {
//                    //Set the line width
//                    _Shape.SetLineWidth(3);
//                }
//                //Set the fill color
//                var fillColor = new VEColor(142, 191, 0, 0.3);
//                _Shape.SetFillColor(fillColor);

//                _Shape.SetDescription(_toolTip);

//                //Si es un Pushpin, no setea el line
//                if ((shapeType == _POLYLINE_SHAPE_TYPE) || (shapeType == _POLYGON_SHAPE_TYPE)) {
//                    _Shape.HideIcon();
//                }
//                // Add the pushpins to the _Map
//                _Layers.AddShape(_Shape);
//                //_Map.SetInfoBoxStyles();
//            }
//            else {
            //debugger;
                if (shapeType != "Pushpin") {
                    //Si es puspin No se usa, ya que lo carga como cluster....con esto MarkerClusterer()
                    //Ruben Agrego esto!!!                    
//                    _Map.setCenter(_Shape.getBounds().getCenter());
                    //                    _Map.addOverlay(_Shape);

                    var _location = new google.maps.LatLng(pointLatLong[0].lat(), pointLatLong[0].lng());

                    //Agrego el marcador
                    //debugger;
                    marker = new google.maps.Marker({
                        position: _location,
                        map: _Map,
                        //shadow: shadow,
                        animation: google.maps.Animation.DROP,
                        icon: image,
                        shadow: shadow
                        //shape: shape,

                    });
                }
                else {
                    //var marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()));
                    //GetIcon(iconName);
                    if (pointLatLong.length == 1) {

                        //Armo la referencia
                        var _location = new google.maps.LatLng(pointLatLong[0].lat(), pointLatLong[0].lng());


//                        var image = "../Skins/Images/Map/IconMap" + iconName + ".png";
//                        var shadow = "../Skins/Images/Map/IconMapShadow.png";

                        //Agrego el marcador
                        //debugger;
                        marker = new google.maps.Marker({
                            position: _location,
                            map: _Map,
                            //shadow: shadow,
                            animation: google.maps.Animation.DROP,
                            icon: image,
                            shadow: shadow
                            //shape: shape,

                        });

                        //var marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()), _Icon);
                    }
                    else {

                        //Armo la referencia
                        var _location = SetCenterOfPolygon(pointLatLong);   //new google.maps.LatLng(pointLatLong[0].lat(), pointLatLong[0].lng());

                        //Agrego el marcador
                        //debugger;
                        marker = new google.maps.Marker({
                            position: _location,
                            map: _Map,
                            animation: google.maps.Animation.DROP,
                            icon: image,
                            shadow: shadow
                            //shape: shape,

                        });
                        //var marker = new GMarker(SetCenterOfPolygon(pointLatLong), _Icon);
                    }
                    //Se guarda en el array de puntos para armar el cluster...
                    //_Markers.push(marker);
                    //debugger;
                    //Ruben Agrega esto!!!
                    //Ver esto...
                    //_Map.addOverlay(marker);
                    //marker.setMap(_Map);

//                    google.maps.event.addListener(marker, 'click', function() {
//                        infowindow.open(map, marker);
//                    });

//                    google.maps.event.addListener(marker, 'click', function(event) {
//                        ShowLoading();
//                        marker.openInfoWindowHtml(_toolTip);
//                    });


                    /* now inside your initialise function */
                    var infowindow = new google.maps.InfoWindow({
                        content: _toolTip
                    });


                    google.maps.event.addListener(marker, 'click', function(event) {
                        //ShowLoading();
                        infowindow.open(_Map, marker);
                    });

                    //_Limits.extend(_location);
                    _Limits.extend(_location); 
                    //_Map.fitBounds(_Limits);

                    
//                    //Si es click derecho, hago algo...sino oculto
//                    GEvent.addListener(marker, "click", function() {
//                        ShowLoading();
//                        marker.openInfoWindowHtml(_toolTip);
//                    });
                    //return marker;
                }
//            }
        }
        function GetShape(shapeType, pointLatLong, iconName) {
            var image = new google.maps.MarkerImage('../Skins/Images/Map/IconMap' + iconName + '.png');
            var shadow = new google.maps.MarkerImage('../Skins/Images/Map/IconMapShadow.png');

            //Verifica que tipo de mapa se esta usando y construye el SHAPE.
            switch (_MapType.toLowerCase()) {
                case _MAP_TYPE_GOOGLE:
                    //En base al type construye una polilinea, punto o poligono
                    switch (shapeType) {
                        case "Pushpin":
                            //_Shape = createInfoMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()), infobox);
                            //Ruben Agrega esto!!!
                            //_Shape = new GMarker(pointLatLong);
                            //Armo la referencia
                            
                            //SE SUPONE QUE NO DEBE VOLVER A CARGARSE EL PUSHPING....
                            var _location = new google.maps.LatLng(pointLatLong[0].lat(), pointLatLong[0].lng());
                            //debugger;
                            //Agrego el marcador
                            _Shape = new google.maps.Marker({
                                position: _location,
                                map: _Map,
                                animation: google.maps.Animation.DROP,
                                icon: image,
                                shadow: shadow
                                //shape: shape,

                            });

                            break;
                        case "Polygon":
                            //_Shape = new GPolygon(pointLatLong, "#669933", 5, 0.7, "#996633", 0.4);
                            _Shape = new google.maps.Polygon({
                                path: pointLatLong, //new google.maps.MVCArray()
                                map: map,
                                strokeColor: defaultColor,
                                strokeWeight: 3,
                                strokeOpacity: 0.5,
                                fillColor: defaultColor,
                                fillOpacity: 0.3,
                                clickable: false
                            });

                            break;
                        case "Polyline":
                            //_Shape = new GPolyline(pointLatLong, "#669933", 5, 0.7, "#996633", 0.4);
                            _Shape = new google.maps.Polyline({
                                path: pointLatLong,
                                map: _Map,
                                strokeColor: '#669933',
                                strokeWeight: 5,
                                strokeOpacity: 0.7,
                                clickable: false
                            });

                            break;
                    }
                    break;

//                case _MAP_TYPE_VIRTUAL_EARTH:
//                    //En base al type construye una polilinea, punto o poligono
//                    switch (shapeType) {
//                        case _PUSHPIN_SHAPE_TYPE:
//                            _Shape = new VEShape(VEShapeType.Pushpin, pointLatLong);
//                            break;
//                        case _POLYGON_SHAPE_TYPE:
//                            _Shape = new VEShape(VEShapeType.Polygon, pointLatLong);
//                            break;
//                        case _POLYLINE_SHAPE_TYPE:
//                            _Shape = new VEShape(VEShapeType.Polyline, pointLatLong);
//                            break;
//                    }
//                    break;
            }
        }
        function AddPoint(idProcess, shapeType, idEntity, entityObject, iconName, pointLatLong) {
            //debugger;
            //Construye el _Shape
            GetShape(shapeType, pointLatLong, iconName);
            //Setea los atributos para el shape y lo agrega al layer.
            SetAttributeAndAddShape(idProcess, shapeType, idEntity, entityObject, iconName, pointLatLong);

//            //Analiza al Poligono.
//            //Si es un Pushpin, no setea el line
//            //Ruben Agrega esto!!!
//            if ((shapeType == _POLYGON_SHAPE_TYPE) || (shapeType == _POLYLINE_SHAPE_TYPE)) {
//                //if (shapeType == _POLYGON_SHAPE_TYPE) {
//                //Construye el punto en el centro del poligono.
//                //_Shape = new VEShape(VEShapeType.Pushpin, SetCenterOfPolygon(pointLatLong));
//                GetShape(_PUSHPIN_SHAPE_TYPE, SetCenterOfPolygon(pointLatLong), iconName);
//                //SetAttributeAndAddShape(_PUSHPIN_SHAPE_TYPE, idEntity, entityObject, SetCenterOfPolygon(pointLatLong));
//                SetAttributeAndAddShape(idProcess, _PUSHPIN_SHAPE_TYPE, idEntity, entityObject, iconName, pointLatLong);
//            }
        }


        function showAddress() {
            var geocoder = new google.maps.Geocoder();
            var address = document.getElementById("address").value;

            geocoder.geocode({ 'address': address }, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    //var latLng = results[0].geometry.location;
                    var lat = results[0].geometry.location.wa;
                    var lng = results[0].geometry.location.xa;
//                    alert(lat);
//                    alert(lng);
                    var me = new google.maps.LatLng(lat, lng);
                    _Map.setCenter(me);
                } else {
                    alert("Geocode was not successful for the following reason: " + status);
                }
            });
        }


//        function showAddress() {
//            var address = document.getElementById("address").value;
//            if (geocoder) {
//                //geocoder.getLatLng(address,
//                geocoder.geocode(address,
//                         function(point) {
//                             if (!point) {
//                                 alert(address + " not found");
//                             } else {
//                             //clearMap();
//                                 _Map.setCenter(point, 13);
//                             }
//                         }
//                       );
//            }
//        }
        function showPopUpFilter(eventArgs) {
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            //            StopEvent(eventArgs);
            //Busca el popup
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorFilter');
            //muestra el popup
            modalPopupBehavior.show();
        }
        function SetProcessSelected() {
            //debugger;
            //Inicializamos!
            _strProcessSelected = '';
            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                for (i = 0; i < document.getElementById('ctl00_ContentMain_divFilter').childNodes.length; i++) {
                    //El texto sale asi: "<INPUT id=ctl00_ContentMain_chkProcess_377 name=ctl00$ContentMain$chkProcess_377 value="" CHECKED type=checkbox><LABEL for=ctl00_ContentMain_chkProcess_377>Observatorio de la Ciudad Autónoma de Buenos Aires</LABEL>"	String
                    //Solo nos importa la primer parte ID=....
                    var _STARTPOSITION = 7; //donde empieza
                    //Con esto buscamos donde termina el primer espacio, para acceder al ID...
                    var _lenId = document.getElementById('ctl00_ContentMain_divFilter').childNodes(i).innerHTML.substr(_STARTPOSITION, 50).indexOf(' ');
                    var _idNameChk = document.getElementById('ctl00_ContentMain_divFilter').childNodes(i).innerHTML.substr(_STARTPOSITION, _lenId).split('=')[1];

                    //teniendo el ID, buscamos el control y vemos si esta Checked o no....
                    var _checkbox = document.getElementById(_idNameChk);
                    if (_checkbox != null) {
                        if (_checkbox.checked) {
                            //Obtenemos el Id del PRocess
                            var _idProcessSelected = _checkbox.id.substr(_checkbox.id.lastIndexOf('_') + 1, _checkbox.id.length);
                            //Los guiones son para delimitar y poder buscar bien al momento de filtrar realmente...
                            _strProcessSelected += '-' + _idProcessSelected + '-,';
                        }
                    }
                }
            }
            else {   //FireFox
            //Si es FireFox o GoogleChrome, los Nodes arrancan de 1 y no se accede con parentesis sino con corchete.... tambien hay que reemplazar unas comillas porque sino no encuentra el control!
                for (i = 1; i < document.getElementById('ctl00_ContentMain_divFilter').childNodes.length; i++) {
                    //El texto sale asi: "<INPUT id=ctl00_ContentMain_chkProcess_377 name=ctl00$ContentMain$chkProcess_377 value="" CHECKED type=checkbox><LABEL for=ctl00_ContentMain_chkProcess_377>Observatorio de la Ciudad Autónoma de Buenos Aires</LABEL>"	String
                    //Solo nos importa la primer parte ID=....
                    var _STARTPOSITION = 7; //donde empieza
                    //Con esto buscamos donde termina el primer espacio, para acceder al ID...
                    var _lenId = document.getElementById('ctl00_ContentMain_divFilter').childNodes[i].innerHTML.substr(_STARTPOSITION, 50).indexOf(' ');
                    var _idNameChk = document.getElementById('ctl00_ContentMain_divFilter').childNodes[i].innerHTML.substr(_STARTPOSITION, _lenId).split('=')[1];

                    //teniendo el ID, buscamos el control y vemos si esta Checked o no....
                    var _checkbox = document.getElementById(_idNameChk.replace('"', '').replace('"', '').replace('$', '_').replace('$', '_'));
                    if (_checkbox.checked) {
                        //Obtenemos el Id del PRocess
                        var _idProcessSelected = _checkbox.id.substr(_checkbox.id.lastIndexOf('_') + 1, _checkbox.id.length);
                        //Los guiones son para delimitar y poder buscar bien al momento de filtrar realmente...
                        _strProcessSelected += '-' + _idProcessSelected + '-,';
                    }
                }
            }

            //Al salir del ciclo le saco el ultimo caracter....
            _strProcessSelected = _strProcessSelected.substr(0, _strProcessSelected.length - 1);
        }
        function FilterProcess() {
            SetProcessSelected();
            
            InitMap();

            //Busca el popup
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorFilter');
            //muestra el popup
            modalPopupBehavior.hide();
        }
    </script>

    <script type="text/javascript">
        function OnClientDragEnd(dock) {
            var restrictionZone = $get('myMap');
            var restrBounds = $telerik.getBounds(restrictionZone);
            var dockBounds = $telerik.getBounds(dock.get_element());
            var inBounds = Telerik.Web.UI.ResizeExtender.containsBounds(restrBounds, dockBounds);

            if (!inBounds) {
                if (dockBounds.x < restrBounds.x) {
                    dock.set_left(restrBounds.x);
                }
                else if (restrBounds.x + restrBounds.width < dockBounds.x + dockBounds.width) {
                    dock.set_left(restrBounds.x + restrBounds.width - dockBounds.width);
                }

                if (dockBounds.y < restrBounds.y) {
                    dock.set_top(restrBounds.y);
                }
                else if (restrBounds.y + restrBounds.height < dockBounds.y + dockBounds.height) {
                    dock.set_top(restrBounds.y + restrBounds.height - dockBounds.height);
                }
            }
        }
    </script>

    <input id="inputPointProcess" type="hidden" value="<% =_PointLatLongProcess%>" />
    <input id="inputPoints" type="hidden" value="<% =_PointsLatLong%>" />
    <input id="MapType" type="hidden" value="<% =_MapType%>" />

    <div id='myMap'>
    </div>
    <div id='FilterMap'>
        <asp:Button ID="btnShowFilter" runat="server" Text="" OnClientClick="javascript:showPopUpFilter(this);" CssClass="btnShowFilter" />
    </div>
    <!--Abro Popup de Filter -->
    <asp:Panel ID="pnlPopUpFilter" runat="server">
        <asp:Button ID="btnHiddenFilter" runat="server" Visible="False" CausesValidation="False" />
        <ajaxToolkit:ConfirmButtonExtender ID="cbeFilter" runat="server" TargetControlID="btnHiddenFilter"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpeFilter" Enabled="False"
            ConfirmText="" />
        <ajaxToolkit:ModalPopupExtender ID="mpeFilter" runat="server" TargetControlID="btnHiddenFilter"
            BehaviorID="programmaticModalPopupBehaviorFilter" PopupControlID="pnlConfirmFilter"
            CancelControlID="btnCloseFilter" BackgroundCssClass="ModalPopUp" DynamicServicePath=""
            Enabled="True" />
        <div>
            <asp:Panel ID="pnlConfirmFilter" runat="server" Style="display: none" CssClass="pnlBgFilter">
                <asp:Label ID="lblTitleFilter" runat="server" Text="Filter" CssClass="lblTitleFilter"></asp:Label>
                <asp:Label ID="lblSubTitleFilter" runat="server" Text="Filter Process"
                    CssClass="lblSubTitleFilter"></asp:Label>
                <br />
                <div id="divFilter" runat="server" class="Check">
                </div>
                <asp:Button ID="btnFilter" runat="server" Text="Apply" OnClientClick="FilterProcess();"
                    CausesValidation="false" CssClass="btnFilterApply" />
                <asp:Button ID="btnCloseFilter" runat="server" Text="x" CssClass="btnFilterClose" />
            </asp:Panel>
        </div>
    </asp:Panel>
    <!-- Fin de Popup de Filter -->

</asp:Content>
