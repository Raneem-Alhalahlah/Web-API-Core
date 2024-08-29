const urll = "https://localhost:7261/api/Categories/getAllCategories";
var option = document.getElementById("dropdown");

async function getAllCategories() {
  let response2 = await fetch(urll);
  let AllCategories = await response2.json();
  AllCategories.forEach((element) => {
    let option = document.createElement("option");
    option.value = element.categoryId;
    option.textContent = element.categoryName;
    dropdown.appendChild(option);
  });
}

getAllCategories();

const url = "https://localhost:7261/api/Products";

var form = document.getElementById("form");

async function addproduct() {
  event.preventDefault();

  var formData = new FormData(form);

  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  var data = response;
  alert("Add New product successfully");
}
