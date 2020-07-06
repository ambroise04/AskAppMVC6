function ask_question(obj, event) {
    event = window.event
    event.preventDefault();

    let data = $("#form-ask-question").serialize();
    save(data)
    $("#form-ask-question").trigger("reset");
}

function save(data) {
    $.ajax({
        url: "Home/Create",
        type: "POST",
        dataType: "json",
        data: data,
        success: function (data) {
            if (data["status"]) {
                toastr.success(data["message"])
            } else {
                toastr.error(message);
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    })
}