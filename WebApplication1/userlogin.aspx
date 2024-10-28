<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userlogin.aspx.cs" Inherits="WebApplication1.userlogin" %>

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
                                    <h3>Member Login</h3>
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
                                    <label>Member Id</label>
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Member Id"></asp:TextBox>
                                </div>
                                
                                <div class="form-group formGroupCustom">
                                    <label>Password</label>
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                </div>

                                <div class="form-group formGroupCustom">
                                    <asp:Button class="btn btn-success btn-lg btn-block btnBlockCustom" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                                </div>
                                <div class="form-group formGroupCustom">
                                    <a href="usersignup.aspx"><input id="Button2" class="btn btn-primary btn-lg btn-block btnBlockCustom" type="button" value="Sign Up" /></a>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                <a href="homepage.aspx">Back to Home</a>&nbsp;&nbsp;&nbsp;
                <a href="EmailSent.aspx"> Forgot Password?</a><br /><br />
            </div>
        </div>
    </div>

</asp:Content>
