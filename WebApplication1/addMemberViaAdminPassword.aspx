<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="addMemberViaAdminPassword.aspx.cs" Inherits="WebApplication1.addMemberViaAdminPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="150px" src="imgs/generaluser.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Reset Password</h3>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <hr>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">


                                <div class="form-group formGroupCustom">
                                    <label>Emaild</label>
                                    <asp:TextBox CssClass="form-control" ID="newEmail1" runat="server" TextMode="Email" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="form-group formGroupCustom">

                                    <label>Password In Email</label>
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                                </div>

                                <div class="form-group formGroupCustom">

                                    <label>New Password</label>
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group formGroupCustom">

                                    <label>Confirm Password</label>
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                                </div>



                                <div class="form-group formGroupCustom">
                                    <asp:Button class="btn btn-success btn-lg btn-block btnBlockCustom" ID="Button1" runat="server" Text="Set Password" />
                                </div>

                            </div>
                        </div>


                    </div>
                </div>
                <a href="homepage.aspx">< < Back to Home</a><br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
