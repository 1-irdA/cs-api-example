# cs-api-example

## Docker Compose

```sh
docker-compose build
docker-compose up -d
# wait a little during database initialization
# http://localhost/swagger 
```

## Kubernetes

```sh
kubectl apply -f .\K8s\api-deployment.yml,.\K8s\db-deployement.yml
```