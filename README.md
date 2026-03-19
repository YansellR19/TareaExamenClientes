Sistema de Tienda con Seguridad - UNICDA
Estudiante: Yansell

Asignatura: Programación III

Entrega: Examen Parcial 2 (RAE 6)

Descripción
Este proyecto evoluciona el sistema de gestión de tienda agregando una capa completa de Autenticación y Autorización utilizando ASP.NET Core Identity.

Características de Seguridad (RAE 6)
Se implementaron los siguientes requisitos técnicos:

Identity & EF Core: Configuración de IdentityDbContext y migraciones para tablas de seguridad (AspNetUsers, AspNetRoles).

Gestión de Roles: Implementación de 3 niveles de acceso: Admin, Editor y Cliente.

Protección de Rutas: Uso de atributos [Authorize] para restringir el acceso a controladores y acciones específicas.

Política Personalizada: Creación de la política SoloAdminUnicda que valida el rol y el dominio del correo electrónico.

UI Adaptativa: El menú de navegación oculta o muestra opciones (como el Panel Admin) según el usuario logueado.

Acceso Denegado: Página personalizada para informar a los usuarios cuando no tienen permisos suficientes.

Credenciales de Prueba
Para facilitar la corrección, el sistema cuenta con un Seeder que crea automáticamente el siguiente acceso:

Usuario Admin: admin@unicda.edu.do

Contraseña: Admin123!

Rol: Administrador Total

Instalación
Clonar el repositorio.

Actualizar la cadena de conexión en appsettings.json.

Ejecutar las migraciones:

Bash

dotnet ef database update
Iniciar la aplicación:

Bash

dotnet run