# Documentação do Projeto TaskManager

## Visão Geral

**TaskManager** é uma API RESTful desenvolvida com **.NET 8** e **C# 12**, voltada para o gerenciamento de tarefas. Ela permite a criação, atualização, exclusão e consulta de tarefas, incluindo filtros por **status** e **data de vencimento**.

O projeto segue boas práticas de arquitetura utilizando **Controllers**, **DTOs**, **Services** e **AutoMapper** para mapeamento de objetos. Também conta com uma suíte de testes unitários utilizando **xUnit**.

### Desafio Proposto

> Criar uma aplicação web de "Gestão de Tarefas" simples, com as funcionalidades de criar, editar, remover e visualizar tarefas de forma organizada, incluindo busca por tarefas.

---

## Pré-requisitos

Para executar o projeto localmente, é necessário ter:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado  
- **Visual Studio 2022** ou superior  
- (Opcional) **Postman**, **Insomnia** ou outra ferramenta para testar APIs REST  

---

## Como Executar o Projeto

1. Clone este repositório para sua máquina:

   ```bash
   git clone https://github.com/seu-usuario/TaskManager.git
   ```

2. Abra a solução `TaskManager.sln` no Visual Studio 2022.

3. Defina o projeto `TaskManager` como projeto de inicialização.

4. (Opcional) Configure o banco de dados, se estiver utilizando um banco persistente. Caso contrário, um banco em memória será utilizado por padrão.

5. Execute a aplicação (`F5` ou `Ctrl + F5`).

6. A API estará disponível em:

   ```
   http://localhost:{porta}/api/TaskItem
   ```

   > A porta será exibida no console do Visual Studio.

---

## Testando a API

### 1. Swagger

Acesse:

```
http://localhost:{porta}/swagger
```

Interface interativa com todos os endpoints disponíveis.

### 2. Postman

- Importe uma coleção ou crie requisições manuais conforme os exemplos abaixo.
- Envie as requisições e valide as respostas retornadas.

---

## Endpoints Disponíveis

### 1. Criar Tarefa

- **Método**: `POST`  
- **URL**: `/api/TaskItem`  
- **Body (JSON)**:

```json
{
  "title": "Título da tarefa",
  "description": "Descrição detalhada",
  "dueDate": "2023-10-15T14:30:00",
  "status": "Pendente"
}
```

- **Resposta**:

```json
{
  "message": "Tarefa criada com sucesso.",
  "data": {
    "id": 1,
    "title": "string",
    "description": "string",
    "dueDate": "2025-06-05T03:00:25.519Z",
    "status": "Pendente"
  }
}
```

---

### 2. Atualizar Tarefa

- **Método**: `PUT`  
- **URL**: `/api/TaskItem/{taskItemId}`  
- **Body (JSON)**:

```json
{
  "title": "Novo título",
  "description": "Nova descrição",
  "dueDate": "2023-10-20T10:00:00",
  "status": "Em progresso"
}
```

- **Resposta**:

```json
{
  "message": "Tarefa atualizada com sucesso.",
  "data": {
    "id": 1,
    "title": "Novo título",
    "description": "Nova descrição",
    "dueDate": "2023-10-20T10:00:00",
    "status": "Em progresso"
  }
}
```

---

### 3. Excluir Tarefa

- **Método**: `DELETE`  
- **URL**: `/api/TaskItem/{taskItemId}`  
- **Resposta**:

```json
{
  "message": "Tarefa excluída com sucesso.",
  "data": {
    "id": 1,
    "title": "string",
    "description": "string",
    "dueDate": "2025-06-05T03:00:25.519Z",
    "status": "Pendente"
  }
}
```

---

### 4. Listar Todas as Tarefas

- **Método**: `GET`  
- **URL**: `/api/TaskItem`  
- **Resposta**: Lista de tarefas obtida com sucesso.

---

### 5. Filtrar por Status

- **Método**: `GET`  
- **URL**: `/api/TaskItem/status/{status}`  
- **Exemplo**: `/api/TaskItem/status/Pendente`  
- **Resposta**: Lista de tarefas com status 'Pendente' obtida com sucesso.

---

### 6. Filtrar por Data de Vencimento

- **Método**: `GET`  
- **URL**: `/api/TaskItem/dueDate/{date}`  
- **Exemplo**: `/api/TaskItem/dueDate/2023-10-15`  
- **Resposta**: Lista de tarefas com data de vencimento '2023-10-15' obtida com sucesso

---

## Observações Importantes

- Os campos `title`, `description`, `dueDate` e `status` são **obrigatórios** para criação e atualização de tarefas.
- Valores aceitos para o campo `status`:
  - `Pendente`
  - `Em progresso`
  - `Concluído`
- O formato da data deve ser:
  - **Para dueDate**: `"yyyy-MM-ddTHH:mm:ss"`
  - **Para filtro por data**: `"yyyy-MM-dd"`

---

## Testes Automatizados

O projeto inclui uma suíte de testes automatizados utilizando o framework **xUnit**.

Para rodar os testes:

1. Abra o **Gerenciador de Testes** no Visual Studio.
2. Execute todos os testes da solução (`Ctrl + R, A`).
3. Verifique os resultados no painel de testes.

---

## Dúvidas ou Problemas

Caso enfrente dificuldades:

- Verifique se todos os **pré-requisitos** foram atendidos.
- Certifique-se de que a **porta correta** está sendo utilizada (exibida ao iniciar a aplicação).
- Consulte os **logs do Visual Studio** para mensagens detalhadas de erro.

---