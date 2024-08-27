let c = Number(localStorage.getItem("CategoryID"));
const url = `https://localhost:7261/api/Categories/${c}`;

var form = document.getElementById("form");

async function updatecategory() {
  event.preventDefault();

  var formData = new FormData(form);

  let response = await fetch(url, { method: "PUT", body: formData });
  var data = response;
  alert("Category is updated");
}
