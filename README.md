# Proyecto: Sistema de Gestión de Tienda
**Estudiante:** Yansell
**Institución:** UNICDA

## Descripción
Este proyecto es una aplicación web desarrollada en **ASP.NET Core MVC** que gestiona Clientes, Categorías, Productos y Pedidos.

## Tecnologías Utilizadas
* **Lenguaje:** C#
* **Framework:** .NET 9.0 (MVC)
* **ORM:** Entity Framework Core
* **Base de Datos:** SQL Server

## Requisitos del RAE 5 Cumplidos
1. **Relaciones de Base de Datos:**
   - **1 a Muchos (1:N):** Una Categoría tiene varios Productos.
   - **Muchos a Muchos (N:N):** Un Pedido puede tener múltiples Productos a través de la tabla `PedidoProducto`.
2. **Consultas con LINQ (Eager Loading):**
   - Uso de `.Include()` para mostrar la Categoría en la lista de Productos.
   - Uso de `.ThenInclude()` para mostrar los detalles de productos dentro de un Pedido.
3. **Validaciones:**
   - Validación personalizada en el modelo `Cliente` para verificar mayoría de edad (+18).

## Cómo ejecutar el proyecto
1. Clonar el repositorio.
2. Configurar la cadena de conexión en `appsettings.json`.
3. Ejecutar las migraciones:
   ```bash
   dotnet ef database update
