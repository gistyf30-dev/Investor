var UserName = "";
var Id = "";

$(document).ready(function () {
    debugger;
    GetData();
});
$("#DrpInvestors").change(function () {
    debugger
    var obj = {};
    var Url;
    var type;
    var Id = $(this).val();
    if (Id == "All") {
        Url = "/Investor/SelectAllInvestors";
        type='All'
    }
    else {
        Url = "/Investor/SingleInvestor";
        type = 'Change'
    }
    obj.InvestorId = Id;
    $.ajax({
        type: "POST",
        url: SiteURL + Url,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && ((response.ObjClsLInvestor!=null && response.ObjClsLInvestor[0].StatusDetail.StatusCode == 200) || response[0].StatusDetail.StatusCode == 200)) {
             
                BindGrid(response, type);
            }
            else {
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
})
function GetData() {
    debugger;
    $("#DrpInvestors").empty();


    $.ajax({
        type: "POST",
        url: SiteURL + "/Investor/SelectAllInvestors",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && response[0].StatusDetail.StatusCode==200) {
                $("#DrpInvestors").append("<option value='All'>All</option>")
                $.each(response, function (i) {
                  
                    $("#DrpInvestors").append('<option value="' + response[i].InvestorId + '">' + response[i].Name + '</option>');
                })
              
                BindGrid(response);
            }
            else {
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
}

function ViewData(Id) {
    debugger;
    var obj = {};
    obj.InvestorId = Id;


    $.ajax({
        type: "POST",
        url: SiteURL + "/Investor/SingleInvestor",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && response.ObjClsLInvestor[0].StatusDetail.StatusCode == 200) {
                $("#UserMasterModal").modal("show");
                $("#txtName").val(response.ObjClsLInvestor[0].Name);
                $("#txtPhone").val(response.ObjClsLInvestor[0].Phone);
                $("#txtEmail").val(response.ObjClsLInvestor[0].Email);
                $("#txtCountry").val(response.ObjClsLInvestor[0].Country);
                $("#txtFundsInvestedIn").val(response.ObjClsLInvestor[0].FundsInvestedIn);
                $("#txtName").prop("disabled", true);
                $("#txtPhone").prop("disabled", true);
                $("#txtEmail").prop("disabled", true);
                $("#txtCountry").prop("disabled", true);
                $("#txtFundsInvestedIn").prop("disabled", true);
            }
            else {
                $("#UserMasterModal").modal("hide");
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
}

function BindInvestorDrp() {
    debugger;
    $("#DrpFundInvestors").empty();

    $.ajax({
        type: "POST",
        url: SiteURL + "/Investor/SelectAllInvestors",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && response[0].StatusDetail.StatusCode == 200) {
    
                $.each(response, function (i) {

                    $("#DrpFundInvestors").append('<option value="' + response[i].InvestorId + '">' + response[i].Name + '</option>');
                })

            }
            else {
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
}

function BindFundDrp() {
    debugger;
    $("#DrpFunds").empty();

    $.ajax({
        type: "POST",
        url: SiteURL + "/Investor/getFundList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && response[0].StatusDetail.StatusCode == 200) {

                $.each(response, function (i) {

                    $("#DrpFunds").append('<option value="' + response[i].FundId + '">' + response[i].FundName + '</option>');
                })

            }
            else {
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
}

function BindGrid(response, Type) {
    if (Type == 'Change') {
        response = response.ObjClsLInvestor;
    }
    else {
        response = response;
    }
    debugger
    $('#Tbl_Investor').dataTable({
        "bDestroy": true,
    }).fnDestroy();
    NewReleaseRaskTable = $('#Tbl_Investor').DataTable({
        initComplete: function () {
            $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
        },
        data: response,
        "lengthChange": true,
        "searching": true,
        "paging": true,
        "ordering": false,
        "info": true,
        "autoWidth": false,
        "scrollY": "45vh",
        "scrollCollapse": true,
        "language": {
            "oPaginate": {
                sNext: '<i class="fa fa-forward"></i>',
                sPrevious: '<i class="fa fa-backward"></i>',
            }
        },
        "columnDefs": [
            { "width": "20%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "16%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "20%", "targets": 4 },
            { "width": "2%", "targets": 5 },
            { "width": "2%", "targets": 6 }
        ],
        "columns": [
            { "data": "Name" },
            { "data": "Phone" },
            { "data": "Email" },
            { "data": "Country" },
            { "data": "FundsInvestedIn" },
            {
                "mRender": function (data, type, row) {
                    return "<a href='#' id='btnView' Editvalue='" + row.InvestorId + "'><i class='fa fa-eye'></i></a>";
                }
            },
            {
                "mRender": function (data, type, row) {
                    return " <a href='#' id='btnDelete' FundInvestedvalue='" + row.FundsInvestedIn + "'  Deletevalue='" + row.InvestorId + "'; ><i class='fa fa-trash text-danger'></i></a>";
                }
            },
        ],
    });
}


$("#btnAdd").click(function () {
    debugger
    $("#headAdd").text("Add Investor");
    $("#divFundInvestedIn").hide();
    $("#txtName").prop("disabled", false);
    $("#txtPhone").prop("disabled", false);
    $("#txtEmail").prop("disabled", false);
    $("#txtCountry").prop("disabled", false);
    $("#btnSubmit").show();
    clear();
    placeholder();
})
$("#btnSubmit").click(function () {
    InsertData();
});
$("#btnFundSubmit").click(function () {
    InsertFundData();
});
function InsertFundData() {
    debugger
    var obj = {};
    obj.FundId = $("#DrpFunds option:selected").val();
    obj.InvestorId = $("#DrpFundInvestors option:selected").val();
    $.ajax({
        type: "POST",
        url: SiteURL + "/Investor/AddFundForInvestor",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && response.StatusDetail.StatusCode == 200) {
                clear();
                $("#FundsModal").modal('hide');
                GetData();
                Swal.fire({
                    icon: 'success',
                    title: 'Inserted',
                    text: 'Funds Added Successfully',
                    customClass: {
                        confirmButton: 'btn btn-success'
                    }
                });
            }
            else {
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
}
function InsertData() {
    debugger

    var obj = {};
    obj.UserName = "Admin";
    obj.Name = $("#txtName").val();
    obj.Phone = $("#txtPhone").val();
    obj.Email = $("#txtEmail").val();
    obj.Country = $("#txtCountry").val();

    $.ajax({
        type: "POST",
        url: SiteURL + "/Investor/AddInvestors",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            debugger;
            if (response != null && response.StatusDetail.StatusCode == 200) {
                clear();
                $("#UserMasterModal").modal('hide');
                GetData();
                Swal.fire({
                    icon: 'success',
                    title: 'Inserted',
                    text: 'Investor Added Successfully',
                    customClass: {
                        confirmButton: 'btn btn-success'
                    }
                });
            }
            else {
                Swal.fire({
                    title: 'Warning',
                    text: response.StatusDetail.Status,
                    icon: "warning",
                })
            }
        },
        error: function (x, e) {
            FindError(x, e)
        }
    });
}
$("#btnAddFund").click(function () {
    debugger

    BindInvestorDrp();
    BindFundDrp();
});
//Edit Data
$('body').on('click', '#btnView', function () {
    debugger
    $("#headAdd").text("View Investor");
    Id = $(this).attr('Editvalue');
    ViewData(Id);
    $("#divFundInvestedIn").show();
    $("#btnSubmit").hide();
    
});


// Delete Data
$('body').on('click', '#btnDelete', function () {
    debugger
    InvestorId = $(this).attr('Deletevalue');
    FundInvestedvalue = $(this).attr('FundInvestedvalue');
    if (FundInvestedvalue == "") {
        DeleteData();
    }
    else {
        Swal.fire({
            title: 'Warning',
            text: "You cant delete this user as he/she has a fund added",
            icon: "warning",
        })
    }
});
function DeleteData() {
    debugger;
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to Delete Record!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Delete!',
        customClass: {
            confirmButton: 'btn btn-primary mr-2',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    }).then(function (response) {
        if (response.value) {
            debugger

            var obj = {};
            obj.InvestorId = InvestorId;

            $.ajax({
                type: "POST",
                url: SiteURL + "/Investor/DeleteInvestor",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger
                    if (response != null && response.StatusDetail.StatusCode == 200) {
                        GetData();
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted',
                            text: response.StatusDetail.Status,
                            customClass: {
                                confirmButton: 'btn btn-danger'
                            }
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Warning',
                            text: response.StatusDetail.Status,
                            icon: "warning",
                        })
                    }
                }
            });
        }
    });
}

function validate(id, text, Validation) {
    debugger
    //validate empty
    if (Validation == "Empty") {
        debugger
        text = "Please enter " + text;
        if ($(id).val() == "") {
            Swal.fire({
                icon: 'info',
                title: 'Alert!',
                text: text,
                customClass: {
                    confirmButton: 'btn btn-success'
                }
            });
            return false;
        }
        return true;
    }
    //validate Numeric
    if (Validation == "Numeric") {
        debugger
        text = "Please enter number only " + text;
        if ($(id).val() != "") {
            debugger
            if ($.isNumeric($(id).val()) != true) {
                Swal.fire({
                    icon: 'info',
                    title: 'Alert!',
                    text: text,
                    customClass: {
                        confirmButton: 'btn btn-success'
                    }
                });
                return false;
            }
        }
        return true;
    }
    //validate DropDown
    if (Validation == "DrpEmpty") {
        debugger
        text = "Please select " + text;
        if ($(id + " option:selected").val() == undefined || $(id + " option:selected").val() == "") {
            $(id).data('select2').$container.addClass('form-controlerror')
            Swal.fire({
                icon: 'info',
                title: 'Alert!',
                text: text,
                customClass: {
                    confirmButton: 'btn btn-success'
                },
                didClose: () => {
                    $(id).focus();
                },
            });
            return false;
        }
        return true;
    }
}

//Clear Data
function clear() {
    $("#txtName").val("");
    $("#txtPhone").val("");
     $("#txtEmail").val("");
    $("#txtCountry").val("");
    $('#flexCheckDefault').prop('checked', false);
}
