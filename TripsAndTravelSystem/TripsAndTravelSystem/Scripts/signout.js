async function signout() {
    await SignOut();
    localStorage.setItem("userId", 0);
    localStorage.setItem("redirectTo", "http://localhost:59738/");
    window.location = localStorage.getItem("redirectTo");
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