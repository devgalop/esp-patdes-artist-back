# Instrucciones de configuración

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Docker Desktop](https://docs.docker.com/desktop/setup/install/windows-install/)
- [Imagen PostgreSQL](#configuración-postgresql)
- [Entity Framework CLI](#instalación-de-entity-framework-cli)

## Configuración PostgreSQL

Para instalar una imagen de Postgres con la ayuda de *Docker* es necesario seguir los siguientes pasos:

- Se debe hacer pull a la imagen de postgres con la ayuda del siguiente comando

```bash
#Si desea la versión latest 
docker pull postgres

#Si desea instalar cualquier otra versión modifique VERSION_TAG por la versión elegida
docker pull postgres:VERSION_TAG
```

- Una vez finalice la descarga de la imagen, puede verificar el estado de la imagen con la ayuda del siguiente comando:

```bash
docker images
```

- Para ejecutar la base de datos deberá correr el comando:

```bash
# Este comando ejecuta la imagen de postgres previamente instalada en docker
# --name: Esta parámetro permite darle un nombre (tag) al contenedor
# -e POSTGRES_PASSWORD: Este parámetro crea la contraseña para la base de datos
# -e POSTGRES_USER: Este parámetro crea el usuario para la base de datos
# -e POSTGRES_DB: Este parámetro crea la base de datos
# -p: son los puertos por los que se expone la base de datos PuertoHost:PuertoContenedor
# -v: crea el volumen para almacenar los datos localmente
# -d: ejecución en modo detach de la imagen

docker run --name postgres-db \
-e POSTGRES_PASSWORD=mysecretpassword \
-e POSTGRES_USER=myuser \
-e POSTGRES_DB=myowndatabase \
-p 5432:5432 \
-v postgres-data:/var/lib/postgresql/data \
-d postgres
```

- Una vez esté corriendo el contenedor, puede realizar la validación con el comando:

```bash
docker ps 
```

- Para interactuar con la base de datos en SQL Shell puede utilizar:

```bash
docker exec -it postgres-db psql -U myuser -d myowndatabase
```

- Si prefiere hacer uso de un GUI para interactuar con la base de datos, puede utilizar **pgAdmin**. Para instalar la imagen debe correr el siguiente comando:

```bash
docker pull dpage/pgadmin4
```

- Una vez instalada la imagen, puede ejecutar un contenedor de la siguiente manera:

```bash
# Crea un contenedor usando la imagen dpage/pgAdmin4
# --name: Este parámetro le asigna un nombre al contenedor
# -p: Este parámetro mapea los puertos por los que se va a exponer la aplicacion PuertoLocal:PuertoContenedor
# -e PGADMIN_DEFAULT_EMAIL: correo con el cual se realiza el login
# -e PGADMIN_DEFAULT_PASSWORD: contraseña para el acceso a pgAdmin
# -d: ejecución en modo detach

docker run --name pgadmin -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=user@domain.com -e PGADMIN_DEFAULT_PASSWORD=mysecretpassword -d dpage/pgadmin4
```

- Cuando este corriendo el contenedor, podrá acceder a *localhost:5050* para tener el control de la base de datos.

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
ConnectionStrings__Postgresql="Host=localhost;Port=5432;Database=myowndatabase;Username=myuser;Password=mysecretpassword"
POSTGRES_USER="myuser"
POSTGRES_PASSWORD="mysecretpassword"
POSTGRES_DB="myowndatabase"
```

Para configurar las variables de entorno para entorno de desarrollo puede hacerlo de la siguiente manera:

- **Archivo launchSettings.json**: En el archivo `launchSettings.json` se pueden agregar las variables de entorno de la siguiente manera:

```json
{
  "profiles": {
    "YourProjectName": {
      "commandName": "Project",
      "environmentVariables": {
        "ConnectionStrings__Postgresql": "Host=localhost;Port=5432;Database=myowndatabase;Username=myuser;Password=mysecretpassword",
        "POSTGRES_USER": "myuser",
        "POSTGRES_PASSWORD": "mysecretpassword",
        "POSTGRES_DB": "myowndatabase"
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
dotnet user-secrets set "ConnectionStrings:Postgresql" "Host=localhost;Port=5432;Database=myowndatabase;Username=myuser;Password=mysecretpassword"
dotnet user-secrets set "POSTGRES_USER" "myuser"
dotnet user-secrets set "POSTGRES_PASSWORD" "mysecretpassword"
dotnet user-secrets set "POSTGRES_DB" "myowndatabase"
```
