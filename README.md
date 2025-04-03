# Gestión de Pedidos API

API RESTful para la gestión de pedidos, construida con .NET 8, CQRS, Clean Architecture, MediatR, FluentValidation y autenticación JWT.

---

## 🚀 Características principales

- Crear, consultar, actualizar y eliminar pedidos
- Validación de transiciones de estado
- Historial de cambios de estado por pedido
- Autenticación mediante JWT
- Documentación interactiva con Swagger

---

## 🛠️ Tecnologías

- ASP.NET Core 8
- Entity Framework Core
- MediatR (CQRS)
- FluentValidation
- JWT Authentication
- Swagger (Swashbuckle)

---

## ▶️ Cómo ejecutar

1. Clona el repositorio:

```bash
git clone https://github.com/franjarop/GestionPedidosAPI.git
cd GestionPedidosAPI
```

2. Ejecuta el proyecto:

```bash
dotnet run --project GestionPedidosAPI
```

3. Abre Swagger en tu navegador:

```
https://localhost:7140/swagger
```

---

## Autenticación

Para acceder a los endpoints protegidos (crear, actualizar, eliminar), debes autenticarte usando JWT.

### Login

```
POST /api/pedidos/login
```

Body:

```json
{
  "user": "javiss",
  "password": "1717"
}
```

Te devolverá un `token`:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

### Usar el token en Swagger

1. Haz clic en el botón `Authorize`
2. Pega el token con el prefijo **Bearer**:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

3. Presiona `Authorize` y luego podrás probar los endpoints protegidos.

---

## 🔓 Endpoints públicos

| Método | Ruta                          | Descripción                              |
|--------|-------------------------------|------------------------------------------|
| GET    | `/api/pedidos/obtener`        | Lista todos los pedidos                  |
| GET    | `/api/pedidos/obtenerporid/{id}` | Obtiene un pedido por su ID          |
| POST   | `/api/pedidos/login`          | Obtiene un token JWT                     |

---

## 🔐 Endpoints protegidos (requieren token)

| Método | Ruta                            | Descripción                            |
|--------|---------------------------------|----------------------------------------|
| POST   | `/api/pedidos/registrar`        | Crear un nuevo pedido                  |
| PUT    | `/api/pedidos/actualizar`       | Actualizar estado del pedido           |
| DELETE | `/api/pedidos/eliminarporid/{id}` | Eliminar un pedido (y su historial)  |

---

## 📦 Notas

- La base de datos utilizada es SQL Server (puedes ajustar la cadena de conexión en `appsettings.json`)

---

## 📬 Contacto

Desarrollado por: **Javier Robles**  
