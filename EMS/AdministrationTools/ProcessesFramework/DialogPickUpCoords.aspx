<%@ Page Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="DialogPickUpCoords.aspx.cs" Inherits="Condesus.EMS.WebUI.PF.DialogPickUpCoords"
    Title="Pick Up Coordinates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPopUp" runat="server">
    <style type="text/css">
        v\:*
        {
            behavior: url(#default#VML);
        }
    </style>

    <script src="../../AppCode/JavaScriptCore/markerclusterer_packed.js" type="text/javascript"></script>

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
        //Event onload
        //window.attachEvent('onload', load);
        //window.attachEvent('onunload', GUnload);
        if (_BrowserName == _IEXPLORER) {   //IE and Opera
            window.attachEvent('onload', load);
            window.attachEvent('onunload', GUnload);
        }
        else {   //FireFox
            document.addEventListener('DOMContentLoaded', load, false);
            document.addEventListener('onunload', GUnload, false);
        }

        //<![CDATA[
        var map;
        var polyShape;
        var polygonMode;
        var polygonDepth = "20";
        var polyPoints = [];
        var marker;
        var geocoder = null;

        var fillColor = "#0000FF"; // blue fill
        var lineColor = "#000000"; // black line
        var opacity = .5;
        var lineWeight = 2;

        var kmlFillColor = "7dff0000"; // half-opaque blue fill

        function load() {
            if (GBrowserIsCompatible()) {
                map = new GMap2(document.getElementById("map"));
                map.setCenter(new GLatLng(0, 0), 1);
                //                map.setCenter(new GLatLng(37.4224, -122.0838), 13);
                map.addControl(new GLargeMapControl());
                map.addControl(new GMapTypeControl());
                //                GEvent.addListener(map, 'click', mapClick);
                //Le hago el fix al evento click cuando se hace click sobre un overlay ya existente (para el caso de un poligono si hace click sobre una zona marcada...)
                GEvent.addListener(map, "click", function(overlay, latlng, overlaylatlng) {
                    if (overlay == null) {
                        mapClick(null, latlng);
                    }
                    else {
                        //Caso contrario, es un punto sobre un area ya seleccionada en un poligono por ejemplo.
                        mapClick(null, overlaylatlng);
                    }
                });

                geocoder = new GClientGeocoder();

                //Si hay datos cargados en el inputPoints, quiere decir que se debe mostrar los datos en el mapa.
                if (document.getElementById('hdnPointsLatLong').value != "") {
                    GetGoogleMap();
                }
            }
        }
        //Este metodo, permite setear el tipo de seleccion de coordenadas
        //utilizadas. puntos, poligono o polilinea
        function SetDrawModeType() {
            var _drawModeType = "Pushpin";

            if (document.getElementById("drawMode_point").checked) {
                _drawModeType = "Pushpin";
            }
            else {
                if (document.getElementById("drawMode_polyline").checked) {
                    _drawModeType = "Polyline";
                }
                else {
                    if (document.getElementById("drawMode_polygon").checked) {
                        _drawModeType = "Polygon";
                    }
                }
            }
            document.getElementById('ctl00_ContentPopUp_drawModeType').value = _drawModeType;
        }
        // mapClick - Handles the event of a user clicking anywhere on the map
        // Adds a new point to the map and draws either a new line from the last point
        // or a new polygon section depending on the drawing mode.
        function mapClick(marker, clickedPoint) {
            //Setea en una variable oculta el tipo de coordenadas. (punto, poligono o polilinea)
            SetDrawModeType();
            polygonMode = document.getElementById("drawMode_polygon").checked;

            //Si esta seleccionado un punto, limpia y carga...
            if (document.getElementById("drawMode_point").checked) {
                //entonces limpia el ultimo punto...
                if (polyPoints.length >= 1) {
                    //Si esta en null, no hay nada para limpiar...
                    if (polyShape != null) {
                        map.removeOverlay(polyShape);
                        // pop last element of polypoint array
                        polyPoints.pop();
                    }
                }
            }
            //Si viene mal el punto de seleccion, no hace nada...
            if (clickedPoint != 'undefined') {
                // Push onto polypoints of existing polygon
                polyPoints.push(clickedPoint);
                drawCoordinates();
            }
        }
        // Clear current Map
        function clearMap() {
            map.clearOverlays();
            polyPoints = [];
        }
        // Toggle from Polygon PolyLine mode
        function toggleDrawMode() {
            //Limpia todo cuando se cambia el modo...
            clearMap();
            //map.clearOverlays(); //no es necesario lo hace el clearmap...
            polyShape = null;
            polygonMode = document.getElementById("drawMode_polygon").checked;
            drawCoordinates();
        }
        // Delete last Point
        // This function removes the last point from the Polyline/Polygon and redraws
        // map.
        function deleteLastPoint() {
            //Si esta en null, no hay nada para limpiar...
            if (polyShape != null) {
                map.removeOverlay(polyShape);

                // pop last element of polypoint array
                polyPoints.pop();
                drawCoordinates();
            }
        }
        // drawCoordinates
        function drawCoordinates() {
            //Re-create Polyline/Polygon
            if (polygonMode) {
                polyShape = new GPolygon(polyPoints, lineColor, lineWeight, opacity, fillColor, opacity);
            } else {
                polyShape = new GPolyline(polyPoints, lineColor, lineWeight, opacity);
            }
            map.clearOverlays();

            //Si el indice es menor a cero, no se puede pedir, porque da error...
            if ((polyPoints.length - 1) >= 0) {
                // Grab last point of polyPoints to add marker
                marker = new GMarker(polyPoints[polyPoints.length - 1]);
                map.addOverlay(marker);
                map.addOverlay(polyShape);
                //logCoordinates();
                SaveLatLong();
            }
        }
        function SaveLatLong() {
            //Limpia la variable
            document.getElementById('ctl00_ContentPopUp_inputPoints').value = "";

            // check mode
            if (polygonMode) {
                //Debe tener info, sino no hace nada
                if (polyPoints.length > 0) {
                    // loop to print coords
                    for (var i = 0; i < (polyPoints.length); i++) {
                        var lat = polyPoints[i].lat();
                        var longi = polyPoints[i].lng();
                        document.getElementById('ctl00_ContentPopUp_inputPoints').value += lat + "|" + longi + ";";
                    }
                }
            } else {
                // loop to print coords 
                for (var i = 0; i < (polyPoints.length); i++) {
                    var lat = polyPoints[i].lat();
                    var longi = polyPoints[i].lng();
                    document.getElementById('ctl00_ContentPopUp_inputPoints').value += lat + "|" + longi + ";";
                }
            }
        }
        function showAddress() {
            var address = document.getElementById("address").value;
            if (geocoder) {
                geocoder.getLatLng(address,
                         function(point) {
                             if (!point) {
                                 alert(address + " not found");
                             } else {
                                 clearMap();
                                 map.setCenter(point, 13);
                             }
                         }
                       );
            }
        }


        //]]>
    </script>

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function returnToParent() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get the coords
            var _inputCoordsField = document.getElementById('ctl00_ContentPopUp_inputPoints').value;
            var _drawModeTypeField = document.getElementById('ctl00_ContentPopUp_drawModeType').value;
            oArg.inputCoords = _inputCoordsField;
            oArg.drawModeType = _drawModeTypeField;
            oArg.NoChange = false;

            //get a reference to the RadWindow
            var oWnd = GetRadWindow();

            //set the argument to the RadWindow 
            oWnd.argument = oArg;
            //close the RadWindow
            oWnd.close();
        }
        function returnWithoutChange() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();
            oArg.NoChange = true;

            //get a reference to the RadWindow
            var oWnd = GetRadWindow();

            //set the argument to the RadWindow 
            oWnd.argument = oArg;
            //close the RadWindow
            oWnd.close();
        }
    </script>

    <%----------------LOAD MAP------------------------%>

    <script type="text/javascript">
        //Global Const/Variable
        //CONSTANT
        var _EARTHRADIUS = 6371;
        var _POLYGON_SHAPE_TYPE = "Polygon";
        var _POLYLINE_SHAPE_TYPE = "Polyline";
        var _PUSHPIN_SHAPE_TYPE = "Pushpin";
        var _PROCESS_OBJECT_NAME = "processgroupprocess";
        var _MEASUREMENT_OBJECT_NAME = "measurement";
        var _FACILITY_OBJECT_NAME = "geographicareafacility";
        var _DEVICE_OBJECT_NAME = "measurementdevice";
        var _CALCULATE_OBJECT_NAME = "calculate";
        var _MAP_TYPE_GOOGLE = "googlemap";
        var _MAP_TYPE_VIRTUAL_EARTH = "virtualearth";

        //VARIABLES
        var _Points = null;
        //var _Map = null;
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
        var _ZoomLevel = 1;
        //Variables para la visualizacion (filtros de la pagina)
        var _ChkProcess;
        var _ChkMeasurement;
        var _ChkFacility;
        var _ChkDevice;
        var _ChkCalculate;
        //Variables para el click derecho sobre el pushpin..
        var _Hide;
        var _CurrentShape = null;
        var _IsPanning = false;

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
        function GetGoogleMap() {
            var _zoom = 20;
            var _size = -1;
            var _style = 0;

            if (GBrowserIsCompatible()) {
                //Carga los puntos pasados desde el CodeBehind.
                _Points = document.getElementById('hdnPointsLatLong').value;
                _Markers = new Array(0);
                if (_MarkerCluster != null) {
                    _MarkerCluster.clearMarkers();
                }
                
                //Obtiene y setea todos los puntos
                SetPoint(_Points);
                //Calcula el centro del mapa y zoom.
                CalculateCenterAndZoom();
                //Ahora setea el centro y el zoom
                map.setCenter(GetLatLongPoint(_CenterLatitudeLongitude.lat(), _CenterLatitudeLongitude.lng()), _ZoomLevel);

                _zoom = _zoom == -1 ? null : _zoom;
                _size = _size == -1 ? null : _size;
                _style = _style == "-1" ? null : parseInt(_style, 10);

                //Finalmente agrega todos los puntos que corresponden como cluster y el resto en forma normal.
                _MarkerCluster = new MarkerClusterer(map, _Markers);
            }
        }

    </script>

    <script type="text/javascript">
        //FUNCIONES GENERICAS
        function GetLatLongPoint(latitude, longitude) {
            return new GLatLng(latitude, longitude);
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
            var mapWidth = parseFloat(document.getElementById('map').clientWidth);
            var mapHeight = parseFloat(document.getElementById('map').clientHeight);

//            var mapWidth = parseFloat(document.getElementById('map').style.width);
//            var mapHeight = parseFloat(document.getElementById('map').style.height);
            
            
            
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

            //Si es el unico punto, entonces pone un zoom grande.
            if (dLat == 0 && dLon == 0) {
                return 1;
            }
            else {
                return d;
            }
        }
        function SetCenterOfPolygon(pointsOfPolygon) {
            var _minLat = 0;
            var _maxLat = 0;
            var _minLong = 0;
            var _maxLong = 0;

            //Recorre todos los puntos del poligono y busca el mayor-menor para lat-long.
            for (var x = 0; x < pointsOfPolygon.length - 1; x = x + 1) {
                //Obtiene el mayor y menor latitude
                _maxLat = GetMax(pointsOfPolygon[x].lat(), _maxLat);
                _minLat = GetMin(pointsOfPolygon[x].lat(), _minLat);
                //Obtiene el mayor y menor longitude
                _maxLong = GetMax(pointsOfPolygon[x].lng(), _maxLong);
                _minLong = GetMin(pointsOfPolygon[x].lng(), _minLong);
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
            
            //Hace la seleccion del tipo de marca (Poligono, punto o linea)
            switch (_shapeType) {
                case "Pushpin":
                    document.getElementById("drawMode_point").checked = true;
                    break;
                case "Polygon":
                    document.getElementById("drawMode_polygon").checked = true;
                    break;
                case "Polyline":
                    document.getElementById("drawMode_polyline").checked = true;
                    break;
            }
            for (var i = 0; i < _record.length - 1; i = i + 1) {
                _shapeObject = _record[i].split("|")[0]; //El primer campo siempre viene el Objeto (ClassName)
                _shapeIdEntity = _record[i].split("|")[1]; //El segundo campo siempre viene el Id del Objeto (IdEntity)
                _shapeType = _record[i].split("|")[2]; //El tercer campo siempre viene el tipo (Poligono, punto, linea)

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
                }
                //Ahora arma el Shape con el type y el array de puntos.
                AddPoint(_shapeType, _arrayVELatLong);
            }
        }
        function SetAttributeAndAddShape(shapeType, pointLatLong) {
            if (shapeType != "Pushpin") {
                //Si es puspin No se usa, ya que lo carga como cluster....con esto MarkerClusterer()
                map.addOverlay(_Shape);
            }
            else {
                if (pointLatLong.length == 1) {
                    var marker = new GMarker(new GLatLng(pointLatLong[0].lat(), pointLatLong[0].lng()));
                }
                else {
                    var marker = new GMarker(SetCenterOfPolygon(pointLatLong));
                }
                //Se guarda en el array de puntos para armar el cluster...
                _Markers.push(marker);
            }
        }
        function GetShape(shapeType, pointLatLong) {
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
        }
        function AddPoint(shapeType, pointLatLong) {
            //Construye el _Shape
            GetShape(shapeType, pointLatLong);
            //Setea los atributos para el shape y lo agrega al layer.
            SetAttributeAndAddShape(shapeType, pointLatLong);

            //Analiza al Poligono.
            //Si es un Pushpin, no setea el line
            if (shapeType == _POLYGON_SHAPE_TYPE) {
                //Construye el punto en el centro del poligono.
                GetShape(_PUSHPIN_SHAPE_TYPE, SetCenterOfPolygon(pointLatLong));
                SetAttributeAndAddShape(_PUSHPIN_SHAPE_TYPE, pointLatLong);
            }
        }
    </script>

    <div style="padding: 10px 10px 0px 10px;">
        <table id="tblContentForm" runat="server" class="ContentFormPickUpCoordinates" cellpadding="0"
            cellspacing="0">
            <!-- Parent -->
            <tr>
                <td class="tdInfo">
                    <asp:Label ID="lblSearchTitle" runat="server" Text="Search for location (by city, state, country)"
                        CssClass="labelTitleSearch"></asp:Label>
                    <input type="text" name="address" id="address" value="" class="inputSearch" />
                    <asp:Button ID="btnSearch" runat="server" Text="" OnClientClick="showAddress(); return false"
                        CssClass="buttonSearch" />
                </td>
            </tr>
            <tr>
                <td class="tdMap">
                    <div id="map" class="divMap">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="tdInfo">
                    <div style="float: left">
                        <asp:Label ID="lblDrawMode" runat="server" Text="Draw Mode:" CssClass="labelTitleDrawMode"></asp:Label>
                        <input type="radio" name="drawMode" id="drawMode_point" value="point" onclick="toggleDrawMode();"
                            checked="checked" />
                        <asp:Label ID="lblPoint" runat="server" Text="Point" CssClass="labelDrawMode"></asp:Label>
                        <input type="radio" name="drawMode" id="drawMode_polyline" value="polyline" onclick="toggleDrawMode();" />
                        <asp:Label ID="lblPolyline" runat="server" Text="PolyLine" CssClass="labelDrawMode"></asp:Label>
                        <input type="radio" name="drawMode" id="drawMode_polygon" value="polygon" onclick="toggleDrawMode();" />
                        <asp:Label ID="lblPolygon" runat="server" Text="Polygon" CssClass="labelDrawMode"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnDeleteLastPoint" runat="server" Text="Delete Last Point" OnClientClick="deleteLastPoint(); return false"
                            class="buttondeleteLastPoint" />
                        <asp:Button ID="btnClearMap" runat="server" Text="Clear Map" OnClientClick="clearMap(); return false"
                            class="buttonClearMap" />
                    </div>
                    <div style="float: right">
                        <input id="hdnPointsLatLong" type="hidden" value="<% =_PointsLatLong%>" />
                        <input id="inputPoints" name="inputPoints" type="hidden" runat="server" />
                        <input id="drawModeType" name="drawModeType" type="hidden" runat="server" />
                        <asp:Button ID="btnCancel" runat="server" Text="" OnClientClick="returnWithoutChange(); return false;"
                            CausesValidation="False" class="buttonCancel" />
                        <asp:Button ID="btnConfirm" runat="server" OnClientClick="returnToParent(); return false;"
                            CausesValidation="False" Text="" class="buttonSave" />

                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
