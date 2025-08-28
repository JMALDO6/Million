# 🏠 Million API

API RESTful para la gestión de propiedades inmobiliarias, desarrollada en .NET 8 con arquitectura modular basada en CQRS, MediatR, FluentValidation y Entity Framework Core.

---

## 🚀 Tecnologías utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Swagger](https://swagger.io/)
- [nUnit](https://nunit.org/)
- [Moq](https://github.com/moq/moq4)
- [Docker](https://www.docker.com/)
- [Serilog](https://serilog.net/)

---
## ⚙️ Configuración Inicial
1. **Clona el repositorio:** `git clone https://github.com/JMALDO6/Million.git` 
2. **Restaura la base de datos** utilizando el backup proporcionado en el repositorio. 
3. **Creación de usuarios adicionales:** Si deseas agregar más usuarios, puedes utilizar el proyecto `Million.TestUserCreator`, diseñado para insertar usuarios directamente en la base de datos. 
4. **Ejecución local (IIS Express):** Si ejecutas el proyecto localmente, configura la cadena de conexión en `Million.API/appsettings.json` con la información de tú maquina: `"ConnectionStrings": { "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Million_JulianMaldonado;User Id=N5AppUser;Password=Test1;TrustServerCertificate=True;" }`
5. **Ejecución en Docker:** Este API puede ejecutarse dentro de un contenedor Docker. Para conectarse al SQL Server local desde Docker, utiliza la siguiente cadena de conexión: `"ConnectionStrings": { "DefaultConnection": "Server=host.docker.internal,64746;Database=Million_JulianMaldonado;User Id=N5AppUser;Password=Test1;TrustServerCertificate=True;" }`

## 🔐 Autenticación y consumo de servicios
Antes de consumir los endpoints protegidos, es necesario generar un **token JWT** mediante el endpoint de autenticación.

**1. Obtener el token** 
Realiza una petición `POST` al endpoint de login: `POST /api/Auth/login` con `Content-Type: application/json`
**Body de ejemplo:** `{ "email": "adminuser", "password": "Admin1234!" }`
Si las credenciales son válidas, recibirás una respuesta como esta: `{ "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." }`

**2. Usar el token en las peticiones** 
Incluye el token en el encabezado `Authorization` de cada solicitud a los endpoints protegidos: `Authorization: Bearer {token}`
**Ejemplo con curl:** `curl -X GET https://localhost:{puerto}/api/propiedades -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."`

💡 Puedes probar todo esto directamente desde Swagger. Una vez que obtengas el token, haz clic en "Authorize" (ícono de candado) y pégalo allí para habilitar el acceso a los endpoints protegidos.

## 📌 Endpoints disponibles 
 La API expone los siguientes endpoints para la gestión de propiedades e imágenes: 
 - `POST /api/Properties` → Crear una nueva propiedad 
 - `GET /api/Properties` → Obtener propiedades con filtros opcionales 
 - `PATCH /api/Properties/{propertyId}/Price` → Actualizar el precio de una propiedad 
 - `PUT /api/Properties/{propertyId}` → Actualizar una propiedad existente 
 - `POST /api/Properties/{propertyId}/PropertyImages` → Asignar imágenes a una propiedad 
 ## 👥 Roles y permisos 
 En el backup de la base de datos se han creado dos roles: `Admin` y `User`. 
 - `User`: Solo tiene acceso al endpoint `GET /api/Properties` 
 - `Admin`: Tienen acceso completo a todos los endpoints 
 > 🔐 Recuerda que para consumir los endpoints protegidos debes autenticarte y usar el token JWT en el encabezado `Authorization: Bearer {token}`
