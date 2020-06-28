let id = 0;
let $modal = $("#response-modal");

function answer(obj, e) {
    e = window.event;
    e.preventDefault();
    id = obj.getAttribute("target");
    $modal.modal("show");
}

function sendResponse() {
    var data = new FormData();
    var form_data = $('#response-form').serializeArray();
    $.each(form_data, function (key, input) {
        data.append(input.name, input.value);
    });
    data.append("target", id);

    $.ajax({
        url: "Home/Reply",
        type: "POST",
        dataType: "json",
        data: data,
        processData: false,
        contentType: false,
        success: function (data) {
            manageResult(data["status"], data["message"]);
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    })
}

function manageResult(status, message) {
    if (status) {
        toastr.success(message);
        $modal.modal("hide");        
        getQuestions(); //Function implemented in retrieve-question.js file
    } else {
        toastr.error(message);
    }
}