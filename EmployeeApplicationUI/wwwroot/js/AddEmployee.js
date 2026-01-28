$("#SaveAdd").click(function () {


    var workingDays = [];
    $(".form-check-input:checked").each(function () {
        workingDays.push($(this).val());
    });


    var data = {
        FirstName: $("#FirstName").val(),
        MiddleName: $("#MiddleName").val(),
        LastName: $("#LastName").val(),
        BirthDay: $("#BirthDate").val(),
        DailyRate: $("#DailyRate").val(),
        WorkingDays: workingDays.join(",")
    };

    $.ajax({
        url: "https://localhost:7238/api/Employee",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function () {
            alert("Sucess");
        },
        error: function (error) {
            console.log(error);
            alert("Save failed");
        }
    });

});