$("#editFirstNameBtn").click(() => {
    $("#firstNameForm").validate();
    if (!$("#firstNameForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const name = document.querySelector('input[name="newFirstName"]').value;
    await EditFirstName(userId, name);
});

$("#editLastNameBtn").click(async () => {
    $("#lastNameForm").validate();
    if (!$("#lastNameForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const name = document.querySelector('input[name="newLastName"]').value;
    await EditLastName(userId, name);
});

$("#editPhoneNumberBtn").click(async () => {
    $("#phoneNumberForm").validate();
    if (!$("#phoneNumberForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const phone = document.querySelector('input[name="newPhoneNumber"]').value;
    await EditPhoneNumber(userId, phone);
});

$("#editPasswordBtn").click(async () => {
    $("#editPasswordForm").validate();
    if (!$("#editPasswordForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const password = document.querySelector('input[name="newPassword"]').value;
    await EditPassword(userId, password);
});

$("#editEmailBtn").click(async () => {
    $("#editEmailForm").validate();
    if (!$("#editEmailForm").valid()) {
        return;
    }
    const userId = localStorage.getItem("userId");
    const email = document.querySelector('input[name="newEmail"]').value;
    await EditEmail(userId, email);
});

$("#editPhotoBtn").click(async () => {
    $("#editPhotoForm").validate();
    if (!$("#editPhotoForm").valid()) {
        return;
    }
    const photoFile = document.querySelector('input[name="newAgencyPhoto"]');
    const extension = photoFile.files[0].name.split('.').pop();
    const userId = localStorage.getItem("userId");
    fileReader.addEventListener('load', async () => {
        const photo = photoFile.files[0].split(',')[1];
        await EditPhoto(userId, photo, extension);
    });
    if (photo.value) {
        fileReader.readAsDataURL(photo.files[0]);
    }
});



async function EditFirstName(userId, name) {
    const data = {
        UserId: userId,
        Name: name
    }
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

async function EditLastName(userId, name) {
    const data = {
        UserId: userId,
        Name: name
    }
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

async function EditPhoneNumber(userId, phoneNumber) {
    const data = {
        UserId: userId,
        PhoneNumber: phoneNumber
    }
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

async function EditPassword(userId, pass) {
    const data = {
        UserId: userId,
        Password: pass
    };
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

async function EditEmail(userId, email) {
    const data = {
        UserId: userId,
        Email: email
    };
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

async function EditPhoto(userId, photo, extension) {
    const data = {
        UserId: userId,
        Photo: photo,
        PhotoExtension: extension
    };
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
