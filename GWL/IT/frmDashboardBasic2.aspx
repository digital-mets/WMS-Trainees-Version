<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDashboardBasic2.aspx.cs" Inherits="GWL.IT.frmDashboardBasic2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />
    <!-- App css -->
    <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />

    <link href="assets/css/vendor/dataTables.bootstrap4.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="modal fade" id="TotalAssetmodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog  modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Unbilled Clients</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body" style="overflow-y: auto">

                        <%--  Table Content--%>

                        <table id="basic-datatable" class="table dt-responsive nowrap w-100">
                        </table>

                        <table id="basic-datatableexport" style="display: none">
                        </table>

                    </div>
                    <div class="modal-footer">

                        <button onclick="exportTableToExcel('basic-datatableexport','')" type="button" class="btn btn-info">Extract</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                    </div>
                </div>
            </div>
        </div>
        <!-- Begin page -->
        <div class="wrapper">
            <!-- Start Content-->
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="page-title-box">
                            <div class="page-title-right">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="input-group" style="display: none">
                                            <input type="text" class="form-control form-control-light" id="dash-period" />
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-primary border-primary text-white">
                                                    <i class="mdi mdi-calendar-range font-13"></i>
                                                </span>
                                            </div>
                                        </div>

                                        <label for="Customer" class="mr-1">Customer : </label>
                                        <select id="Customer" class="form-control form-control-md parameters mr-2" style="width: 8rem">
                                        </select>
                                        <%--       
                                        <label for="Warehouse" class="mr-1">Warehouse : </label>
                                        <select id="Warehouse" class="form-control form-control-md parameters mr-2" style="width: 8rem">
                                        </select>--%>

                                        <label for="Area" class="mr-1">Area : </label>
                                        <select id="Area" class="form-control form-control-md parameters mr-2" style="width: 8rem">
                                        </select>

                                        <div class="input-group date">
                                            <input type="text" class="form-control form-control-light" id="daterangetrans" style="text-align: center; width: 20rem" />
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-primary border-primary text-white">
                                                    <i class="mdi mdi-calendar-range font-13"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <a href="javascript:  Dashboard();" class="btn btn-primary ml-2" id="reviewDash">
                                        <i class="mdi mdi-autorenew"></i>
                                    </a>
                                 <%--   <a href="javascript:  Dashboard();" class="btn btn-primary ml-2">
                                          <i class="tiny material-icons">cached</i>
                                    </a>--%>
                                </div>
                            </div>
                            
                        </div>
                    </div>

                    <div class="col-12">
                        <h4 class="page-title"></h4>
                    </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="tabbable-panel" style="padding: 0">
                                <div class="tabbable-line">
                                    <nav>
                                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <a class="nav-link active" onclick="opentabs(event, 'tab_default_1')" id="tab-inbound" data-toggle="tab" href="#" role="tab">INBOUND</a>
                                            <a class="nav-link tab-inactive" onclick="opentabs(event, 'tab_default_2')" data-toggle="tab" href="#" role="tab">OUTBOUND</a>
                                            <a class="nav-link tab-inactive" onclick="opentabs(event, 'tab_default_3')" data-toggle="tab" href="#" role="tab">PRODUCTIVITY</a>
                                            <a class="nav-link tab-inactive" onclick="opentabs(event, 'tab_default_4')" data-toggle="tab" href="#" role="tab">TRUCK TRANSACTION</a>
                                            <a class="nav-link tab-inactive" onclick="opentabs(event, 'tab_default_5')" data-toggle="tab" href="#" role="tab">CONTRACT</a>
                                        </div>
                                    </nav>

                                    <div class="tab-content" id="tabcontent">
                                        <div class="tab-pane active" id="tab_default_1">
                                            <div class="row">

                                                <div class="col-lg-12 px-3 pb-3">
                                                    <div class="row">

                                                       <div class="col-12">
                                                        <div class="card border">
                                                            <div class="card-body">

                                                                <div class="row">
                                                                   <%-- CARD 1 NEW --%>

                                                                        <div class="card border shadow-none mr-3" style="width: 8rem">
                                                                            <a onclick="cardClick(5,'New')" data-toggle="modal" style="cursor: pointer;">
                                                                               <div class="card-body text-center">
                                                                                    <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                    <h3><span id="New" style="color: blue">0</span></h3>
                                                                                    <p class="text-muted font-15 mb-0">New</p>
                                                                               </div>
                                                                            </a>
                                                                        </div>

                                                                    <%-- CARD 2 PENDING --%>
                                                                       <div class="card border shadow-none mr-3" style="width: 8rem">
                                                                            <a onclick="cardClick(0,'Pending')" data-toggle="modal" style="cursor: pointer;">
                                                                                <div class="card-body text-center ">
                                                                                    <%--<i class="dripicons-briefcase text-warning" style="font-size: 24px;"></i>--%>
                                                                                    <h3><span id="Pending" style="color: blue">0</span></h3>
                                                                                    <p class="text-muted font-15 mb-0">Pending</p>
                                                                                </div>
                                                                            </a>
                                                                       </div>

                                                                    <%-- CARD 3 ONGOING --%>
                                                                    <div class="card border shadow-none mr-3" style="width: 8.5rem">
                                                                        <a onclick="cardClick(4,'Ongoing')" data-toggle="modal" style="cursor: pointer;">
                                                                            <div class="card-body text-center">
                                                                                    <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                <h3><span id="Ongoing" style="color: blue">0</span></h3>
                                                                                <p class="text-muted font-15 mb-0">Ongoing</p>
                                                                            </div>
                                                                        </a>
                                                                    </div>

                                   
                                                                    <%-- CARD 4 SUBMITTED --%>
                                                                    <div class="card shadow-none border mr-3" style="width: 8.5rem">
                                                                        <a onclick="cardClick(1,'Submitted')" data-toggle="modal" style="cursor: pointer;">
                                            
                                                                                <div class="card-body text-center ">
                                                                                    <%--<i class="dripicons-checklist text-success" style="font-size: 24px;"></i>--%>
                                                                                    <h3><span id="Submitted" style="color: blue">0</span></h3>
                                                                                    <p class="text-muted font-15 mb-0">Submitted</p>
                                                                                </div>
                                            
                                                                        </a>
                                                                    </div>

                                                                    <%-- CARD 5 CANCELLED --%>
                                                                    <div class="card border shadow-none mr-5" style="width: 8.5rem">
                                                                        <a onclick="cardClick(2,'Cancelled')" data-toggle="modal" style="cursor: pointer;">
                                            
                                                                                <div class="card-body text-center">
                                                                                    <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                    <h3><span id="Cancel" style="color: red">0</span></h3>
                                                                                    <p class="text-muted font-15 mb-0">Cancelled</p>
                                                                                </div>
                                                                        </a>
                                                                    </div>

                                                                    <%-- CARD 6 TOTAL TRANSACTIONS --%>
                                                                    <div class="card border shadow-none mr-3 ml-10" style="width: 8.5rem">
                                                                        <a onclick="cardClick(3,'TotalTransactions')" data-toggle="modal" style="cursor: pointer;">
                                            
                                                                                <div class="card-body text-center">
                                                                                    <%--<i class="dripicons-graph-line text-muted" style="font-size: 24px;"></i>--%>
                                                                                    <h3><span id="totalTransactions" style="color: blue">0</span> 
                                                                                        <%--<i class="mdi mdi-checkbox-marked-circle-outline text-warning"></i>--%>
                                                                                    </h3>
                                                                                    <p class="text-muted font-15 mb-0">Total Transactions</p>
                                                                                </div>
                                                                        </a>
                                                                    </div>

                                                                     <%-- CARD 7 BILLED--%>
                                                                     <div class="card border shadow-none" style="width: 8.5rem">
                                                                        <a onclick="cardClick(6,'Billed')" data-toggle="modal" style="cursor: pointer;">
                                            
                                                                                <div class="card-body text-center">
                                                                                    <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                    <h3><span id="Billed" style="color: blue">0</span></h3>
                                                                                    <p class="text-muted font-15 mb-0">Billed</p>
                                                                                </div>
                                                                        </a>
                                                                    </div>
                                                                    
                                                                    <div class="container col-12">
                                                                        <div class="row">

                                                                            <%-- Graph 1 --%>
                                                                                <div class="col-lg-4">
                                                                                    <div class="card border">
                                                                                        <div class="card-body">
                                                                                            <div class="dropdown float-right">
                                                                                                <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                                                                                    <i class="mdi mdi-dots-vertical"></i>
                                                                                                </a>
                                                                                                <div class="dropdown-menu dropdown-menu-right">
                                                                                                    <!-- item-->
                                                                                                    <a onclick="cardClick(0,'Transaction')" class="dropdown-item">Details</a>

                                                                                                </div>
                                                                                            </div>
                                                                                            <%--<h4 class="header-title mb-3">YIELD HIT or MISS Monitoring</h4>--%>
                                                                                            <h4 class="header-title mb-3">Transaction</h4>
                                                                                             <div class="table-responsive" style="overflow: auto; height: 20rem">
                                                                                              <div id="Yield"></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                              <%-- Graph 2 EXCLUDED--%>
                                                                                <%--<div class="col-sm-3">
                                                                                    <div class="card border">
                                                                                        <div class="card-body">
                                                                                            <div class="dropdown float-right">
                                                                                                <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                                                                                    <i class="mdi mdi-dots-vertical"></i>
                                                                                                </a>
                                                                                                <div class="dropdown-menu dropdown-menu-right">
                                                                                                    <!-- item-->
                                                                                                    <a onclick="cardClick(0,'Perfect Shipment')" class="dropdown-item">Details</a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <h4 class="header-title mb-3">Perfect Shipment</h4>
                                                                                             <div class="table-responsive" style="overflow: auto; height: 20rem">
                                                                                              <div id="perfectShipment"></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>--%>

                                                                                <%-- Graph 2 CLEAN INVOICE --%>
                                                                                <div class="col-sm-4 mr-5">
                                                                                    <div class="card border">
                                                                                        <div class="card-body">
                                                                                            <div class="dropdown float-right">
                                                                                                <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                                                                                    <i class="mdi mdi-dots-vertical"></i>
                                                                                                </a>
                                                                                                <div class="dropdown-menu dropdown-menu-right">
                                                                                                    <!-- item-->
                                                                                                    <a onclick="cardClick(0,'Clean Invoice')" class="dropdown-item">Details</a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <h4 class="header-title mb-3">Clean Invoice</h4>
                                                                                             <div class="table-responsive" style="overflow: auto; height: 20rem">
                                                                                              <div id="cleanInvoice"></div>
                                                                                            </div>
                                                                                        </div>
                                                                                </div>
                                                                            </div>


                                                                            <%-- 2 Rows 4 Cards --%>
                                                                            <div class="col-sm-3">
                                                                                <div class="row row-cols-2 align-content-center">

                                                                                <div class="col">
                                                                                    <div class="card border shadow-none" style="width: 8rem;">
                                                                                    <a onclick="cardClick(6,'Cold')" data-toggle="modal" style="cursor: pointer;">                 
                                                                                   <div class="card-body text-center">
                                                                                     <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                     <h3><span id="Cold" style="color: blue">0</span></h3>
                                                                                     <p class="text-muted font-15 mb-0">Cold</p>
                                                                                   </div>                
                                                                                 </a>
                                                                                </div>

                                                                                </div>

                                                                                <div class="col">
                                                                                    <div class="card border shadow-none" style="width: 8rem;">
                                                                                    <a onclick="cardClick(6,'Dry')" data-toggle="modal" style="cursor: pointer;">                 
                                                                                    <div class="card-body text-center">
                                                                                     <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                     <h3><span id="Dry" style="color: blue">0</span></h3>
                                                                                     <p class="text-muted font-15 mb-0">Dry</p>
                                                                                    </div>                
                                                                                    </a>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col">
                                                                                    <div class="card border shadow-none" style="width: 8rem;">
                                                                                    <a onclick="cardClick(6,'Aircon')" data-toggle="modal" style="cursor: pointer;">                 
                                                                                    <div class="card-body text-center">
                                                                                     <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                     <h3><span id="Aircon" style="color: blue">0</span></h3>
                                                                                     <p class="text-muted font-15 mb-0">Aircon</p>
                                                                                    </div>                
                                                                                    </a>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col">
                                                                                    <div class="card border shadow-none " style="width: 8rem;">
                                                                                    <a onclick="cardClick(6,'Chiller')" data-toggle="modal" style="cursor: pointer;">                 
                                                                                    <div class="card-body text-center">
                                                                                     <!--<i class="dripicons-user-group text-danger" style="font-size: 24px;"></i>-->
                                                                                     <h3><span id="Chiller" style="color: blue">0</span></h3>
                                                                                     <p class="text-muted font-15 mb-0">Chiller</p>
                                                                                    </div>                
                                                                                    </a>
                                                                                    </div>
                                                                                 </div>

                                                                               </div>
                                                                            </div>
                                                                            <!-- end 4 cards -->
                                                                        </div>
                                                                    </div>
                                                                        
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                  </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane active" id="tab_default_2">
                                        <div class="container col-12">

                
                                       </div>
                                       <!-- End Container -->
                                    </div>
                                    <!-- End of Tab 2 -->



                                    <!-- Start of Tab 3 -->
                                    <div class="tab-pane active" id="tab_default_3">
                                        <div class="container col-12">

                
                                        </div>
                                    </div>
                                    <!-- End of Tab 3 -->



                                    <!-- Start of Tab 4 -->
                                    <div class="tab-pane active" id="tab_default_4">
                                        <div class="container col-12">

                
                                        </div>
                                    </div>
                                    <!-- End of Tab 4 -->

                                    <!-- Start of Tab 5 -->
                                    
                                    <div class="tab-pane active" id="tab_default_5">
                                        <div class="container col-12">

                
                                        </div>
                                    </div>
                                    <!-- End of Tab 5 -->
                                </div>
                                    <!-- End of TAB CONTENT-->
                            </div>
                        </div>
                    </div>
                </div>

            <!-- container -->

       </div>
      </div>
        <!-- END wrapper -->
    </form>

    <!-- Common JS -->
    <script src="../js/PerfSender.js" type="text/javascript"></script>


    <!-- bundle -->
    <script src="assets/js/vendor.min.js"></script>
    <script src="assets/js/app.min.js"></script>

    <!-- third party js -->
    <script src="assets/js/vendor/Chart.bundle.min.js"></script>
    <script src="assets/js/vendor/apexcharts.min.js"></script>
    <script src="assets/js/vendor/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="assets/js/vendor/jquery-jvectormap-world-mill-en.js"></script>
    <!-- third party js ends -->

    <!-- demo app -->

    <!-- end demo js-->

    <!-- Datatables js -->
    <script src="//cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/vendor/dataTables.bootstrap4.js"></script>
    <script src="assets/js/vendor/dataTables.responsive.min.js"></script>
    <script src="assets/js/vendor/responsive.bootstrap4.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>

    <!-- Custom js -->
    <script src="../js/Dash.js" type="text/javascript"></script>
    <%-- TABLES--%>
    <script src="../js/ConstructTable.js"></script>
    <%-- CHARTS--%>
    <script src="../js/ConstructChart.js"></script>

</body>
</html>
