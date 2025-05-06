function validateEmail() {
    const emailInput = document.getElementById("Email");
    const emailError = document.getElementById("email-error");
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Regex kiểm tra định dạng email

    if (!emailPattern.test(emailInput.value)) {
        emailError.textContent = "Email không đúng định dạng.";
        return false;
    } else {
        emailError.textContent = ""; // Xóa lỗi nếu email hợp lệ
        return true;
    }
}

function validatePhoneNumber() {
    const phoneInput = document.getElementById("SDT");
    const phoneError = document.getElementById("phone-error");
    const phonePattern = /^[0-9]{10,11}$/; // Regex kiểm tra số điện thoại (10-11 chữ số)

    if (!phonePattern.test(phoneInput.value)) {
        phoneError.textContent = "Số điện thoại không đúng định dạng. Vui lòng nhập 9-10 chữ số.";
        return false;
    } else {
        phoneError.textContent = ""; // Xóa lỗi nếu số điện thoại hợp lệ
        return true;
    }
}

function validatePassword() {
    const passwordInput = document.getElementById("MatKhau");
    const confirmPasswordInput = document.getElementById("ConfirmPassword");
    const passwordError = document.getElementById("password-error");

    if (passwordInput.value !== confirmPasswordInput.value || passwordInput.value === "") {
        passwordError.textContent = "Mật khẩu và xác nhận mật khẩu không khớp.";
        return false;
    } else {
        passwordError.textContent = ""; // Xóa lỗi nếu mật khẩu khớp
        return true;
    }
}

function checkFormValidity() {
    const nameInput = document.getElementById("Ten");
    const emailValid = validateEmail();
    const phoneValid = validatePhoneNumber();
    const passwordValid = validatePassword();

    // Kiểm tra tất cả các trường
    const isFormValid =
        nameInput.value.trim() !== "" &&
        emailValid &&
        phoneValid &&
        passwordValid;

    // Bật hoặc tắt nút "Đăng ký"
    const submitButton = document.getElementById("submit-button");
    submitButton.disabled = !isFormValid;
}

// Gắn sự kiện `oninput` cho tất cả các trường
document.getElementById("Ten").addEventListener("input", checkFormValidity);
document.getElementById("Email").addEventListener("input", checkFormValidity);
document.getElementById("SDT").addEventListener("input", checkFormValidity);
document.getElementById("MatKhau").addEventListener("input", checkFormValidity);
document.getElementById("ConfirmPassword").addEventListener("input", checkFormValidity);
