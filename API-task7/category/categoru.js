async function getAllCategories() {
  const url = "https://localhost:7261/api/Categories/getAllCategories";
  var respons = await fetch(url);
  var result = await respons.json();

  var contener = document.getElementById("contriner");

  result.forEach((category) => {
    contener.innerHTML += `
      <div class="card" style="width: 18rem;">
        <img class="card-img-top" src="${category.categoryImage}" alt=" (image not found)">
        <div class="card-body">
          <h5 class="card-title">${category.categoryName}</h5>
          <button class="btn btn-primary" onclick="storeID(${category.categoryId})">product</button>

        </div>
      </div>`;
  });
}

function storeID(categoryId) {
  localStorage.setItem("CategoryID", categoryId);
  window.location.href = "/product/product.html";
}

getAllCategories();
