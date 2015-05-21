<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Trace="false" AutoEventWireup="true" CodeBehind="Reservation.aspx.cs" Inherits="FlightWeb.Reservation" %>

<asp:Content ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("span[class*=glyphicon-plus]").click(function () {
                if ($(this).hasClass("glyphicon-plus")) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                } else {
                    $(this).closest("tr").next().remove();
                }
                $(this).toggleClass("glyphicon-minus").toggleClass("glyphicon-plus");

            });
        });
    </script>
    <div style="padding: 10px;">
        <asp:GridView ID="gvFlights" CssClass="table" runat="server" OnRowDataBound="gvFlights_OnRowDataBound" AutoGenerateColumns="False">
            <Columns>
                <%--<asp:BoundField DataField="ID" HeaderText="ID" />--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <span style="cursor: pointer" class="glyphicon glyphicon-plus"></span>
                        <%--<img alt="" style="cursor: pointer" src="images/plus.png" />--%>
                        <asp:Panel ID="pnlSeatReservations" runat="server" Style="display: none">
                            <asp:GridView ID="gvSeatReservations" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="Seat_ID" HeaderText="Seat ID" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="From" HeaderText="From" />
                <asp:BoundField DataField="To" HeaderText="To" />
                <asp:BoundField DataField="Plane" HeaderText="Plane" />
                <asp:BoundField DataField="DepartureTime" HeaderText="DepartureTime" DataFormatString="{0:g}" />
                <asp:BoundField DataField="ArrivalTime" HeaderText="ArrivalTime" DataFormatString="{0:g}" />
                <asp:BoundField DataField="TimeSpent" HeaderText="Time" DataFormatString="{0:mm\:ss}" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-4">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-3">
                                <p>Name:</p>
                            </div>
                            <div class="col-sm-9">
                                <p>
                                    <asp:Label runat="server" ID="lblName" />
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <p>Address:</p>
                            </div>
                            <div class="col-sm-9">
                                <p>
                                    <asp:Label runat="server" ID="lblAddress" />
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <p>PostalCode:</p>
                            </div>
                            <div class="col-sm-9">
                                <p>
                                    <asp:Label runat="server" ID="lblPostalCode" />
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <p>City:</p>
                            </div>
                            <div class="col-sm-9">
                                <p>
                                    <asp:Label runat="server" ID="lblCity" />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4 col-sm-offset-4">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-3">
                                <p>Total TravelTime:</p>
                            </div>
                            <div class="col-sm-9">
                                <p>
                                    <asp:Label runat="server" ID="lblTotalTravelTime" />
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <p>Total Price:</p>
                            </div>
                            <div class="col-sm-9">
                                <p>
                                    <asp:Label runat="server" ID="lblTotalPrice" />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick" CssClass="btn btn-default" />
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm booking" OnClick="btnConfirm_OnClick" CssClass="btn btn-primary" />
    </div>

</asp:Content>
