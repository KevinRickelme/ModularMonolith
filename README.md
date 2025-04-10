# 📦 Projeto Monolito Modular com Clean Architecture

## 🧱 Arquitetura Geral

Esse projeto adota a abordagem de **Monolito Modular**, que se posiciona entre o Monolito tradicional e a Arquitetura de Microsserviços. 

A ideia é manter **um único deploy** (como um monolito), mas organizar o sistema internamente em **módulos bem isolados**, cada um representando um contexto de negócio. Isso permite que cada módulo evolua de forma quase independente, com menos acoplamento, e possa futuramente ser extraído como um microsserviço, se necessário.

A arquitetura também está fortemente baseada em **Clean Architecture**, com camadas bem definidas e separação de responsabilidades entre domínio, aplicação e infraestrutura.

---

## 📁 Estrutura de Módulos

O projeto está dividido em múltiplos módulos funcionais:

- **Empresas**
- **Funcionários**
- **EmpresaFuncionario** (representando vínculos entre empresas e funcionários)

Além disso, há camadas comuns para reutilização de componentes:

- `Common.Application`
- `SharedKernel`, que pode funcionar como se fosse um `Common.Domain` e `Common.Infra.Data`

---

## 🧩 Padrões Arquiteturais Utilizados

### ✅ CQRS (Command Query Responsibility Segregation)

Utilizo **CQRS** para separar claramente operações de leitura e escrita:
- **Commands**: para modificar estado (ex: `AddEmpresaCommand`)
- **Queries**: para buscar dados (ex: `GetEmpresaByIdQuery`)

Essa separação melhora a clareza e testabilidade.

---

### ✅ MediatR

O projeto adota o **MediatR** como infraestrutura para orquestrar comandos e queries, desacoplando controladores da lógica de aplicação, facilitando a manutenção e aplicação de pipelines como validações, logs, etc.

---

### ✅ Injeção de Dependência e Isolamento

Os módulos se comunicam através de **interfaces**, por exemplo:
- `IEmpresaConsultaService`
- `IFuncionarioConsultaService`

Esses contratos evitam acoplamento direto entre módulos e permitem que apenas a camada de aplicação dependa de contratos externos.

---

## ✅ Funcionalidades Implementadas

### 🔹 Módulo Empresas
- Criar, atualizar, remover e buscar empresas
- Consultas por ID, CNPJ ou múltiplos IDs
- Serviço de consulta para uso externo (`IEmpresaConsultaService`)

### 🔹 Módulo Funcionários
- Cadastro de funcionários
- Consultas por ID, nome ou múltiplos IDs
- Serviço de consulta exposto via interface (`IFuncionarioConsultaService`)

### 🔹 Módulo EmpresaFuncionario (Vínculos)
- Gerencia a relação N:N entre empresas e funcionários
- Suporte a relação rica: data de admissão, cargo, etc.
- Consulta agregada: `ObterEmpresaComFuncionariosQuery`
- Usa os serviços de consulta de Empresas e Funcionários para compor a resposta

---

## 🔧 Boas Práticas Adotadas

- DTOs separados por finalidade (leitura/escrita)
- Separação de camadas:
  - Domain: regras de negócio
  - Application: lógica de aplicação e orquestração
  - Infra: persistência e implementação de serviços
- Reuso de código em `Common` sem gerar acoplamento excessivo
- Adaptação entre camadas usando `AutoMapper`

---

## ✅ SOLID

O projeto está condizente com os princípios **SOLID**:

- **S (Single Responsibility Principle)**: Cada classe possui uma responsabilidade clara (handlers, services, entidades).
- **O (Open/Closed Principle)**: É possível estender comportamentos com novos handlers e services sem modificar os existentes.
- **L (Liskov Substitution Principle)**: Interfaces são bem definidas e implementações podem ser trocadas sem quebrar o funcionamento.
- **I (Interface Segregation Principle)**: Interfaces são específicas ao seu propósito (ex: consulta vs escrita).
- **D (Dependency Inversion Principle)**: Os módulos dependem de **abstrações** (`IEmpresaConsultaService`) e não de implementações concretas.

---

## 🚀 Conclusão

Esse projeto aplica conceitos modernos de arquitetura, com boas práticas de organização de código, isolamento de responsabilidades e uma base sólida para crescer de forma modular e escalável.

Mesmo sendo um monolito, ele já está preparado para uma possível transição para microsserviços.

Este projeto foi criado a partir de estudos sobre Arquitetura Limpa e Arquitetura de Monolito Modular.
