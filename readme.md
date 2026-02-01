# Vallora

> ⚠️ Projeto de estudo — uso restrito  
> Este repositório foi criado exclusivamente para fins educacionais e de aprendizado.
> Não é permitido o uso comercial ou redistribuição sem autorização da autora.

Vallora é uma API de **gestão e organização de investimentos**, projetada para centralizar
operações financeiras, histórico de transações e, futuramente, apoiar análises fiscais
como apuração de imposto de renda.

O projeto segue princípios de **DDD**, **Clean Architecture** e **boas práticas modernas**
do ecossistema .NET.

# Vallora – Transaction Service

O **Transaction Service** é responsável por registrar, persistir e publicar eventos de transações financeiras
no ecossistema **Vallora**.

Este serviço segue princípios de **Clean Architecture**, **DDD** e **12-Factor App**, com foco em:
- clareza de domínio
- isolamento de responsabilidades
- facilidade de evolução para microsserviços

---

## Responsabilidade do Serviço

- Criar transações financeiras
- Garantir consistência do Aggregate `Transaction`
- Persistir dados via EF Core
- Publicar eventos de domínio via **Outbox Pattern**
- Integrar com mensageria (Kafka)

> Este serviço **não** calcula performance, impostos ou consolida carteiras  
> Ele apenas **registra fatos financeiros**

---

## Arquitetura
Vallora.TransactionService
├── Api              → Minimal APIs (entrada)
├── Application      → Casos de uso, comandos, eventos
├── Domain           → Entidades, Value Objects, regras
└── Infrastructure   → Persistência, Kafka, Outbox, EF

---

### Princípios aplicados
- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS (Commands separados de Queries)
- Outbox Pattern
- Fail-fast para configurações críticas

---

## Como rodar o serviço

### Pré-requisitos

- .NET SDK 10+
- Docker e Docker Compose
- PostgreSQL (local ou via container)

---

### Rodando localmente (sem Docker)

1. Configure as variáveis de ambiente via **launchSettings.json** ou IDE:

```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "ConnectionStrings__PostgreSql": "Host=localhost;Port=5432;Database=transactions;Username=admin;Password=senha"
}

2. Depois execute
```
dotnet run

3. API disponível em:
```
http://localhost:5105

### Rodando com docker
1. Crie o arquivo .env baseado no exemplo:
```
cp .env.example .env

2.	Ajuste os valores conforme seu ambiente
3.	Suba o serviço:
```
docker compose up --build

---

## Configuração & Segurança

- Nenhuma connection string é versionada no repositório
- Configurações sensíveis **não** devem existir em:
  - `appsettings.json`
  - `appsettings.Development.json`

### Desenvolvimento local

Para execução local (fora do Docker), o projeto suporta o uso do arquivo:

`appsettings.Development.local.json`

Este arquivo:
- sobrescreve as configurações de `appsettings.Development.json`
- **não é versionado**
- deve ser usado apenas para desenvolvimento local

Exemplo:

```json
{
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Port=5432;Database=transactions;Username=admin;Password=senha"
  }
}
```

> ⚠️ **Importante:**  
> O arquivo `appsettings.Development.local.json` deve constar no `.gitignore`.

### Execução com Docker

Quando executado via Docker Compose:
- as configurações são fornecidas via variáveis de ambiente
- o arquivo `.env` é utilizado
- o host do banco deve ser o nome do serviço Docker (ex.: `postgres`)

Este serviço segue os princípios do **12-Factor App**, mantendo configuração
fora do código e permitindo mudança de ambiente sem recompilação.

---

## Banco de Dados & Migrations

As migrations do Entity Framework Core estão localizadas em:
> Infrastructure/Persistence/Migrations

Para aplicar as migrations no banco configurado:

```bash
dotnet ef database update

> ⚠️ **Atenção:** certifique-se de que a variável  
> `ConnectionStrings__PostgreSql` esteja configurada antes de executar o comando.

---

## Eventos & Outbox

Este serviço utiliza o **Outbox Pattern** para garantir:

- consistência transacional entre banco de dados e mensageria
- publicação confiável de eventos de domínio
- desacoplamento entre persistência e integração externa

### Como funciona

1. A transação é persistida no banco de dados
2. O evento de domínio é gravado na tabela de Outbox dentro da mesma transação
3. Um processo/worker dedicado lê os eventos pendentes
4. O evento é publicado no broker de mensagens (Kafka)
5. Após sucesso, o evento é marcado como publicado

Esse modelo garante **eventual consistency** entre serviços e evita perda de eventos
em cenários de falha parcial.

---

## Status do Projeto

Em desenvolvimento  
Projeto de estudos avançados em arquitetura backend  
Foco em Clean Architecture, DDD, CQRS (quando aplicável) e integração assíncrona

---

## Nome do Projeto

**Vallora** — plataforma central de gestão, rastreio e organização de investimentos financeiros,
com visão de longo prazo e suporte futuro a obrigações fiscais (ex.: imposto de renda).

---

## Licença

Este projeto foi criado exclusivamente para fins educacionais e de estudo.

Não é permitida a reprodução, distribuição ou uso comercial do código sem autorização
expressa da autora.