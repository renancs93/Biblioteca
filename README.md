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

Realizar Login

![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura_login.PNG)

Criar um Server

![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura00.PNG)

![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura01.PNG)
![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura02.PNG)

Criar um banco de dados chamado: livraria

![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura03.PNG)

![Screenshot](https://github.com/renancs93/Biblioteca/blob/master/DockerPostgreSql/imagens/Captura04.PNG)

## Instalando dependêcias
Acessar a pasta Biblioteca/clientapp (Frontend) e rodar o comando pelo terminal para instalar todas as dependências necessárias:
```
npm install
```

## Iniciando a API
- Dar um play na aplicação pelo Visual Studio com a solução Biblioteca aberta, o servidor do IIS irá rodar e o navegador irá abrir (API deve estar na porta 50598), juntamente com a aplicação web, no seguinte endereço: http://localhost:50598/

## Rodando a aplicação Web manualmente (caso a opção anterior não rode a aplicação web automaticamente)
Com a API rodando, acesse a pasta pasta Biblioteca/clientapp pelo terminal e execute o seguinte o comando:
```
npm run serve
```

Será informado no terminal o endereço da aplicação web, geralmente estará acessível nos seguintes endereços: http://localhost:8080/ ou http://localhost:8081/