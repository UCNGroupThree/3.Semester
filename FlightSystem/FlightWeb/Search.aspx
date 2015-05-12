﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="FlightWeb.Search" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=15.1.1.100, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            <%--var pageUrl = '<%=ResolveUrl("~/Search.aspx")%>';--%>
            $("#<%=btnSearch.ClientID%>").click(function () {
                //console.log("This happen?");
                $('#<%=modalHeaderText.ClientID%>').html("Searching");
                $('#modalBody').html("");
                $('#<%=modalFooter.ClientID%>').html("");
                $('#divModal').modal();
            });
            //BootstrapDialog.alert('I want banana!');
        });
    </script>

    <div style="padding: 20px">
        <asp:UpdatePanel ID="UpdatePanelFrom" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group">
                    <asp:Label CssClass="control-label col-sm-2" ID="lblCountryFrom" runat="server">From Country:</asp:Label>
                    <div class="col-sm-10 form">
                        <asp:DropDownList ID="ddlCountryFrom" CssClass="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountryFrom_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="FindFligtValidator" Display="Static" SetFocusOnError="True" InitialValue="-1" ControlToValidate="ddlCountryFrom" ErrorMessage="Please Select a country"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="control-label col-sm-2" ID="lblFrom" runat="server">From:</asp:Label>
                    <div class="col-sm-10 form">
                        <asp:DropDownList ID="ddlFrom" Enabled="False" CssClass="form-control" runat="server" DataTextField="City" DataValueField="ID">
                            <asp:ListItem Value="-1" Text="--- Select Country first ---"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CustomValidator ID="ValidatorFrom" runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" OnServerValidate="IntValidator_OnServerValidate" ControlToValidate="ddlFrom" ErrorMessage="Please Select an airport"></asp:CustomValidator>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelTo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group">
                    <asp:Label CssClass="control-label col-sm-2" ID="lblCountryTo" runat="server">To Country:</asp:Label>
                    <div class="col-sm-10 form">
                        <asp:DropDownList ID="ddlCountryTo" CssClass="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountryTo_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" InitialValue="-1" ControlToValidate="ddlCountryTo" ErrorMessage="Please Select a country"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="control-label col-sm-2" ID="lblTo" runat="server">To:</asp:Label>
                    <div class="col-sm-10 form">
                        <asp:DropDownList ID="ddlTo" Enabled="False" CssClass="form-control" runat="server">
                            <asp:ListItem Value="-1" Text="--- Select Country first ---"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CustomValidator ID="ValidatorTo" runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" OnServerValidate="IntValidator_OnServerValidate" ControlToValidate="ddlTo" ErrorMessage="Please Select an airport"></asp:CustomValidator>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-2" ID="lblDepart" runat="server">Departure time:</asp:Label>
            <div class="col-sm-10 form">
                <asp:TextBox CssClass="form-control" ID="txtDepart" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="ValidatorDepart" runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" OnServerValidate="ValidatorDepart_OnServerValidate" ControlToValidate="txtDepart" ErrorMessage="Please make a valid dateformat, like: dd-MM-yyyy HH:mm"></asp:CustomValidator>
                <cc1:CalendarExtender ID="calDepart" TargetControlID="txtDepart" Format="dd-MM-yyyy HH:mm" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-2" ID="lblPersons" runat="server">Persons:</asp:Label>
            <div class="col-sm-10 form">
                <asp:DropDownList ID="ddlPersons" CssClass="form-control" runat="server">
                    <asp:ListItem Selected="True" Value="1" />
                    <asp:ListItem Value="2" />
                    <asp:ListItem Value="3" />
                    <asp:ListItem Value="4" />
                    <asp:ListItem Value="5" />
                    <asp:ListItem Value="6" />
                    <asp:ListItem Value="7" />
                    <asp:ListItem Value="8" />
                    <asp:ListItem Value="9" />
                    <asp:ListItem Value="10" />
                </asp:DropDownList>
            </div>
        </div>
        <%--<button type="button" id="btnSearch" class="btn btn-primary">Search</button>--%>
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_OnClick" UseSubmitBehavior="False" CssClass="btn btn-default" Text="Search" />
    </div>


    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="divModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanelAnswer" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content" runat="server">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" runat="server" id="modalHeaderText"></h4>
                        </div>
                        <div class="modal-body">

                            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" DynamicLayout="true" runat="server">
                                <ProgressTemplate>
                                    <asp:Image runat="server" ImageUrl="~/Content/Img/loader.gif" />
                                    Please wait, while we are looking for a flight..
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div id="modalBody">
                                <asp:Label ID="lblModalError" Enabled="False" runat="server"></asp:Label>
                                <asp:Panel ID="modalFlightsPanel" Enabled="True" runat="server">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <p>From:</p>
                                            </div>
                                            <div class="col-sm-9">
                                                <p>
                                                    <asp:Label runat="server" ID="lblModalFrom" />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <p>To:</p>
                                            </div>
                                            <div class="col-sm-9">
                                                <p>
                                                    <asp:Label runat="server" ID="lblModalTo" />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <p>Stops:</p>
                                            </div>
                                            <div class="col-sm-9">
                                                <p>
                                                    <asp:Label runat="server" ID="lblModalStops" />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <p>TravelTime:</p>
                                            </div>
                                            <div class="col-sm-9">
                                                <p>
                                                    <asp:Label runat="server" ID="lblModalTravelTime" />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <p>Price:</p>
                                            </div>
                                            <div class="col-sm-9">
                                                <p>
                                                    <asp:Label runat="server" ID="lblModalPrice" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                        <asp:Panel ID="modalFooter" runat="server" CssClass="modal-footer">
                            <asp:Button ID="btnClose" runat="server" Text="Close" data-dismiss="modal" aria-hidden="true" CssClass="btn btn-default"/>
                            <asp:Button ID="btnBook" runat="server" PostBackUrl="Reservation.aspx" Text="Book Seats" CssClass="btn btn-primary"/>
                        </asp:Panel>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>

            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>