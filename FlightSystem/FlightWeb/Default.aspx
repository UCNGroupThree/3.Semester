<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FlightWeb.Search" %>

<asp:Content ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            <%--var pageUrl = '<%=ResolveUrl("~/Search.aspx")%>';--%>
            $("#<%=btnSearch.ClientID%>").click(function () {
                $('#<%=modalHeaderText.ClientID%>').html("Searching");
                $('#modalBody').html("");
                $('#<%=modalFooter.ClientID%>').html("");
                $('#divModal').modal();

            });
            $('#<%=txtDepart.ClientID%>').datepicker({
                format: "dd-mm-yyyy 00:01",
                startDate: "<%=DateTime.Now.ToString("d")%>",
                orientation: "top auto",
                autoclose: true,
                todayHighlight: true
            });
            
        });
    </script>

    <div style="padding: 20px">
        <asp:UpdatePanel ID="UpdatePanelFrom" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group">
                    <span class="control-label col-sm-2">From Country:</span>
                    <div class="col-sm-10 form">
                        <asp:DropDownList ID="ddlCountryFrom" CssClass="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountryFrom_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" InitialValue="-1" ControlToValidate="ddlCountryFrom" ErrorMessage="Please Select a country"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <span class="control-label col-sm-2">From:</span>
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
                    <span class="control-label col-sm-2">To Country:</span>
                    <div class="col-sm-10 form">
                        <asp:DropDownList ID="ddlCountryTo" CssClass="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountryTo_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" InitialValue="-1" ControlToValidate="ddlCountryTo" ErrorMessage="Please Select a country"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <span class="control-label col-sm-2">To:</span>
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
            <span class="control-label col-sm-2">Departure time:</span>
            <div class="col-sm-10 form">
                <asp:TextBox CssClass="form-control" ID="txtDepart" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="ValidatorDepart" runat="server" ValidationGroup="FindFligtValidator" Display="Dynamic" SetFocusOnError="True" OnServerValidate="ValidatorDepart_OnServerValidate" ControlToValidate="txtDepart" ErrorMessage="Please make a valid dateformat, like: dd-MM-yyyy HH:mm"></asp:CustomValidator>
            </div>
        </div>
        <div class="form-group">
            <span class="control-label col-sm-2">Persons:</span>
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
                            <asp:Button ID="btnBook" runat="server" PostBackUrl="Reservation.aspx" OnClick="btnBook_OnClick" Text="Book Seats" CssClass="btn btn-primary"/>
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
