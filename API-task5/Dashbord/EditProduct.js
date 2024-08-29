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

let n = Number(localStorage.getItem("productId"));
const url = `https://localhost:7261/api/Products/${n}`;

var form = document.getElementById("form");

async function updateProduct() {
  event.preventDefault();

  var formData = new FormData(form);

  let response = await fetch(url, { method: "PUT", body: formData });
  var data = response;
  alert(" product is updated");
}
