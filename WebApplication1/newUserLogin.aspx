<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="newUserLogin.aspx.cs" Inherits="WebApplication1.newUserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        
        function Login() {

            var userDetails = {
                memberId: $("#TextBox1").val(),
                password: $("#TextBox2").val()
            }

            $.ajax({
                url: "Crud.asmx/NewUserLogin",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(userDetails),
                dataType: "json",
                cache: false,
                async: false,
                success: function (response) {

                    console.log(response);
                    if (response.d.code === 1 && response.d.message == "Login Successfull") {
                        alert(response.d.message);
                        var path = GenerateUrl(response.d, "SetPassword.aspx");
                        redirectToNewPage(path);
                    }
                    else if (response.d.code === 2 && response.d.message == "Login Successfull") {
                        alert(response.d.message);
                        redirectToNewPage("homepage.aspx");

                    }
                    else {
                        alert("Please Enter a Valid Credentials");
                    }


                },
                error: function (error) {
                    console.log(error.txt);
                    alert(error.txt);
                }
            });
        }

        function GenerateUrl(data , path) {
            var searchParams = new URLSearchParams();
            searchParams.set('key1', data.memberId);
            searchParams.set('key2', data.code);
            return path +"?"+searchParams.toString();
        }
        function redirectToNewPage(path)  {
            window.location.href = path;
            
        }
    </script>
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
                                    <label for="TextBox1">Member Id</label>
                                    <input type="text" class="form-control" id="TextBox1" placeholder="Member Id">
                                </div>
                                
                                <div class="form-group formGroupCustom">
                                    <label for="TextBox2">Password</label>
                                    <input type="password" class="form-control" id="TextBox2" placeholder="Password">
                                </div>

                                <div class="form-group formGroupCustom">
                                    <button class="btn btn-success btn-lg btn-block btnBlockCustom" onclick="Login(this); return false" id="Button1" >Login</button>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <a href="homepage.aspx">Back to Home</a>&nbsp;&nbsp;&nbsp;
               
            </div>
        </div>
    </div>
</asp:Content>
