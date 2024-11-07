using EdgardInventario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdgardInventario
{
    public class Inventario
    {
        private List<Producto> listaProductos;

        public Inventario()
        {
            listaProductos = new List<Producto>();
        }

        public void AñadirProducto(Producto item)
        {
            if (EsNombreValido(item.Nombre) && EsPrecioValido(item.Precio))
            {
                listaProductos.Add(item);
                Console.WriteLine($"Producto '{item.Nombre}' añadido correctamente.");
            }
        }

        private bool EsNombreValido(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Error: El nombre no debe estar vacío.");
                return false;
            }
            return true;
        }

        private bool EsPrecioValido(double precio)
        {
            if (precio <= 0)
            {
                Console.WriteLine("Error: El precio debe ser mayor a cero.");
                return false;
            }
            return true;
        }

        public void ModificarPrecio(string nombre, double nuevoPrecio)
        {
            Producto encontrado = listaProductos.Find(p => string.Equals(p.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
            if (encontrado != null)
            {
                encontrado.Precio = nuevoPrecio;
                Console.WriteLine($"Se ha actualizado el precio de '{nombre}' a {nuevoPrecio}.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado en el inventario.");
            }
        }

        public void RemoverProducto(string nombre)
        {
            Producto productoARemover = listaProductos.Find(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (productoARemover != null)
            {
                listaProductos.Remove(productoARemover);
                Console.WriteLine($"Producto '{nombre}' eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró el producto en la lista.");
            }
        }

        public void ClasificarProductosPorPrecio()
        {
            var categorias = listaProductos
                .GroupBy(p => p.Precio < 100 ? "Bajo" : p.Precio <= 500 ? "Medio" : "Alto")
                .Select(g => new { Categoria = g.Key, Total = g.Count() });

            Console.WriteLine("Clasificación de productos por rango de precio:");
            foreach (var grupo in categorias)
            {
                Console.WriteLine($"Rango: {grupo.Categoria}, Cantidad: {grupo.Total}");
            }
        }

        public void CrearResumenInventario()
        {
            if (!listaProductos.Any())
            {
                Console.WriteLine("El inventario está vacío. No hay productos para mostrar.");
                return;
            }

            int cantidadProductos = listaProductos.Count;
            double promedioPrecios = listaProductos.Average(p => p.Precio);
            Producto productoCostoso = listaProductos.OrderByDescending(p => p.Precio).FirstOrDefault();
            Producto productoEconomico = listaProductos.OrderBy(p => p.Precio).FirstOrDefault();

            Console.WriteLine("Resumen del Inventario:");
            Console.WriteLine($"Total de productos: {cantidadProductos}");
            Console.WriteLine($"Precio promedio: {promedioPrecios:F2}");
            Console.WriteLine($"Producto más caro: {productoCostoso?.Nombre}, Precio: {productoCostoso?.Precio}");
            Console.WriteLine($"Producto más barato: {productoEconomico?.Nombre}, Precio: {productoEconomico?.Precio}");
        }

        public IEnumerable<Producto> ObtenerProductosPorPrecioMinimo(double precioMin)
        {
            return listaProductos
                .Where(p => p.Precio > precioMin)
                .OrderBy(p => p.Precio)
                .ToList();
        }
    }
}
