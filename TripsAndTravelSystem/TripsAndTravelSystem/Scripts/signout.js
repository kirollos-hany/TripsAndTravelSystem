async function signout() {
    await SignOut();
    localStorage.removeItem("isLogged");
    localStorage.setItem("userId", 0);
    localStorage.setItem("redirectTo", "http://localhost:59738/");
    if (window.location.href === "http://localhost:59738/") {
        window.location.reload();
    }
    window.location.href = localStorage.getItem("redirectTo");
}

$("#signout").click(() => {
    signout();
});

async function SignOut() {
    const url = "http://localhost:59738/user/signout";
    fetch(url, {
        method: "GET",
    }).then(response => console.log(response)).catch(err => console.log(err));
}

