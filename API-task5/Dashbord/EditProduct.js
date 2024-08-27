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
