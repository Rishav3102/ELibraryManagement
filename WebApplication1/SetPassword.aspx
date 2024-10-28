<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SetPassword.aspx.cs" Inherits="WebApplication1.SetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var searchParams = new URLSearchParams(window.location.search);
        var key1Value = searchParams.get('key1');
        var key2Value = searchParams.get('key2');
        
        window.onload = ()=>{
            
            $("#memberId").val(key1Value);
        }
       
        
        function SetPassword() {
            var confirmPassword = $("#confirmPassword").val();
            var password = $("#password").val();
            if (password === confirmPassword) {
                
                var userDetials = {
                    memberId : key1Value,
                    code : key2Value,
                    password : password
                }

                $.ajax({
                    url: "Crud.asmx/SetPassword",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(userDetials),
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function (response) {
                        alert(response.d);
                        if (response.d === "Password Reset Successfull") {
                            window.location.href = "homepage.aspx";
                        }
                        
                        
                    },
                    error: function (error) {
                        alert(error.txt);
                    }
                });
            }
            else {
                alert("Password And Confirm Password Are Not Same");
            }
            
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
                                <h3>Set Password</h3>
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
                                <label for="memberId">Member Id</label>
                                <input type="email" class="form-control" id="memberId" placeholder="Member Id" readonly="true">
                            </div>
                            <div class="form-group formGroupCustom">
                                <label for="password">New Password</label>
                                <input type="password" class="form-control" id="password" placeholder="New Password">
                            </div>
                            <div class="form-group formGroupCustom">
                                <label for="confirmPassword">Confirm Password</label>
                                <input type="password" class="form-control" id="confirmPassword" placeholder="Confirm Password">
                            </div>
                            <div class="form-group formGroupCustom">
                                <button class="btn btn-success btn-lg btn-block btnBlockCustom" id="resetButton" onclick="SetPassword(this); return false">Set Password</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a href="homepage.aspx">&lt; Back to Home</a><br><br>
        </div>
    </div>
</div>

</asp:Content>
