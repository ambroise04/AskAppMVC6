"use strict";
var $input = $(".search-input");

$(document).ready(function () {
    $input.on("keyup", function () {
        let key = $(this).val();
        search(key);
    })
})

function search(key) {
    $.ajax({
        url: "Home/GetQuestions",
        type: "GET",
        dataType: "json",
        data: { search: key },
        success: function (data) {
            listing(data["questions"]);
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    })
}