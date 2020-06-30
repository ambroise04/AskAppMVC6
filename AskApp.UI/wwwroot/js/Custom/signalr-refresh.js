"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/refresh").build();

$(document).ready(function () {
    connection.on("Refresh", function () {
        getQuestions();
    });

    connection.start().then(function () {
        console.log("connection started");
    }).catch(function (err) {
        console.log(err.toString());
    });
})