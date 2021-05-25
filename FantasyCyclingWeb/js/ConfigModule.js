var CONFIG = (function () {
    var my = {},
		privateVariable = 1;
    var vm = null;

    my.init = function (model) {
        vm = model;
    };


    my.AddConfig = function () {

   
        vm.ConfigName = $("#txtConfigName").val();

        $.ajax({
            cache: false,
            async: true,
            contentType: 'application/json',
            type: "POST",
            url: "/Home/AddConfig",
            data: JSON.stringify(vm),
            success: function (data) {

                

            },
            error: function (data) {
                alert("Unable to save the config.  My bad.");
            }
        });

    };

    my.AddTeamUID = function ()
    {
        //todo: look up the TeamUID to make sure it is valid
       // var TeamYear = {};
        var teamURL = $("#txtTeamURL").val();
        vm.URLToAddTeam = teamURL;
       // var year = $("#txtYear").val();

        //TeamYear.TeamUID = teamUID;
       // TeamYear.Year = year;

        // vm.TeamUIDS.push(TeamYear);


        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/Home/AddTeamToConfig",
            data: vm,
            dataType: "json",
            success: function (data) {

                $("#mytable tbody").html(data.TeamListHTML);

                vm.TeamUIDS = data.TeamListData;
                
                $("#txtTeamURL").val("");
                $("#txtTeamURL").focus();
            },
            error: function (data) {
                alert("Unable to parse that team URL.  Try again or send the URL to ryan.");
            }
        });
    };


    my.RemoveTeamUID = function (id) {

        //vm.TeamUIDS.
        for (var i = 0; i < vm.TeamUIDS.length; i++) {
            var obj = vm.TeamUIDS[i];

            if (vm.TeamUIDS[i].GUID == id) {
                

                vm.TeamUIDS.splice(i, 1);

                $("#tr_" + id).hide();
            }
        }
    };
    return my;
}());