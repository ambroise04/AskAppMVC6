let questionId = 0;
let $qstModal = $("#question-modal-delete");

function del(obj, e) {
    e = window.event;
    e.preventDefault();
    questionId = obj.getAttribute("target");
    $qstModal.modal("show");
}

function sendDeletion() {   
    $.ajax({
        url: "Home/Delete",
        type: "POST",
        dataType: "json",
        data: { id: questionId },
        success: function (data) {
            manageDeleteResult(data["status"], data["message"]);
        },
        error: function (xhr) {
            manageResult(data["status"], data["message"]);
            console.log(xhr.responseText);
        }
    })
}

function manageDeleteResult(status, message) {
    if (status) {
        toastr.success(message);
        $qstModal.modal("hide");
        getQuestions(); //Function implemented in retrieve-question.js file
    } else {
        toastr.error(message);
    }
}