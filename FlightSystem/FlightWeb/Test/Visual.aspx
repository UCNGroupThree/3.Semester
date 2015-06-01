<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visual.aspx.cs" Inherits="FlightWeb.Test.Visual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-2.1.4.js"></script>
    <script
        src="http://maps.googleapis.com/maps/api/js">
    </script>
    <script>
        $(document).ready(function () {

            function makePath(map, data) {
                var split = data.split(";");
                var locations = [];
                for (var i = 0; i < split.length; i++) {
                    var lat = split[i].split(",")[0];
                    //console.log("lat: " + lat);
                    var lon = split[i].split(",")[1];
                    //console.log("lon: " + lon);
                    var l = new google.maps.LatLng(lat, lon);
                    //console.log("l: " + l);
                    locations.push(l);

                }
                //console.log(locations);

                var flightPath = new google.maps.Polyline({
                    path: locations,
                    strokeColor: "#0000FF",
                    strokeOpacity: 0.8,
                    strokeWeight: 2
                });

                flightPath.setMap(map);
            }

            function initialize() {
                
                var data = document.getElementById("HiddenData").value;


                var x = new google.maps.LatLng(56.0662608, 10.4440763);
                
                var mapProp = {
                    center: x,
                    zoom: 6,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

                if (data.indexOf(":") > -1) {
                    var fSplit = data.split(":");
                    for (var i = 0; i < fSplit.length; i++) {
                        makePath(map, fSplit[i]);
                    }
                } else {
                    makePath(map, data);
                }
            }

            google.maps.event.addDomListener(window, 'load', initialize);
        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <h4><asp:Label runat="server" ID="lblHeader"/></h4>
        
        <div id="googleMap" style="width: 700px; height: 500px;"></div>
        <asp:HiddenField ID="HiddenData" ClientIDMode="Static" runat="server" />
    </form>
</body>
</html>
