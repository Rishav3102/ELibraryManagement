<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DefaultersPage.aspx.cs" Inherits="WebApplication1.DefaultersPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            makeBootStrapTable();
            loadDataFromDb();
        }

        function loadDataFromDb() {
            $.ajax({
                url: "Crud.asmx/GetDefaultersData",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                cache: false,
                async: false,
                success: function (response) {
                    var data = JSON.parse(response.d);
                    $("#data-table").bootstrapTable('load', data); 
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

        function sendEmailsToDefaulters() {

            var DefaultersDetailArray = $("#data-table").bootstrapTable('getSelections');
            var stringifiedDefaultersDetailArray = JSON.stringify(DefaultersDetailArray);
            $.ajax({
                url: "Crud.asmx/SendMailsToDefaulters",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ data: stringifiedDefaultersDetailArray }),
                cache: false,
                async: false,
                success: function (response) {
                    alert('Mails Sent Successfully to all the Defaulters');
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function makeBootStrapTable() {
            $("#data-table").bootstrapTable('destroy').bootstrapTable({
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
                        field: 'state',
                        checkbox: true,
                        align: 'center',
                        valign: 'middle'
                    },
                    {
                        field: 'memberName',
                        align: 'center',
                        title: 'Name',
                    },
                    {
                        field: 'bookName',
                        align: 'center',
                        title: 'Book Name',
                    },
                    {
                        field: 'issueDate',
                        align: 'center',
                        title: 'Issue Date',
                    },
                    {
                        field: 'dueDate',
                        align: 'center',
                        title: 'Due Date',
                    },
                    {
                        field: 'email',
                        align: 'center',
                        title: 'Email'
                    },
                    {
                        field: 'lateFees',
                        align: 'center',
                        title: 'Late Fees'
                    },
                    {
                        title: 'Delete',
                        align: 'center',
                        clickToSelect: false,
                        events: window.operateEvents,
                        formatter: operateFormatter
                    }

                ]
            });
        }

        
      
        function operateFormatter(value, row, index) {
            return [

                '<a class="remove" href="javascript:void(0)" title="Remove">',
                '<i class="fa fa-trash"></i>',
                '</a>'
            ].join('')
        }


        window.operateEvents = {

            'click .remove': function (e, value, row, index) {
                var data = {
                    memberId: row.memberId,
                    bookName: row.bookName
                }
                
                $.ajax({
                    url: "Crud.asmx/DeleteDefaulter",
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify(data),
                    cache: false,
                    async: false,
                    success: function (response) {
                        if (response.d) {
                            alert("Defaulter Deleted Successfully");
                            loadDataFromDb();
                        }
                       
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }
        }

        function DeleteSelectedItems() {

            var DefaultersDataObject = $("#data-table").bootstrapTable('getSelections');
            var stringifiedData = JSON.stringify(DefaultersDataObject);
            $.ajax({
                url: "Crud.asmx/DeleteSelectedDefaulters",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ data: stringifiedData }),
                cache: false,
                async: false,
                success: function (response) {
                    if (response.d) {
                        alert("Defaulters Deleted Successfully");
                        loadDataFromDb();
                    }

                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>




        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-10">
                        <center>
                            <h4>Defaulters List</h4>
                        </center>
                    </div>


                    <div class="col-md-2">
                        <button class="btn btn-success" onclick="sendEmailsToDefaulters(this) ; return false">Send Emails</button>
                        <button id="remove" class="btn btn-danger remove" onClick="DeleteSelectedItems(this) ; return false">
                            <i class="fa fa-trash"></i>Delete
                        </button>
                    </div>
                </div>



                <div class="row">
                    <div class="col">
                        <hr>
                    </div>
                </div>


                <div class="row">

                    <div class="col">
                        <table id="data-table"
                            class="table table-striped table-bordered table-hover" data-toolbar="#toolbar"
                            data-search="true"
                            data-detail-view="true"
                            data-show-export="true"
                            data-click-to-select="true"
                            data-detail-formatter="detailFormatter"
                            data-minimum-count-columns="2"
                            data-pagination="true"
                            data-id-field="id"
                            data-page-list="[10, 25, 50, 100, all]"
                            data-show-footer="true"
                            data-side-pagination="server"
                            data-response-handler="responseHandler">  

                        </table>
                    </div>

                </div>


            </div>
        </div>

    </center>

</asp:Content>
