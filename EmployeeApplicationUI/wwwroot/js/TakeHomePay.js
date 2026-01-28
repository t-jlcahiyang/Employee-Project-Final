$(document).on("click", ".btn-takehome", function () {

    var row = $(this).closest("tr");

    var EmployeeNumber = row.find("td:eq(1)").text();
    var DailyRate = row.find("td:eq(6)").text();
    $("#StartDate").val('');
    $("#EndDate").val('');
    $("#CompDailyRate").val(DailyRate);
    $("#CompEmpNumber").val(EmployeeNumber);


    $("#ModalForPay").modal("show");
});

$("#CheckTakeHomePay").click(function () {
    var startDate = $("#StartDate").val();
    var endDate = $("#EndDate").val();
    var dailyRate = $("#CompDailyRate").val();

    var data = {
        StartDate: startDate,
        EndDate: endDate,
        DailyRate: dailyRate
    };

    $.ajax({
        url: "https://localhost:7238/api/Employee/TakeHomePay",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            alert("Take home pay: PHP " + response.takeHomePay);
            console.log("test", response);
        },
        error: function (xhr, status, error) {
            alert("error computing take home pay");
            console.log("error: " + xhr.status + error);
        }
    });
});