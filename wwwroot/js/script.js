const container = document.getElementById('container');
const registerBtn = document.getElementById('registeration'); // registeration olarak düzeltildi
const loginBtn = document.getElementById('login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active"); // Burada .active olarak düzeltildi
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active"); // Burada .active olarak düzeltildi
});