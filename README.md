# cs-api-example

C# API with Docker, Docker Compose and K8s with (persist branch) and without persistent data (main branch)

## Docker Compose

```sh
docker-compose build
docker-compose up -d
# wait a little during database initialization
# http://localhost:5000/swagger 
```

## Kubernetes

```sh
kubectl apply -f .\K8s\api-deployment.yml,.\K8s\db-deployement.yml
# wait a little during liveness and readyness probe
# http://localhost:5000/swagger 
```
