$(document).ready(function () {
    console.log("ready!");

    $("#login-btn").click(function (e) {
        let email = $('#email').val();
        let password = $('#password').val();

        if (ADValidation.ValidateEmail(email) && ADValidation.ValidatePassword(password)) {
            return true;
        } else {
            e.preventDefault();
            $('#auth-form-error').html('Email or Password is incorrect');
        }
    });
});