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

## 🔄 Atualização de Arquitetura — Event Sourcing no módulo EmpresaFuncionario

### ✅ O que foi feito?

O módulo `EmpresaFuncionario` passou a adotar o padrão **Event Sourcing** para persistência e reconstrução de estado. Em vez de gravar diretamente o estado atual da entidade no banco de dados, agora o sistema **salva eventos imutáveis** que representam mudanças ocorridas ao longo do tempo (ex: `FuncionarioAdmitidoEvent`, `CargoAlteradoEvent`, `DepartamentoAlteradoEvent`).

A entidade `EmpresaFuncionario` agora:

- Aplica mudanças somente através de eventos (`Apply(Event)`).
- Expõe um método `ReplayEvents(...)` que permite **reidratar** a entidade a partir de seus eventos históricos.
- Armazena eventos gerados localmente em `_eventos`, para posterior persistência no **Event Store**.
- Possui métodos de negócio como `Admitir(...)`, `AlterarCargo(...)` e `AlterarDepartamento(...)` que **geram eventos** em vez de alterar diretamente o estado.

Um modelo de leitura (read model / projeção) foi criado para expor os dados atualizados de forma eficiente via API, mantendo a separação entre **Command (escrita)** e **Query (leitura)** conforme os princípios de CQRS.

Além disso, foi criado uma estrutura genérica de Eventos, contendo desde uma classe abstrata, interfaces, até um repositório genérico para eventos, onde possui um método para salvar em uma tabela genérica de eventos e obtê-los através de seu StreamId.

Outra observação é que decidi por separar as tabelas de cada evento de Entidades, logo, os eventos de EmpresaFuncionario ficarão numa tabela chamada EventosEmpresaFuncionario. 

---

### 💡 Por que implementar Event Sourcing aqui?

A modelagem de `EmpresaFuncionario` representa uma **relação rica entre duas entidades (Empresa e Funcionário)**, que pode passar por diversas mudanças ao longo do tempo:

- Troca de departamento
- Alterações de cargo
- Reversões de vínculo
- Auditoria de histórico contratual

Essas mudanças são **sequenciais, rastreáveis e sensíveis ao tempo**, o que torna esse módulo **ideal para Event Sourcing**, pois:

- Permite **rastrear toda a evolução do vínculo** desde sua criação.
- Facilita **auditorias** e reconstruções de estado em diferentes pontos no tempo.
- Evita perda de informações sobre alterações históricas.
- Permite recuperar o estado atual apenas aplicando os eventos.
- Prepara o sistema para suportar futuros cenários de integração via eventos.


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
