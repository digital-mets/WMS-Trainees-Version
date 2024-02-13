<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCP.aspx.cs" Inherits="GWL.IT.frmCP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GEARS - Capacity Planning</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />
    <!-- App css -->
    <link href="../IT/assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../IT/assets/css/app.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/enjoyhint/css/overlay.css" rel="stylesheet" type="text/css" />
    <link href="../IT/assets/css/vendor/dataTables.bootstrap4.css" rel="stylesheet" type="text/css" />
    <link href="../IT/assets/css/vendor/responsive.bootstrap4.css" rel="stylesheet" type="text/css" />
    <style>
        .table td, .table th {
        padding: .30rem;
        vertical-align: top;
        border-top: 1px solid #eef2f7;
    }

        input:read-only {
      background-color: #F1F3FA;
    }

        #newtab {
        position: fixed; /* Sit on top of the page content */
        display: none; /* Hidden by default */
        width: 100%; /* Full width (cover the whole page) */
        height: 100%; /* Full height (cover the whole page) */
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: rgba(0,0,0,0.5); /* Black background with opacity */
        z-index: 2; /* Specify a stack order in case you're using a different order for other elements */
        cursor: pointer; /* Add a pointer on hover */
    }

    </style>

    <!-- Common JS -->
    <script src="../js/PerfSender.js" type="text/javascript"></script>

    <!-- bundle -->
    <script src="../IT/assets/js/vendor.min.js"></script>
    <script src="../IT/assets/js/app.min.js"></script>

    <!-- third party js -->
    <script src="../IT/assets/js/vendor/Chart.bundle.min.js"></script>
    <script src="../IT/assets/js/vendor/apexcharts.min.js"></script>
    <script src="../IT/assets/js/vendor/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="../IT/assets/js/vendor/jquery-jvectormap-world-mill-en.js"></script>
    <!-- third party js ends -->

    <!-- demo app -->

    <!-- end demo js-->

    <!-- Datatables js -->
    <script src="//cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <script src="../IT/assets/js/vendor/dataTables.bootstrap4.js"></script>
    <script src="../IT/assets/js/vendor/dataTables.responsive.min.js"></script>
    <%--<script src="../IT/assets/js/vendor/responsive.bootstrap4.min.js"></script>--%>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
</head>
    
<body>
    <div id="newtab" onclick="offnewtab()">
        <div id="text">
            Capacity Planning
            <p style="font-size: 14px;">
               <br /> Capacity Plan has been open into a new tab, please click to refresh.
            </p>

        </div>
    </div>

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

                        <table id="basic-datatableexport" style="display:none" >
                        </table>
                       
                         

                    </div>
                    <div class="modal-footer">
                          
                        <button onclick="exportTableToExcel('basic-datatableexport','')" type="button" class="btn btn-info" >Extract</button>
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
                        <div class="page-title-box" >
                            <div class="page-title-right">
                                <div class="form-inline">
                                    <div class="form-group">
                                          <div class="input-group" style="display:none">
                                            <input type="text" class="form-control form-control-light" id="dash-period"  />
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-primary border-primary text-white">
                                                    <i class="mdi mdi-calendar-range font-13"></i>
                                                </span>
                                            </div>
                                        </div>

                                       <div class="input-group date" >
                                            <input type="text" class="form-control form-control-light" id="daterangetrans" style="text-align: center; width:20rem" />
                                             <div class="input-group-append">
                                                <span class="input-group-text bg-primary border-primary text-white">
                                                    <i class="mdi mdi-calendar-range font-13"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <a href="javascript:  Dashboard();" class="btn btn-primary ml-2">
                                        <i class="mdi mdi-autorenew"></i>
                                    </a>
                                     <a href="javascript:  Dashboard();" class="btn btn-warning ml-2">
                                        <i class="mdi mdi-import"></i>
                                    </a>
                                     <a href="javascript:  Dashboard();" class="btn btn-danger ml-2">
                                        <i class="mdi mdi-check-circle"></i>
                                    </a>
                                    <a href="javascript:  Dashboard();" class="btn btn-primary ml-2">
                                        <i class="mdi mdi-email"></i>
                                    </a>
                                   <a href="javascript:  Dashboard();" class="btn btn-success ml-2">
                                        <i class="mdi mdi-content-save"></i>
                                    </a>
                                </div>
                            </div>
                            <h4 class="page-title">
                                <a href="..\IT\frmCP.aspx" target="_blank" onclick="onnewtab();">Capacity Planning</a>
                                <span class="badge badge-danger-lighten">This week is not yet finalized</span>
                            </h4>
                        </div>
                    </div>
                    <!-- end col-->
                </div>
                <!-- end row-->


                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                 
                                <div class="dropdown float-right">
                                    <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                        <i class="mdi mdi-dots-vertical"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <!-- item-->
                                        <a onclick="cardClick(5,'Pending Orders')" class="dropdown-item">Details</a>

                                    </div>
                                </div>
                               <div class="label mb-3" style="padding-right:10px">
                                     <span>
                                         Year: 
                                         <input type="date" name="Year" value=" " />
                                     </span>
                                     <span style="padding-left:10px">
                                          List of Orders:  
                                    <select name="cars" id="cars" style="border-style:none">
                                        <option value="volvo">ALL</option>
                                        <option value="saab">MO0000001</option>
                                        <option value="opel">MO0000002</option>
                                        <option value="audi">MO0000003</option>
                                      </select>
                                    </span>

                                     <span style="padding-left:10px">
                                         Quantity Type:  
                                    <select name="cars" id="cars" style="border-style:none">
                                        <option value="volvo">Batches</option>
                                        <option value="saab">Kilogram</option>
                                        <option value="opel">No of Load</option>
                                      </select>
                                    </span>

                                </div>

                                <%--<h4 class="header-title mb-3">
                                    
                                  
                                    <span style="padding-left:10px">
                                          Production Site:  
                                    <select name="cars" id="cars" style="border-style:none">
                                        <option value="volvo">MLI Bancal</option>
                                        <option value="saab">MLI Cavite</option>
                                        <option value="opel">MLI Cebu</option>
                                        <option value="audi">MLI Davao</option>
                                      </select>
                                    </span>

                                   
                                    <span style="padding-left:10px">
                                    Customer:
                                          <select  name="cars" id="cars" style="border-style:none">
                                        <option value="volvo">ALL</option>
                                        <option value="saab">FBC - Frabelle</option>
     
                                      </select>
                                    </span>
                                </h4> --%>

                             

                                <div class="table-responsive" style="overflow:auto; height:25rem " >
                                    <table class="table table-centered table-nowrap table-hover mb-0">
                                        <tbody id="detpo">
                                             <tr>
                                                 <th  style="width:18rem">
                                                     SKU
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Mon</a></h5>
                                                    <span class="text-muted font-13">31-May</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Tue</a></h5>
                                                    <span class="text-muted font-13">1-Jun</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Wed</a></h5>
                                                    <span class="text-muted font-13">2-Jun</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Thu</a></h5>
                                                    <span class="text-muted font-13">3-Jun</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Fri</a></h5>
                                                    <span class="text-muted font-13">4-Jun</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Sat</a></h5>
                                                    <span class="text-muted font-13">5-Jun</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Sun</a></h5>
                                                    <span class="text-muted font-13">6-Jun</span>
                                                 </th>
                                                 <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Total</a></h5>
                                                    <span class="text-muted font-13">in Batches</span>
                                                 </th>
                                                   <th>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Total Order</a></h5>
                                                    <span class="text-muted font-13">in Batches</span>
                                                 </th>
                                                 <th></th>
                                             </tr>  
                                            <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700026 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Cheesedog KS 1 Kg (17 pcs)</span>
                                                    <span class="badge badge-warning-lighten">C</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                   <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">

                                                       </i> 5.38

                                                   </span>
                                                </td>
                                            </tr>
                                         <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700019 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Hotdog KS 1.14 Kg </span>
                                                       <span class="badge badge-danger-lighten">S</span>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                    <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>
                                                </td>
                                            </tr>

                                              <tr >
                                                <td >
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700035 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Chicken Franks KS w/ cheese</span>
                                                    <span class="badge badge-warning-lighten">C</span>
                                                    <span class="badge badge-info-lighten">K</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
     <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700018 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Chicken Footlong 1 Kg</span>
                                                    <span class="badge badge-info-lighten">K</span>
                                                    <span class="badge badge-success-lighten">F</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
     <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700026 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Cheesedog KS 1 Kg (17 pcs)</span>
                                                    <span class="badge badge-warning-lighten">C</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
     <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700026 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Cheesedog KS 1 Kg (17 pcs)</span>
                                                    <span class="badge badge-warning-lighten">C</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
     <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700026 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Cheesedog KS 1 Kg (17 pcs)</span>
                                                    <span class="badge badge-warning-lighten">C</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
     <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">7700026 - Bossing</a></h5>
                                                    <span class="text-muted font-13">Cheesedog KS 1 Kg (17 pcs)</span>
                                                    <span class="badge badge-warning-lighten">C</span>
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
     <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">

                                                       </i> 5.38

                                                   </span>                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                                <!-- end table-responsive-->

                                <div class="table-responsive" style="overflow:auto; height:15rem; background-color:#F1F3FA " >
                                    <table class="table table-centered table-nowrap table-hover mb-0">
                                        <tbody id="detpo">
                                             
                                            <tr>
                                                <td style="width:18rem">
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Total</a></h5>
                                           
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                             
                                            </tr>
                                         <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Total Orders</a></h5>
                
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="354" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                    <a href="javascript: void(0);" class="action-icon"><i class="mdi  mdi-open-in-app"></i></a>
                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body"></a></h5>
                                                    
                                                </td>
                                                <td>
                                                    <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">
                                                       </i> 5
                                                   </span>
                                                   
                                                </td>
                                                
                                                <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                <td>
                                                    <span class="text-success mr-2">
                                                       <i class="mdi mdi-arrow-up-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                       <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                                <td>
                                                    <span class="text-danger mr-2">
                                                       <i class="mdi mdi-arrow-down-bold">
                                                       </i> 5
                                                   </span>                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                    <a href="javascript: void(0);" class="action-icon"><i class="mdi  mdi-open-in-app"></i></a>
                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                    
                                                       <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Percent with Cheese</a>

                                                              <span class="badge badge-warning-lighten">C</span>
                                                       </h5>
                                                </td>
                                                <td>
                                                   <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                    <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                    <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                     <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                    <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                      <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                     <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                     <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                     <input type="text" readonly="true" value="40%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                    <a href="javascript: void(0);" class="action-icon"><i class="mdi  mdi-open-in-app"></i></a>
                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                      <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Percent Small SKU</a>
                                                             <span class="badge badge-danger-lighten">S</span>

                                                      </h5>
                                                </td>
                                                <td>
                                                    <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                     <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                     <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                    <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                    <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                     <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                   <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                    <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                     <input type="text" readonly="true" value="8%" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                    <a href="javascript: void(0);" class="action-icon"><i class="mdi  mdi-open-in-app"></i></a>
                                                </td>
                                            </tr>

                                              <tr>
                                                <td>
                                                    <h5 class="font-14 my-1"><a href="javascript:void(0);" class="text-body">Total Metric Ton</a></h5>
                                                </td>
                                                <td>
                                                   <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                
                                                <td>
                                                  <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                  <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                  <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                  <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                 <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                       <td>
                                                 <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                                <td>
                                                 <input type="number" readonly="true" value="130720" style="width:5rem; border-style:none"/>
                                                   
                                                </td>
                                               <%-- <td>
                                                    <span class="text-muted font-13">Supplier</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">National Bookstore</h5>
                                                </td>
                                                <td>
                                                    <span class="text-muted font-13">Order vs Recevied</span>
                                                    <h5 class="font-14 mt-1 font-weight-normal">3,000 out of 5,000</h5>
                                                </td>--%>
                                                <td class="table-action" style="width: 90px;">
                                                    <a href="javascript: void(0);" class="action-icon"><i class="mdi  mdi-open-in-app"></i></a>
                                                </td>
                                            </tr>

                                         

                                         

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <!-- end card body-->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col-->
                </div>
                <!-- end row-->


            


            </div>
            <!-- container -->


        </div>
        <!-- END wrapper -->
    </form>



    

     <script>

        
      document.addEventListener("DOMContentLoaded", () => {

                     $('#daterangetrans').daterangepicker({
                opens: 'center'
            }, function (start, end, label) {
                console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
              //  reinitializeWithDateRange(start.format('MM/DD/YYYY'), end.format('MM/DD/YYYY'));
                });
 
     
          
            $("#dash-period").datepicker({
                format: "M-yyyy",
                viewMode: "months",
                minViewMode: "months",
                autoclose: true
            });

            $('#dash-period').datepicker( 'option' , 'onSelect', function (date) { // 'onSelect' here, but could be any datepicker event
                    //$(this).change(); // Lauch the "change" evenet of the <input> everytime someone click a new date
              
                });

            var strArray=['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

            $.ajax({
                type: "POST",
                data: JSON.stringify({ Code: "BOOKDATE" }),
                url: "/PerformSender.aspx/SS",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    bookdate = result.d;
                    var d = new Date(bookdate);
                    $('#dash-period').val(strArray[d.getMonth() - 1] + '-' + d.getFullYear());

                    var datesd = new Date();
                    var dateEd = new Date();

                    $('#daterangetrans').data('daterangepicker').setStartDate(datesd);
                    $('#daterangetrans').data('daterangepicker').setEndDate(dateEd);

                  //  Dashboard();
                }

            });


        });

        function reinitChart() {
                        !(function (o) {
                "use strict";
                function t() {
                    (this.$body = o("body")), (this.charts = []);
                }
                (t.prototype.respChart = function (r, a, e, n) {
                    (Chart.defaults.global.defaultFontColor = "#8391a2"), (Chart.defaults.scale.gridLines.color = "#8391a2");
                    var i = r.get(0).getContext("2d"),
                        s = o(r).parent();
                    return (function () {
                        var t;
                        switch ((r.attr("width", o(s).width()), a)) {
                            case "Line":
                                t = new Chart(i, { type: "line", data: e, options: n });
                                break;
                            case "Bar":
                                t = new Chart(i, { type: "bar", data: e, options: n });
                                break;
                            case "Doughnut":
                                t = new Chart(i, { type: "doughnut", data: e, options: n });
                        }
                        return t;
                    })();
                }),
                    (t.prototype.initCharts = function () {
                        var t,
                            r,
                            a,
                            e = [];
                        return (
                            0 < o("#task-area-chart").length &&
                            ((t = {
                                labels: [
                                    "Sprint 1",
                                    "Sprint 2",
                                    "Sprint 3",
                                    "Sprint 4",
                                    "Sprint 5",
                                    "Sprint 6",
                                    "Sprint 7",
                                    "Sprint 8",
                                    "Sprint 9",
                                    "Sprint 10",
                                    "Sprint 11",
                                    "Sprint 12",
                                    "Sprint 13",
                                    "Sprint 14",
                                    "Sprint 15",
                                    "Sprint 16",
                                    "Sprint 17",
                                    "Sprint 18",
                                    "Sprint 19",
                                    "Sprint 20",
                                    "Sprint 21",
                                    "Sprint 22",
                                    "Sprint 23",
                                    "Sprint 24",
                                ],
                                datasets: [
                                    {
                                        label: "This year",
                                        backgroundColor: o("#task-area-chart").data("bgcolor") || "#727cf5",
                                        borderColor: o("#task-area-chart").data("bordercolor") || "#727cf5",
                                        data: [16, 44, 32, 48, 72, 60, 84, 64, 78, 50, 68, 34, 26, 44, 32, 48, 72, 60, 74, 52, 62, 50, 32, 22],
                                    },
                                ],
                            }),
                                e.push(
                                    this.respChart(o("#task-area-chart"), "Bar", t, {
                                        maintainAspectRatio: !1,
                                        legend: { display: !1 },
                                        tooltips: { intersect: !1 },
                                        hover: { intersect: !0 },
                                        plugins: { filler: { propagate: !1 } },
                                        scales: {
                                            xAxes: [{ barPercentage: 0.7, categoryPercentage: 0.5, reverse: !0, gridLines: { color: "rgba(0,0,0,0.05)" } }],
                                            yAxes: [{ ticks: { stepSize: 10, display: !1 }, min: 10, max: 100, display: !0, borderDash: [5, 5], gridLines: { color: "rgba(0,0,0,0)", fontColor: "#fff" } }],
                                        },
                                    })
                                )),
                            0 < o("#project-status-chart2").length &&
                            ((a = {
                                labels: ["Open", "Partial", "Behind"],
                                datasets: [{ data: [poo, pop, pob], backgroundColor: (r = o("#project-status-chart2").data("colors")) ? r.split(",") : ["#0acf97", "#727cf5", "#fa5c7c"], borderColor: "transparent", borderWidth: "3" }],
                            }),
                                e.push(this.respChart(o("#project-status-chart2"), "Doughnut", a, { maintainAspectRatio: !1, cutoutPercentage: 80, legend: { display: !1 } }))),
                            e
                        );
                    }),
                    (t.prototype.init = function () {
                        var r = this;
                        (Chart.defaults.global.defaultFontFamily = '-apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,Oxygen-Sans,Ubuntu,Cantarell,"Helvetica Neue",sans-serif'),
                            (r.charts = this.initCharts()),
                            o(window).on("resize", function (t) {
                                o.each(r.charts, function (t, r) {
                                    try {
                                        r.destroy();
                                    } catch (t) { }
                                }),
                                    (r.charts = r.initCharts());
                            });
                    }),
                    (o.ChartJs = new t()),
                    (o.ChartJs.Constructor = t);
            })(window.jQuery),
                (function () {
                    "use strict";
                    window.jQuery.ChartJs.init();
                })();

          

        }

      
function cardClick(type, title, withselect=0) {
         
            var objDetails = DBData[type];
            if (objDetails.length > 0) {
                  document.getElementById("exampleModalLabel").innerHTML = title;
            $('#TotalAssetmodal').modal("show");

            let tHead = document.createElement("thead");
            let tBody = document.createElement("tbody");// CREATE TABLE BODY .
            let hTr = document.createElement("tr"); // CREATE ROW FOR TABLE HEAD .

                let col = []; // define an empty array
                if (withselect == 1) {
                    col.push('Select');
                }

            for (var i = 0; i < objDetails.length; i++) {
                 
                for (var key in objDetails[i]) {
                    if (col.indexOf(key) === -1) {
                        col.push(key);
                    }
                }
            }


            // ADD COLUMN HEADER TO ROW OF TABLE HEAD.
            for (var i = 0; i < col.length; i++) {
                let th = document.createElement("th");
                th.setAttribute('class', 'th-lg');
                th.scope = "col";
                th.innerHTML = col[i];
                hTr.appendChild(th);
            }
            tHead.appendChild(hTr);


                console.log(col);
            for (var i = 0; i < objDetails.length; i++) {
                let bTr = document.createElement("tr"); // CREATE ROW FOR EACH RECORD .


                for (var j = 0; j < col.length; j++) {

                    if (j == 0 && withselect==1) {
                        let td = document.createElement("td");
                        let ti = document.createElement("input");
                        ti.setAttribute("type", "checkbox");
                       
                        td.appendChild(ti);
                        bTr.appendChild(td);
                    } else {
                        let td = document.createElement("td");

                        if (isNaN(objDetails[i][col[j]]) || objDetails[i][col[j]] == null || objDetails[i][col[j]] == true || objDetails[i][col[j]] == false) {
                            
                           td.innerHTML = objDetails[i][col[j]];
                            if (Object.prototype.toString.call(objDetails[i][col[j]]) === "[object String]" && (objDetails[i][col[j]]).substring((objDetails[i][col[j]]).length-4) === "000Z") {
                                var Dateval = new Date(objDetails[i][col[j]]);
                                td.innerHTML = Dateval.toLocaleDateString();
                                 td.style.textAlign = "center";
                            }
                            else {
                                td.innerHTML = objDetails[i][col[j]];
                                 td.style.textAlign = "left";
                            }
                           
                           
                        } else {
                           
                                  if (tChar.has(col[j])) {
                               
                                 td.innerHTML = objDetails[i][col[j]];
                                 td.style.textAlign = "left";
                            } else {
                                td.innerHTML = numberWithCommas(objDetails[i][col[j]]);
                                td.style.textAlign = "right";
                            }
                            
                          //  alert( objDetails[i][col[j]]);
                        }
                        bTr.appendChild(td);
                    }

                }
                tBody.appendChild(bTr)
            }
            //$('#basic-datatable table thead').remove();
            //$('#basic-datatable table tbody').remove();

           
            if ($.fn.DataTable.isDataTable('#basic-datatable')) {
                $('#basic-datatable').DataTable().destroy();
            }
                $('#basic-datatable').empty();
               

            // document.getElementById("basic-datatable").removeChild();
            $('#basic-datatable').append(tHead);

            $('#basic-datatable').append(tBody);
            $('#basic-datatable').DataTable({
                "orderCellsTop": true,
                "fixedHeader": true,
               
                "scrollX": true,
                "sScrollY": "310px",
                "render": true,
                "pageLength": 5,
                
                "autoWidth": false,
                "columnDefs": [{

                    "searchable": true,
                    "orderable": false,

                }],
                });


            }

          


            let tHead = document.createElement("thead");
            let tBody = document.createElement("tbody");// CREATE TABLE BODY .
            let hTr = document.createElement("tr"); // CREATE ROW FOR TABLE HEAD .

            let col = []; // define an empty array
            for (var i = 0; i < objDetails.length; i++) {
                for (var key in objDetails[i]) {
                    if (col.indexOf(key) === -1) {
                        col.push(key);
                    }
                }
            }


            // ADD COLUMN HEADER TO ROW OF TABLE HEAD.
            for (var i = 0; i < col.length; i++) {
                let th = document.createElement("th");
                th.setAttribute('class', 'th-lg');
                th.scope = "col";
                th.innerHTML = col[i];
                hTr.appendChild(th);
            }
            tHead.appendChild(hTr);



            for (var i = 0; i < objDetails.length; i++) {
                let bTr = document.createElement("tr"); // CREATE ROW FOR EACH RECORD .


     for (var j = 0; j < col.length; j++) {

                    if (j == 0 && withselect==1) {
                        let td = document.createElement("td");
                        let ti = document.createElement("input");
                        ti.setAttribute("type", "checkbox");
                        ti.checked = true;
                        td.appendChild(ti);
                        bTr.appendChild(td);
                    } else {
                        let td = document.createElement("td");

                   if (isNaN(objDetails[i][col[j]]) || objDetails[i][col[j]] == null || objDetails[i][col[j]] == true || objDetails[i][col[j]] == false) {
                            
                           td.innerHTML = objDetails[i][col[j]];
                            if (Object.prototype.toString.call(objDetails[i][col[j]]) === "[object String]" && (objDetails[i][col[j]]).substring((objDetails[i][col[j]]).length-4) === "000Z") {
                                var Dateval = new Date(objDetails[i][col[j]]);
                                td.innerHTML = Dateval.toLocaleDateString();
                                 td.style.textAlign = "center";
                            }
                            else {
                                td.innerHTML = objDetails[i][col[j]];
                                 td.style.textAlign = "left";
                            }
                           
                           
                        } else {
                            if (tChar.has(col[j])) {
                               
                                 td.innerHTML = objDetails[i][col[j]];
                                 td.style.textAlign = "left";
                            } else {
                                td.innerHTML = numberWithCommas(objDetails[i][col[j]]);
                                td.style.textAlign = "right";
                            }
                          //  alert( objDetails[i][col[j]]);
                        }
                        bTr.appendChild(td);
                    }

                }
                tBody.appendChild(bTr)
            }

            $('#basic-datatableexport').empty();
            $('#basic-datatableexport').append(tHead);
            $('#basic-datatableexport').append(tBody);
      





        }

         var pop = 0, poo = 0, pob = 0, Total = "[]", cashdate = "[]";

                var received = "" 
                var order = "" 
                var armonth = ""

        function Dashboard() {
            let items;

            var vUserID = '<%=Session["userid"] %>';
            var vType = 'DashboardPC';
           // var vParam = $("#dash-period").val();
            var vParam = $("#daterangetrans").val();
            var vScheama = '<%=Session["schemaname"] %>';
            var extsession = '<%=Session["token"]%>';
           

            fetch('<%=Session["APIURL"] %>'+"/GetDashboard", {
                method: "POST",
                //body: "{}",
                async: true,
                body: JSON.stringify({ UserID: vUserID, Scheama: vScheama, Type: vType, Param: vParam, session: extsession }),
                headers: {
                    "Content-Type": "application/json"
                },
            }).then(function (response) {
                return response.json()
            }).then(function (data) {
                console.log(data);

                 DBData = data;

                $('#oo').text(numberWithCommas(data[0].length));
                $('#pi').text(numberWithCommas(data[1].length));
                $('#fs').text(numberWithCommas(data[2].length));

                var paid = 0;

                for (var i = 0; i < data[3].length; i++) {

                    if (data[3][i].Status == "W") {
                        paid++;
                    }
               
                }

                $('#paid').text(numberWithCommas(paid)+' out of '+numberWithCommas(data[3].length));
              

                pop = data[4][0].POPartial;
                poo = data[4][0].POOpen;
                pob = data[4][0].POBehind;
                $('#pop').text(numberWithCommas(pop));
                $('#poo').text(numberWithCommas(poo));
                $('#pob').text(numberWithCommas(pob));
                $('#totpo').text(data[5].length);
                var table = "";

                for (var i = 0; i < data[5].length; i++) {

                    var badge = ((data[5][i].DaysDue < 0) ? "badge-danger-lighten" : (data[5][i].POStatus == "New" ? "badge-success-lighten" : "badge-warning-lighten"));
                  

                 table += "                           <tr> "
                 table += "                               <td> "
                 table += "                                   <h5 class='font-14 my-1'><a href='javascript:void(0);' class='text-body'>"+data[5][i].DocNumber+" - "+data[5][i].ItemCode+" ("+data[5][i].ShortDesc+")</a></h5> "
                 table += "                                   <span class='text-muted font-13'>Due in "+data[5][i].DaysDue+" days</span> "
                 table += "                               </td> "
                 table += "                               <td> "
                 table += "                                   <span class='text-muted font-13'>Status</span> "
                 table += "                                   <br /> "
                 table += "                                   <span class='badge "+badge+"'>"+((data[5][i].DaysDue<0)? "Overdue":data[5][i].POStatus)+"</span> "
                 table += "                               </td> "
                 table += "                               <td> "
                 table += "                                   <span class='text-muted font-13'>Supplier</span> "
                 table += "                                   <h5 class='font-14 mt-1 font-weight-normal'>"+data[5][i].Name+"</h5> "
                 table += "                               </td> "
                 table += "                               <td> "
                 table += "                                   <span class='text-muted font-13'>Order vs Recevied</span> "
                 table += "                                   <h5 class='font-14 mt-1 font-weight-normal'>"+numberWithCommas(data[5][i].ReceivedQty)+" out of "+numberWithCommas(data[5][i].OrderQty)+"</h5> "
                 table += "                               </td> "
                 table += "                               <td class='table-action' style='width: 90px;'> "
                 table += "                                   <a href='javascript: void(0);' class='action-icon'><i class='mdi  mdi-open-in-app'></i></a> "
                 table += "                               </td> "
                 table += "                           </tr> "
                   
               
                }

                $('#detpo').empty();
                $('#detpo').append(table);



                received = "[" 
                order = "[" 
                armonth = "["
                for (var i = 0; i < data[6].length; i++) {
                    received += data[6][i].ReceivedQty
                    order += (data[6][i].OrderQty -data[6][i].ReceivedQty)
                    armonth += "'"+data[6][i].Month+"'"
                    if (data[6].length > i + 1) {
                        received += ", "
                        order += ", "
                        armonth +=", "
                    }

                }

                received += "]"
                order += "]" 
                armonth += "]"

                e = ["#727cf5", "#e3eaef"];
                (t = $("#high-performing-product2").data("colors")) && (e = t.split(","));
                r = {
                    chart: { height: 257, type: "bar", stacked: !0 },
                    plotOptions: { bar: { horizontal: !1, columnWidth: "20%" } },
                    dataLabels: { enabled: !1 },
                    stroke: { show: !0, width: 2, colors: ["transparent"] },
                    series: [
                        //{ name: "Collected", data: [65, 59, 80, 81, 56, 89, 40, 32, 65, 59, 80, 81] },
                        //{ name: "Receivables", data: [89, 40, 32, 65, 59, 80, 81, 56, 89, 40, 65, 59] },
                        { name: "Received", data: eval(received) },
                        { name: "Order", data: eval(order) },
                    ],
                    zoom: { enabled: !1 },
                    legend: { show: !1 },
                    colors: e,
                    //xaxis: { categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"], axisBorder: { show: !1 } },
                    xaxis: { categories: eval(armonth), axisBorder: { show: !1 } },
                    yaxis: {
                        labels: {
                            formatter: function (e) {
                                return e + "";
                            },
                            offsetX: -15,
                        },
                    },
                    fill: { opacity: 1 },
                    tooltip: {
                        y: {
                            formatter: function (e) {
                                return "" + e + "";
                            },
                        },
                    },
                };
                new ApexCharts(document.querySelector("#high-performing-product2"), r).render();


                table = "";
                for (var i = 0; i < data[7].length; i++) {

                    var badge = ((data[7][i].DaysDue < 0) ? "badge-danger-lighten" : (data[7][i].POStatus == "New" ? "badge-success-lighten" : "badge-warning-lighten"));
                  

                 table += "<tr> "
                 table += "    <td> "
                 table += "        <div class='media'> "
                 table += "           <img class='mr-2 rounded-circle' src='./assets/images/users/user.png' width='40' alt='Generic placeholder image'> "
                 table += "    <div class='media-body'> "
                 table += "    <h5 class='mt-0 mb-1'>"+data[7][i].Fullname+"<small class='font-weight-normal ml-3'>"+data[7][i].DocDate.substring(0, 10)+"</small></h5> "
                 table += "       <span class='font-13'>"+data[7][i].Remarks+"</span> "
                 table += "          </div> "
                 table += "          </div> "
                 table += "    </td> "

                 table += "    <td> "
                 table += "        <span class='text-muted font-13'>Purchase Order</span> "
                 table += "        <br /> "
                 table += "    <p class='mb-0'>"+data[7][i].DocNumber+"</p> "
                 table += "      </td> "
                 table += "</tr> "
                   
               
                }

                $('#ofs').empty();
                $('#ofs').append(table);


               table = "";
                for (var i = 0; i < data[8].length; i++) {

          

                 table += "<li class='mb-4'> "
                 table += " <p class='text-muted mb-1 font-13'>"
                 table += "  <i class='mdi mdi-calendar'></i>"+data[8][i].DocNumber+" "
                 table += "  </p>    "
                 table += "   <h5>"+data[7][i].Remarks+"</h5> "
                 table += "   </li> "
                   
               
                }

                $('#cl').empty();
                $('#cl').append(table);




                reinitChart();
                



            }

            ).catch(function (error) {
                console.log(error);
            })
        
    }

         function onnewtab() {
    document.getElementById("newtab").style.display = "block";
}

function offnewtab() {
    document.getElementById("newtab").style.display = "none";
   window.location.reload();
}

    </script>

</body>
</html>
