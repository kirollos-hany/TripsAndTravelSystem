function deletePost(postId) {
    const userId = localStorage.getItem("userId");
    const data = {
        UserId: userId,
        PostId: postId
    };
    const url = "http://localhost:59738/agency/deletepost";
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