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

function handleButtonContextMenu(event, button) {

    // oncontextmenu is when this method is called
    $.ajax({
        url: '/Home/HandleButtonFlag',
        type: 'POST',
        data: { bN: button.value },
        success: function (result) {
            console.log('Button right-click detected: ' + button.value);
            $("#grid").html(result);
        }
    });
    event.preventDefault();
}
