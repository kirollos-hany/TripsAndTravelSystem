$("#editFirstNameBtn").click(async () => {
    $("#firstNameForm").validate();
    if (!$("#firstNameForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const name = document.querySelector('input[name="newFirstName"]').value;
    const data = {
        UserId: userId,
        Name: name
    };
    await EditFirstName(data);
});

$("#editLastNameBtn").click(async () => {
    $("#lastNameForm").validate();
    if (!$("#lastNameForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const name = document.querySelector('input[name="newLastName"]').value;
    const data = {
        UserId: userId,
        Name: name
    };
    await EditLastName(data);
});

$("#editPhoneNumberBtn").click(async () => {
    $("#phoneNumberForm").validate();
    if (!$("#phoneNumberForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const phone = document.querySelector('input[name="newPhoneNumber"]').value;
    const data = {
        UserId: userId,
        PhoneNumber: phone
    };
    await EditPhoneNumber(data);
});

$("#editPasswordBtn").click(async () => {
    $("#editPasswordForm").validate();
    if (!$("#editPasswordForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const password = document.querySelector('input[name="newPassword"]').value;
    const data = {
        UserId: userId,
        Password: password
    };
    await EditPassword(data);
});

$("#editEmailBtn").click(async () => {
    $("#editEmailForm").validate();
    if (!$("#editEmailForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const email = document.querySelector('input[name="newEmail"]').value;
    const data = {
        Email: email,
        UserId : userId
    };
    await EditEmail(data);
});


$("#editPhotoBtn").click(async () => {
    $("#editPhotoForm").validate();
    if (!$("#editPhotoForm").valid()) {
        return;
    }
    const photoFile = document.querySelector('input[name="newAgencyPhoto"]');
    const userId = localStorage.getItem("userId");
    console.log("kkk");
    const extension = photoFile.files[0].name.split('.').pop();
    const fileReader = new FileReader();
    fileReader.addEventListener('load', async () => {
        console.log(extension);
        const data = {
            Photo:fileReader.result.split(',')[1],
            PhotoExtension: extension,
            UserId: userId
        };
        console.log(data);
        await EditPhoto(data);
    });
    if (photoFile.value) {
        fileReader.readAsDataURL(photoFile.files[0]);
    }
});



async function EditFirstName(data) {
    const url = "http://localhost:59738/user/editfirstname";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            document.querySelector("#editfirstnameerror").innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            document.querySelector("#editfirstnameerror").innerHTML = responseData.ErrorMessage;
        }
    }).catch(err=>alert(err));
}

async function EditLastName(data) {
    const url = "http://localhost:59738/user/editlastname";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            document.querySelector("#editlastnameerror").innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            document.querySelector("#editlastnameerror").innerHTML = responseData.ErrorMessage;
        }
    }).catch(err=>alert(err));

}

async function EditPhoneNumber(data) {
    const url = "http://localhost:59738/user/editphonenumber";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            document.querySelector("#editphonenumbererror").innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            document.querySelector("#editphonenumbererror").innerHTML = responseData.ErrorMessage;
        }
    }).catch(err=>alert(err));
}

async function EditPassword(data) {
    const url = "http://localhost:59738/user/editpassword";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            document.querySelector("#editpassworderror").innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            document.querySelector("#editpassworderror").innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}

async function EditEmail(data) {
    const url = "http://localhost:59738/user/editemail";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            document.querySelector("#editemailerror").innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            document.querySelector("#editemailerror").innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}


async function EditPhoto(data) {
    const url = "http://localhost:59738/user/editphoto";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            document.querySelector("#editphotoerror").innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            document.querySelector("#editphotoerror").innerHTML = responseData.ErrorMessage;
        }
    }).catch(err => alert(err));
}