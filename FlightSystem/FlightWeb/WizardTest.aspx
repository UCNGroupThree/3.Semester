<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WizardTest.aspx.cs" Inherits="FlightWeb.WizardTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/wizard-theme.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderWidth="1px" Height="100%" Width="100%" OnNextButtonClick="Wizard1_NextButtonClick">
                <WizardSteps>
                    <asp:WizardStep runat="server" Title="Flight Search">
                        <div style="padding: 20px">
                            <asp:UpdatePanel ID="UpdatePanelFrom" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label class="control-label col-sm-2" ID="lblCountryFrom" runat="server">From Country:</asp:Label>
                                        <div class="col-sm-10 form">
                                            <asp:DropDownList ID="ddlCountryFrom" class="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountryFrom_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label class="control-label col-sm-2" ID="lblFrom" runat="server">From:</asp:Label>
                                        <div class="col-sm-10 form">
                                            <asp:DropDownList ID="ddlFrom" Enabled="False" CssClass="form-control" runat="server" DataTextField="City" DataValueField="ID">
                                                <asp:ListItem Value="-1" Text="--- Select Country first ---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdatePanelTo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label class="control-label col-sm-2" ID="lblCountryTo" runat="server">To Country:</asp:Label>
                                        <div class="col-sm-10 form">
                                            <asp:DropDownList ID="ddlCountryTo" class="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountryTo_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label class="control-label col-sm-2" ID="lblTo" runat="server">To:</asp:Label>
                                        <div class="col-sm-10 form">
                                            <asp:DropDownList ID="ddlTo" Enabled="False" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="-1" Text="--- Select Country first ---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <asp:Label class="control-label col-sm-2" ID="lblDepart" runat="server">Depart:</asp:Label>
                                <div class="col-sm-10 form">
                                    <asp:TextBox class="form-control" ID="txtDepart" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calDepart" TargetControlID="txtDepart" Format="dd-MM-yyyy HH:mm" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label class="control-label col-sm-2" ID="lblPersons" runat="server">Persons:</asp:Label>
                                <div class="col-sm-10 form">
                                    <asp:DropDownList ID="ddlPersons" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Flight Selection">
                        <div style="padding: 10px">

                            <asp:GridView ID="GridViewFlights" CssClass="table" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <%--<asp:BoundField DataField="ID" HeaderText="ID" />--%>
                                    <asp:BoundField DataField="From" HeaderText="From" />
                                    <asp:BoundField DataField="To" HeaderText="To" />
                                    <asp:BoundField DataField="Plane" HeaderText="Plane" />
                                    <asp:BoundField DataField="DepartureTime" HeaderText="DepartureTime" DataFormatString="{0:g}" />
                                    <asp:BoundField DataField="ArrivalTime" HeaderText="ArrivalTime" DataFormatString="{0:g}" />
                                    <asp:BoundField DataField="TimeSpent" HeaderText="Time" DataFormatString="{0:mm\:ss}" />
                                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                                </Columns>
                            </asp:GridView>
                            <table class="table">
                                <tr>
                                    <td>Total TravelTime:</td>
                                    <td><asp:Label runat="server" ID="lblTravelTime">000</asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Total Price:</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTotalPrice">000</asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Seat Selection">
                        <div style="padding: 10px;">
                            SeatSelection
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Billing">
                        <div style="padding: 10px;">
                            Payment
                        </div>
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>

        </ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="Wizard1$ddlFrom" EventName="SelectedIndexChanged"/>
        </Triggers>--%>
    </asp:UpdatePanel>

    <%--<script type="text/javascript">
        
        $(document).ready(function () {
            var pageUrl = '<%=ResolveUrl("~/WizardTest.aspx")%>';

            function populate(list, control) {
                if (list.length > 0) {
                    $(control).removeAttr("disabled");
                    $(control).empty().append('<option selected="selected" value="-1">--- Please select ---</option>');
                    $.each(list, function (key, value) {
                        $(control).append($("<option></option>").val(value.Value).html(value.Text));
                    });
                }
                else {
                    control.empty().append('<option selected="selected" value="-1">No Airports<option>');
                }
            }

            function makeAirportBox(sender, returnTo) {
                var country = $(sender).val();
                $(returnTo).attr("disabled", "disabled");
                if (country === "-1") {
                    $(returnTo).empty().append('<option selected="selected" value="-1">--- Select country first ---</option>');
                } else {
                    $(returnTo).empty().append('<option selected="selected" value="-1">Loading...</option>');
                    $.ajax({
                        url: pageUrl + "/GetAirportsFromCountry",
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify({ country: country }),
                        dataType: "json",
                        success: function (result) {
                            populate(result.d, returnTo);
                        },
                        error: function (xhr, status) {
                            console.log("An error occurred: " + status);
                            console.log(xhr.status);
                            console.log(xhr.responseText);
                        }

                    });
                }
            }

            $("#<%=ddlCountryFrom.ClientID%>").change(function () {
                makeAirportBox(this, "#<%=ddlFrom.ClientID%>");
            });

            $(<%=ddlCountryTo.ClientID%>).change(function () {
                makeAirportBox(this, "#<%=ddlTo.ClientID%>");
            });
        });
        
    </script>--%>
</asp:Content>
