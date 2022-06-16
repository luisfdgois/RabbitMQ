
## ORDER PAYMENT SERVICE

Obs: Esse é um projeto simples que tem como intenção a aplicação prática do conhecimento que venho adiquirindo ao longo do meu aprendizado em serviços de mensageria. A intenção é que ataulizações e mudanças (com melhorias) vão sendo realizadas a medida que novos problemas/necessidades vão surgindo. Sugestões e criticas são sempre bem-vindas.

#### Sobre

O Projeto é composto por um serviço de pedidos (OrderService) que expõe uma API (Rest) Gateway com dois endpoints, um para registrar pedidos e o outro para listar os pedidos registrados.
O outro serviço (CreditCard.Service) simula, de forma simples, o processo de análise e aprovação de créditos por uma administradora de cartão de crédito. A ideia é expandir e adicionar mais serviços de acordo com os tipos/opções de pagamentos que ficarão disponíveis.
A comunicação entre esses serviços é feita de forma assíncrona, utlizando o RabbitMQ.

#### Fluxo

![order-payment-service](https://user-images.githubusercontent.com/50787263/174161888-06fbed7b-a092-47e8-83af-189140f07c38.png)
