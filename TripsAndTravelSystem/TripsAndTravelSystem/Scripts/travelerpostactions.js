function likePost(postId) {
    const data = {
        PostId: postId,
    };
    const url = "http://localhost:59738/index/likepost";
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

function dislikePost(postId) {
    const data = {
        PostId: postId,
    };
    const url = "http://localhost:59738/index/dislikepost";
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

function favoritePost(postId) {
    const data = {
        PostId: postId,
    };
    const url = "http://localhost:59738/index/addfavoritepost";
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

