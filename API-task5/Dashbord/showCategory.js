async function getAllCategories() {
  const url = "https://localhost:7261/api/Categories/getAllCategories";
  var respons = await fetch(url);
  var result = await respons.json();

  var contener = document.getElementById("tableForCategory");

  result.forEach((category) => {
    contener.innerHTML += ` <tr>
          <td scope="row">${category.categoryId}</td>
          <td>${category.categoryName}</td>
  <td>
          <img src="${category.categoryImage}" alt="No Image Available" style="width: 50px; height: 50px;" onerror="this.onerror=null; this.src='default-image-path.png';" />
        </td>          <td><a href="../Dashbord/EditCategory.html" onclick="storeID(${category.categoryId})">Edit</a></td>
        </tr>
       `;
  });
}

function storeID(categoryId) {
  localStorage.setItem("CategoryID", categoryId);
}

getAllCategories();
