<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="WebApplication1.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        $(document).ready(function () {
            
                $.ajax({
                    url: "Crud.asmx/makeGraphs",
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function (response) {
                        makeChartBookStock(response.d);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });

        });

        function makeChartBookStock(dataForGraphs) {
            const ctx = document.getElementById('myChart1');
            
            var lables = dataForGraphs.map((curr) => curr.BookId);
            var frequency = dataForGraphs.map((curr) => curr.ActualStock);
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: lables,
                    datasets: [{
                        label: 'Actual Stock',
                        data: frequency,
                        borderWidth: 1,
                        borderColor: '#FF6384',
                        backgroundColor: '#FFB1C1',
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });

            const ctx1 = document.getElementById('myChart2');
            var lables = dataForGraphs.map((curr) => curr.BookId);
            var frequency = dataForGraphs.map((curr) => curr.BookIssued);
            new Chart(ctx1, {
                type: 'bar',
                data: {
                    labels: lables,
                    datasets: [{
                        label: "Book Issued",
                        data: frequency,
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        }
        
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div id="ChartsContainer" class="card">
            <div>
                <canvas id="myChart1"></canvas>
            </div>
            <div>
                <canvas id="myChart2"></canvas>
            </div>
        </div>
    </section>

    <section>
        <img src="imgs/home-bg.jpg" class="img-fluid" />
    </section>

    <section>

        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Features</h2>
                        <p><b>Our 3 Primary Features -</b></p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img width="150px" src="imgs/digital-inventory.png" />
                        <h4>Digital Book Inventory</h4>
                        <p class="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                        <img width="150px" src="imgs/search-online.png" />
                        <h4>Search Books</h4>
                        <p class="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                        <img width="150px" src="imgs/defaulters-list.png" />
                        <h4>Defaulter List</h4>
                        <p class="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                    </center>
                </div>
            </div>
        </div>

    </section>
    <section>
        <img src="imgs/in-homepage-banner.jpg" class="img-fluid" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Process</h2>
                        <p><b>We have a simple 3 Step Process</b></p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img width="150px" src="imgs/sign-up.png" />
                        <h4>Sign Up</h4>
                        <p class="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                        <img width="150px" src="imgs/search-online.png" />
                        <h4>Search Books</h4>
                        <p class="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                        <img width="150px" src="imgs/library.png" />
                        <h4>Visit Us</h4>
                        <p class="text-justify">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                    </center>
                </div>
            </div>
        </div>

    </section>
</asp:Content>
