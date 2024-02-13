let DateStart;
let DateEnd;
let YieldChart;      
let perfectShipment;
let cleanInvoice;
let Customer;
let Warehouse;
let Area;
let TransType;

$(document).ready(async function () {
   

    await Parameters(); // Setup parameters

});


document.addEventListener("DOMContentLoaded", () => {
    
    $('#daterangetrans').daterangepicker({
        opens: 'center'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        DateStart = start.format('YYYY-MM-DD');
        DateEnd = end.format('YYYY-MM-DD');
        updateData();
        console.log(DateStart)
        console.log(DateEnd)
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

        //var datesd = new Date(bookdate);
        var currentDate = new Date();
        var datesd = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1);;
        console.log(datesd);
        var dateEd = new Date();

        $('#daterangetrans').data('daterangepicker').setStartDate(datesd);
        $('#daterangetrans').data('daterangepicker').setEndDate(dateEd);
        DateStart = datesd.getFullYear() + '-' + ('0' + (datesd.getMonth() + 1)).slice(-2) + '-' + ('0' + datesd.getDate()).slice(-2);
        DateEnd = dateEd.getFullYear() + '-' + ('0' + (dateEd.getMonth() + 1)).slice(-2) + '-' + ('0' + dateEd.getDate()).slice(-2);
        
        GetData();
        
    }

});

});

function opentabs(evt, TabID) {
    //loaderin();
    evt.preventDefault();
    evt.target.classList.add('active')
    console.log('check');    //fix chart display when changing parameters 
    window.dispatchEvent(new Event('resize'));
    //fix datatables display when changing parameters 
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });

    var tabs = document.querySelectorAll('.nav-link');
    tabs.forEach(function (tab) {
        tab.classList.remove('active');
    });
    //var i, tabcontent, tablinks;
    //tabcontent = document.getElementsByClassName("tab-pane");
    //for (i = 0; i < tabcontent.length; i++) {
    //    tabcontent[i].style.display = "none";
    //}
    //document.getElementById(TabID).style.display = "block";
    //var clicked = 'INBOUND';
    //updateData(clicked)
    //GetData(clicked)
    
    if (TabID === 'tab_default_1') {
        var clicked = 'INBOUND';
        GetData(clicked);
        updateData(clicked)
    }
    else if (TabID === 'tab_default_2') {
        var clicked = 'OUTBOUND';
        GetData(clicked);
        updateData(clicked)
    }
    else if (TabID === 'tab_default_3') {
        //CODE HERE
    }
    else if (TabID === 'tab_default_4') {
        //CODE HERE
    }
    else if (TabID === 'tab_default_5') {
        //CODE HERE
    }
    else {
        var clicked = 'INBOUND';
        GetData(clicked);
        updateData(clicked)
        var tab1 = document.getElementById('tab-inbound')
        tab1.classList.add('active')
    }
 
    //loaderout();
}

function updateValue() {
        updateData
    }
    

var reviewDash = document.getElementById('reviewDash');
reviewDash.addEventListener('click', opentabs)




function GetData(clicked) {
    Customer = $('#Customer').val();
    Area = $('#Area').val();
    TransType = 'INBOUND';
    document.getElementById('tab_inbound')
    
    let parameters = {
        DateStart,
        DateEnd,
        TransType,
        Customer,
        Area,
        }
    $.ajax({
        type: "POST",
        url: "frmDashboardBasic2.aspx/GetData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{_data:' + JSON.stringify(parameters) + '}',
        success: function (data) {
            // CARDS START
            let Card1 = JSON.parse(data.d[0]);
            let Card2 = JSON.parse(data.d[1]);
            let Card3 = JSON.parse(data.d[2]);
            let Card4 = JSON.parse(data.d[3]);
            let Card5 = JSON.parse(data.d[4]);
            let Card6 = JSON.parse(data.d[5]);
            let Card7 = JSON.parse(data.d[6]);

            let Chart1 = JSON.parse(data.d[9]);
            let Chart2 = JSON.parse(data.d[10]);
            let Chart3 = JSON.parse(data.d[11]);

            let Cold = JSON.parse(data.d[12]);
            let Dry = JSON.parse(data.d[13]);
            let Aircon = JSON.parse(data.d[14]);
            let Chiller = JSON.parse(data.d[15]);


            let dates = Chart1.map(entry => entry.x);
            let dates2 = Chart2.map(entry => entry.x);
            let dates3 = Chart3.map(entry => entry.x);

            console.log(dates);
            document.getElementById('New').innerHTML = Card1[0].Count;
            document.getElementById('Pending').innerHTML = Card2[0].Count;
            document.getElementById('Ongoing').innerHTML = Card3[0].Count;
            document.getElementById('Submitted').innerHTML = Card4[0].Count;
            document.getElementById('Cancel').innerHTML = Card5[0].Count;
            document.getElementById('totalTransactions').innerHTML = Card6[0].Count;
            document.getElementById('Billed').innerHTML = Card7[0].Count;

            document.getElementById('Cold').innerHTML = Cold[0].Count;
            document.getElementById('Dry').innerHTML = Dry[0].Count;
            document.getElementById('Aircon').innerHTML = Aircon[0].Count;
            document.getElementById('Chiller').innerHTML = Chiller[0].Count;

            // CARDS END

            table(data);

            // Yield START
            let result1 = Chart1.reduce(function (r, a) {
                r[a.name] = r[a.name] || [];
                r[a.name].push({ x: a.x, y: a.y });
                return r;
            }, Object.create(null));
            console.log(Chart1);
            console.log(result1);
            let seriesYield = [];

            const totalValues1 = Object.keys(result1).reduce((total, category) => {
                total[category] = result1[category].reduce((sum, item) => sum + item.y, 0);
                return total;
            }, {});

            // Convert counts to percentages
            const result1Percentages = Object.keys(result1).reduce((percentages, category) => {
                percentages[category] = result1[category].map(item => ({
                    x: item.x,
                    y: (item.y / totalValues1[category]) * 100, // Convert to percentage
                }));
                return percentages;
            }, {});

            if (result1.NEW) seriesYield.push({ name: 'NEW', type: 'bar', data: result1.NEW, color: '#007bff' });
            if (result1.PENDING) seriesYield.push({ name: 'PENDING', type: 'bar', data: result1.PENDING, color: '#fd7e14' });
            if (result1.ONGOING) seriesYield.push({ name: 'ONGOING', type: 'bar', data: result1.ONGOING, color: '#7E8299' });
            if (result1.SUBMITTED) seriesYield.push({ name: 'SUBMITTED', type: 'bar', data: result1.SUBMITTED, color: '#ffc107' });
            if (result1.CANCELLED) seriesYield.push({ name: 'CANCELLED', type: 'bar', data: result1.CANCELLED, color: '#dc3545' });
            console.log(seriesYield);

            options2 = {
                series: seriesYield,
                //markers: {
                //    size: 0,
                //    style: 'hollow',
                //    hover: {
                //        opacity: 5,
                //    }
                //},
                plotOptions: {
                    bar: {
                        horizontal: false, // Set to true if you want horizontal bars
                        //columnWidth: '100px', // Set the width of the bars (percentage or pixels)
                        //barWidth: 50,
                        barHeight: '100%', // Set the height of the bars (percentage or pixels)
                        //distributed: false, // Set to true for distributed bars
                    }
                },
                xaxis: {
                    type: 'numeric',
                    //categories: ,
                    //title: {
                    //    text: 'storage'
                    //},
                    fill: {
                        type: 'gradient',
                        gradient: {
                            shade: 'dark',
                            type: "horizontal",
                            shadeIntensity: 0.5,
                            gradientToColors: undefined, // optional, if not defined - uses the shades of same color in series
                            inverseColors: true,
                            opacityFrom: 1,
                            opacityTo: 1,
                            stops: [0, 50, 100],
                            colorStops: []
                        }
                    }
                },
                chart: {
                    type: 'bar',
                    options3d: {
                        enabled: true,
                        alpha: 15,
                        beta: 30,
                        depth: 200
                    },
                    dropShadow: {
                        enabled: true,
                        top: 1, // Shadow position from the top
                        left: 1, // Shadow position from the left
                        blur: 3, // Shadow blur radius
                        opacity: 0.2, // Shadow opacity
                    },
                    height: 300,
                    stacked: true,
                    //heigh: '80px',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                            customIcons: []
                        },
                        export: {
                            csv: {
                                filename: 'daily',
                                headerCategory: 'category',
                                headerValue: 'value',
                            },
                            svg: {
                                filename: 'daily',
                            },
                            png: {
                                filename: 'daily',
                            }
                        },
                        autoSelected: 'zoom'
                    },

                },

                dataLabels: {
                    enabled: true,
                    position: 'top',
                    dropShadow: {
                        enabled: true,
                        left: 2,
                        top: 2,
                        opacity: 0.5
                    }
                },
                //stroke: {
                //    curve: 'smooth',
                //    width: [2, 2],
                //},
                colors: ["#3F51B5", '#2196F3'],

                noData: {
                    text: 'No data'
                },
                legend: {
                    show: false,
                    showForSingleSeries: false,
                    showForNullSeries: true,
                    showForZeroSeries: true,
                    position: 'top',
                    horizontalAlign: 'center',
                    floating: false,
                    fontSize: '10px',

                    fontWeight: 400,
                    formatter: undefined,
                    inverseOrder: false,
                    width: undefined,
                    height: undefined,
                    tooltipHoverFormatter: undefined,
                    customLegendItems: [],
                    offsetX: 0,
                    offsetY: 0,
                    labels: {
                        
                    },
                    markers: {
                        width: 12,
                        height: 12,
                        strokeWidth: 0,
                        strokeColor: '#fff',
                        fillColors: undefined,
                        radius: 12,
                        customHTML: undefined,
                        onClick: undefined,
                        offsetX: 0,
                        offsetY: 0
                    },
                    itemMargin: {
                        horizontal: 5,
                        vertical: 0
                    },
                    onItemClick: {
                        toggleDataSeries: true
                    },
                    onItemHover: {
                        highlightDataSeries: true
                    },
                }

            };

            YieldChart = new ApexCharts(document.querySelector("#Yield"), options2);
            YieldChart.render();


            //let result2 = Chart2.reduce(function (r, a) {
            //    r[a.name] = r[a.name] || [];
            //    r[a.name].push({ x: a.x, y: a.y });
            //    return r;
            //}, Object.create(null));

            //let seriesShipment = [];

            //const totalValues = {};
            //const combinedData = [];

            //Object.keys(result2).forEach(category => {
            //    totalValues[category] = result2[category].reduce((total, item) => total + item.y, 0);
            //});

            //Object.keys(result2).forEach(category => {
            //    result2[category].forEach(item => {
            //        const percentage = (item.y / totalValues[category]) * 100;
            //        combinedData.push({ category, x: item.x, y: percentage });
            //    });
            //});

            //if (result2.NEW) seriesShipment.push({ name: 'NEW', type: 'bar', data: result2.NEW, color: '#0000FF'});
            //if (result2.PENDING) seriesShipment.push({ name: 'PENDING', type: 'bar', data: result2.PENDING, color: '#0000FF' });
            //if (result2.ONGOING) seriesShipment.push({ name: 'ONGOING', type: 'bar', data: result2.ONGOING, color: '#0000FF' });
            //if (result2.SUBMITTED) seriesShipment.push({ name: 'SUBMITTED', type: 'bar', data: result2.SUBMITTED, color: '#0000FF' });
            //if (result2.CANCELLED) seriesShipment.push({ name: 'CANCELLED', type: 'bar', data: result2.CANCELLED, color: '#0000FF' });


            ////Perfect Transaction
            //options3 = {
            //    series: seriesShipment,
            //    markers: {
            //        size: 0,
            //        style: 'hollow',
            //        hover: {
            //            opacity: 5,
            //        }
            //    },
            //    plotOptions: {
            //        bar: {
            //            horizontal: false, // Set to true if you want horizontal bars
            //            //columnWidth: '100px', // Set the width of the bars (percentage or pixels)
            //            barWidth: 100,
            //            //barHeight: '100%', // Set the height of the bars (percentage or pixels)
            //            //distributed: false, // Set to true for distributed bars
            //        }
            //    },

            //    xaxis: {
            //        //categories: dates,
            //        //title: {
            //        //    text: 'Dates'
            //        //},
            //        fill: {
            //            type: 'gradient',
            //            gradient: {
            //                shade: 'dark',
            //                type: "horizontal",
            //                shadeIntensity: 0.5,
            //                gradientToColors: undefined, // optional, if not defined - uses the shades of same color in series
            //                inverseColors: true,
            //                opacityFrom: 1,
            //                opacityTo: 1,
            //                stops: [0, 50, 100],
            //                colorStops: []
            //            }
            //        }


            //    },
            //    chart: {
            //        type: 'bar',
            //        options3d: {
            //            enabled: true,
            //            alpha: 15,
            //            beta: 30,
            //            depth: 200
            //        },
            //        //dropShadow: {
            //        //    enabled: false,
            //        //    enabledOnSeries: undefined,
            //        //    top: 0,
            //        //    left: 0,
            //        //    blur: 3,
            //        //    color: '#000',
            //        //    opacity: 0.35
            //        //},
            //        height: 300,
            //        stacked: true,
            //        //stackType: '100%',
            //        toolbar: {
            //            show: true,       
            //            offsetX: 0,
            //            offsetY: 0,
            //            tools: {
            //                download: true,
            //                selection: true,
            //                zoom: true,
            //                zoomin: true,
            //                zoomout: true,
            //                pan: true,
            //                reset: true | '<img src="/static/icons/reset.png" width="20">',
            //                customIcons: []
            //            },
            //            export: {
            //                csv: {
            //                    filename: 'daily',
            //                    headerCategory: 'category',
            //                    headerValue: 'value',
            //                },
            //                svg: {
            //                    filename: 'daily',
            //                },
            //                png: {
            //                    filename: 'daily',
            //                }
            //            },
            //            autoSelected: 'zoom'
            //        },

            //    },
            //    dataLabels: {
            //        enabled: true,
            //        position: 'top',
            //        dropShadow: {
            //            enabled: true,
            //            left: 2,
            //            top: 2,
            //            opacity: 0.5
            //        }
            //    },
            //        //stroke: {
            //        //    curve: 'smooth',
            //        //    width: [2, 6],
            //        //    color: ['#000000']
            //        //},
            //    colors: ["#3F51B5", '#2196F3'],

            //    noData: {
            //        text: 'No data'
            //    },
                
            //    legend: {
            //        show: false,
            //        showForSingleSeries: false,
            //        showForNullSeries: true,
            //        showForZeroSeries: true,
            //        position: 'top',
            //        horizontalAlign: 'center',
            //        floating: false,
            //        fontSize: '10px',
            //        fontWeight: 400,
            //        formatter: undefined,
            //        inverseOrder: false,
            //        width: undefined,
            //        height: undefined,
            //        tooltipHoverFormatter: undefined,
            //        customLegendItems: [],
            //        offsetX: 0,
            //        offsetY: 0,
            //        labels: {
            //            colors: undefined,
            //            useSeriesColors: false
            //        },
            //        markers: {
            //            width: 12,
            //            height: 12,
            //            strokeWidth: 0,
            //            strokeColor: '#fff',
            //            fillColors: undefined,
            //            radius: 12,
            //            customHTML: undefined,
            //            onClick: undefined,
            //            offsetX: 0,
            //            offsetY: 0
            //        },
            //        itemMargin: {
            //            horizontal: 5,
            //            vertical: 0
            //        },
            //        onItemClick: {
            //            toggleDataSeries: true
            //        },
            //        onItemHover: {
            //            highlightDataSeries: true
            //        },
            //    }

            //};

            //perfectShipment = new ApexCharts(document.querySelector("#perfectShipment"), options3);
            //perfectShipment.render();


            // CLEAN INVOICE //
            let result3 = Chart3.reduce(function (r, a) {
                r[a.name] = r[a.name] || [];
                r[a.name].push({ x: a.x, y: a.y });
                return r;
            }, Object.create(null));

            let seriesInvoice = [];

            //Object.keys(result3).forEach(key => {
            //    seriesInvoice.push({ name: key, type: 'bar', data: result3[key], color: '#30cfcc' });
            //});

            //if (result3.COLD) seriesInvoice.push({ name: 'COLD', type: 'bar', data: result3.COLD, color: '#30cfcc' });
            //if (result3.DRY) seriesInvoice.push({ name: 'DRY', type: 'bar', data: result3.DRY, color: '#30cfcc' });
            //if (result3.AIRCON) seriesInvoice.push({ name: 'AIRCON', type: 'bar', data: result3.AIRCON, color: '#30cfcc' });
            //if (result3.CHILLER) seriesInvoice.push({ name: 'CHILLER', type: 'bar', data: result3.CHILLER, color: '#30cfcc' });

            if (result3.NEW) seriesInvoice.push({ name: 'NEW', type: 'bar', data: result3.NEW, color: '#0000FF' });
            if (result3.PENDING) seriesInvoice.push({ name: 'PENDING', type: 'bar', data: result3.PENDING, color: '#0000FF' });
            if (result3.ONGOING) seriesInvoice.push({ name: 'ONGOING', type: 'bar', data: result3.ONGOING, color: '#0000FF' });
            if (result3.SUBMITTED) seriesInvoice.push({ name: 'SUBMITTED', type: 'bar', data: result3.SUBMITTED, color: '#0000FF' });
            if (result3.CANCELLED) seriesInvoice.push({ name: 'CANCELLED', type: 'bar', data: result3.CANCELLED, color: '#0000FF' });



            //Clean Invoice
            options4 = {
                series: seriesInvoice,
                markers: {
                    size: 0,
                    style: 'hollow',
                    hover: {
                        opacity: 5,
                    }
                },  

                xaxis: {
                    //categories: dates,
                    //title: {
                    //    text: 'Dates'
                    //},
                    fill: {
                        type: 'gradient',
                        gradient: {
                            shade: 'dark',
                            type: "horizontal",
                            shadeIntensity: 0.5,
                            gradientToColors: undefined, // optional, if not defined - uses the shades of same color in series
                            inverseColors: true,
                            opacityFrom: 1,
                            opacityTo: 1,
                            stops: [0, 50, 100],
                            colorStops: []
                        }
                    }


                },
                chart: {
                    type: 'bar',
                    options3d: {
                        enabled: true,
                        alpha: 15,
                        beta: 30,
                        depth: 200
                    },
                    dropShadow: {
                        enabled: true,
                        top: -10, // Shadow position from the top
                        left: 1, // Shadow position from the left
                        blur: 20, // Shadow blur radius
                        opacity: .3, // Shadow opacity
                    },
                    height: 300,
                    stacked: true,
                    //stackType: '100%',
                    //heigh: '80px',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                            customIcons: []
                        },
                        export: {
                            csv: {
                                filename: 'daily',
                                headerCategory: 'category',
                                headerValue: 'value',
                            },
                            svg: {
                                filename: 'daily',
                            },
                            png: {
                                filename: 'daily',
                            }
                        },
                        autoSelected: 'zoom'
                    },

                },

                dataLabels: {
                    enabled: true,
                    position: 'top',
                    dropShadow: {
                        enabled: true,
                        left: 2,
                        top: 2,
                        opacity: 0.5
                    }
                },
                //stroke: {
                //    curve: 'smooth',
                //    width: [2, 2],
                //},
                colors: ["#3F51B5", '#2196F3'],

                noData: {
                    text: 'No data'
                },
                legend: {
                    show: false,
                    showForSingleSeries: false,
                    showForNullSeries: true,
                    showForZeroSeries: true,
                    position: 'top',
                    horizontalAlign: 'center',
                    floating: false,
                    fontSize: '10px',

                    fontWeight: 400,
                    formatter: undefined,
                    inverseOrder: false,
                    width: undefined,
                    height: undefined,
                    tooltipHoverFormatter: undefined,
                    customLegendItems: [],
                    offsetX: 0,
                    offsetY: 0,
                    labels: {
                        colors: undefined,
                        useSeriesColors: false
                    },
                    markers: {
                        width: 12,
                        height: 12,
                        strokeWidth: 0,
                        strokeColor: '#fff',
                        fillColors: undefined,
                        radius: 12,
                        customHTML: undefined,
                        onClick: undefined,
                        offsetX: 0,
                        offsetY: 0
                    },
                    itemMargin: {
                        horizontal: 5,
                        vertical: 0
                    },
                    onItemClick: {
                        toggleDataSeries: true
                    },
                    onItemHover: {
                        highlightDataSeries: true
                    },
                }
            };

            cleanInvoice = new ApexCharts(document.querySelector("#cleanInvoice"), options4);
            cleanInvoice.render();


            //loaderout();
        },
        error: function (error) {
            Swal.fire("", error.responseJSON.Message, "error");
        }
    
    });

};

function updateData(clicked) {
    TransType = clicked;
    Customer = $('#Customer').val();
    Area = $('#Area').val();

    let parameters = {
        DateStart,
        DateEnd,
        TransType,
        Customer,
        Area
        }
    $.ajax({
        type: "POST",
        url: "frmDashboardBasic2.aspx/GetData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{_data:' + JSON.stringify(parameters) + '}',
        success: function (data) {
            // CARDS START
            let Card1 = JSON.parse(data.d[0]);
            let Card2 = JSON.parse(data.d[1]);
            let Card3 = JSON.parse(data.d[2]);
            let Card4 = JSON.parse(data.d[3]);
            let Card5 = JSON.parse(data.d[4]);
            let Card6 = JSON.parse(data.d[5]);
            let Card7 = JSON.parse(data.d[6]);

            let Chart1 = JSON.parse(data.d[9]);
            let Chart2 = JSON.parse(data.d[10]);
            let Chart3 = JSON.parse(data.d[11]);


            let Cold = JSON.parse(data.d[12]);
            let Dry = JSON.parse(data.d[13]);
            let Aircon = JSON.parse(data.d[14]);
            let Chiller = JSON.parse(data.d[15]);


            let dates = Chart1.map(entry => entry.x);
            let dates2 = Chart2.map(entry => entry.x);
            let dates3 = Chart3.map(entry => entry.x);


            document.getElementById('New').innerHTML = Card1[0].Count;
            document.getElementById('Pending').innerHTML = Card2[0].Count;
            document.getElementById('Ongoing').innerHTML = Card3[0].Count;
            document.getElementById('Submitted').innerHTML = Card4[0].Count;
            document.getElementById('Cancel').innerHTML = Card5[0].Count;
            document.getElementById('totalTransactions').innerHTML = Card6[0].Count;
            document.getElementById('Billed').innerHTML = Card7[0].Count;

            document.getElementById('Cold').innerHTML = Cold[0].Count;
            document.getElementById('Dry').innerHTML = Dry[0].Count;
            document.getElementById('Aircon').innerHTML = Aircon[0].Count;
            document.getElementById('Chiller').innerHTML = Chiller[0].Count;


            // CARDS END

            //Yield START
            let result1 = Chart1.reduce(function (r, a) {
                r[a.name] = r[a.name] || [];
                r[a.name].push({ x: a.x, y: a.y });
                return r;
            }, Object.create(null));
            console.log(result1);
            let seriesYield = [];

       
            if (result1.NEW) seriesYield.push({ name: 'NEW', type: 'bar', data: result1.NEW, color: '#007bff' });
            if (result1.PENDING) seriesYield.push({ name: 'PENDING', type: 'bar', data: result1.PENDING, color: '#fd7e14' });
            if (result1.ONGOING) seriesYield.push({ name: 'ONGOING', type: 'bar', data: result1.ONGOING, color: '#7E8299' });
            if (result1.SUBMITTED) seriesYield.push({ name: 'SUBMITTED', type: 'bar', data: result1.SUBMITTED, color: '#ffc107' });
            if (result1.CANCELLED) seriesYield.push({ name: 'CANCELLED', type: 'bar', data: result1.CANCELLED, color: '#dc3545' });
            YieldChart.updateSeries(seriesYield);

            
            // PERFECT SHIPMENT EXCLUDED // 
            //let result2 = Chart2.reduce(function (r, a) {
            //    r[a.name] = r[a.name] || [];
            //    r[a.name].push({ x: a.x, y: a.y });
            //    return r;
            //}, Object.create(null));
            //console.log(result2);
            //let seriesShipment = [];

            ////if (result2.COLD) seriesShipment.push({ name: 'COLD', type: 'bar', data: result2.COLD, color: '#30cfcc' });
            ////if (result2.DRY) seriesShipment.push({ name: 'DRY', type: 'bar', data: result2.DRY, color: '#30cfcc' });
            ////if (result2.AIRCON) seriesShipment.push({ name: 'AIRCON', type: 'bar', data: result2.AIRCON, color: '#30cfcc' });
            ////if (result2.CHILLER) seriesShipment.push({ name: 'CHILLER', type: 'bar', data: result2.CHILLER, color: '#30cfcc' });
            //if (result2.NEW) seriesShipment.push({ name: 'NEW', type: 'bar', data: result2.NEW, color: '#007bff' });
            //if (result2.PENDING) seriesShipment.push({ name: 'PENDING', type: 'bar', data: result2.PENDING, color: '#fd7e14' });
            //if (result2.ONGOING) seriesShipment.push({ name: 'ONGOING', type: 'bar', data: result2.ONGOING, color: '#7E8299' });
            //if (result2.SUBMITTED) seriesShipment.push({ name: 'SUBMITTED', type: 'bar', data: result2.SUBMITTED, color: '#ffc107' });
            //if (result2.CANCELLED) seriesShipment.push({ name: 'CANCELLED', type: 'bar', data: result2.CANCELLED, color: '#dc3545' });
            //perfectShipment.updateSeries(seriesShipment);

            

            let result3 = Chart3.reduce(function (r, a) {
                r[a.name] = r[a.name] || [];
                r[a.name].push({ x: a.x, y: a.y });
                return r;
            }, Object.create(null));

            let seriesInvoice = [];

            //if (result3.COLD) seriesInvoice.push({ name: 'COLD', type: 'bar', data: result3.COLD, color: '#30cfcc' });
            //if (result3.DRY) seriesInvoice.push({ name: 'DRY', type: 'bar', data: result3.DRY, color: '#30cfcc' });
            //if (result3.AIRCON) seriesInvoice.push({ name: 'AIRCON', type: 'bar', data: result3.AIRCON, color: '#30cfcc' });
            //if (result3.CHILLER) seriesInvoice.push({ name: 'CHILLER', type: 'bar', data: result3.CHILLER, color: '#30cfcc' });

            if (result3.NEW) seriesInvoice.push({ name: 'NEW', type: 'bar', data: result3.NEW, color: '#0000FF' });
            if (result3.PENDING) seriesInvoice.push({ name: 'PENDING', type: 'bar', data: result3.PENDING, color: '#0000FF' });
            if (result3.ONGOING) seriesInvoice.push({ name: 'ONGOING', type: 'bar', data: result3.ONGOING, color: '#0000FF' });
            if (result3.SUBMITTED) seriesInvoice.push({ name: 'SUBMITTED', type: 'bar', data: result3.SUBMITTED, color: '#0000FF' });
            if (result3.CANCELLED) seriesInvoice.push({ name: 'CANCELLED', type: 'bar', data: result3.CANCELLED, color: '#0000FF' });
            cleanInvoice.updateSeries(seriesInvoice);


            table(data);


            //loaderout();
        },
        error: function (error) {
            Swal.fire("", error.responseJSON.Message, "error");
        }
    });
}


//TABLE START

function table(data) {


    let Table1 = JSON.parse(data.d[4]);
    let Table2 = JSON.parse(data.d[6]);

    (async () => {
        await ConstTABLE(2, Table1, 'Table1-datatable', 0, 'asc');

    })();
        
    (async () => {
        await ConstTABLE(2, Table2, 'Table2-datatable', 0, 'asc');

    })();
        
};

//TABLE END


//CHART START

    function chart(data) {


    //    let Chart1 = JSON.parse(data.d[6]);
    //    let Chart2 = JSON.parse(data.d[8]);

    //    (async () => {
    //        await ConstTABLE(2, Table1, 'Table1-datatable', 0, 'asc');

    //})();
        
};



//CHART END

    //const calendarpickDiv = document.getElementById('calendarpick');

    //// Add a click event listener
    //calendarpickDiv.addEventListener('click', function() {
    //    // Code to execute when the div is clicked
    //    console.log('The calendarpick div was clicked!');

    //    const tdElement = document.querySelector('td.active.day');
    //    let  value = tdElement.textContent;


    //    const thElement = document.querySelector('th.datepicker-switch');
    //    let monthyear = thElement.textContent;
    //    monthyear = monthyear.split(" ");
    //    console.log(monthyear[1]);

    //    value = monthyear[0] + ' ' + value + ', ' + monthyear[1]

    //    let dateComponents = value.split(' ');

    //    // Extracting month, day, and year
    //    let month = dateComponents[0];
    //    let day = dateComponents[1].replace(',', ''); // Removing the comma from the day
    //    let year = dateComponents[2];

    //    // Converting month to numeric value
    //    let monthIndex = new Date(Date.parse(month + ' 1, 2000')).getMonth() + 1;
    //    let formattedDate = `${monthIndex.toString().padStart(2, '0')}/${day.padStart(2, '0')}/${year}`;
    //    console.log(formattedDate);
    //    console.log( value);
    //});


// FUNCTION: Fetch and setup parameters data
async function Parameters() {
    $.ajax({
        type: "POST",
        url: "frmDashboardBasic2.aspx/Parameter",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var vals = ["ALL"];

            // CustomerCode SELECT START
            let dataCC = JSON.parse(data.d[0]);
            let options = "<option value='Code' disabled='disabled'>Name</option><option value='ALL'>ALL</option>";

            dataCC.forEach((obj) => {
                options += "<option value='" + obj.Code + "'>" + obj.Name + "</option>";
                console.log(obj.Code)
            });

            $('#Customer').append(options);
            //$('#paramCustomerCode').val(vals).trigger("change"); 
            //// CustomerCode SELECT END

            // AreaCode SELECT START
            let dataA = JSON.parse(data.d[1]);
            let dataDA = JSON.parse(data.d[7]);
            //console.log(dataDA[0].Code);
            //options = "<option value='Code' disabled='disabled'>Name</option><option value=" + dataA[0].Code + ">ALL</option>";
            options = "<option value='Code' disabled='disabled'>Area</option><option value='ALL'>ALL</option>";

            dataA.forEach((obj) => {
                options += "<option value='" + obj.Code + "'>" + obj.Code + "</option>";
                console.log(obj.code)
            });
            
            $('#Area').append(options);
            // AreaCode SELECT END

            //// WarehouseCode SELECT START
            //let dataWC = JSON.parse(data.d[2]);
            //options = "<option value='Code' disabled='disabled'>Name</option><option value='ALL'>ALL</option>";

            //dataWC.forEach((obj) => {
            //    options += "<option value='" + obj.Code + "'>" + obj.Name + "</option>";
            //});

            //$('#paramWarehouse').append(options);
            //$('#paramWarehouse').val(vals).trigger("change"); 
            //// WarehouseCode SELECT END

            // TruckType SELECT START
            //let dataTT = JSON.parse(data.d[3]);
            //options = "<option value='Code' disabled='disabled'>Name</option><option value='ALL'>ALL</option>";

            //dataTT.forEach((obj) => {
            //    options += "<option value='" + obj.Code + "'>" + obj.Name + "</option>";
            //});

            //$('#paramTruck').append(options);
            // TruckType SELECT END

            //// Year SELECT START
            //let dataYear = JSON.parse(data.d[4]);
            //var YR = dataYear;
            //options = "<option value='Code' disabled='disabled'>Name</option><option value=" + YR[0].Year + ">" + YR[0].Year + "</option>";

            //dataYear.forEach((obj) => {
            //    options += "<option value='" + obj.Year + "'>" + obj.Year + "</option>";
            //});

            //$('#paramYear').append(options);
            //// Year SELECT END

            //$('select.parameters').select2({
            //    width: "100px",
            //    //dropdownCssClass: "dropdowncss",
            //    templateResult: templateResult2,
            //    templateSelection: templateSelection,
            //    matcher: matchStart
            //});

            //// Month SELECT START
            //let dataMonth = JSON.parse(data.d[5]);

            //const d = new Date();
            //let month = d.getMonth() + 1;
            //options = "<option value='Code' disabled='disabled'>Name</option><option value=" + month + ">" + month + "</option>";

            //dataMonth.forEach((obj) => {
            //    options += "<option value='" + obj.Month + "'>" + obj.MonthName + "</option>";
            //});

            //$('#paramMonth').append(options);
            //// Month SELECT END


            //// Client SELECT START
            //let dataclient = JSON.parse(data.d[6]);

            //options = "<option value='Code' disabled='disabled'>Name</option><option value=''></option>";

            //dataclient.forEach((obj) => {
            //    options += "<option value='" + obj.Code + "'>" + obj.Name + "</option>";
            //});

            //$('#paramClient').append(options);
            //// Client SELECT END



            //GetData(); // Setup Charts
            //updateSeries();
        },
        error: function (error) {
            Swal.fire("", error.responseJSON.Message, "error");
        }

    });
}
