**DOCKER**

To start app you need to use docker.

Use:

docker compose `compose.yml`

**!IMPORTANT!**

You need to install cert because notifications service works over https protocol. In powershell type:

1. `dotnet dev-certs https --clean`
2. `dotnet dev-certs https --trust -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p test`
4. Then docker compose `compose.yml` (It should throw error because I was using external network, but it will also print command which should be used to create one)
5. docker compose again
6. Frontend: `localhost:8080`
