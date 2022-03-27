var BARCHARTRACE
    = (function () {
    var my = {},
        privateVariable = 1;
    var vm = null;
        var ALL_DATA = {};
    my.init = function (model) {
        vm = model;

        var allData = {};
        var raceList = vm.RaceList;
        //var allData = vm.BarChartData;

        for (var x=0; x < vm.BarChartData.length; x++)
        {
            var record = vm.BarChartData[x];

            allData[record.Name] = {};

            for (var y = 0; y < record.Items.length; y++)
            {
                allData[record.Name][record.Items[y].Key] = record.Items[y].Value; 
            }
        }
        my.ALL_DATA = allData;
       
    };



    return my;
}());