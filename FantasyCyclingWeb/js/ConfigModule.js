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

    my.AddPDCTeamUID = function ()
    {
        //todo: look up the PDCTeamUID to make sure it is valid
       // var PDCTeamYear = {};
        var PDCTeamURL = $("#txtPDCTeamURL").val();
        vm.URLToAddPDCTeam = PDCTeamURL;
       // var year = $("#txtYear").val();

        //PDCTeamYear.PDCTeamUID = PDCTeamUID;
       // PDCTeamYear.Year = year;

        // vm.PDCTeamUIDS.push(PDCTeamYear);


        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/Home/AddPDCTeamToConfig",
            data: vm,
            dataType: "json",
            success: function (data) {

                $("#mytable tbody").html(data.PDCTeamListHTML);

                vm.PDCTeamUIDS = data.PDCTeamListData;
                
                $("#txtPDCTeamURL").val("");
                $("#txtPDCTeamURL").focus();
            },
            error: function (data) {
                alert("Unable to parse that PDCTeam URL.  Try again or send the URL to ryan.");
            }
        });
    };


    my.RemovePDCTeamUID = function (id) {

        //vm.PDCTeamUIDS.
        for (var i = 0; i < vm.PDCTeamUIDS.length; i++) {
            var obj = vm.PDCTeamUIDS[i];

            if (vm.PDCTeamUIDS[i].GUID == id) {
                

                vm.PDCTeamUIDS.splice(i, 1);

                $("#tr_" + id).hide();
            }
        }
    };
    return my;
}());