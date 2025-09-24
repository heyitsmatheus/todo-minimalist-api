# ✅ Todo Minimalist

![.NET](https://img.shields.io/badge/.NET-10-blue)
![SQLite](https://img.shields.io/badge/SQLite-lightgrey)
![GitHub Actions](https://img.shields.io/badge/CI-CD-green)

Uma **API minimalista de lista de tarefas (To-Do)** criada em **.NET 10**, com foco em:  
- Explorar as **novidades do .NET 10** e o modelo de **Minimal APIs**.  
- Implementar **CRUD completo**, agora com **persistência em SQLite**.  
- Configurar **Integração Contínua (CI)** usando **GitHub Actions**.  
- Adicionar **autenticação JWT** para proteger endpoints.

---

## 🚀 Funcionalidades
| Funcionalidade | Status |
|----------------|--------|
| Criar tarefas | ✅ |
| Listar tarefas | ✅ |
| Atualizar tarefas | ✅ |
| Excluir tarefas | ✅ |
| Persistência SQLite via EF Core | ✅ |
| Swagger/OpenAPI | ✅ |
| GitHub Actions CI | ✅ |
| Autenticação JWT | ✅ |

---

## 🔐 Autenticação

- **Login** via endpoint `/login` gera token JWT.  
- **Formato de resposta padrão OAuth2**:

```json
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "token_type": "Bearer",
  "expires_in": 3600,
  "scope": "read write"
}
````

* Todos os endpoints `/todo` estão protegidos e exigem header:

```
Authorization: Bearer <access_token>
```

* Teste de autenticação disponível via **Swagger UI** com botão **Authorize**.

---

## 💾 Banco de Dados

* A API utiliza **SQLite**, leve e sem necessidade de instalação adicional.
* O banco (`todo.db`) é criado automaticamente na primeira execução.
* Tabelas gerenciadas com **EF Core Migrations**:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🧠 Objetivo do Projeto

Servir como laboratório para:

* Testar novos recursos do .NET 10.
* Praticar boas práticas de desenvolvimento e integração contínua.
* Evoluir gradualmente para recursos avançados de backend, segurança e DevOps.

---

## 📈 Próximos Desenvolvimentos

1. **Persistência de usuários real**

   * Criar tabela `Users` com senha **hash (BCrypt/Argon2)**.
   * Login e registro de usuários no banco.

2. **Refresh Tokens**

   * Permitir renovação de JWT sem login repetido.

3. **Autorização baseada em roles e scopes**

   * Diferenciar acesso a endpoints, ex: `Admin` vs `User`.

4. **Versionamento da API**

   * Ex.: `/api/v1/todo`, `/api/v2/todo`.

5. **Testes automatizados**

   * Unitários e de integração com `xUnit` e `FluentAssertions`.

6. **Logging e monitoramento avançados**

   * Serilog, log estruturado, monitoramento de performance e erros.

7. **Rate limiting e caching**

   * Middleware para proteger e otimizar endpoints GET.

---

## 🧠 O que aprender com este projeto

* Minimal APIs no .NET 10 e EF Core.
* Configuração de **JWT** e autenticação segura.
* Boas práticas de CRUD, validação e tratamento de erros.
* Swagger/OpenAPI e documentação de APIs.
* Integração contínua com **GitHub Actions**.
* Conceitos de roles, scopes e refresh tokens.

---

## 🛠️ Tecnologias

* [.NET 10](https://dotnet.microsoft.com/)
* Minimal APIs
* SQLite + EF Core
* Swagger / OpenAPI
* JWT Authentication
* GitHub Actions (CI)

---

## ▶️ Como Executar

```bash
# Clonar repositório
git clone https://github.com/heyitsmatheus/todo-minimalist-api.git
cd todo-minimalist/TodoMinimalist

# Restaurar dependências
dotnet restore

# Rodar localmente
dotnet run
```

* Acesse **Swagger UI** em: `https://localhost:5001/swagger`
* Faça login via `/login` e use o token nos endpoints `/todo`.

```
```
