# ✅ Todo Minimalist

![.NET](https://img.shields.io/badge/.NET-10-blue)
![SQLite](https://img.shields.io/badge/SQLite-lightgrey)
![GitHub Actions](https://img.shields.io/badge/CI-CD-green)

Uma **API minimalista de lista de tarefas (To-Do)** criada em **.NET 10**, com foco em:  
- Explorar as **novidades do .NET 10** e o modelo de **Minimal APIs**.  
- Implementar **CRUD completo**, agora com **persistência em SQLite**.  
- Configurar **Integração Contínua (CI)** usando **GitHub Actions**.  

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

---

## 💾 Banco de Dados
- A API utiliza **SQLite**, leve e sem necessidade de instalação adicional.  
- O banco (`todo.db`) é criado automaticamente na primeira execução.  
- Tabelas gerenciadas com **EF Core Migrations**:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🧠 Objetivo do Projeto

Servir como laboratório para:

- Testar novos recursos do .NET 10.
- Praticar boas práticas de desenvolvimento e integração contínua.
- Evoluir gradualmente para recursos avançados de backend e DevOps.

---

## 🛠️ Tecnologias
- [.NET 10](https://dotnet.microsoft.com/)
- Minimal APIs
- GitHub Actions (CI)
- Swagger / OpenAPI

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