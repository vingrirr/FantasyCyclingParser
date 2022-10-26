var RIDER_ADMIN = (function () {
    var my = {},
		privateVariable = 1;
    var vm = null;

    my.init = function (model) {
        //vm = model;
       
    };

    my.CloseModal = function () {
        $("#modal_centered").modal('hide');
    }

    my.ShowModal = function (PDC_RiderID, name) {
        
        $("#riderName")[0].text = name;
        $("#txtPDCRiderID")[0].value = PDC_RiderID;


        $("#modal_centered").modal('show');

    }


    my.AssignRiderPhoto = function () {
        vm = {};

        vm.riderPhotoURL = $("#txtPhotoURL").val();
        vm.pdcRiderID = $("#txtPDCRiderID").val();
        vm.pcs_riderURL = $("#txtPCSRiderURL").val();
//        vm.name = $("#txtRiderName").val();

        $.ajax({
            cache: false,
            async: true,
            contentType: 'application/json',
            type: "POST",
            url: "/Admin/AssignRiderPhoto",
            data: JSON.stringify(vm),
            success: function (data) {



            },
            error: function (data) {
                alert("Unable to save the photo.");
            }
        });

    };
 


    return my;
}());