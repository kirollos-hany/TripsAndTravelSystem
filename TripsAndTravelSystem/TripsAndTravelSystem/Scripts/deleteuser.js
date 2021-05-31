async function deleteUser(userid) {
    const data = { UserId: userid };
    const url = "http://localhost:59738/admin/deleteuser";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.Successful) {
            window.location.reload();
        } else {
            alert(responseData.Message);
        }
    })
}