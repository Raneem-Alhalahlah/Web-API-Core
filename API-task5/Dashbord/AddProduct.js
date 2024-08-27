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
