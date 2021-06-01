$("#favoriteBtn").hide();
$("#questionsBtn").hide();
$("#favBtnIcon").hide();


if (localStorage.getItem("userId") != 0 && localStorage.getItem("redirectTo") !== null) {
    const registerModal = document.getElementById("registerModalBtn");
    registerModal.removeAttribute('data-bs-toggle');
    registerModal.removeAttribute('data-bs-target');
    registerModal.remove();
    const loginModal = document.getElementById("loginModalBtn");
    loginModal.removeAttribute('data-bs-toggle');
    loginModal.removeAttribute('data-bs-target');
    loginModal.innerHTML = "SignOut";
    loginModal.addEventListener('click', () => {
        signout();
    });
    $("#favoriteBtn").show();
    $("#questionsBtn").show();
    $("#favBtnIcon").show();

    const errorMsg = document.getElementById("loginerror");
    const loginData = {
        Email: localStorage.getItem("email"),
        Password: localStorage.getItem("password"),
    }
    if (localStorage.getItem("redirectTo") !== window.location.href) {
        login(loginData, errorMsg);
    } else {
        loginNoRedirect(loginData, errorMsg);
    }
}


async function Likes(L, postId) {
        if (document.getElementById("my" + postId).innerHTML == L) {
            document.getElementById("my" + postId).innerHTML = L + 1;
        }
        else {
            document.getElementById("my" + postId).innerHTML = L;
        }
}
