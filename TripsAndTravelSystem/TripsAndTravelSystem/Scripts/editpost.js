function editPostTitle(postId) {
    $("#editTitleForm").validate();
    if (!($("#editTitleForm").valid())){
        return;
    }
    const url = "http://localhost:59738/agency/editposttitle";
    const data = {
        PostId: postId,
        NewTitle: document.querySelector('input[name="newTitle"]').value
    };
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
            document.querySelector('#titleerror').innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}

function editPostPhoto(postId) {
    $("#editTripPhotoForm").validate();
    if (!$("#editTripPhotoForm").valid()) {
        return;
    }
    const url = "http://localhost:59738/agency/editphoto";
    const photoFile = document.querySelector('input[name="newPostPhoto"]');
    const fileReader = new FileReader();
    fileReader.addEventListener('load', () => {
        const data = {
            Photo: fileReader.result.split(',')[1],
            PhotoExtension: photoFile.files[0].name.split('.').pop(),
            PostId: postId
        };
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
                document.querySelector('#photoerror').innerHTML = responseData.ErrorMessage;
            }
        }).catch(err => alert(err));
    });
    if (photoFile.value) {
        fileReader.readAsDataURL(photoFile.files[0]);
    }
}

function editPostDetails(postId) {
    $("#editDetailsForm").validate();
    if (!$("#editDetailsForm").valid()) {
        return;
    }
    const url = "http://localhost:59738/agency/editpostdetails";
    const data = {
        Details: document.querySelector('textarea[name="newPostDetails"]').value,
        PostId: postId
    };
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
            document.querySelector('#detailserror').innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}

function editTripDate(postId) {
    $("#editTripDateForm").validate();
    if (!$("#editTripDateForm").valid()) {
        return;
    }
    const url = "http://localhost:59738/agency/edittripdate";
    const data = {
        TripDate: document.querySelector('input[name="newPostDate"]').value,
        PostId: postId
    };
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
            document.querySelector('#dateerror').innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}

function editTripDestination(postId) {
    $("#editDestinationForm").validate();
    if (!$("#editDestinationForm").valid()) {
        return;
    }
    const url = "http://localhost:59738/agency/editdestination";
    const data = {
        Destination: document.querySelector('input[name="newPostDestination"]').value,
        PostId: postId
    };
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
            document.querySelector('#destinationerror').innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}

function editPrice(postId) {
    $("#editPriceForm").validate();
    if (!$("#editPriceForm").valid()) {
        return;
    }
    const url = "http://localhost:59738/agency/editprice";
    const data = {
        Price: document.querySelector('input[name="newPostPrice"]').value,
        PostId: postId
    };
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
            document.querySelector('#priceerror').innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}




