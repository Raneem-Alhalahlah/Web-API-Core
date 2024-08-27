async function getAllprpduct() {
  const url = "https://localhost:7261/api/Products";

  let request = await fetch(url);
  let data = await request.json();
  let cards = document.getElementById("tableForproduct");

  data.forEach((product) => {
    cards.innerHTML += ` <tr>
          <td scope="row">${product.productId}</td>
          <td>${product.productName}</td>
          <td>${product.description}</td>
            <td>${product.price}</td>
               <td>
          <img src="${product.productImage}" alt="No Image Available" style="width: 50px; height: 50px;" onerror="this.onerror=null; this.src='default-image-path.png';" />
        </td>
          <td><a href="../EditProduct.html" onclick="storeData(${product.productId})">Edit</a></td>
        </tr>`;
  });
}

function storeData(productId) {
  localStorage.setItem("productId", productId);
}

getAllprpduct();
