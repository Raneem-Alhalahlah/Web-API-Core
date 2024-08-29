async function getAllprpduct() {
  const url = "https://localhost:7261/api/CartItem";

  let request = await fetch(url);
  let data = await request.json();
  let cards = document.getElementById("tbody");

  data.forEach((product) => {
    cards.innerHTML += ` <tr>
              <td>${product.cartId}</td>
              <td>${product.product.productName}</td>
              <td>${product.quantity}</td>
               <td>Edit</td>
                 <td>
          </td>
          </tr>`;
  });
}

function storeData(productId) {
  localStorage.setItem("productId", productId);
}

getAllprpduct();
