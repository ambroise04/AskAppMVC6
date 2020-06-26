function ask_question(obj, event) {
    event = window.event
    event.preventDefault();

    let data = $("#form-ask-question").serialize();
    save(data)
}

function save(data) {
    $.ajax({
        url: "Home/Create",
        type: "POST",
        dataType: "json",
        data: data,
        success: function (data) {
            getQuestions();
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    })
}