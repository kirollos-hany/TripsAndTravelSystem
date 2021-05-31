function acceptPost(postId){
    const url = "http://localhost:59738/admin/acceptpost";
    const data = {
        PostId : postId
    }
    fetch(url, {
        method: "POST",
        headers: {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.PostId != 0) {
            window.location.reload();
        } else {
            alert(responseData.ErrorMessage);
        }
    }).catch(err => alert(err));
}

function denyPost(postId) {
    const url = "http://localhost:59738/admin/acceptpost";
    const data = {
        PostId: postId
    }
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.PostId != 0) {
            window.location.reload();
        } else {
            alert(responseData.ErrorMessage);
        }
    }).catch(err => alert(err));
}