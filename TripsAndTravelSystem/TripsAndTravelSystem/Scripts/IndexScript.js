window.addEventListener('beforeunload', (e) => {
    e.preventDefault();
    localStorage.clear();
});
const userId = localStorage.getItem("userId");
if (userId != 0 && localStorage.getItem("redirectTo") !== null) {
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
    console.log(localStorage.getItem("redirectTo"));
    window.location = localStorage.getItem("redirectTo");
}

$('.fa-thumbs-up').click(function () {
    $(this).toggleClass('fas far');
})
$('.fa-star').click(function () {
    $(this).toggleClass('fas far');
})

async function Likes(L, postId) {
        if (document.getElementById("my" + postId).innerHTML == L) {
            document.getElementById("my" + postId).innerHTML = L + 1;
        }
        else {
            document.getElementById("my" + postId).innerHTML = L;
        }
}
