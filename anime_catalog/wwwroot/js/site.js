// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*------------------Поиск--------------------------*/

document.getElementById('button_reseach').onclick = function () {
    // Remove any element-specific value, falling back to stylesheets
    document.getElementById('research').style.display = 'block';
    document.getElementById('button_reseach').style.display = 'none';
};

// Закрыть раскрывающийся список, если пользователь щелкнет за его пределами.
document.getElementById('main').onclick = function () {
    // Remove any element-specific value, falling back to stylesheets
    document.getElementById('research').style.display = 'none';
    document.getElementById('button_reseach').style.display = 'block';
};