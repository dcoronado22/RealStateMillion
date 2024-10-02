
# RealEstateMillion

RealEstateMillion es un sistema de gestión inmobiliaria diseñado para manejar propiedades de alto valor. Utiliza una arquitectura hexagonal simplificada para mantener una clara separación de responsabilidades y facilitar la escalabilidad y mantenimiento del sistema.

## Características

- Gestión de propiedades y propietarios
- Filtrado avanzado de propiedades
- Seguimiento de imágenes de propiedades
- Registro de trazas de propiedades (historial de ventas, etc.)

## Tecnologías Utilizadas

- .NET 8
- Entity Framework Core
- SQL Server
- C#

## Estructura del Proyecto

El proyecto sigue una arquitectura hexagonal (también conocida como "Puertos y Adaptadores") y está organizado en las siguientes capas:

- `RealEstateMillion.Domain`: Contiene las entidades del dominio y las interfaces del repositorio.
- `RealEstateMillion.Application`: Contiene la lógica de negocio y los servicios de aplicación.
- `RealEstateMillion.Infrastructure`: Implementa los repositorios y maneja la persistencia de datos.
- `RealEstateMillion.Api`: Expone los endpoints RESTful para interactuar con el sistema.

## Configuración

1. Clona el repositorio:

```bash
git clone https://github.com/dcoronado22/RealEstateMillion.git
```

2. Restaura los paquetes NuGet:

```bash
dotnet restore
```

3. Actualiza la cadena de conexión en `appsettings.json` en el proyecto API:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tuservidor;Database=RealEstateMillion;User Id=tuusuario;Password=tucontraseña;"
}
```

4. Aplica las migraciones para crear la base de datos:

```bash
dotnet ef database update --project RealEstateMillion.Infrastructure --startup-project RealEstateMillion.Api
```

En caso de no funcionar el anterior ejecutra este:

```bash
dotnet ef database update --project RealEstateMillion.Infrastructure/RealEstateMillion.Infrastructure.csproj --startup-project RealStateMillion.Api/RealStateMillion.Api.csproj
```

## Uso

1. Ejecuta el proyecto API:

La API estará disponible en https://localhost:5001 (o el puerto que hayas configurado).
Puedes usar herramientas como Postman o Swagger (si está configurado) para probar los endpoints.

## Endpoints Principales

- **GET** `/api/properties`: Obtiene todas las propiedades.
- **GET** `/api/properties/{id}`: Obtiene una propiedad específica.
- **POST** `/api/properties`: Crea una nueva propiedad.
- **PUT** `/api/properties/{id}`: Actualiza una propiedad existente.
- **DELETE** `/api/properties/{id}`: Elimina una propiedad.
- **GET** `/api/properties/filter`: Filtra propiedades basadas en varios criterios.
