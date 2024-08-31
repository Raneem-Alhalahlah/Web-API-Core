async function getAllprpduct() {
  const url = "https://localhost:7261/api/CartItem";

  let request = await fetch(url);
  let data = await request.json();
  let cards = document.getElementById("tbody");

  data.forEach((product) => {
    cards.innerHTML += ` <tr>
              <td>${product.cartId}</td>
              <td>${product.product.productName}</td>
              <td> <input type="number" id="cartidQuantity" placeholder="${product.quantity}"></td>
              <td><button onclick="editQuantity(${product.cartItemId})"value="Edit">Edit</td>
              <td><button onclick="deletItem(${product.cartItemId})"value="Edit">delet</td>

          </tr>`;
  });
}

function storeData(productId) {
  localStorage.setItem("productId", productId);
}

getAllprpduct();

async function editQuantity(id) {
  let url = `https://localhost:7261/api/CartItem/${id}`;
  var cartItemID = document.getElementById("cartidQuantity");
  var object = {
    cartItemId: id,
    quantity: cartItemID.value,
  };
  let request = await fetch(url, {
    method: "PUT",
    body: JSON.stringify(object),
    headers: {
      "Content-Type": "application/json",
    },
  });
  alert("Item Edited Successfully!");
}

async function deletItem(id) {
  let url = `https://localhost:7261/api/CartItem/DeleteItem/${id}`;
  var cartItemID = document.getElementById("cartidQuantity");
  var object = {
    cartItemId: id,
  };
  let request = await fetch(url, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  });
  alert("Item Deleted Successfully!");
}
