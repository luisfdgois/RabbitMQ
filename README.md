
## ORDER PAYMENT SERVICE

This project consists of an OrderService that exposes a REST API with two endpoints: one for registering orders and another for listing registered orders. The CreditCard service simulates a simplified process of credit analysis and approval by a credit card company. The goal is to expand and add more services according to the types/options of payments that will be available. The communication between these services is done asynchronously using RabbitMQ.

The project aims to practically apply the knowledge I have been acquiring throughout my learning journey in message queuing services. Updates and changes will be made as new problems/needs arise.

### Flow

![order-payment-service](https://user-images.githubusercontent.com/50787263/174161888-06fbed7b-a092-47e8-83af-189140f07c38.png)


### Execution

Prior installation of Docker on the machine is required for execution.

In the terminal, navigate to the root directory of the projects, where the docker-compose.yml file is located, and run the following command:

```bash
docker-compose up -d
```

