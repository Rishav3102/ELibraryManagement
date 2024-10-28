<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminAddMember.aspx.cs" Inherits="WebApplication1.adminAddMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

       

        $(window).on('load', function () {
            registerBootstrapTable();
            loadUserDetailTable();
        });

        function loadUserDetailTable() {
            $.ajax({
                url: "Crud.asmx/GetData",
                type: "POST",
                cache: false,
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                    var dataList = JSON.parse(response.d);
                    $('#table-body').bootstrapTable('load', dataList);
                },
                error: function (error) {
                    alert(error.responseText);
                }
            });
        }

        function registerBootstrapTable() {
            $('#table-body').bootstrapTable({
                cache: true,
                striped: true,
                pagination: true,
                editable: true,
                sidePagination: 'client',
                useRowAttrFunc: true,
                pageSize: 10,
                pageList: [10, 20, 50],
                maintainSelected: true,
                search: false,
                showColumns: false,
                showSave: false,
                columns: [
                    {
                        field: 'username',
                        title: 'Name',
                    },
                    {
                        field: 'dob',
                        title: 'DOB',
                    }, {
                        field: 'contactNumber',
                        title: 'Contact No.',
                    },
                    {
                        field: 'email',
                        title: 'Email',
                    },
                    {
                        field: 'state',
                        title: 'State',
                    },
                    {
                        field: 'city',
                        title: 'City',
                    },
                    {
                        field: 'pincode',
                        title: 'Pincode',
                    },
                    {
                        field: 'address',
                        title: 'Address',
                    },
                    {
                        field: 'userId',
                        title: 'MemberId',
                    },
                    {
                        field: 'AccountStatus',
                        title: 'Account Status',
                    }
                ]
            });
        }

        function addMember() {
            var userData = {
                username: $("#TextBox3").val(),
                dob: $("#TextBox2").val(),
                contactNumber: $("#TextBox1").val(),
                email: $("#TextBox4").val(),
                state: $("#DropDownList1").val(),
                city: $("#TextBox6").val(),
                pincode: $("#TextBox7").val(),
                address: $("#TextBox5").val(),
                userId: $("#TextBox8").val(),
            };
            var userDataJSON = JSON.stringify(userData);

            $.ajax({
                url: "Crud.asmx/CrudOperation",
                type: "POST",
                cache: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ 'userData': userDataJSON }),
                success: function (response) {
                    loadUserDetailTable();
                    alert("Registration Successful . Please Check Your Email");
                },
                error: function (error) {
                    alert(error.responseText);
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
                                    <img width="100px" src="imgs/generaluser.png" alt="User Image">
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Add Member</h4>
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
                                <label for="TextBox3">Full Name</label>
                                <input type="text" class="form-control" id="TextBox3" placeholder="Full Name">
                            </div>
                            <div class="col-md-6">
                                <label for="TextBox2">Date of Birth</label>
                                <input type="date" class="form-control" id="TextBox2" placeholder="Date of Birth">
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label for="TextBox1">Contact No</label>
                                <input type="number" class="form-control" id="TextBox1" placeholder="Contact No">
                            </div>
                            <div class="col-md-6">
                                <label for="TextBox4">Email Id</label>
                                <input type="email" class="form-control" id="TextBox4" placeholder="Email ID">
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label for="DropDownList1">State</label>
                                <select class="form-control" id="DropDownList1">
                                    <option value="select">Select</option>
                                    <option value="Andhra Pradesh">Andhra Pradesh</option>
                                    <option value="Arunachal Pradesh">Arunachal Pradesh</option>
                                    <option value="Assam">Assam</option>
                                    <option value="Bihar">Bihar</option>
                                    <option value="Chhattisgarh">Chhattisgarh</option>
                                    <option value="Rajasthan">Rajasthan</option>
                                    <option value="Goa">Goa</option>
                                    <option value="Gujarat">Gujarat</option>
                                    <option value="Haryana">Haryana</option>
                                    <option value="Himachal Pradesh">Himachal Pradesh</option>
                                    <option value="Jammu and Kashmir">Jammu and Kashmir</option>
                                    <option value="Jharkhand">Jharkhand</option>
                                    <option value="Karnataka">Karnataka</option>
                                    <option value="Kerala">Kerala</option>
                                    <option value="Madhya Pradesh">Madhya Pradesh</option>
                                    <option value="Maharashtra">Maharashtra</option>
                                    <option value="Manipur">Manipur</option>
                                    <option value="Meghalaya">Meghalaya</option>
                                    <option value="Mizoram">Mizoram</option>
                                    <option value="Nagaland">Nagaland</option>
                                    <option value="Odisha">Odisha</option>
                                    <option value="Punjab">Punjab</option>
                                    <option value="Rajasthan">Rajasthan</option>
                                    <option value="Sikkim">Sikkim</option>
                                    <option value="Tamil Nadu">Tamil Nadu</option>
                                    <option value="Telangana">Telangana</option>
                                    <option value="Tripura">Tripura</option>
                                    <option value="Uttar Pradesh">Uttar Pradesh</option>
                                    <option value="Uttarakhand">Uttarakhand</option>
                                    <option value="West Bengal">West Bengal</option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label for="TextBox6">City</label>
                                <input type="text" class="form-control" id="TextBox6" placeholder="City">
                            </div>
                            <div class="col-md-4">
                                <label for="TextBox7">Pincode</label>
                                <input type="number" class="form-control" id="TextBox7" placeholder="Pincode">
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <label for="TextBox5">Full Address</label>
                                <textarea class="form-control" id="TextBox5" placeholder="Full Address"></textarea>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <span class="badge badge-pill badge-info">Login Credentials</span>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                Member
                                <label for="TextBox8">ID</label>
                                <input type="text" class="form-control" id="TextBox8" placeholder="User Id">
                            </div>

                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="form-group formGroupCustom">
                                    <button class="btn btn-success btn-lg btnBlockCustom" id="Button1" onclick="addMember(this);return false" type="button">Add Member</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="homepage.aspx">&lt; &lt; Back to Home</a><br>
                <br>
            </div>
        </div>
        <div class="row">
            
                <table id="data-table" class="table table-striped table-bordered table-hover ">
                    <thead>
                        <tr>
                            
                        </tr>
                    </thead>
                    <tbody id="table-body">
                        <!-- Data will be inserted here dynamically -->
                    </tbody>
                </table>
            

        </div>
    </div>
</asp:Content>
