$('#registerBtn').click(() => {
    $("#registerForm").validate({
        rules: {
            role: { required: true }
        },
        messages: {
            role: {
                required: "Please select a role<br/>"
            }
        },
    });
    if (!$('#registerForm').valid()) {
        return;
    }
    const FirstName = document.querySelector('input[name="first_name"]');
    const LastName = document.querySelector('input[name="last_name"]');
    const Email = document.querySelector('input[name="email_register"]')
    const PhoneNumber = document.querySelector('input[name="phone_number"]');
    const Password = document.querySelector('input[name="password_register"]');
    const ConfirmPassword = document.querySelector('input[name="confirm_password"]');
    const UserRole = document.querySelector('input[name="role"]:checked');
    const ProfilePhoto = document.querySelector('input[name="profile_photo"]');
    const errorMsg = document.querySelector('#registererror');
    if(!(Password.value === ConfirmPassword.value)){
        errorMsg.innerHTML = 'Passwords do not match';
        return;
    }
    const extension = ProfilePhoto.files[0].name.split('.').pop();
    const fileReader = new FileReader();
    fileReader.addEventListener('load',async()=>{
        const data = createRegisterObject(FirstName.value, LastName.value, Email.value, PhoneNumber.value, Password.value, UserRole.value, fileReader.result.split(',')[1], extension);
        await PostRegisterData(data, errorMsg);
    });
    if(ProfilePhoto.value){
        fileReader.readAsDataURL(ProfilePhoto.files[0]);
    }

    errorMsg.innerHTML = "";



});

function createRegisterObject(firstName, lastName, email, phoneNumber, password, userRole, profilePhoto, photoExtension){
    return {
        FirstName: firstName,
        LastName : lastName,
        Email: email,
        PhoneNumber: phoneNumber,
        Password: password,
        UserRole: userRole,
        ProfilePhoto: profilePhoto,
        PhotoExtension: photoExtension
    };
}

async function PostRegisterData(data, error){
    const url = "http://localhost:59738/user/register";
    fetch(url, {
        method: "POST",
        headers: {
            "content-type" : "application/json; charset=UTF-8"
        },
        body: JSON.stringify(data)
    }).then(response => response.json()).then((responseData) => {
        if (responseData.UserId != 0) {
            error.innerHTML = "Successful Registeration";
            const loginData = { Email: data.Email, Password: data.Password };
            loginUser(loginData);
        } else {
            error.innerHTML = responseData.ErrorMessage;
        }
    }).catch(error => console.log(error));
}

async function loginUser(loginData, error) {
    await login(loginData, error);
}


