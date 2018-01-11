<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SOILED_Detail_Request.aspx.cs" Inherits="NLN_Linen.Views.SOILED.SOILED_Detail_Request" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="shadowBox">
                <div class="page-container">
                    <div class="container">
                        <div>
                            <br />
                        </div>
                        <div>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Soiled Request : Detail</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Business Unit : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblBusinessUnit" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Visible="false">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Laundry Name : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblLaundry" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Pickup Cycle Time : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblCyclePickup" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Laundry Cycle Time : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblCycleToLaundry" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Soiled Request No. : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblSOILEDReqNo" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Status" CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdSOILEDStatus" runat="server" RepeatDirection="Horizontal" Width="300px" Enabled="false">
                                                        <asp:ListItem Text="Complete" Value="C" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <%--<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                                &nbsp;--%>
                                                <asp:Button ID="btnTopImgPrint" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btnTopImgPrint_Click" OnClientClick="target = '_blank';"/>
                                                <%--<asp:ImageButton ID="btnTopImgPrint" runat="server" ImageUrl="~/Images/print.png" OnClick="btnTopImgPrint_Click" Height="40px" />--%>
                                                &nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div>
                        <hr />
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" PageSize="10" Width="100%"
                                            AutoGenerateColumns="false"
                                            DataKeyNames="ID" CssClass="table table-striped table-bordered table-hover footable"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No." ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CUS_NAME" HeaderText="Department Name" />
                                                <asp:BoundField DataField="SOILED_REQUEST_TYPE_NAME" HeaderText="Type Bag" />
                                                <asp:BoundField DataField="SOILED_REQUEST_WEIGHT" HeaderText="Weight" />
                                                <asp:BoundField DataField="SOILED_REQUEST_QTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="updProgressMain" AssociatedUpdatePanelID="UpdatePanel" runat="server">
                                    <ProgressTemplate>
                                        <div style="text-align: center;">
                                            <img style="position: relative; top: 50%;" src="~/Images/loader.gif" runat="server" alt="Loading" height="40" width="40" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                    <div>
                        <hr />
                    </div>
                    <!--Print Invoice-->
                    <div id="printSoiledModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="printModalLabel" aria-hidden="true" style="width: 1070px; top: 5%; left: 30%;">
                        <%--<div class="modal-dialog">--%>
                        <div class="modal-content" style="width: 80%;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <h3 id="printModalLabel">Print Soiled List</h3>

                            </div>
                            <div class="modal-body" style="overflow: auto;">
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BackColor="#CCCCFF"
                                    EnableTheming="True" AsyncRendering="False" SizeToReportContent="True">
                                </rsweb:ReportViewer>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                        <%--</div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
