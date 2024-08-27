const url = "https://localhost:7261/api/Categories";

var form = document.getElementById("form");

async function addCategory() {
  event.preventDefault();

  var formData = new FormData(form);

  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  var data = response;
  alert("Add New Category successfully");
}
