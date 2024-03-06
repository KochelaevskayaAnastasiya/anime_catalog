/*------------------Перемотка наверх--------------------------*/
const backToTop = document.getElementById("back-to-top");
backToTop.addEventListener('click', (event) => {
    event.preventDefault();
    window.scrollTo({
  top: 0,
  behavior: "smooth",});
  });

/*------------------Добавление текста при копировании--------------------------*/
document.addEventListener('copy', (event) => {
  const pagelink = '\n\nБольше информации на сайте Animeworld';
  event.clipboardData.setData('text/plain', document.getSelection() + pagelink);
  event.preventDefault();
});

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
