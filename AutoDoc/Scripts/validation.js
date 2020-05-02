var ADValidation = {
    libName: "Auto Doc Validation"
};

ADValidation.ValidateEmail = function (email) {
    if (email.match("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$")) {
        return true;
    } else {
        return false;
    }
}

ADValidation.ValidatePassword = function (password) {
    if (password.length > 6) {
        return true;
    } else {
        return false;
    }
}

ADValidation.ValidateName = function (name) {
    if (name.length > 0) {
        return true;
    } else {
        return false;
    }
}