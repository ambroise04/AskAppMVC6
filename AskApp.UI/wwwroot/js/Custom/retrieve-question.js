$(document).ready(function () {
    getQuestions();
})

function getQuestions() {
    $.ajax({
        url: "Home/GetQuestions",
        type: "GET",
        dataType: "json",
        success: function (data) {
            listing(data["questions"]);
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    })
}