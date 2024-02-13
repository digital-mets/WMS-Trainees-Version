<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMapping.aspx.cs" Inherits="GWL.frmMapping" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Mapping</title>

    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">--%>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">

    <script type="importmap">
        {
            "imports": {
                "three": "../node_modules/three/build/three.module.js",
                "OrbitControls": "../node_modules/three/examples/jsm/controls/MapControls.js",
                "Sky": "../node_modules/three/examples/jsm/objects/Sky.js",
                "Stats": "../node_modules/three/examples/jsm/libs/stats.module.js",
                "@tweenjs/tween.js": "../node_modules/@tweenjs/tween.js/dist/tween.esm.js"
            }
        }
    </script>

    <style type="text/css">
        body {
            padding: 0px;
            margin: 0px;
            position: absolute;
            width: 100vw;
            height: 100vh;
        }

        #main {
            width: 100%;
            height: 100%;
            position: absolute;
        }

        /*#container {
            display: none;
            width: 100%;
            height: 100%;
            position: absolute;
            background-color: rgba(128, 128, 128, 0.9);
        }*/

        #cube-container {
            margin: auto;
            top: 25%;
            width: 80%;
            height: 50%;
            position: relative;
            background-color: white;
            padding: 10px;
            border-radius: 10px;
        }

        #cube-content {
            width: 100%;
            height: 100%;
            position: absolute;
            top: 30%;
            margin: auto;
            left: 10%;
            overflow-x:scroll;
        }

       
        table, th, td {
            text-align: center;
            box-sizing: border-box;
            margin: auto;
        }

        #modal-body{
            margin-left: 3px;
        }

        /*table tr th:nth-of-type(2n), td:nth-of-type(2n) {
            border-right: 5px solid #f00;
        }*/

        /*table tr>th:nth-child(1n) {
            border-bottom: 5px solid #f00;
        }*/
        #l5 td{
            border-top: 5px solid #20356b;
            border-left: 4px solid  #20356b;
            border-right: 4px solid #20356b;
            padding: 0px;
            min-width: 60px;
        }

        #l1 td{
            border-bottom: 5px solid #20356b;
            border-left: 4px solid  #20356b;
            border-right: 4px solid #20356b;
            padding: 0px;
            min-width: 60px;
        }

        #l4 td, #l3 td, #l2 td {
            border-bottom: 3px solid #ffd800;
            border-top: 3px solid #ffd800;
            border-left: 4px solid  #20356b;
            border-right: 4px solid #20356b;
            padding: 0px;
            min-width: 60px;
        }
  
       

         tr td:nth-child(2n+2), th:nth-child(2n+2) {
            /*border-right-width: 6px;*/ /* 1px (default) + 5px thicker 
            border-left: 5px solid #20356b;*/
        }

         tr td:nth-child(2n+1), th:nth-child(2n+1) {
            /*border-right-width: 6px;*/ /* 1px (default) + 5px thicker 
            border-right: 2px solid #20356b;*/

        }
         #data-cell-table tr:nth-child(2) {
             border-top: 5px solid #20356b;
         }

         #data-cell-table tr:nth-child(5) {
             border-bottom: 5px solid #20356b;
         }

         #data-cell-table tr td:nth-child(0n+1) {
             border-left: 5px solid #20356b;
         }

         #data-cell-table tr td:nth-child(2n+0) {
             border-right: 5px solid #20356b;
         }

        #close-modal {
            padding: 0px;
            margin: 0px;
            right: 0px;
            position: relative;
            cursor: pointer;
            float: right;
        }

        #row-container {
            width: auto !important;
            overflow-x: auto;
            overflow-y: auto;
        }


        .btn-cell {
            width: 100%;
            display: flex;
            height: 50px;
            background-color: #34eb64;
            border: none;
        }

        .btn-pallet {
            width: 100%;
            display: flex;
            height: 50px;
            background-color: #34eb64;
            border: none;
        }

        
        #data-cell-modal, #pallet-modal{
            background-color: rgba(128, 128, 128, 0.9)
        }

        #data-cell-table{
            border: 1px solid;
        }

        #data-cell-table tr td{
            min-width: 60px;
            border: 1px solid;
            padding: 0px;
        }

        #table-header{
            border-top-style: hidden;
            border-right-style: hidden;
            border-left-style: hidden;
        }

        #pallet-container{
            width: 100%;
        }

        #pallet-content{
            width: 100%;
            display: flex;
            flex-direction: column;
            justify-content: center;

        }

        #pallet-footer{
            text-align: center;
            display: flex;
            width: 100%;
            flex-wrap: wrap;
            justify-content: space-around;
            align-items: center;
            text-align: left;
        }

    </style>

</head>
<body>
    <div id="main"></div>

    <!-- Modal -->
    <div class="modal fade" id="container" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="rackRoom"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body mx-3" >
                    <div id="row-container"> 
                        <div class="row">
                        <div class="col-12">
                            <table id="rack-table">
                                <tr>
                                    <th></th>
                                    <th class="tbnRackHead">R01</th>
                                    <th>R02</th>
                                    <th>R03</th>
                                    <th>R04</th>
                                    <th>R05</th>
                                    <th>R06</th>
                                    <th>R07</th>
                                    <th>R08</th>
                                    <th>R09</th>
                                    <th>R10</th>
                                    <th>R11</th>
                                    <th>R12</th>
                                    <th>R13</th>
                                    <th>R14</th>
                                    <th>R15</th>
                                    <th>R16</th>
                                    <th>R17</th>
                                    <th>R18</th>
                                    <th>R19</th>
                                    <th>R20</th>
                                </tr>
                                <tr id="l5">
                                    <th class="tbnRackHeadLeft">L5</th>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>

                                </tr>
                                <tr id="l4">
                                    <th>L4</th>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>

                                </tr>
                                <tr id="l3">
                                    <th>L3</th>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>

                                </tr>
                                <tr id="l2">
                                    <th>L2</th>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>

                                </tr>
                                <tr id="l1">
                                    <th>L1</th>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>
                                    <td>
                                        <button class="btn-cell"></button>
                                    </td>

                                </tr>
                            </table>
                        </div>
                    </div>

                    </div>
       

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" class="btn btn-primary">Understood</button>--%>
                </div>
             
            </div>
        </div>
    </div>

    <div class="modal fade" id="data-cell-modal" tabindex="-1" aria-labelledby="data-cell-title" aria-hidden="true">
         <div class="modal-dialog modal-dialog-centered">
         <div class="modal-content">
         <div class="modal-header">
         <h5 class="modal-title" id="data-cell-title"></h5>
         <button type="button" class="close" data-dismiss="modal" aria-label="Close">
         <span aria-hidden="true">&times;</span>
         </button>
        </div>
        <div class="modal-body">
            <table id="data-cell-table">
                    <tr>
                        <th colspan="2" id="table-header"></th>
                    </tr>
                    <tr>
                        <td><button class="btn-pallet"></button></td>
                        <td><button class="btn-pallet"></button></td>
                    </tr>

                    <tr>
                        <td><button class="btn-pallet"></button></td>
                        <td><button class="btn-pallet"></button></td>
                    </tr>

                    <tr>
                        <td><button class="btn-pallet"></button></td>
                        <td><button class="btn-pallet"></button></td>
                    </tr>

                    <tr>
                        <td><button class="btn-pallet"></button></td>
                        <td><button class="btn-pallet"></button></td>
                    </tr>

            </table>
        </div>
        <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        </div>
        </div>
        </div>
    </div>

    <div class="modal fade" id="pallet-modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
        <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Pallet</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
        </div>
        <div class="modal-body" id="pallet-container">
            <div id="pallet-content">
                <div class="form-group row">
                    <label for="docNumber" class="col-sm-4 col-form-label">Doc Number</label>
                    <div class="col-sm-8">
                        <input type="text" readonly class="form-control" id="docNumber">
                    </div>
                </div>

                <div class="form-group row">
                    <label for="itemCode" class="col-sm-4 col-form-label">Item Code</label>
                    <div class="col-sm-8">
                        <input type="text" readonly class="form-control" id="itemCode">
                    </div>
                </div>

                <div class="form-group row">
                    <label for="description" class="col-sm-4 col-form-label">Description</label>
                    <div class="col-sm-8">
                        <input type="text" readonly class="form-control" id="description">
                    </div>
                </div>

                <div class="form-group row">
                    <label for="customerCode" class="col-sm-4 col-form-label">Customer Code</label>
                    <div class="col-sm-8">
                        <input type="text" readonly class="form-control" id="customerCode">
                    </div>
                </div>

                <div class="form-group row">
                    <label for="palletID" class="col-sm-4 col-form-label">Pallet ID</label>
                    <div class="col-sm-8">
                        <input type="text" readonly class="form-control" id="palletID">
                    </div>
                </div>
                </div>
            </div>
        <div class="modal-footer" id="pallet-footer">
        <button type="button" id="download" class="btn btn-primary">Button 1</button>
        <button type="button" class="btn btn-primary">Button 2</button>
        <button type="button" class="btn btn-primary">Button 3</button>
        </div>
        </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.7.0.min.js"
        integrity="sha256-2Pmvv0kuTBOenSvLm6bvfBSSHrUJ+3A7x6P5Ebd07/g="
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js"
        integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s"
        crossorigin="anonymous"></script>

    <script type="module" src="../Assets/Editor/sampMapping.js"></script>
</body>
</html>
