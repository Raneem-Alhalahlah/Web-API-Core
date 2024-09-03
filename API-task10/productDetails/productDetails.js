var n = Number(localStorage.getItem("productId"));

const url = `https://localhost:7261/api/Products/GetProductById?id=${n}`;
async function getproduct() {
  let request = await fetch(url);
  let data = await request.json();
  let cards = document.getElementById("contener");

  cards.innerHTML += `      <div class="card" style="width: 18rem;">
      <img class="card-img-top" src="${data.productImage}" alt=" (image not found)">
      <div class="card-body">
          <h5 class="card-title">${data.productName}</h5>
          <h5 class="card-title">${data.description}</h5>
          <h5 class="card-title">${data.price}</h5>
         <input type="number" id="inputnumber" />

       <button type="button" class="btn btn-outline-info" onclick="addToCart()">Add To Cart</button>
      </div>
      </div>;`;
}

getproduct();
var urlOfCart = "https://localhost:7261/api/CartItem";
async function addToCart() {
  var inputnumber = document.getElementById("inputnumber");
  const formData = {
    cartId: localStorage.getItem("cartId"),
    productId: localStorage.getItem("productId"),
    quantity: inputnumber.value,
  };

  var request = await fetch(urlOfCart, {
    method: "POST",
    body: JSON.stringify(formData),
    headers: {
      "Content-Type": "application/json",
    },
  });

  alert("successfuly");
}
