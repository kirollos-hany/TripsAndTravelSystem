$('.fa-thumbs-up').click(function () {
    $(this).toggleClass('fas far');
})
$('.fa-star').click(function () {
    $(this).toggleClass('fas far');
})

async function Likes(L, postId) {
        if (document.getElementById("my" + postId).innerHTML == L) {
            document.getElementById("my" + postId).innerHTML = L + 1;
            await EditPostLikes(L + 1, postId); 
        }
        else {
            document.getElementById("my" + postId).innerHTML = L;
            await EditPostLikes(L, postId);
        }
}
