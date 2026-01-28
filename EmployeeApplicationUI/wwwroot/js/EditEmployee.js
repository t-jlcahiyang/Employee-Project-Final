$(document).on("click", ".btn-edit", function () {

    var row = $(this).closest("tr");

    var ID = row.find("td:eq(0)").text();
    var FirstName = row.find("td:eq(2)").text();
    var MiddleName = row.find("td:eq(3)").text();
    var LastName = row.find("td:eq(4)").text();
    var Bdate = row.find("td:eq(5)").text();
    var BirthDate = Bdate.split("T")["0"];
    var DailyRate = row.find("td:eq(6)").text();
    var WorkingDays = row.find("td:eq(7)").text();

    $("#ID").val(ID);
    $("#EditFirstName").val(FirstName);
    $("#EditMiddleName").val(MiddleName);
    $("#EditLastName").val(LastName);
    $("#EditBirthDate").val(BirthDate);
    $("#EditDailyRate").val(DailyRate);
    $(".form-check-input").prop("checked", false);
    if (WorkingDays) {
        WorkingDays.split(",").forEach(day => {
            $(`#ModalForEdit .form-check-input[value='${day}']`).prop("checked", true);
        });
    }
    $("#ModalForEdit").modal("show");
});


$("#SaveEdit").click(function () {
    var WorkingDays = [];
    $("#ModalForEdit .form-check-input:checked").each(function () {
        WorkingDays.push($(this).val());
    });

    var data = {
        Id: $("#ID").val(),
        FirstName: $("#EditFirstName").val(),
        MiddleName: $("#EditMiddleName").val(),
        LastName: $("#EditLastName").val(),
        BirthDay: $("#EditBirthDate").val(),
        DailyRate: $("#EditDailyRate").val(),
        Workingdays: WorkingDays.join(",")
    };


    $.ajax({
        url: `https://localhost:7238/api/Employee/${data.Id}`,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function () {
            alert("Sucess");
            $("#ModalForEdit").modal("hide");
            location.reload();
        },
        error: function (error) {
            console.log(error);
            alert("Update failed");
        }
    });
});