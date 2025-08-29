# Proyecto de Gestión de Equipos

Sistema de gestión de equipos desarrollado con .NET 9 Blazor Server, Entity Framework Core y MySQL.

## Requerimientos del Servidor

### Software Necesario

- **.NET 9 SDK** - [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/9.0)
- **MySQL Server 8.0+** - [Descargar aquí](https://dev.mysql.com/downloads/mysql/)
- **MinIO Server** (opcional) - Para almacenamiento de archivos - [Descargar aquí](https://min.io/download)

### Configuración de Base de Datos

1. Instalar MySQL Server
2. Crear la base de datos:
   ```sql
   CREATE DATABASE proyecto_QA;
   ```
3. Configurar usuario y permisos según sea necesario

### Configuración de MinIO (Opcional)

Si planeas usar la funcionalidad de almacenamiento de archivos:

1. Instalar MinIO Server
2. Configurar bucket para almacenamiento de imágenes
3. El directorio `Minio/data/` sugiere una configuración local de desarrollo

## Paso a Paso para Ejecutar el Proyecto

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd proyectoobed
```

### 2. Restaurar Dependencias

```bash
dotnet restore Proyecto/Proyecto.sln
```

### 3. Configurar Base de Datos

#### Configurar Variables de Entorno

El proyecto utiliza variables de entorno para la cadena de conexión a la base de datos. Puedes configurar la variable de entorno `db` en un archivo `.env` en el directorio del proyecto:

**Opción 1: Archivo .env**
```bash
# Crear archivo .env en el directorio raíz del proyecto
db=Server=localhost;Database=proyecto_QA;Uid=tu_usuario;Pwd=tu_password;
```

**Opción 2: Variables de entorno del sistema**
```bash
# Linux/macOS
export ConnectionStrings__DefaultConnection="Server=localhost;Database=proyecto_QA;Uid=tu_usuario;Pwd=tu_password;"

# Windows (PowerShell)
$env:ConnectionStrings__DefaultConnection="Server=localhost;Database=proyecto_QA;Uid=tu_usuario;Pwd=tu_password;"

# Windows (CMD)
set ConnectionStrings__DefaultConnection=Server=localhost;Database=proyecto_QA;Uid=tu_usuario;Pwd=tu_password;
```

### 4. Compilar la Solución

```bash
dotnet build Proyecto/Proyecto.sln
```

### 5. Ejecutar la Aplicación

```bash
cd Proyecto
dotnet run
```

La aplicación estará disponible en:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

## Estructura del Proyecto

```
proyectoobed/
├── Proyecto/           # Aplicación principal Blazor Server
├── BD/                 # Capa de datos Entity Framework
├── Bucket/             # Servicio MinIO para almacenamiento
├── Bycript/            # Servicio de hash BCrypt
└── README.md
```

## Funcionalidades Principales

- **Autenticación**: Sistema basado en cookies con roles (Admin/Usuario)
- **Gestión de Equipos**: CRUD completo para equipos y componentes
- **Gestión de Empleados**: Control de personal y asignaciones
- **Reportes**: Sistema de reportes y bitácora de equipos
- **Almacenamiento**: Gestión de imágenes y archivos con MinIO

## Tecnologías Utilizadas

- **.NET 9**: Framework principal
- **Blazor Server**: Frontend interactivo del lado del servidor
- **Entity Framework Core**: ORM para base de datos
- **MySQL**: Sistema de gestión de base de datos
- **MudBlazor**: Componentes de interfaz de usuario
- **AutoMapper**: Mapeo de objetos
- **BCrypt**: Hash de contraseñas
- **MinIO**: Almacenamiento de objetos

## Comandos de Desarrollo

### Compilación y Ejecución

```bash
# Compilar toda la solución
dotnet build Proyecto/Proyecto.sln

# Ejecutar en modo desarrollo
cd Proyecto
dotnet run

# Ejecutar en modo release
dotnet run --configuration Release
```

## Configuración de Desarrollo

### Autenticación

El sistema utiliza autenticación basada en cookies con:
- Políticas de autorización: "AdminOnly" y "UserOnly"
- Expiración de cookies: 7 días
- Endpoints de API: `/api/auth/login` y `/api/auth/logout`

### Base de Datos

- **Nombre**: `proyecto_QA`
- **Proveedor**: MySQL con `MySql.EntityFrameworkCore`
- **Modelos principales**: Area, Empleado, Equipo, Reporte, Usuario, etc.

## Notas Importantes

- **Seguridad**: Las credenciales de la base de datos se configuran mediante variables de entorno
- **Naming**: Algunos nombres de interfaces contienen errores tipográficos
- **Arquitectura**: Separación limpia con DTOs, servicios y capa de datos

## Solución de Problemas

1. **Error de conexión a base de datos**: Verificar que MySQL esté ejecutándose y las credenciales sean correctas
2. **Problemas de compilación**: Ejecutar `dotnet restore` y `dotnet clean` seguido de `dotnet build`
3. **Errores de migración**: Verificar que la base de datos existe y el usuario tiene permisos

