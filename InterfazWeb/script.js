class Product {
    constructor(id, name, quantity, price) {
        this.id = id;
        this.name = name;
        this.quantity = quantity;
        this.price = price;
    }
}

class Inventory {
    constructor() {
        this.products = [];
        this.nextId = 1;
    }

    addProduct(name, quantity, price) {
        const product = new Product(this.nextId++, name, quantity, price);
        this.products.push(product);
        console.log(`Producto agregado con ID: ${product.id}`);
        this.showProducts(); // Actualizar la tabla después de agregar
    }

    showProducts() {
        const tbody = document.querySelector('#tablaProductos tbody');
        tbody.innerHTML = '';

        if (this.products.length === 0) {
            console.log("No hay productos en el inventario.");
            return;
        }

        this.products.forEach(product => {
            const fila = document.createElement('tr');

            fila.innerHTML = `
                <td>${product.id}</td>
                <td>${product.name}</td>
                <td>${product.quantity}</td>
                <td>$${product.price.toFixed(2)}</td>
                <td><button class="eliminar" onclick="eliminarProducto(${product.id})">Eliminar</button></td>
            `;

            tbody.appendChild(fila);
        });
    }

    searchProduct(id) {
        const product = this.products.find(p => p.id === id);
        if (product) {
            alert(`ID: ${product.id} | Nombre: ${product.name} | Cantidad: ${product.quantity} | Precio: $${product.price.toFixed(2)}`);
        } else {
            alert(`Producto con ID ${id} no encontrado.`);
        }
    }

    deleteProduct(id) {
        const index = this.products.findIndex(p => p.id === id);
        if (index !== -1) {
            this.products.splice(index, 1);
            console.log(`Producto con ID ${id} eliminado.`);
            this.showProducts(); // Actualizar la tabla después de eliminar
        } else {
            console.log(`Producto con ID ${id} no encontrado.`);
        }
    }
}

// Instancia global del inventario
const inventory = new Inventory();

// Función para agregar un producto desde el formulario
document.getElementById('formAgregar').addEventListener('submit', function (event) {
    event.preventDefault();

    const nombre = document.getElementById('nombre').value;
    const cantidad = parseInt(document.getElementById('cantidad').value);
    const precio = parseFloat(document.getElementById('precio').value);

    if (nombre && !isNaN(cantidad) && !isNaN(precio)) {
        inventory.addProduct(nombre, cantidad, precio);
        document.getElementById('formAgregar').reset();
    } else {
        alert('Por favor, completa todos los campos correctamente.');
    }
});

// Función para eliminar un producto
window.eliminarProducto = function (id) {
    inventory.deleteProduct(id);
};

// Función para buscar un producto por ID
document.getElementById('btnBuscar').addEventListener('click', function () {
    const searchId = parseInt(prompt("Ingresa el ID del producto a buscar:"));
    if (!isNaN(searchId)) {
        inventory.searchProduct(searchId);
    } else {
        alert("Error: Ingresa un ID válido.");
    }
});