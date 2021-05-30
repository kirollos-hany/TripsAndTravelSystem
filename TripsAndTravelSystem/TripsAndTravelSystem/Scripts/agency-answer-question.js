async function AnswerQuestion(questionId, answer){
    const url = "https://localhost:44332/agency/AnswerQuestion"
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
    }).then(response => response.json()).then(data => {
        console.log(data);
        location.reload();
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