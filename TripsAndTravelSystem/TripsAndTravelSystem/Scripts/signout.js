function signout() {
    localStorage.setItem("userId", 0);
    localStorage.removeItem("redirectTo");
    const url = "http://localhost:59738/";
    window.location = url;
}

$("#signout").click(() => {
    signout();
})