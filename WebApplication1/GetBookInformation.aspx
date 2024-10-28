<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GetBookInformation.aspx.cs" Inherits="WebApplication1.GetBookInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>


        var searchParams;
        var bookImageUrl;
        var bookName;
        window.onload = () => {
            searchParams = new URLSearchParams(window.location.search);
            bookImageUrl = searchParams.get('bookImgUrl');
            bookName = searchParams.get('bookName');
            if (bookImageUrl != null) {
                $('#bookImage').attr('src', bookImageUrl.substring(2));
            }
            else {
                $('#bookImage').attr('src',"./book_inventory/books1.png");
            }
            $('#bookNameTextBox').val(bookName);
        }
        $(document).ready(function () {
             
        });

        function getBookDescription() {
            $.ajax({
                url: "https://www.googleapis.com/books/v1/volumes?q=" + bookName,
                type: "GET",
                contentType: "application/json",
                dataType: "json",
                cache: false,
                async: false,
                success: function (response) {
                    $("#bookDescriptionTextBox").val(response.items[0].volumeInfo.description);
                },
                error: function (error) {
                    alert(error.txt);
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" id="bookGptContainer">
        <div class="card" id="bookGptCard">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4 mx-auto">
                        <img width="150px" src="imgs/books.png" alt="Image" id="bookImage" />
                    </div>
                    <div class="col-md-8 mx-auto">
                        <input type="text" id="bookNameTextBox" placeholder="Book Name" readonly />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 mx-auto" id="descriptionBoxContainer">
                        <textarea type="text" id="bookDescriptionTextBox" placeholder="Book Description"></textarea>
                    </div>
                </div>
                <div class="row">
                    <center>
                        <div class="col-md-4 mx-auto">
                            <button class="btn btn-primary" id="buttonFindDescription" onclick="getBookDescription(this) ; return false">Find Description</button>
                        </div>
                    </center> 

                </div>
            </div>
        </div>

    </div>
</asp:Content>
