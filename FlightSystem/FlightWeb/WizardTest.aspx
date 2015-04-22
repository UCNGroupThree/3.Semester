<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WizardTest.aspx.cs" Inherits="FlightWeb.WizardTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/wizard-theme.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderWidth="1px" Height="100%" Width="100%">
        <WizardSteps>
            <asp:WizardStep runat="server" Title="Flight Search">
                <div style="padding: 20px">
                        <div class="form-group">
                            <asp:Label class="control-label col-sm-2" ID="lblCountryFrom" runat="server">From Country:</asp:Label>
                            <div class="col-sm-10 form">
                                <asp:DropDownList ID="ddlCountryFrom" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label class="control-label col-sm-2" ID="lblFrom" runat="server">From:</asp:Label>
                            <div class="col-sm-10 form">
                                <asp:DropDownList ID="ddlFrom" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label class="control-label col-sm-2" ID="lblCountryTo" runat="server">To Country:</asp:Label>
                            <div class="col-sm-10 form">
                                <asp:DropDownList ID="ddlCountryTo" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label class="control-label col-sm-2" ID="lblTo" runat="server">To:</asp:Label>
                            <div class="col-sm-10 form">
                                <asp:DropDownList ID="ddlTo" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
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
                    
                            <asp:Table ID="tblFoundFlights" CssClass="table" runat="server">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>From</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>To</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Depart</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Arrival</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Price</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                            <table class="table">
                                <tr>
                                    <td>Total TravelTime:</td>
                                    <td><asp:Label runat="server" ID="lblTravelTime">000</asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Total Price:</td>
                                    <td><asp:Label runat="server" ID="lblTotalPrice">000</asp:Label></td>
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



</asp:Content>
