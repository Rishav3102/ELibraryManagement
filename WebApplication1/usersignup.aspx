<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="usersignup.aspx.cs" Inherits="WebApplication1.signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {

        });

        
        function ChooseNormalAddress(event) {
            console.log("ChooseNormalAddress");
            var textBoxFullAddress = document.getElementById('<%=TextBox5.ClientID%>');
            var address = $("#AddressDiv").text();

            textBoxFullAddress.value = address;
            
            
        }

        function ChooseSuggestedAddress(event) {

            console.log("ChooseSuggestedAddress");
            var textBoxFullAddress = document.getElementById('<%=TextBox5.ClientID%>');
            var SuggestedAddress = $("#SuggestedAddressDiv").text();

            textBoxFullAddress.value = SuggestedAddress;
           
            
        }

       
        function ValidateAddress() {

            var textBoxAddressLine1 = document.getElementById('<%= TextBox10.ClientID %>');
            var textBoxAddressLine2 = document.getElementById('<%= TextBox6.ClientID %>');
            var textBoxAddressCity = document.getElementById('<%= TextBox7.ClientID %>');
            var textBoxAddressPostalOrZip = document.getElementById('<%= TextBox13.ClientID %>');
            var textBoxAddressProvinceOrState = document.getElementById('<%= TextBox11.ClientID %>');
            var textBoxAddressCountry = document.getElementById('<%= TextBox12.ClientID %>');

            var addressLine1 = textBoxAddressLine1.value;
            var addressLine2 = textBoxAddressLine2.value;
            var addressCity = textBoxAddressCity.value;
            var addressPostalOrZip = textBoxAddressPostalOrZip.value;
            var addressProvinceOrState = textBoxAddressProvinceOrState.value;
            var addressCountry = textBoxAddressCountry.value;

            var data = {
                addressLine1 : addressLine1,
                addressLine2: addressLine2,
                addressCity: addressCity,
                addressPostalOrZip: addressPostalOrZip,
                addressProvinceOrState: addressProvinceOrState,
                addressCountry: addressCountry
            }

            var address = addressLine1 + " " + addressLine2 + " " + addressCity + "  " + addressProvinceOrState + " " + addressCountry + " " + addressPostalOrZip + " " + addressProvinceOrState;
            
            $("#AddressDiv").text(address);
            var AddressData = JSON.stringify(data);
            $.ajax({
                url: "Crud.asmx/ValidateAddress",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({'data' : AddressData}),
                dataType: "json",
                cache: false,
                async: false,
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if (data.status == "success") {
                        var suggestedAddress = data.data[0].line1 + " " +((data.data[0].line2 !== undefined) ? data.data[0].line2 : "") + " " + data.data[0].city + " " + data.data[0].provinceOrStateName + " " + data.data[0].provinceOrState + " " + data.data[0].postalOrZip + " " + data.data[0].countryName;
                            $("#SuggestedAddressDiv").text(suggestedAddress);  
                    }
                    else {
                        alert("Please enter a valid address");
                    }``
                },
                error: function (error) {
                    if (error.responseJSON && error.responseJSON.message) {
                        alert(error.responseJSON.message);
                    } else if (error.responseText) {
                        alert(error.responseText);
                    } else {
                        alert("An error occurred");
                    }
                }
            });


            
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100px" src="imgs/generaluser.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Member Sign Up</h4>
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
                            <div class="col-md-6">
                                <label>Full Name</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Full Name"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Date of Birth</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Contact No</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Contact No" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Email Id</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Email ID" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <label>Address[Line1]</label>
                                <%--<asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                    <asp:ListItem Text="Select" Value="select" />
                                    <asp:ListItem Text="Andhra Pradesh" Value="Andhra Pradesh" />
                                    <asp:ListItem Text="Arunachal Pradesh" Value="Arunachal Pradesh" />
                                    <asp:ListItem Text="Assam" Value="Assam" />
                                    <asp:ListItem Text="Bihar" Value="Bihar" />
                                    <asp:ListItem Text="Chhattisgarh" Value="Chhattisgarh" />
                                    <asp:ListItem Text="Rajasthan" Value="Rajasthan" />
                                    <asp:ListItem Text="Goa" Value="Goa" />
                                    <asp:ListItem Text="Gujarat" Value="Gujarat" />
                                    <asp:ListItem Text="Haryana" Value="Haryana" />
                                    <asp:ListItem Text="Himachal Pradesh" Value="Himachal Pradesh" />
                                    <asp:ListItem Text="Jammu and Kashmir" Value="Jammu and Kashmir" />
                                    <asp:ListItem Text="Jharkhand" Value="Jharkhand" />
                                    <asp:ListItem Text="Karnataka" Value="Karnataka" />
                                    <asp:ListItem Text="Kerala" Value="Kerala" />
                                    <asp:ListItem Text="Madhya Pradesh" Value="Madhya Pradesh" />
                                    <asp:ListItem Text="Maharashtra" Value="Maharashtra" />
                                    <asp:ListItem Text="Manipur" Value="Manipur" />
                                    <asp:ListItem Text="Meghalaya" Value="Meghalaya" />
                                    <asp:ListItem Text="Mizoram" Value="Mizoram" />
                                    <asp:ListItem Text="Nagaland" Value="Nagaland" />
                                    <asp:ListItem Text="Odisha" Value="Odisha" />
                                    <asp:ListItem Text="Punjab" Value="Punjab" />
                                    <asp:ListItem Text="Rajasthan" Value="Rajasthan" />
                                    <asp:ListItem Text="Sikkim" Value="Sikkim" />
                                    <asp:ListItem Text="Tamil Nadu" Value="Tamil Nadu" />
                                    <asp:ListItem Text="Telangana" Value="Telangana" />
                                    <asp:ListItem Text="Tripura" Value="Tripura" />
                                    <asp:ListItem Text="Uttar Pradesh" Value="Uttar Pradesh" />
                                    <asp:ListItem Text="Uttarakhand" Value="Uttarakhand" />
                                    <asp:ListItem Text="West Bengal" Value="West Bengal" />
                                </asp:DropDownList>--%>
                                <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Address[Line1]"></asp:TextBox>
                                 
                            </div>
                            <div class="col-md-4">
                                <label>Address[Line2]</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Address[Line2]"></asp:TextBox>
                                
                            </div>
                            <div class="col-md-4">
                                <label>Address[City]</label>
                                 <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Address[City]"></asp:TextBox>
                                
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Address[PostalOrZip]</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox13" runat="server" placeholder="Address[PostalOrZip]" TextMode="Number"></asp:TextBox>
                               
                            </div>
                            <div class="col-md-4">
                                <label>Address[ProvinceOrState]</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Address[ProvinceOrState]"></asp:TextBox>
                                
                            </div>
                            <div class="col-md-4">
                                <label>Address[Country]</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox12" runat="server" placeholder="Address[Country]"></asp:TextBox>
                                
                            </div>

                        </div>
                        <div class="row">
                            <center>
                                <div class="col-md-2">

                                   
                                    <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#staticBackdrop" onclick="ValidateAddress(this);return false">
                                        Validate  <i class="fa-solid fa-location-dot"></i>
                                    </button>

                                    <!-- Modal -->
                                    <div class="modal fade" id="staticBackdrop"  tabindex="-1">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h1 class="modal-title fs-5" id="staticBackdropLabel">Please Choose One Address</h1>
                                                    
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label>Address</label>
                                                            <div id="AddressDiv" data-bs-dismiss="modal" class="custom-div" onclick="ChooseNormalAddress(this);return false"></div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <label>Suggested Address</label>
                                                            <div id="SuggestedAddressDiv"  data-bs-dismiss="modal" class="custom-div" onclick="ChooseSuggestedAddress(this);return false"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </center>

                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Full Address</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Full Address" TextMode="MultiLine"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <hr />

                        <div class="row">
                            <center>
                                <div class="col">
                                    <span class="badge badge-pill badge-info">Login Credentials</span>
                                </div>
                            </center>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                Member
                                <label>&nbsp;ID</label>
                                <asp:TextBox class="form-control" ID="TextBox8" runat="server" placeholder="User Id"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Password</label>
                                <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col">

                                <div class="form-group formGroupCustom">
                                    <asp:Button class="btn btn-success btn-lg btnBlockCustom" ID="Button1" runat="server" Text="Sign Up" OnClick="Button1_Click" />
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
