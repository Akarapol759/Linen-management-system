﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LCS_Request.aspx.cs" Inherits="NLN_Linen.Views.LCS.LCS_Request" %>

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
                                <legend class="scheduler-border">LCS Criteria : Search</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Business Unit : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLBusinessUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLBusinessUnit_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Department Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLCustomer" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="LCS Request No. : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSearchLCSRequestNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Status : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLLCSStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Complete" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O"></asp:ListItem>
                                                        <asp:ListItem Text="Cancel" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnRequest" runat="server" Text="New" CssClass="btn btn-info" OnClick="btnRequest_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />
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
                                            OnRowCommand="GridView_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                                            DataKeyNames="Id" CssClass="table table-striped table-bordered table-hover footable"
                                            OnPageIndexChanging="GridView_PageIndexChanging"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:ButtonField CommandName="detailRecord"
                                                    ButtonType="Image" HeaderText="Detail" ImageUrl="~/Images/edit-icon.png" ControlStyle-Height="25px"></asp:ButtonField>
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                <asp:BoundField DataField="BU_SHORT_NAME" HeaderText="Business Unit" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="LCS_REQUEST_NO" HeaderText="LCS Request No." />
                                                <asp:BoundField DataField="CUS_NAME" HeaderText="Department Name" />
                                                <asp:BoundField DataField="CYCLE_TIME_SHELF_COUNT" HeaderText="Shelft Count Time" />
                                                <asp:BoundField DataField="CREATE_DATE" HeaderText="LCS Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="LCS_REQUEST_STATUS_NAME" HeaderText="Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
<%--                                <asp:UpdateProgress ID="updProgressMain" AssociatedUpdatePanelID="UpdatePanel" runat="server">
                                    <ProgressTemplate>
                                        <div style="text-align: center;">
                                            <img style="position: relative; top: 50%;" src="~/Images/loader.gif" runat="server" alt="Loading" height="40" width="40" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
