$(document).on("click", ".btn-delete", function () {
    if (!confirm("Are you sure you want to delete?")) return;

    var row = $(this).closest("tr");
    var id = row.find("td:eq(0)").text();

    $.ajax({
        url: `https://localhost:7238/api/Employee/${id}`,
        type: "DELETE",
        success: function (response) {
            alert("Success");
            row.remove();
        },
        error: function (error) {
            alert("failed to delete");
        }
    });
});
