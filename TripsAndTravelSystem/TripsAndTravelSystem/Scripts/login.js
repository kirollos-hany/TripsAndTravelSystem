$("#loginBtn").click(async () => {
    $("#loginForm").validate();
    if (!$("#loginForm").valid()) {
        return;
    }
    const email = document.querySelector('input[name="email"]').value;
    const password = document.querySelector('input[name="password"]').value;
    const errorMsg = document.getElementById("loginerror");
    const loginData = {
        Email: email,
        Password: password,
    }
    await login(loginData, errorMsg);
});

async function login(loginData, error) {
    const url = "http://localhost:59738/user/login";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(loginData)
    }).then(response => response.json()).then((data) => {
        if (data.UserId !== 0) {
            error.innerHTML = data.ErrorMessage;
            localStorage.setItem("userId", data.UserId);
            localStorage.setItem("redirectTo", data.RedirectUrl);
            localStorage.setItem("email", loginData.Email);
            localStorage.setItem("password", loginData.Password);
            window.location.href = data.RedirectUrl;
        } else if (data.UserId === 0) {
            error.innerHTML = data.ErrorMessage;
        }
    }).catch(err => console.log(err));
}

async function loginNoRedirect(loginData, error) {
    const url = "http://localhost:59738/user/login";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(loginData)
    }).then(response => response.json()).then((data) => {
        if (data.UserId !== 0) {
            error.innerHTML = data.ErrorMessage;
            localStorage.setItem("userId", data.UserId);
            localStorage.setItem("redirectTo", data.RedirectUrl);
            localStorage.setItem("email", loginData.Email);
            localStorage.setItem("password", loginData.Password);
        } else if (data.UserId === 0) {
            error.innerHTML = data.ErrorMessage;
        }
    }).catch(err => console.log(err));
}

