"use strict";
function theBest(obj) {
    var responseId = obj.getAttribute("target");
    var questionId = obj.getAttribute("parent");
    changeTheBest(responseId, questionId);
}

function changeTheBest(responseId, questionId) {
    $.ajax({
        url: "Home/ChangeBest",
        type: "POST",
        dataType: "json",
        data: { responseId: responseId, questionId: questionId },
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