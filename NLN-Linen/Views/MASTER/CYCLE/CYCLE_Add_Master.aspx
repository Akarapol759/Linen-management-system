<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CYCLE_Add_Master.aspx.cs" Inherits="NLN_Linen.Views.MASTER.CYCLE.CYCLE_Add_Master" %>

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
                                <legend class="scheduler-border">Cycle Master : Create</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Business Unit : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLBusinessUnit" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Cycle Type : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLCycleType" runat="server" CssClass="form-control" Width="280px">
                                                        <asp:ListItem Text="Shelf Count" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Delivery" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Pickup" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="To Laundry" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Cycle Time : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DDLTime" runat="server" CssClass="form-control" Width="170px">
                                                        <asp:ListItem Text="01:00" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="02:00" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="03:00" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="04:00" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="05:00" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="06:00" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="07:00" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="08:00" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="09:00" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="10:00" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="11:00" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="12:00" Value="12"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLSymbol" runat="server" CssClass="form-control" Width="70px">
                                                        <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Symbol : " CssClass="col-md-5 control-label"></asp:Label>
                                                
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click" />
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
