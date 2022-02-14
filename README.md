# Calculator on microservices

This project is created to understand microservice architecture.  
[Kafka](https://docs.confluent.io/clients-confluent-kafka-dotnet/current/overview.html) and [RabbitMQ](https://www.rabbitmq.com/) are used here.  
Docker support available [docker-compose.yml](https://github.com/BattleWarriorXXL/Calculator.Microservices/blob/main/docker-compose.yml).  
Kubernetes support available [microservices.yml](https://github.com/BattleWarriorXXL/Calculator.Microservices/blob/main/microservices.yml).  

To start docker write the following commands:
```
docker-compose build
docker-compose up
```

To start Kubernetes cluster write the following commands:
```
minikube start
kubectl apply -f microservices.yml
```
