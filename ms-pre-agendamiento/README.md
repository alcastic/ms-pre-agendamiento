# ms-pre-agendamiento

## Instalación

### 0.- Dependencias
* instalar [.Net Core 3.1](https://dot.net/core)
* ejecutar

- [.Net Core 3.1](https://dot.net/core)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/?view=azure-cli-latest)
- [Docker](https://docs.docker.com/get-docker/)

### 1.- Clonar el código fuente.
```sh
$ https://github.com/aldemars/ms-pre-agendamiento.git
```

### 2.- Login en Azure
```sh
az login [--allow-no-subscriptions]
         [--identity]
         [--only-show-errors]
         [--password]
         [--service-principal]
         [--tenant]
         [--use-cert-sn-issuer]
         [--use-device-code]
         [--username]
```
o simplemente el siguiente comando y seguir las instrucciones:
```sh
az login
```

### 3.- Iniciar el servicio.
Puedes iniciar el servicio usando el CLI de dotnet, con docker, o con kubectl

* Start Local: `$ make up`
* Check Start Local: `$ curl http://localhost:8080/HealthCheck`
* Stop Local: `$ make down`

#### 3.1 dotnet CLI
actualizar appsettings.json con el string de conección a la base de datos
```json
"ConnectionStrings": {
    "database": "<Connection str>"
  }
```
para el ambiente dev, el string de conección se puede obtener:

```sh
az keyvault secret show --name 'ConnectionStrings' --vault-name 'dev-pre-agendamiento' --query value
```
y luego ejecutar;
```sh
$ dotnet run
```
#### 3.3 Usando docker
actualizar appsettings.json con el string de conección a la base de datos
```json
"ConnectionStrings": {
    "database": "<Connection str>"
  }
```
```sh
$ docker build ms-pre-agendamiento/ -t preagendamiento:local
$ docker run -d -p 5000:80 preagendamiento:local
```
#### 3.4 Usando kubectl
actualizar manifests/deployment.yml con el string de conección a la base de datos
y la imagen
```yaml
env:
    - name: ConnectionStrings__database
      value: "<Connection str>"
```
```sh
$ kubectl create -f manifests/deployment.yml
$ kubectl create -f manifests/service.yml
```