# Instrucciones de configuración

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Entity Framework CLI](#instalación-de-entity-framework-cli)
- SqlLite

## Instalación de Entity Framework CLI

- Lo primero que debes hacer es validar si tienes instaldo el componente de CLI para Entity Framework

```bash
dotnet ef
```

- Si la ejecución de ese comando te genera algún tipo de error, debes ejecutar lo siguiente:

```bash
# Si quieres instalar de forma global para todos los proyectos (Recomendado)
dotnet tool install --global dotnet-ef

# Si quieres solo instalar la herramienta en el proyecto local
dotnet tool install dotnet-ef
```

- Para validar que la instalación se realizó de forma correcta, puedes ejecutar el comando `dotnet ef` nuevamente y verificar que no te genere ningún error.

## Variables de entorno

Se deben configurar las siguientes variables de entorno:

```bash
"ConnectionStrings__DefaultConnection": "Data Source=database.db"
```

Para configurar las variables de entorno para entorno de desarrollo puede hacerlo de la siguiente manera:

- **Archivo launchSettings.json**: En el archivo `launchSettings.json` se pueden agregar las variables de entorno de la siguiente manera:

```json
{
  "profiles": {
    "YourProjectName": {
      "commandName": "Project",
      "environmentVariables": {
        "ConnectionStrings__DefaultConnection": "Data Source=database.db"
      }
    }
  }
}
```

- **Variables de entorno del sistema**: También puedes configurar las variables de entorno a nivel del sistema operativo. En Windows, puedes hacerlo a través del Panel de Control > Sistema > Configuración avanzada del sistema > Variables de entorno. En Linux o macOS, puedes agregar las variables a tu archivo `.bashrc`, `.zshrc` o el archivo de configuración de tu shell.

- **Secretos de usuario**: Para entornos de desarrollo, también puedes utilizar la funcionalidad de secretos de usuario de .NET para almacenar las variables de entorno de forma segura. Puedes configurar los secretos de usuario con el siguiente comando:

```bash
#Inicializa los secretos de usuario para el proyecto
dotnet user-secrets init

#Agrega las variables de entorno a los secretos de usuario
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=database.db"
```
