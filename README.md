# LibreriaGI - Sistema de Gestión de Inventario para Librería

## 📚 Descripción del Proyecto

**LibreriaGI** es un sistema completo de gestión de inventario diseñado específicamente para librerías. El proyecto implementa una arquitectura cliente-servidor que permite el control eficiente de productos, categorías, ingresos y salidas de inventario.

## 🏗️ Arquitectura del Sistema

El proyecto está estructurado en dos componentes principales:

### 🖥️ Libreria.Server (API Backend)
- **Framework**: ASP.NET Core 8.0
- **Base de Datos**: SQL Server con Entity Framework Core
- **Patrón**: API REST con controladores
- **Documentación**: Swagger/OpenAPI integrado
- **Mapeo**: Actualmente, los DTOs se manejan manualmente

### 💻 Libreria.Client (Frontend Web)
- **Framework**: ASP.NET Core 9.0 MVC
- **UI**: Bootstrap para interfaz responsiva
- **Comunicación**: HTTP Client para consumo de API
- **Validación**: jQuery Validation para formularios

## 🗄️ Modelos de Datos

### 📖 Categoría
- Gestión de clasificaciones de productos
- Relación uno a muchos con productos

### 📦 Producto
- Información básica del producto (nombre, descripción, precio)
- Control de stock actual
- Asociación con categoría
- Relaciones con movimientos de inventario

### ➕ Ingreso
- Registro de entrada de productos al inventario
- Fecha, cantidad y comentarios
- Vinculación con producto específico

### ➖ Salida
- Registro de salida de productos del inventario
- Fecha, cantidad y motivo de salida
- Control de stock disponible

## 🚀 Funcionalidades Principales

- **Gestión de Categorías**: Crear, editar, eliminar y consultar categorías
- **Gestión de Productos**: CRUD completo con control de stock
- **Control de Inventario**: Seguimiento de ingresos y salidas
- **Interfaz Web**: Panel de administración intuitivo y responsivo

## 🛠️ Tecnologías Utilizadas

- **Backend**: ASP.NET Core 8.0, Entity Framework Core, SQL Server
- **Frontend**: ASP.NET Core MVC, Bootstrap, jQuery
- **Herramientas**: Swagger, Entity Framework Tools
- **Base de Datos**: SQL Server con migraciones automáticas

## 📁 Estructura del Proyecto

```
LibreriaGI/
├── Libreria.Server/          # API Backend
│   ├── Controllers/          # Controladores de la API
│   ├── Models/               # Modelos de datos
│   ├── Data/                 # Contexto de base de datos
│   ├── DTO/                  # Objetos de transferencia
│   └── Migrations/           # Migraciones de base de datos
├── Libreria.Client/          # Aplicación Web Frontend
│   ├── Controllers/          # Controladores MVC
│   ├── Views/                # Vistas Razor
│   ├── Models/               # Modelos de vista
│   └── wwwroot/              # Archivos estáticos
└── LibreriaGI.sln           # Solución de Visual Studio
```

## 🚀 Instalación y Configuración

### Prerrequisitos
- .NET 8.0 SDK o superior
- SQL Server (LocalDB o servidor)
- Visual Studio 2022 o VS Code

### Pasos de Instalación
1. Clonar el repositorio
2. Crear archivo `appsettings.json` con la configuración de base de datos (ver ejemplo abajo)
3. Ejecutar migraciones de base de datos
4. Compilar y ejecutar ambos proyectos

### 🔐 Configuración Segura de Base de Datos

**IMPORTANTE**: Por seguridad, el archivo `appsettings.json` no se incluye en el repositorio.

Crea un archivo `appsettings.json` en la carpeta `Libreria.Server/` con una de las siguientes opciones:

#### 🌐 **Opción 1: Base de Datos Azure SQL (Producción)**
```json
{
  "ConnectionStrings": {
    "cnx": "Server=tcp:tu-servidor.database.windows.net,1433;Initial Catalog=LibreriaGI;Persist Security Info=False;User ID=tu-usuario;Password=tu-contraseña-fuerte;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

#### 💻 **Opción 2: Base de Datos Local (Desarrollo)**
```json
{
  "ConnectionStrings": {
    "cnx": "Data Source=.;Initial Catalog=LibreriaGI;Integrated Security=True;Persist Security Info=True;Encrypt=False;Trust Server Certificate=True"
  }
}
```

**Recomendaciones de Seguridad:**
- Usa contraseñas fuertes (mínimo 16 caracteres) para Azure
- Configura reglas de firewall en Azure SQL Database
- Considera usar Azure Key Vault para credenciales en producción
- Para desarrollo local, usa Windows Authentication cuando sea posible

## 📊 Características del Sistema

- **Gestión Completa de Inventario**: Control total sobre productos y stock
- **Interfaz Intuitiva**: Diseño responsivo y fácil de usar
- **API REST**: Arquitectura escalable y mantenible
- **Validaciones**: Control de datos en frontend y backend
- **Base de Datos Relacional**: Estructura normalizada y eficiente

## 🎯 Casos de Uso

- Librerías que requieren control de inventario
- Gestión de stock de productos educativos
- Control de ingresos y salidas de mercancía
- Administración de categorías de productos

## 🔧 Mantenimiento

El sistema incluye:
- Migraciones automáticas de base de datos
- Validaciones de datos
- Interfaz de administración completa

## 🚀 Futuras Implementaciones

- **Sistema de Reportes**: Generación de reportes de inventario, movimientos y estadísticas
- **Dashboard**: Panel principal con métricas y gráficos de rendimiento
- **Notificaciones**: Alertas de stock bajo y vencimientos
- **Exportación de Datos**: Funcionalidad para exportar información en diferentes formatos
- **Auditoría**: Registro detallado de cambios y movimientos en el sistema
- **Logging y Manejo de Errores**: Sistema de registro de eventos y manejo robusto de errores
- **Integración de AutoMapper**: Automatizar la transformación de datos entre modelos y DTOs para reducir el trabajo manual

## 📝 Licencia

Este proyecto está bajo la licencia especificada en el archivo LICENSE.txt.

---

**Desarrollado para el curso de Desarrollo de Servicios Web I - V Ciclo**