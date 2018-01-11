<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SOILED_Edit_Request.aspx.cs" Inherits="NLN_Linen.Views.SOILED.SOILED_Edit_Request" %>

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
                                <legend class="scheduler-border">Soiled Request : Edit</legend>
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
                                                    <asp:RadioButtonList ID="RdSOILEDStatus" runat="server" RepeatDirection="Horizontal" Width="300px">
                                                        <asp:ListItem Text="Complete" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O" Selected="True"></asp:ListItem>
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
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click" />
                                                &nbsp;
                                                <button type="button" class="btn btn-info" data-toggle="modal" data-target="#addModal">
                                                    Add 
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
                                            DataKeyNames="ID" CssClass="table table-striped table-bordered table-hover footable"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:ButtonField CommandName="deleteRecord" Text="Delete" HeaderText="Delete" ControlStyle-Height="25px"></asp:ButtonField>
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
                                                    <td width="160px">Department Name :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDLAddCustomer" runat="server" CssClass="form-control" Width="280px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Type Bag :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDLAddTypeBag" runat="server" CssClass="form-control" Width="280px">
                                                            <asp:ListItem Text="Green" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Red" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Qty :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddQty" runat="server" CssClass="form-control" Width="280px"></asp:TextBox>
                                                        <asp:CompareValidator ControlToValidate="txtAddQty" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Weight(Kg) :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddWeight" runat="server" CssClass="form-control" Width="280px"></asp:TextBox>
                                                        <asp:CompareValidator ControlToValidate="txtAddWeight" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Double" ForeColor="Red"></asp:CompareValidator>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
