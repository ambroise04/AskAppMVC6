function listing(questions) {
    let $container = $("#accordionQuestions");
    let content = "";
    $container.html("");

    if (questions.length == 0) {
        content = "No question to display"
    }

    let first = true;
    let show = "show";
    $.each(questions, function (i, question) {
        let responseContent = "";
        if (question.responses.length == 0) {
            responseContent = "No answer to display"
        }
        $.each(question.responses, function (j, response) {
            responseContent += `
                <div class="row">
                    <div class="col-md-8">
                        <h6 class="my-blue-color">${response.responder}</h6>
                        <p>
                            ${response.message}
                        </p>
                    </div>
                    <div class="col-md-4 d-flex justify-content-end">
                        <p class="grey-text desc-text"><i class="far fa-clock mr-1"></i>Answer posted ${response.elapsedTime}</p>
                    </div>
                </div>
            `
        })
        content += `
            <div class="card">
                <div class="card-header bg-white border-bottom-0" id="headingOne">
                    <div class="mb-0">
                        <div class="row link" data-toggle="collapse" data-target="#collapse${question.id}" aria-expanded="${first}" aria-controls="collapse${question.id}">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6 d-flex">
                                        <i class="far fa-user fa-4x align-self-center mr-3"></i>
                                        <div>
                                            <h5 class="my-blue-color">${question.publisher}</h5>
                                            <p>${question.message}</p>
                                        </div>
                                    </div>
                                    <div class="col-md-3 d-flex flex-sm-row flex-md-column posted-info">
                                        <p class="grey-text mb-0 desc-text"><i class="far fa-clock mr-1"></i>${question.elapsedTime}</p>
                                        <p class="grey-text my-0 desc-text"><i class="far fa-comment-dots mr-1"></i>Number of reponses : <span class="my-blue-color">0</span></p>
                                    </div>
                                    <div class="col-md-3 text-right d-flex flex-sm-row flex-md-column justify-content-between align-items-center">
                                        <a class="btn btn-sm btn-outline-info action"><i class="fas fa-reply mr-1"></i> Answer</a>
                                        <a class="btn btn-sm btn-outline-danger action"><i class="far fa-trash-alt mr-1"></i>Delete</a>
                                    </div>
                                    <hr />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="collapse${question.id}" class="collapse ${show} ml-4 border-0" aria-labelledby="headingOne" data-parent="#accordionQuestions">                    
                    <div class="card-body my-bg-white-color rounded-top">
                        ${responseContent}
                    </div>
                </div>
            </div>
        `
        first = false;
        show = "";
    })
    $container.html(content);
}