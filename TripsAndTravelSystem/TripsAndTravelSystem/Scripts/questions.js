async function AnswerQuestion(questionId, answer){
    const url = "http://localhost:59738/agency/AnswerQuestion"
    const data = {
        "questionId" : questionId,
        "answer" : answer
    }

    fetch(url, {
        method: "POST",
        headers : {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.questionId != 0) {
            location.reload();
        } else {
            alert("Failed to submit answer, try again");
        }
    }).catch(error => console.log(error));
}

async function OnSubmitAnswer(questionId){
    const answer = document.getElementById("answerTextArea");
    if(answer.value.length > 0){
        await AnswerQuestion(questionId, answer.value);
    }else{
        alert("You can't submit an empty answer");
    }
}

async function sendQuestion(agencyId, question) {
    const data = {
        AgencyId: agencyId,
        Question: question
    }
    const url = "http://localhost:59738/index/addquestion";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.AgencyId != 0) {
            window.location.reload();
        } else {
            alert("Failed to send question, please try again");
        }
    }).catch(err => alert(err));

}

$("#addQuestionBtn").click(async () => {
    const question = document.querySelector('input[name="questionText"]').value;
    const selectedAgency = document.querySelector('#selectAgency').value;
    if (question.length > 0 && selectedAgency !== "default") {
        await sendQuestion(selectedAgency, question);
    } else if (selectedAgency === "default") {
        alert("Please pick an agency");
    } else {
        alert("Can't send empty question");
    }
});