var n = Number(localStorage.getItem("productId"));

const url = `https://localhost:7261/api/Products/GetProductById?id=${n}`;
async function getproduct() {
  let request = await fetch(url);
  let data = await request.json();
  let cards = document.getElementById("contener");
  console.log(data);

  cards.innerHTML += `      <div class="card" style="width: 18rem;">
      <img class="card-img-top" src="${data.productImage}" alt=" (image not found)">
      <div class="card-body">
          <h5 class="card-title">${data.productName}</h5>
          <h5 class="card-title">${data.description}</h5>
          <h5 class="card-title">${data.price}</h5>
      </div>
      </div>;`;
}

getproduct();
