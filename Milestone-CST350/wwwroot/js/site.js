$(document).on("click", "#grid-button", function (event) {

    event.preventDefault();
    var buttonNumber = $(this).val();
    console.log("button number: " + buttonNumber);

    $.ajax({
        datatype: 'json',
        method: 'POST',
        url: "/Home/HandleButtonClick",
        data: { "bN": buttonNumber },
        success: function (data) {
            console.log(data);
            $("#grid").html(data);
        }
    })
})
