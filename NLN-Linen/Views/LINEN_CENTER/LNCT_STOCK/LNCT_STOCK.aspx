<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LNCT_STOCK.aspx.cs" Inherits="NLN_Linen.Views.LINEN_CENTER.LNCT_STOCK.LNCT_STOCK" %>

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
                                <legend class="scheduler-border">Criteria : Search</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-4">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Business Unit : " CssClass="col-md-4 control-label"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="DDLBusinessUnit" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-4">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Item Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSearchConsumtionNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-4">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Stock : " CssClass="col-md-4 control-label"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="DDLStock" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="All" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Available" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="Out of stock" Value="O"></asp:ListItem>
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
                                                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#addModal">
                                                    Add New Record
                                                </button>
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" PageSize="10" Width="100%"
                                            OnRowCommand="GridView_RowCommand" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"
                                            DataKeyNames="Id" OnSorting="GridViewInvoice_Sorting" CssClass="table table-striped table-bordered table-hover footable"
                                            OnPageIndexChanging="GridView_PageIndexChanging"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <%--<asp:ButtonField CommandName="detail" ControlStyle-CssClass="btn btn-info"
                                                    ButtonType="Button" Text="Detail" HeaderText="Detail">
                                                    <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField CommandName="editRecord"
                                                    ButtonType="Image" HeaderText="Edit" ImageUrl="~/Images/edit-icon.png" ControlStyle-Height="25px"></asp:ButtonField>
                                                <asp:ButtonField CommandName="cancelRecord"
                                                    ButtonType="Image" HeaderText="CN" ImageUrl="~/Images/delete-icon.png" ItemStyle-HorizontalAlign="Center" ControlStyle-Height="25px" ControlStyle-Width="25px"></asp:ButtonField>--%>
                                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false"/>
                                                <asp:BoundField DataField="BU_FULL_NAME" HeaderText="BU" SortExpression="BU_FULL_NAME" />
                                                <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" SortExpression="ITEM_CODE" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM NAME" SortExpression="ITEM_NAME"/>
                                                <asp:BoundField DataField="LNCT_STOCK_QTY" HeaderText="ITEM STOCK" SortExpression="LNCT_STOCK_QTY"/>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

