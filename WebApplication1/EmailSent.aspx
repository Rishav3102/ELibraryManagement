<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EmailSent.aspx.cs" Inherits="WebApplication1.EmailSent" %>

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
                                    <label>Email</label>
                                    <asp:TextBox CssClass="form-control" ID="emailTextBox" runat="server" placeholder="Email"></asp:TextBox>
                                </div>

                                <div class="form-group formGroupCustom">
                                    <asp:Button class="btn btn-success btn-lg btn-block btnBlockCustom" ID="Button1" runat="server" Text="Send Mail" OnClick="Button1_Click" />
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
