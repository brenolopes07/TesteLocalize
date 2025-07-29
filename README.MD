# 🧪TesteLocalize

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Render](https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=white)

## 🎯 Objetivo do Teste Técnico

O projeto `TesteLocalize` foi desenvolvido como uma solução para o "Teste Técnico - Cadastro de Empresas com Consulta via CNPJ". O objetivo principal era avaliar a capacidade de construir uma aplicação que envolvesse:

* **Cadastro e autenticação de usuários:** Com armazenamento seguro de senhas (hash) e login utilizando um mecanismo robusto (JWT).
* **Cadastro de empresas com base em CNPJ:** Permitindo que usuários logados cadastrem empresas apenas com o CNPJ.
* **Integração com API pública (ReceitaWS):** Para buscar automaticamente os dados da empresa a partir do CNPJ fornecido.
* **Organização do código, boas práticas e tratamento de erros:** Foco em Clean Architecture, princípios SOLID e manuseio adequado de exceções e validações.

## 📌 Visão Geral

`TesteLocalize` é uma aplicação backend construída em C# com .NET 8, projetada com uma arquitetura limpa (Clean Architecture) e seguindo os princípios SOLID. Ela oferece funcionalidades para gerenciamento de usuários e cadastro de empresas, integrando-se dinamicamente com a API da ReceitaWS para obter informações detalhadas de um CNPJ.

**O projeto conta com uma suíte abrangente de testes unitários e de integração para garantir a robustez e a qualidade do código. Além disso, a listagem de empresas inclui funcionalidade de paginação para melhor performance e experiência do usuário.**

## 🚀 Deploy da Aplicação (API & Banco de Dados)

A aplicação `TesteLocalize`, incluindo a **API e o banco de dados**, está deployada na plataforma [Render](https://render.com/).

**Link para Teste da API (Swagger UI):**
➡️ [https://testelocalize.onrender.com/swagger/index.html](https://testelocalize.onrender.com/swagger/index.html)

**Importante:** Ao acessar o link acima, a primeira requisição pode demorar alguns segundos a mais para responder, pois a instância da API (e potencialmente do banco de dados) na Render precisa "acordar" (cold start) após um período de inatividade. Requisições subsequentes serão rápidas.

## ⚙️ Requisitos Técnicos e Funcionalidades Implementadas

O projeto implementa as seguintes funcionalidades conforme os requisitos do teste técnico:

### 1. Cadastro e Autenticação de Usuário
* Permite o cadastro de novos usuários com `Nome`, `E-mail` e `Senha`.
* As senhas são armazenadas de forma segura utilizando **hashing**.
* O login de usuários é feito com autenticação via **JWT (JSON Web Token)**, fornecendo um token para acesso seguro às rotas protegidas.

### 2. Cadastro de Empresa
* Usuários logados podem cadastrar uma empresa informando apenas o `CNPJ`.
* A aplicação realiza uma consulta automática no endpoint da ReceitaWS:
    `GET https://www.receitaws.com.br/v1/cnpj/{cnpj}`
* Os seguintes campos são extraídos da resposta da ReceitaWS e salvos no banco de dados:
    * Nome empresarial
    * Nome fantasia
    * CNPJ
    * Situação
    * Abertura
    * Tipo
    * Natureza jurídica
    * Atividade principal
    * Logradouro, número, complemento, bairro, município, UF, CEP

### ✅ 3. Listagem de Empresas(Paginação)
* Permite listar todas as empresas cadastradas pelo **usuário logado** que fez o cadastro.
* **Implementação de Paginação:** A listagem suporta paginação para um carregamento eficiente dos dados, permitindo especificar o número da página e o tamanho da página.

## 📦 Critérios de Avaliação Atendidos

Este projeto foi desenvolvido com atenção aos seguintes critérios de avaliação:

* **Código limpo, legível e bem estruturado:** Adotando Clean Architecture para clara separação de responsabilidades.
* **Boas práticas de autenticação e segurança:** Uso de JWT e hashing de senhas.
* **Uso adequado da API da ReceitaWS:** Tratamento de requisições e mapeamento de dados.
* **Validações e tratamento de erros:** Implementação de validações de entrada e tratamento de exceções para garantir a robustez da aplicação.

## ✅ Testes Automatizados

**O projeto `TesteLocalize` possui uma suíte abrangente de testes unitários e de integração, garantindo a qualidade, a confiabilidade e o comportamento esperado da aplicação.** Os testes são fundamentais para o processo de desenvolvimento, cobrindo cenários importantes e protegendo contra regressões.

### Executando os Testes

Para executar todos os testes, navegue até a raiz do projeto e execute o seguinte comando:

"dotnet test"

## Estrutura do Projeto

A solução `TesteLocalize` é organizada nas seguintes camadas, aderindo aos princípios da Clean Architecture:

* **TesteLocalize.Application**: Contém a lógica de aplicação, como casos de uso (use cases), orquestração de operações e interfaces de serviço.
* **TesteLocalize.Domain**: O coração da aplicação, contendo as entidades de negócio, regras de negócio, interfaces de repositório e exceções de domínio. É completamente independente de outras camadas.
* **TesteLocalize.Infra**: Responsável pela implementação de detalhes de infraestrutura, como acesso a banco de dados (EF Core), serviços externos (integração ReceitaWS) e implementações de repositórios.
* **TesteLocalize.Test**: Contém os testes unitários e de integração para garantir a qualidade e o comportamento esperado da aplicação.
* **TesteLocalize.WebAPI**: A camada de apresentação, expondo a API RESTful para comunicação com clientes externos, com endpoints e DTOs.

Além das camadas principais, o projeto inclui:

* `.gitattributes` e `.gitignore`: Arquivos para configuração do Git.
* `docker-compose.yml` e `Dockerfile`: Arquivos para orquestração e construção de containers Docker, facilitando o ambiente de desenvolvimento e implantação.
* `README.MD`: Este arquivo, fornecendo uma visão geral e instruções para o projeto.

## Tecnologias Principais

* **.NET 8**
* **C#**
* **ASP.NET Core** (para a construção da API)
* **Entity Framework Core** (para ORM e acesso a dados)
* **JWT (JSON Web Tokens)** (para autenticação)
* **XUnit** (framework de testes unitários)
* **Moq** (biblioteca de mocking para testes)
* **Docker** (para containerização)
* **Render** (para deployment da API e banco de dados)

## Princípios de Design e Arquitetura

Este projeto foi construído com base nos seguintes princípios:

* **Clean Architecture**: Clara separação de camadas para isolar a lógica de negócio dos detalhes de implementação, promovendo testabilidade e manutenção.
* **SOLID Principles**:
    * **S**ingle Responsibility Principle (SRP)
    * **O**pen/Closed Principle (OCP)
    * **L**iskov Substitution Principle (LSP)
    * **I**nterface Segregation Principle (ISP)
    * **D**ependency Inversion Principle (DIP)

## Configuração de Ambiente (`.env_example`)

O projeto utiliza variáveis de ambiente para configurações sensíveis, como string de conexão com o banco de dados, chaves JWT e outras configurações. Um arquivo `.env_example` é fornecido na raiz do projeto como um template.

Para configurar o ambiente localmente, **crie um arquivo `.env`** na raiz do projeto com base no `.env_example` e preencha com suas próprias configurações.

Exemplo de `.env_example`:

### Pré-requisitos

* [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (recomendado para rodar com Docker)
* Um editor de código como [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
* Certifique-se de ter um banco de dados PostgreSQL ou similar acessível (se não usar Docker para o DB).

### Usando Docker 

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/brenolopes07/TesteLocalize](https://github.com/brenolopes07/TesteLocalize)
    cd TesteLocalize
    ```
2.  **Crie o arquivo `.env`:**
    Com base no `.env_example`, crie um arquivo `.env` na raiz do projeto e configure sua string de conexão e chaves JWT.
3.  **Construa e execute os containers:**
    ```bash
    docker-compose up --build
    ```
    Isso irá construir as imagens Docker e iniciar os serviços (API e Banco de Dados, se configurado no `docker-compose.yml`).
    A API estará disponível em `http://localhost:[PORTA_DA_API]` (A porta padrão ASP.NET Core para HTTP é geralmente 5000 em ambiente Docker; verifique seu `Dockerfile` ou `docker-compose.yml`).

### Sem Docker

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/brenolopes07/TesteLocalize](https://github.com/brenolopes07/TesteLocalize)
    cd TesteLocalize
    ```
2.  **Crie o arquivo `.env`:**
    Com base no `.env_example`, crie um arquivo `.env` na raiz do projeto e configure sua string de conexão (para um banco de dados local que você tenha) e chaves JWT.
3.  **Restaure as dependências:**
    ```bash
    dotnet restore
    ```
4.  **Execute as Migrações do Banco de Dados:**
    ```bash
    dotnet ef database update --project TesteLocalize.Infra
    ```
5.  **Execute a API:**
    ```bash
    cd TesteLocalize.WebAPI
    dotnet run
    ```
    A API estará disponível em `http://localhost:[PORTA_DA_API]` (As portas padrão em desenvolvimento são geralmente 5000 para HTTP e 7000 para HTTPS. Verifique em `Properties/launchSettings.json`).

