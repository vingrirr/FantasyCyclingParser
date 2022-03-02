var NewRaceSeason = (function () {
    var my = {},
		privateVariable = 1;

    var vm;

    my.init = function (model) {

        vm = model; 
   

        data = [];
        var item = null;

        //var moreData = new Array(vm.RaceCount);

        //for (var i = 0; i < moreData.length; i++)
        //{
        //    moreData[i] = [vm.KamnaChameleonPoints[i].X,vm.KamnaChameleonPoints[i].Y] ;
        //}

        
        

        ///////////////////////////////////////////////

        //data.push(Zauzage);



        nv.addGraph(function () {
            var zoom = 1;
            var fitScreen = false;
            var width = 600;
            var height = 300;
            var chart = nv.models.lineChart()
                .useInteractiveGuideline(true)
                .showXAxis(false)
                .x(function (d) {
                    return d[0]
                })
                .y(function (d) { return d[1] })
                //.average(function (d) { return d.mean / 100; })
                .duration(300)
                .clipVoronoi(false);

            chart.xAxis
                .axisLabel("Race")
                .tickPadding(10)
                .tickFormat(function (d) {
                    return vm.FantasyResults[d].Race.Month +" "+ vm.FantasyResults[d].Race.DayNum + "<br />"+ vm.FantasyResults[d].Race.Country + "<br />"+ vm.FantasyResults[d].Race.EventName;
                })

            chart.dispatch.on('renderEnd', function () {
                console.log('render complete: cumulative line with guide line');
            });

            //d3.select('#zoomIn').on('click', zoomIn);
            //d3.select('#zoomOut').on('click', zoomOut);

            function setChartViewBox() {
                var w = width * zoom,
                    h = height * zoom;
                chart
                    .width(w)
                    .height(h);
                d3.select('#lineChart svg')
                    .attr('viewBox', '0 0 ' + w + ' ' + h)
                    .transition().duration(500)
                    .call(chart);
            }
            function zoomOut() {
                zoom += .25;
                setChartViewBox();
            }
            function zoomIn() {
                if (zoom <= .5) return;
                zoom -= .25;
                setChartViewBox();
            }
            // This resize simply sets the SVG's dimensions, without a need to recall the chart code
            // Resizing because of the viewbox and perserveAspectRatio settings
            // This scales the interior of the chart unlike the above
            function resizeChart() {
                var container = d3.select('#lineChart');
                var svg = container.select('svg');
                if (fitScreen) {
                    // resize based on container's width AND HEIGHT
                    var windowSize = nv.utils.windowSize();
                    svg.attr("width", windowSize.width);
                    svg.attr("height", windowSize.height);
                } else {
                    // resize based on container's width
                    var aspect = chart.width() / chart.height();
                    var targetWidth = parseInt(container.style('width'));
                    svg.attr("width", targetWidth);
                    svg.attr("height", Math.round(targetWidth / aspect));
                }
            }

            d3.select('#lineChart svg')
                //.attr('perserveAspectRatio', 'xMinYMid')
                //.attr('width', width)
                //.attr('height', 800)
                .datum(model.LineChartVM.ChartData)
                .call(chart);
            //TODO: Figure out a good way to do this automatically
            //nv.utils.windowResize(chart.update);
            nv.utils.windowResize(resizeChart);

            chart.dispatch.on('stateChange', function (e) { nv.log('New State:', JSON.stringify(e)); });
            chart.state.dispatch.on('change', function (state) {
                nv.log('state', JSON.stringify(state));
            });
            return chart;
        });








    
    };
    
    return my;
}());