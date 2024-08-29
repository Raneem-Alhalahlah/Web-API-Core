async function getAllprpduct() {
  let categoryIdFromStorage = localStorage.getItem("CategoryID");
  let url;

  if (categoryIdFromStorage) {
    url = `https://localhost:7261/api/Products/GetProductByCategoryId/${categoryIdFromStorage}`;
  } else {
    url = "https://localhost:7261/api/Products";
  }

  let request = await fetch(url);
  let data = await request.json();
  let cards = document.getElementById("contener");

  data.forEach((product) => {
    cards.innerHTML += `<div class="card" style="width: 18rem;">
      <img class="card-img-top" src="${product.productImage}" alt=" (image not found)">
      <div class="card-body">
          <h5 class="card-title">${product.productName}</h5>
          <button class="btn btn-primary" onclick="storeData(${product.productId})">More Details</button>
      </div>
      </div>`;
  });
}

function storeData(productId) {
  localStorage.setItem("productId", productId);
  window.location.href = "/productDetails/productDetails.html";
}

getAllprpduct();
