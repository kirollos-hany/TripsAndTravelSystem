$("#addPostBtn").click(async () => {
    $("#addPostForm").validate();
    if (!$("#addPostForm").valid()) {
        return;
    }
    const photoFile = document.querySelector('input[name="trip_image"]');
    const extension = photoFile.files[0].name.split('.').pop();
    const fileReader = new FileReader();
    fileReader.addEventListener('load',async () => {
        const photo = fileReader.result.split(',')[1];
        const data = {
            Price: document.querySelector('input[name="trip_price"]').value,
            Title: document.querySelector('input[name="trip_title"]').value,
            Details: document.querySelector('textarea[name="trip_details"]').value,
            TripDate: document.querySelector('input[name="trip_date"]').value,
            Destination: document.querySelector('input[name="trip_destination"]').value,
            TripPhoto: photo,
            PhotoExtension: extension
        };
        await RegisterPostData(data, document.querySelector("#newPosterror"));
    });
    if (photoFile.value) {
        fileReader.readAsDataURL(photoFile.files[0]);
    }
});

async function RegisterPostData(data, error) {
    const url = "http://localhost:59738/agency/addnewpost";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then(responseData => {
        if (responseData.UserId != 0) {
            error.innerHTML = responseData.ErrorMessage;
            window.location.reload();
        } else {
            error.innerHTML = responseData.ErrorMessage;
        }
    });
}