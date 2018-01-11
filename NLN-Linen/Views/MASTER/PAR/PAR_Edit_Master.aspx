<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PAR_Edit_Master.aspx.cs" Inherits="NLN_Linen.Views.MASTER.PAR.PAR_Edit_Master" %>

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
                                <legend class="scheduler-border">Par Master : Edit</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Business Unit : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblBusinessUnit" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                    <asp:Label ID="lblCusId" runat="server" Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="lblBuCode" runat="server" Visible="false">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Department Code : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblCusCode" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Department Name : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblCusName" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Status" CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdStatus" runat="server" RepeatDirection="Horizontal" Width="300px">
                                                        <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
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
                                                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#addModal">
                                                    New Item
                                                </button>
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
                                            AutoGenerateColumns="false" OnRowCommand="GridView_RowCommand"
                                            DataKeyNames="PAR_ID" CssClass="table table-striped table-bordered table-hover footable"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:ButtonField CommandName="editRecord"
                                                    ButtonType="Image" HeaderText="Edit" ImageUrl="~/Images/edit-icon.png" ControlStyle-Height="25px"></asp:ButtonField>
                                                <asp:TemplateField HeaderText="No." ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                                                <asp:BoundField DataField="PAR_QTY" HeaderText="Part Qty" />
                                                <asp:BoundField DataField="PAR_STATUS_NAME" HeaderText="Status" />
                                                <asp:BoundField DataField="PAR_ID" HeaderText="PAR ID" Visible="false"/>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="updProgressMain" AssociatedUpdatePanelID="UpdatePanel" runat="server">
                                    <ProgressTemplate>
                                        <div style="text-align: center; removed: absolute;">
                                            <img style="position: relative; top: 50%;" src="Images/loader.gif" alt="Loading" height="100px" width="100px" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                    <!--Add Record Modal Start here-->
                    <div id="addModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="addModalLabel" aria-hidden="true" style="width: 700px; top: 5%; left: 30%;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal"
                                    aria-hidden="true">
                                    ×</button>
                                <h3 id="addModalLabel">New Item</h3>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="upAdd" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td width="160px">Item Name :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDLAddItem" runat="server" CssClass="form-control" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="DDLAddItem_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="lblDup" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Par Qty :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddParQty" runat="server" CssClass="form-control" Width="280px"></asp:TextBox>
                                                        <asp:CompareValidator ControlToValidate="txtAddParQty" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <hr />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnAddRecord" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnAddRecord_Click" />
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </div>
                    <!--Add Record Modal Ends here-->
                    <!--Edit Record Modal Start here-->
                    <div id="editModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true" style="width: 700px; top: 5%; left: 30%;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal"
                                    aria-hidden="true">
                                    ×</button>
                                <h3 id="editModalLabel">New Item</h3>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td width="160px">Item Name :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEditParId" runat="server" Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="DDLEditItem" runat="server" CssClass="form-control" Width="280px" Enabled="false"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Par Qty :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEditParQty" runat="server" CssClass="form-control" Width="280px"></asp:TextBox>
                                                        <asp:CompareValidator ControlToValidate="txtEditParQty" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Status :
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="RdEditStatus" runat="server" RepeatDirection="Horizontal" Width="280px">
                                                            <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <hr />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnEditRecord" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnEditRecord_Click" />
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </div>
                    <!--Edit Record Modal Ends here-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
