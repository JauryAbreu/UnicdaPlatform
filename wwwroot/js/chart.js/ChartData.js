var doughnutChart = document.getElementById('donut-chart-canvas').getContext('2d');

var myDoughnutChart = new Chart(doughnutChart, {
    type: 'doughnut',
    data: {
        labels: ["...", "...", "..."],
        datasets: [{
            data: [0, 0, 0],
            backgroundColor: [
                "rgb(255, 198, 26)", // yellow
                "rgb(0, 0, 255)", // blue
                "rgb(255, 0, 0)" // red
            ]
        }]
    },
    options: {
        responsive: true,
        title: {
            display: false
        },
        legend: {
            display: true,
            labels: {
                fontColor: "white",
                fontSize: 18
            }
        }
    }
});

var barChart = document.getElementById('bar-chart-canvas').getContext('2d');

var myBarChart = new Chart(barChart, {
    type: 'horizontalBar',
    data: {
        labels: ["...", "...", "..."],
        datasets: [{
            data: [0, 0, 0],
            label: "Category",
            backgroundColor: [
                "rgb(255, 198, 26)", // yellow
                "rgb(0, 0, 255)", // blue
                "rgb(255, 0, 0)" // red
            ]
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    fontColor: "#fff",
                    beginAtZero: true
                }
            }],
            xAxes: [{
                ticks: {
                    fontColor: "#fff"
                }
            }]
        },
        legend: {
            display: false
        },
        responsive: true,
        //maintainAspectRatio: false,
        title: {
            display: false,
            text: ''
        },
        animation: {
            animateScale: true,
            animateRotate: true
        },
        tooltips: {
            callbacks: {
                label: function (item, data) {
                    return data.datasets[item.datasetIndex].label + ": " + data.labels[item.index] + ": " + data.datasets[item.datasetIndex].data[item.index];
                }
            }
        }
    }
});

var monthlyRecapChart = document.getElementById('monthly-recap-canvas').getContext('2d');

var myMonthlyRecapChart = new Chart(monthlyRecapChart, {
    type: 'line',
    data: {
        labels: getElapsedMonths(),
        datasets: [{
            data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            label: "Month",
            backgroundColor: "#e5e5e5"
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    fontColor: "#fff",
                    beginAtZero: true,
                    callback: function (label, index, labels) {
                        return label / 1000 + 'k';
                    },
                    scaleLabel: {
                        display: true,
                        labelString: '1k = 1000'
                    }
                }
            }],
            xAxes: [{
                ticks: {
                    fontColor: "#fff"
                }
            }]
        },
        legend: {
            display: false
        },
        responsive: true,
        title: {
            display: false,
            text: ''
        },
        animation: {
            animateScale: true,
            animateRotate: true
        },
        tooltips: {
            callbacks: {
                label: function (item, data) {
                    return data.datasets[item.datasetIndex].label + ": " + data.labels[item.index] + ": " + data.datasets[item.datasetIndex].data[item.index];
                }
            }
        }
    }
});

$(document).ready(function () {
    GetSalesByCategoryMonth();
    GetMonthlyRecap();
});

$("#btnMonthly").click(function () {
    GetSalesByCategoryMonth();
});

$("#btnDaily").click(function () {
    GetSalesByCategoryToday();
});

function GetSalesByCategoryMonth() {
    $.ajax({
        type: 'GET',
        url: '@Url.Page("index", "SalesByCategoryMonth")',
        success: function (data) {
            if (data[0] !== null) {
                var arrayCategories = data[0].split(";");
                var arraySales = data[1].split(";");

                myDoughnutChart.data.labels = arrayCategories;
                myDoughnutChart.data.datasets[0].data = arraySales;
                myDoughnutChart.data.datasets[0].backgroundColor = asignColor(arraySales);

                if (myDoughnutChart !== undefined) {
                    myDoughnutChart.update();
                }

                myBarChart.data.labels = arrayCategories;
                myBarChart.data.datasets[0].data = arraySales;
                myBarChart.data.datasets[0].backgroundColor = asignColor(arraySales);

                if (myBarChart !== undefined) {
                    myBarChart.update();
                }
            }
        }
    });
}

function GetSalesByCategoryToday() {
    $.ajax({
        type: 'GET',
        url: '@Url.Page("index", "SalesByCategoryToday")',
        success: function (data) {
            if (data[0] !== null) {
                var arrayCategories = data[0].split(";");
                var arraySales = data[1].split(";");

                myDoughnutChart.data.labels = arrayCategories;
                myDoughnutChart.data.datasets[0].data = arraySales;
                myDoughnutChart.data.datasets[0].backgroundColor = asignColor(arraySales);

                if (myDoughnutChart !== undefined) {
                    myDoughnutChart.update();
                }

                myBarChart.data.labels = arrayCategories;
                myBarChart.data.datasets[0].data = arraySales;
                myBarChart.data.datasets[0].backgroundColor = asignColor(arraySales);

                if (myBarChart !== undefined) {
                    myBarChart.update();
                }
            }
        }
    });
}

function GetMonthlyRecap() {
    $.ajax({
        type: 'GET',
        url: '@Url.Page("index", "MonthlyRecapReport")',
        success: function (data) {
            if (data[0] !== null) {
                myMonthlyRecapChart.data.datasets[0].data = data;

                if (myMonthlyRecapChart !== undefined) {
                    myMonthlyRecapChart.update();
                }
            }
        }
    });
}

function randomColor(brightness) {
    function randomChannel(brightness) {
        var r = 255 - brightness;
        var n = 0 | ((Math.random() * r) + brightness);
        var s = n.toString(16);
        return (s.length === 1) ? '0' + s : s;
    }
    return '#' + randomChannel(brightness) + randomChannel(brightness) + randomChannel(brightness);
}

function asignColor(color) {
    var storeColor = [];
    for (var i = 0; i < color.length; {
        storeColor.push(randomColor(1));
    }

        return storeColor;
}

function getElapsedMonths() {
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var d = new Date();
    var currentMonth = d.getMonth() + 1; //Se agrega el +1 porque la funcion cuenta los meses desde 0.
    var elapsedMonths = [];

    for (var i = 0; i < currentMonth; {
        elapsedMonths.push(monthNames[i])
    }

            return elapsedMonths;
}