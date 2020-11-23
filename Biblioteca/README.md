# Projeto Biblioteca/Livraria

## Tecnologias utilizadas:
- ASP.NET CORE 3.1
- VUEJS
- Docker/Docker Compose (Postgres)

## Configurações Iniciais
### 1° Acesso - Configurações do Banco
Se você for acessar o projeto pela primeira vez, será necessário realizar alguns procedimentos iniciais de configurações para o funcionamento.
- 1.1 Tendo o Docker instalado na máquina, acesse a pasta "DockerPostgresSql" pelo terminal e execute o comando para a criação dos containers do banco de dados Postgres e do pgAdmin:
```
docker-compose up -d
```

- 1.2. Com os container rodando, acessar o seguinte endereço: http://localhost:5050, você será direcionado para a tela de login do pgAdmin (credenciais abaixo), iremos configurar um novo servidor e o banco de dados da aplicação.
```
Usuário: admin@email.com
Senha: admin
```

- Seguir os passos (imagens) abaixo:

![Screenshot](./DockerPostgresSql/imagens/Captura_login.PNG)
![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura00.PNG)



## Iniciar Projeto
