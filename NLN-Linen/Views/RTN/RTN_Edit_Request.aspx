<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RTN_Edit_Request.aspx.cs" Inherits="NLN_Linen.Views.RTN.RTN_Edit_Request" %>

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
                                <legend class="scheduler-border">Return Request : Edit</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="RTN Request No. : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblRTNRequestNo" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Visible="false">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Business Unit : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblBusinessUnit" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Department Name : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblCustomer" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="RTN Request Name : "  CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtRequestName" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="RTN Request Date : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblRequestDate" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="RTN Request Time : " CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblRequestTime" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Status" CssClass="col-md-5"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdRTNStatus" runat="server" RepeatDirection="Horizontal" Width="300px">
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
                                                <asp:Button ID="btnTopPrint" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btnPrint_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnTopSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnTopBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Add New Item</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Item Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLAddItem" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <%--<cc1:AutoCompleteExtender ID="txtName_AutoCompleteExtender" runat="server"
                                                        TargetControlID="txtAddItem" MinimumPrefixLength="3" ServiceMethod="GetListofItem">
                                                    </cc1:AutoCompleteExtender>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Item Qty: " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddItemQty" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ControlToValidate="txtAddItemQty" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnAddItem" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnAddItem_Click" OnClientClick="return confirm('Do you want add new item?');" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    <div>
                        <hr />
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" PageSize="100" Width="100%"
                                            OnRowDataBound="GridView_RowDataBound" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" ClientIDMode="AutoID"
                                            DataKeyNames="Id" CssClass="table table-striped table-bordered table-hover footable" TabIndex="5"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No." ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:Label ID="gvlblId" runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblItemName" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RTN Qty" ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="gvtxtRtnQty" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ControlStyle Width="45px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnTopSave" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnButtomSave" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnTopPrint" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnButtomPrint" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <%--<asp:UpdateProgress ID="updProgressMain" AssociatedUpdatePanelID="UpdatePanel" runat="server">
                                    <ProgressTemplate>
                                        <div class="modal fade" aria-hidden="true">
                                            <div class="center">
                                                <img style="position: relative; top: 50%;" src="~/Images/loader.gif" runat="server" alt="Loading" height="40" width="40" />
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>--%>
                            </div>
                        </div>
                    </div>
                    <div>
                        <hr />
                    </div>
                    <div>
                        <div>
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-12" style="text-align: center;">
                                            <%--<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                                &nbsp;--%>
                                            <asp:Button ID="btnButtomPrint" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btnPrint_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnButtomSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnButtomBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            <%--&nbsp;
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Print Invoice-->
                    <div id="printRtnModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="printModalLabel" aria-hidden="true" style="height: 500; width: 1070px; top: 5%; left: 30%;">
                        <%--<div class="modal-dialog">--%>
                        <div class="modal-content" style="width: 80%;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <h3 id="printModalLabel">Print Return List</h3>

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