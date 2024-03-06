const buttonAddProduct = document.querySelector('#addButton');
const nameProduct = document.querySelector('#name');
const costProduct = document.querySelector('#cost');
const numberProduct = document.querySelector('#numberProduct');
const categoryProduct = document.querySelector('#category');

const descriptionProduct = document.querySelector('#description');
const yearProduct = document.querySelector('#year');
const scoreProduct = document.querySelector('#score');
const autherProduct = document.querySelector('#auther');
const countProduct = document.querySelector('#count');
const actionProduct = document.querySelector('#action');



buttonAddProduct.addEventListener('click', (event) => {

	let checkboxes1 = document.querySelectorAll('input[name="check1[]"]:checked');
	let values1 = [];
	checkboxes1.forEach((checkbox) => {
	    values1.push(checkbox.value);
	});

	let checkboxes2 = document.querySelectorAll('input[name="check2[]"]:checked');
	let values2 = [];
	checkboxes2.forEach((checkbox) => {
	    values2.push(checkbox.value);
	});
	checkEmptyFields(nameProduct, descriptionProduct, yearProduct, scoreProduct, autherProduct, countProduct);
	if (values1.length==0){
	document.querySelector('#errorCheck1').textContent = 'Должен быть выбран хотя бы один!';}
	else { document.querySelector('#errorCheck1').textContent = ''; }

	if (values2.length==0){
	document.querySelector('#errorCheck2').textContent = 'Должен быть выбран хотя бы один!';}
	else { document.querySelector('#errorCheck2').textContent = ''; }

	if (actionProduct.value=="") {
	document.querySelector('#errorAction').textContent = 'Выберите возрастное ограничение!';}
	else {document.querySelector('#errorAction').textContent = '';}


});

const checkEmptyFields = (nameProduct, descriptionProduct, yearProduct, scoreProduct, autherProduct, countProduct) => {
    if (!nameProduct.value) {  
        document.querySelector('#errorName').textContent = 'Поле нужно заполнить!';
    } else {
	if (/^[ ,.]+$/.test(nameProduct.value)) {document.querySelector('#errorName').textContent = 'Поле нужно заполнить!';}
	else {document.querySelector('#errorName').textContent = '';checkName(nameProduct);}}

    if (!descriptionProduct.value) {   
        document.querySelector('#errorDescription').textContent = 'Поле нужно заполнить!';
    } else {
	if (/^[ ,.]+$/.test(descriptionProduct.value)) {document.querySelector('#errorDescription').textContent = 'Поле нужно заполнить!';}
	else {document.querySelector('#errorDescription').textContent = ''; }}

	if (document.getElementById("pict").files.length === 0) {
  document.querySelector('#errorImages').textContent = 'Выберете файл!';
}else {document.querySelector('#errorImages').textContent = '';}

	if (!yearProduct.value) {   
        document.querySelector('#errorYear').textContent = 'Поле нужно заполнить!';
    } else {
	if (/^[ ,.]+$/.test(yearProduct.value)){document.querySelector('#errorYear').textContent = 'Поле нужно заполнить!';}
	else {document.querySelector('#errorYear').textContent = '';
	checkYear(yearProduct);}}

    if (!scoreProduct.value) {   
        document.querySelector('#errorScore').textContent = 'Поле нужно заполнить!';
    } else {
	if (/^[ ,.]+$/.test(scoreProduct.value)){document.querySelector('#errorScore').textContent = 'Поле нужно заполнить!';}
	else {document.querySelector('#errorScore').textContent = '';
	checkScore(scoreProduct);}}

    if (!autherProduct.value) {   
        document.querySelector('#errorAuther').textContent = 'Поле нужно заполнить!';
    } else {
	if (/^[ ,.]+$/.test(autherProduct.value)){document.querySelector('#errorAuther').textContent = 'Поле нужно заполнить!';}
	else {document.querySelector('#errorAuther').textContent = '';
	checkAuther(autherProduct);}}

    if (!countProduct.value) {   
        document.querySelector('#errorCount').textContent = 'Поле нужно заполнить!';
    } else {
	document.querySelector('#errorCount').textContent = '';
	checkCount(countProduct);}
}

const checkName = (name) => {
document.querySelector('#errorName').textContent = '';
		if (!(/^[a-zA-ZА-Яа-яЁё :,-–-1234567890]+$/.test(name.value)))
{document.querySelector('#errorName').textContent = 'Допустимы только русские и английские буквы!';}

}

const checkDescription = (descriptione) => {
document.querySelector('#errorDescription').textContent = '';
	if (!(/^[a-zA-ZА-Яа-яЁё ,.-–-1234567890\n]+$/.test(descriptione.value)))
{document.querySelector('#errorDescription').textContent = 'Допустимы только русские и английские буквы!';}

}

const checkScore = (score) => {
document.querySelector('#errorScore').textContent = '';


    if(!(Number(score.value) >= 0) || !(Number(score.value) <= 10)) {
        document.querySelector('#errorScore').textContent = 'Оценка должна быть в диапазоне от 0 до 10!';
    }
	if ((Number(score.value) === score.value && score.value % 1 !== 0)) {
	document.querySelector('#errorScore').textContent = 'В оценке не может быть символов!';
        
    } 

}

const checkAuther = (auther) => {
document.querySelector('#errorAuther').textContent = '';
	if (!(/^[a-zA-ZА-Яа-яЁё ,.-–-1234567890]+$/.test(auther.value)))
{document.querySelector('#errorAuther').textContent = 'Допустимы только русские и английские буквы!';}

}

const checkCount = (count) => {
  
   document.querySelector('#errorCount').textContent = '';
   if(!(Number(count.value) >= 0)) {
        document.querySelector('#errorCount').textContent = 'Количество серий не может быть меньше нуля!';
       
    } 
   if(!(count.value == parseInt(count.value, 10))) {
	document.querySelector('#errorCount').textContent = 'Количество серий - целое число!';
        
    } 

if(isNaN(count.value)) {
	document.querySelector('#errorCount').textContent = 'В количестве серий не может быть символов!';
     
    } 
}


const checkYear = (year) => {
  
   document.querySelector('#errorYear').textContent = '';
   if(!(Number(year.value) >= 1900) || !(Number(year.value) <= 2025)) {
        document.querySelector('#errorYear').textContent = 'Год выпуска должен быть от 1900 до 2025!';
       
    } 
   if(!(year.value == parseInt(year.value, 10))) {
	document.querySelector('#errorYear').textContent = 'Год выпуска - целое число!';
        
    } 

if(isNaN(year.value)) {
	document.querySelector('#errorYear').textContent = 'В году выпуска не может быть символов!';
    } 
}

