﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDashboard4.aspx.cs" Inherits="GWL.IT.frmDashboard4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <!-- App css -->
        <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Begin page -->
        <div class="wrapper">
            <!-- Start Content-->
            <div class="container-fluid">

                <div class="row mt-3">

                    <div class="col-lg-6 col-xl-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-6">
                                        <h5 class="text-muted font-weight-normal mt-0 text-truncate" title="Campaign Sent">Campaign Sent</h5>
                                        <h3 class="my-2 py-1">9,184</h3>
                                        <p class="mb-0 text-muted">
                                            <span class="text-success mr-2"><i class="mdi mdi-arrow-up-bold"></i> 3.27%</span>
                                        </p>
                                    </div>
                                    <div class="col-6">
                                        <div class="text-right">
                                            <div id="campaign-sent-chart" data-colors="#727cf5"></div>
                                        </div>
                                    </div>
                                </div> <!-- end row-->
                            </div> <!-- end card-body -->
                        </div> <!-- end card -->
                    </div> <!-- end col -->

                    <div class="col-lg-6 col-xl-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-6">
                                        <h5 class="text-muted font-weight-normal mt-0 text-truncate" title="New Leads">New Leads</h5>
                                        <h3 class="my-2 py-1">3,254</h3>
                                        <p class="mb-0 text-muted">
                                            <span class="text-danger mr-2"><i class="mdi mdi-arrow-down-bold"></i> 5.38%</span>
                                        </p>
                                    </div>
                                    <div class="col-6">
                                        <div class="text-right">
                                            <div id="new-leads-chart" data-colors="#0acf97"></div>
                                        </div>
                                    </div>
                                </div> <!-- end row-->
                            </div> <!-- end card-body -->
                        </div> <!-- end card -->
                    </div> <!-- end col -->

                    <div class="col-lg-6 col-xl-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-6">
                                        <h5 class="text-muted font-weight-normal mt-0 text-truncate" title="Deals">Deals</h5>
                                        <h3 class="my-2 py-1">861</h3>
                                        <p class="mb-0 text-muted">
                                            <span class="text-success mr-2"><i class="mdi mdi-arrow-up-bold"></i> 4.87%</span>
                                        </p>
                                    </div>
                                    <div class="col-6">
                                        <div class="text-right">
                                            <div id="deals-chart" data-colors="#727cf5"></div>
                                        </div>
                                    </div>
                                </div> <!-- end row-->
                            </div> <!-- end card-body -->
                        </div> <!-- end card -->
                    </div> <!-- end col -->

                    <div class="col-lg-6 col-xl-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-6">
                                        <h5 class="text-muted font-weight-normal mt-0 text-truncate" title="Booked Revenue">Booked Revenue</h5>
                                        <h3 class="my-2 py-1">$253k</h3>
                                        <p class="mb-0 text-muted">
                                            <span class="text-success mr-2"><i class="mdi mdi-arrow-up-bold"></i> 11.7%</span>
                                        </p>
                                    </div>
                                    <div class="col-6">
                                        <div class="text-right">
                                            <div id="booked-revenue-chart" data-colors="#0acf97"></div>
                                        </div>
                                    </div>
                                </div> <!-- end row-->
                            </div> <!-- end card-body -->
                        </div> <!-- end card -->
                    </div> <!-- end col -->
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-lg-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="dropdown float-right">
                                    <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                        <i class="mdi mdi-dots-vertical"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Today</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Yesterday</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Last Week</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Last Month</a>
                                    </div>
                                </div>

                                <h4 class="header-title mb-1">Campaigns</h4>

                                <div id="dash-campaigns-chart" class="apex-charts" data-colors="#ffbc00,#727cf5,#0acf97"></div>

                                <div class="row text-center mt-2">
                                    <div class="col-md-4">
                                        <i class="mdi mdi-send widget-icon rounded-circle bg-light-lighten text-muted"></i>
                                        <h3 class="font-weight-normal mt-3">
                                            <span>6,510</span>
                                        </h3>
                                        <p class="text-muted mb-0 mb-2"><i class="mdi mdi-checkbox-blank-circle text-warning"></i> Total Sent</p>
                                    </div>
                                    <div class="col-md-4">
                                        <i class="mdi mdi-flag-variant widget-icon rounded-circle bg-light-lighten text-muted"></i>
                                        <h3 class="font-weight-normal mt-3">
                                            <span>3,487</span>
                                        </h3>
                                        <p class="text-muted mb-0 mb-2"><i class="mdi mdi-checkbox-blank-circle text-primary"></i> Reached</p>
                                    </div>
                                    <div class="col-md-4">
                                        <i class="mdi mdi-email-open widget-icon rounded-circle bg-light-lighten text-muted"></i>
                                        <h3 class="font-weight-normal mt-3">
                                            <span>1,568</span>
                                        </h3>
                                        <p class="text-muted mb-0 mb-2"><i class="mdi mdi-checkbox-blank-circle text-success"></i> Opened</p>
                                    </div>
                                </div>
                                
                            </div>
                            <!-- end card body-->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col-->

                    <div class="col-lg-7">
                        <div class="card">
                            <div class="card-body">
                                <div class="dropdown float-right">
                                    <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                        <i class="mdi mdi-dots-vertical"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Today</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Yesterday</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Last Week</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Last Month</a>
                                    </div>
                                </div>
                                
                                <h4 class="header-title mb-3">Revenue</h4>

                                <div class="chart-content-bg">
                                    <div class="row text-center">
                                        <div class="col-md-6">
                                            <p class="text-muted mb-0 mt-3">Current Month</p>
                                            <h2 class="font-weight-normal mb-3">
                                                <span>$42,025</span>
                                            </h2>
                                        </div>
                                        <div class="col-md-6">
                                            <p class="text-muted mb-0 mt-3">Previous Month</p>
                                            <h2 class="font-weight-normal mb-3">
                                                <span>$74,651</span>
                                            </h2>
                                        </div>
                                    </div>
                                </div>

                                <div id="dash-revenue-chart" class="apex-charts" data-colors="#0acf97,#fa5c7c"></div>

                            </div>
                            <!-- end card body-->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col-->
                </div>
                <!-- end row-->


                <div class="row">
                    <div class="col-xl-4 col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="dropdown float-right">
                                    <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                        <i class="mdi mdi-dots-vertical"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Settings</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Action</a>
                                    </div>
                                </div>
                                <h4 class="header-title mb-3">Top Performing</h4>

                                <div class="table-responsive">
                                    <table class="table table-striped table-sm table-nowrap table-centered mb-0">
                                        <thead>
                                            <tr>
                                                <th>User</th>
                                                <th>Leads</th>
                                                <th>Deals</th>
                                                <th>Tasks</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <h5 class="font-15 mb-1 font-weight-normal">Jeremy Young</h5>
                                                    <span class="text-muted font-13">Senior Sales Executive</span>
                                                </td>
                                                <td>187</td>
                                                <td>154</td>
                                                <td>49</td>
                                                <td class="table-action">
                                                    <a href="javascript: void(0);" class="action-icon"> <i class="mdi mdi-eye"></i></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h5 class="font-15 mb-1 font-weight-normal">Thomas Krueger</h5>
                                                    <span class="text-muted font-13">Senior Sales Executive</span>
                                                </td>
                                                <td>235</td>
                                                <td>127</td>
                                                <td>83</td>
                                                <td class="table-action">
                                                    <a href="javascript: void(0);" class="action-icon"> <i class="mdi mdi-eye"></i></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h5 class="font-15 mb-1 font-weight-normal">Pete Burdine</h5>
                                                    <span class="text-muted font-13">Senior Sales Executive</span>
                                                </td>
                                                <td>365</td>
                                                <td>148</td>
                                                <td>62</td>
                                                <td class="table-action">
                                                    <a href="javascript: void(0);" class="action-icon"> <i class="mdi mdi-eye"></i></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h5 class="font-15 mb-1 font-weight-normal">Mary Nelson</h5>
                                                    <span class="text-muted font-13">Senior Sales Executive</span>
                                                </td>
                                                <td>753</td>
                                                <td>159</td>
                                                <td>258</td>
                                                <td class="table-action">
                                                    <a href="javascript: void(0);" class="action-icon"> <i class="mdi mdi-eye"></i></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h5 class="font-15 mb-1 font-weight-normal">Kevin Grove</h5>
                                                    <span class="text-muted font-13">Senior Sales Executive</span>
                                                </td>
                                                <td>458</td>
                                                <td>126</td>
                                                <td>73</td>
                                                <td class="table-action">
                                                    <a href="javascript: void(0);" class="action-icon"> <i class="mdi mdi-eye"></i></a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div> <!-- end table-responsive-->

                            </div> <!-- end card-body-->
                        </div> <!-- end card-->
                    </div>
                    <!-- end col-->

                    <div class="col-xl-4 col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="dropdown float-right">
                                    <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                        <i class="mdi mdi-dots-vertical"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Settings</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Action</a>
                                    </div>
                                </div>
                                <h4 class="header-title mb-4">Recent Leads</h4>

                                <div class="media">
                                    <img class="mr-3 rounded-circle" src="assets/images/users/avatar-2.jpg" width="40" alt="Generic placeholder image">
                                    <div class="media-body">
                                        <span class="badge badge-warning-lighten float-right">Cold lead</span>
                                        <h5 class="mt-0 mb-1">Risa Pearson</h5>
                                        <span class="font-13">richard.john@mail.com</span>
                                    </div>
                                </div>

                                <div class="media mt-3">
                                    <img class="mr-3 rounded-circle" src="assets/images/users/avatar-3.jpg" width="40" alt="Generic placeholder image">
                                    <div class="media-body">
                                        <span class="badge badge-danger-lighten float-right">Lost lead</span>
                                        <h5 class="mt-0 mb-1">Margaret D. Evans</h5>
                                        <span class="font-13">margaret.evans@rhyta.com</span>
                                    </div>
                                </div>

                                <div class="media mt-3">
                                    <img class="mr-3 rounded-circle" src="assets/images/users/avatar-4.jpg" width="40" alt="Generic placeholder image">
                                    <div class="media-body">
                                        <span class="badge badge-success-lighten float-right">Won lead</span>
                                        <h5 class="mt-0 mb-1">Bryan J. Luellen</h5>
                                        <span class="font-13">bryuellen@dayrep.com</span>
                                    </div>
                                </div>

                                <div class="media mt-3">
                                    <img class="mr-3 rounded-circle" src="assets/images/users/avatar-5.jpg" width="40" alt="Generic placeholder image">
                                    <div class="media-body">
                                        <span class="badge badge-warning-lighten float-right">Cold lead</span>
                                        <h5 class="mt-0 mb-1">Kathryn S. Collier</h5>
                                        <span class="font-13">collier@jourrapide.com</span>
                                    </div>
                                </div>

                                <div class="media mt-3">
                                    <img class="mr-3 rounded-circle" src="assets/images/users/avatar-1.jpg" width="40" alt="Generic placeholder image">
                                    <div class="media-body">
                                        <span class="badge badge-warning-lighten float-right">Cold lead</span>
                                        <h5 class="mt-0 mb-1">Timothy Kauper</h5>
                                        <span class="font-13">thykauper@rhyta.com</span>
                                    </div>
                                </div>

                                <div class="media mt-3">
                                    <img class="mr-3 rounded-circle" src="assets/images/users/avatar-6.jpg" width="40" alt="Generic placeholder image">
                                    <div class="media-body">
                                        <span class="badge badge-success-lighten float-right">Won lead</span>
                                        <h5 class="mt-0 mb-1">Zara Raws</h5>
                                        <span class="font-13">austin@dayrep.com</span>
                                    </div>
                                </div>
                                   
                            </div>
                            <!-- end card-body -->
                        </div>
                        <!-- end card-->
                    </div>
                    <!-- end col -->  
                    
                    <div class="col-xl-4 col-lg-6">
                        <div class="card cta-box bg-primary text-white">
                            <div class="card-body">
                                <div class="media align-items-center">
                                    <div class="media-body">
                                        <h2 class="mt-0"><i class="mdi mdi-bullhorn-outline"></i>&nbsp;</h2>
                                        <h3 class="m-0 font-weight-normal cta-box-title">Enhance your <b>Campaign</b> for better outreach <i class="mdi mdi-arrow-right"></i></h3>
                                    </div>
                                    <img class="ml-3" src="assets/images/email-campaign.svg" width="120" alt="Generic placeholder image">
                                </div>
                            </div>
                            <!-- end card-body -->
                        </div>
                        <!-- end card-->

                        <!-- Todo-->
                        <div class="card">
                            <div class="card-body">
                                <div class="dropdown float-right">
                                    <a href="#" class="dropdown-toggle arrow-none card-drop" data-toggle="dropdown" aria-expanded="false">
                                        <i class="mdi mdi-dots-vertical"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Settings</a>
                                        <!-- item-->
                                        <a href="javascript:void(0);" class="dropdown-item">Action</a>
                                    </div>
                                </div>
                                <h4 class="header-title mb-2">Todo</h4>

                                <div class="todoapp">
                                    <div data-simplebar style="max-height: 224px">
                                        <ul class="list-group list-group-flush todo-list" id="todo-list"></ul>
                                    </div>
                                </div> <!-- end .todoapp-->

                            </div> <!-- end card-body -->
                        </div> <!-- end card-->

                    </div>
                    <!-- end col -->  
                </div>
                <!-- end row-->
                
            </div> <!-- container -->


        </div>
        <!-- END wrapper -->
    </form>
    
        <!-- bundle -->
        <script src="assets/js/vendor.min.js"></script>
        <script src="assets/js/app.min.js"></script>

        <!-- Apex js -->
        <script src="assets/js/vendor/apexcharts.min.js"></script>

        <!-- Todo js -->
        <script src="assets/js/ui/component.todo.js"></script>

        <script src="assets/js/pages/demo.dashboard-crm.js"></script>
        <!-- end demo js-->
</body>
</html>
