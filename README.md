# GeekShopping
<b>Este projeto foi construído durante o curso "Arquitetura de Microsserviços do 0 com ASP.NET, .NET 6 e C#" na Udemy.</b> <br/>
Link do curso: https://www.udemy.com/course/microservices-do-0-a-gcp-com-dot-net-6-kubernetes-e-docker/ <br/>
<br/>
Nese curso não só aprendi coisas novas como tambem consolidei alguns conhecimento ja que tinha. <br/>
O que eu aprendir nesse curso ?<br/>
ASP.NET;<br/><br/>
.NET 6;<br/>
Oauth2;<br/>
OpenID;<br/>
JWT (JSON Web Tokens);<br/>
Identity Server;<br/>
RabbitMQ;<br/>
API Gateway com Ocelot;<br/>
Swagger OpenAPI e muito mais;<br/>
O que são microsserviços e por que são cada vez mais populares;<br/>
O que é e como Implementar Oauth2, OpenID, JWT e Identity Server;<br/>
Como usar o framework Duende para construir um Identity Server;<br/>
Como trabalhar com Postman;<br/>
Desenvolver microsserviços com .NET 6;<br/>
Publicação e Consumo de Mensagens com RabbitMQ;<br/>
Segurança de microsserviços;<br/>
O que é e Como Implementar um API Gateway com Ocelot;<br/>
Comunicação Síncrona e Assíncrona entre Microsserviços;<br/>
Utilizar JMS para mensageria entre microsserviços .NET 6 com RabbitMQ;<br/>
Configurar uma stack de microsserviços do 0 absoluto;<br/>
As boas práticas a se adotar ao desenvolver microsserviços;<br/>
Como expor e consumir microsserviços através de API's RESTFul;<br/>
Gere documentação de API com Swashbuckle (Swagger).<br/>
EntytFramework Core <br/>
Banco de Dados MySql <br/>
<h5>Execução do Projeto:</h5>
<p>1° Altere a Conection String do Mysql com base na sua instancia do Mysql, no arquivo appseting.json, o nome da ConncetionString deve ser igual ao que ta na classe Startup.cs </p>
<p>1° Cada Microserviço tem o seu próprio Banco de Dados, no appseting.json do projeto aqui, nao tem a connectionString pois usei via UserSrecrets. </p>
<p>Exemplo de ConnectionString pra o Mysql: </p>
<p> 
  "ConnectionStrings": {
    "mysqlConnectionString": "Server=localhost; DataBase=GeekShopping_CartApi; Uid=root; Pwd=suaSenha;"
  }
</p>


