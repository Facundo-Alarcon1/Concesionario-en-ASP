# 🚗 Sistema de Gestión de Concesionaria (SaaS)

Sistema web integral de alta gama desarrollado para la gestión de inventario de vehículos, control de servicios de taller, administración de empleados y emisión de comprobantes, diseñado bajo una arquitectura limpia y moderna.

## 🌟 Características Principales

*   **Arquitectura N-Capas:** Separación estricta entre Lógica de Presentación (WebForms), Lógica de Negocio (Controllers) y Acceso a Datos (DAOs).
*   **Programación Asíncrona:** Implementación profunda de `async/await` con `Task` en C# y ADO.NET (por ejemplo, `ExecuteReaderAsync()`) para maximizar el rendimiento y evitar bloqueos en el hilo principal bajo alta concurrencia.
*   **Diseño UI/UX "Alta Gama":** Interfaz de usuario minimalista y profesional utilizando **Tailwind CSS**, con paleta de colores corporativa premium (*Midnight Blue & Amber*). Componentes estandarizados (Grid Views estilizados a medida, tarjetas con sombras tenues, botones dinámicos).
*   **Gestión Integral:**
    *   **Autos:** CRUD completo con validaciones robustas (tipos de datos, rangos de precios y años lógicos).
    *   **Servicios:** Sistema de agenda (taller mecánico) asignable a empleados específicos.
    *   **Recursos Humanos (RRHH):** Módulo de empleados protegido por roles, exclusivo para Gerentes.
    *   **Seguridad y Login:** Autenticación de sesiones y control de acceso basado en puesto (Ej. Gerente vs Vendedor).

## 🛠️ Stack Tecnológico

*   **Backend:** C#, .NET Framework, ASP.NET Web Forms
*   **Patrones de Diseño:** Data Access Object (DAO), Controller Pattern, Dependency Injection (básica/manual).
*   **Base de Datos:** SQL Server, T-SQL, consultas seguras parametrizadas para evitar SQL Injection (`SqlCommand.Parameters`).
*   **Frontend:** HTML5, Tailwind CSS (via CDN), CSS Custom Properties, SVG Icons.
*   **Tooling:** Visual Studio.

## 🚀 Puntos Técnicos Destacados (Para Entrevistas)

1.  **Manejo del Ciclo de Vida ASP.NET + Tailwind:** 
    A diferencia de proyectos legados, este sistema logra dominar los controles de servidor de ASP.NET (como `GridView`) inyectando clases de utilidad de Tailwind CSS directamente desde el `CodeBehind` (`CssClass`, `HeaderStyle`, `RowStyle`), logrando que una tecnología clásica luzca como una Single Page Application (SPA) moderna de 2026.
2.  **Robustez de Datos (DAO Asíncrono):**
    Cada módulo implementa interfaces (ej. `IAutoDAO`, `IEmpleadoDAO`) lo cual permite mantener el código altamente testeable y acoplado de forma débil. El uso de `using` statements asegura la liberación inmediata de conexiones a SQL Server, garantizando que no existan *memory leaks*.
3.  **Seguridad y Validaciones:**
    Todo formulario está blindado con validaciones en código fuente (ej. `int.TryParse`, `decimal.TryParse`) y sanitización. A nivel de sesión, las páginas críticas (como Empleados) bloquean la carga y redirigen en el evento `Page_Load` si el usuario no cuenta con los privilegios de Gerencia.

## 📸 Capturas de Pantalla

*(Inserta aquí imágenes del Dashboard, Inventario de Autos, y Módulo de Empleados mostrando la paleta Midnight Blue)*

---
*Desarrollado como proyecto de exhibición técnica. Demuestra dominio de tecnologías empresariales (.NET/SQL) aplicadas con estándares de diseño y usabilidad modernos.*
