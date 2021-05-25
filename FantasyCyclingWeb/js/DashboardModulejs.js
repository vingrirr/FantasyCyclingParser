var DB = (function () {
    var my = {},
		privateVariable = 1;

    var vm;

    my.init = function (model) {

        vm = model; 

       // points = vm.Points;
        //points = vm.TeamUIDs;

       // teamNames = vm.TeamNames;
        //CHARTS.FCBarChart(points, teamNames);
       

            //$.ajax({
            //    cache: false,
            //    async: true,
            //    type: "GET",
            //    contentType: "application/json",
            //    url: '/Home/GetData/',
            //    success: function (data) {
                              
            //        var points = [];
            //        var teamNames = [];

            //        points = data.Points;
            //        teamNames = data.TeamNames;
            //        CHARTS.FCBarChart(points, teamNames);
            //        //plot_statistics.setData(data.ChartData);
            //        //plot_statistics.setupGrid();
            //        //plot_statistics.draw();
            //    },
            //    error: function (data) {
            //        var x = 0; 

            //    }
            //}); //end ajax call
        //CHARTS.BarChart();
        //CHARTS.Scatter();
    
    };
    
    return my;
}());