// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Bar Chart Example
var ctx = document.getElementById("myBarChart");
var myLineChart = new Chart(ctx, {
  type: 'bar',
  data: {
      labels: ["Broccoli", "Carrot", "Tomato", "Cucumber", "Watermelon", "Grapes", "Banana", "Peach", "Mango", "Orange", "Hazelnut"],
    datasets: [{
      label: "Revenue",
      backgroundColor: "rgba(2,117,216,1)",
      borderColor: "rgba(2,117,216,1)",
        data: [6215, 15312, 36251, 27841, 45821, 40984, 24984, 30984, 48984, 41984, 20984],
    }],
  },
  options: {
    scales: {
      xAxes: [{
        time: {
          unit: 'money'
        },
        gridLines: {
          display: false
        },
        ticks: {
          maxTicksLimit: 11
        }
      }],
      yAxes: [{
        ticks: {
          min: 0,
          max: 50000,
          maxTicksLimit: 10
        },
        gridLines: {
          display: true
        }
      }],
    },
    legend: {
      display: false
    }
  }
});
