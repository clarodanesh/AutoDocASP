$(document).ready(function () {
    console.log("ready!");

    $("#register-btn").click(function (e) {
        let email = $('#email').val();
        let password = $('#password').val();
        let fname = $('#firstname').val();
        let lname = $('#lastname').val();

        if (ADValidation.ValidateEmail(email) && ADValidation.ValidatePassword(password) && ADValidation.ValidateName(fname) && ADValidation.ValidateName(lname)) {
            return true;
        } else {
            e.preventDefault();
            $('#auth-form-error').html('Fill the form properly');
        }
    });
});