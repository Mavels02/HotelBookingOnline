function showRegisterForm() {
    document.getElementById("login-form").style.display = "none";
    document.getElementById("register-form").style.display = "block";
}

function showLoginForm() {
    document.getElementById("register-form").style.display = "none";
    document.getElementById("login-form").style.display = "block";
}
function validateEmail() {
    const emailInput = document.getElementById("Email");
    const emailError = document.getElementById("email-error");
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Regex kiểm tra định dạng email
    if (emailInput.value.trim() === "") {
        emailError.textContent = "Email không được để trống."
        emailError.style.color = "red";
        return false;
    }
    if (!emailPattern.test(emailInput.value)) {
        emailError.textContent = "Email không đúng định dạng.";
        return false;
    } else {
        emailError.textContent = ""; // Xóa lỗi nếu email hợp lệ
        return true;
    }
}
function validatePassword() {
    const passwordInput = document.getElementById("MatKhau");
    const passwordError = document.getElementById("MatKhau-error");

    if (passwordInput.value.trim() === "") {
        passwordError.textContent = "Mật khẩu không được để trống.";
        passwordError.style.color = "red";
        return false;
    } else if (passwordInput.value.length < 4) {
        passwordError.textContent = "Mật khẩu phải lớn hơn 4 ký tự.";
        passwordError.style.color = "red";
        return false;
    } else {
        passwordError.textContent = ""; // Xóa lỗi nếu mật khẩu hợp lệ
        return true;
    }
}
function validateForm() {
    const emailValid = validateEmail();
    const passwordValid = validatePassword();
    return emailValid && passwordValid; // Trả về true nếu cả email và mật khẩu hợp lệ
}

function toggleSubmitButton() {
    const submitButton = document.getElementById("submit-button");
    const isFormValid = validateForm();

    // Bật hoặc tắt nút "Đăng nhập" dựa trên trạng thái form
    if (isFormValid) {
        submitButton.disabled = false;
        submitButton.classList.remove("disabled-button");
        submitButton.classList.add("enabled-button");
    } else {
        submitButton.disabled = true;
        submitButton.classList.remove("enabled-button");
        submitButton.classList.add("disabled-button");
    }
}
// Gắn sự kiện `input` để kiểm tra trạng thái form khi người dùng nhập
document.getElementById("Email").addEventListener("input", toggleSubmitButton);
document.getElementById("MatKhau").addEventListener("input", toggleSubmitButton);

// Gắn sự kiện `submit` để kiểm tra form trước khi gửi
document.querySelector("form").addEventListener("submit", function (event) {
    if (!validateForm()) {
        event.preventDefault(); // Ngăn không cho form được gửi nếu không hợp lệ
    }
});