# Diretrizes do Repositório

## Estrutura do Projeto e Organização dos Módulos
Este repositório está dividido em `backend/` e `frontend/`. O backend é uma solução .NET 8 em `backend/HabitTracker.sln`, com projetos em camadas: `Controllers/` para pontos de entrada da API, `Services/` para lógica de negócio, `Repositories/` para acesso a dados, `Models/` para entidades, `Dtos/` para contratos de requisição/resposta, `Configuration/` para EF Core e tratamento de exceções, e `Tests/` para cobertura com xUnit. O frontend fica em `frontend/` como uma aplicação Vite + React; o código da aplicação está em `frontend/src/` e os assets estáticos em `frontend/public/`.

## Comandos de Build, Teste e Desenvolvimento
Backend:
- `dotnet restore backend/HabitTracker.sln` instala as dependências da solução.
- `dotnet build backend/HabitTracker.sln` compila todos os projetos do backend.
- `dotnet run --project backend/Controllers/Api.csproj` inicia a API com Swagger em desenvolvimento.
- `dotnet test backend/Tests/Tests.csproj` executa a suíte de testes xUnit.

Frontend:
- `npm install --prefix frontend` instala as dependências Node.
- `npm run dev --prefix frontend` inicia o servidor de desenvolvimento do Vite.
- `npm run build --prefix frontend` gera o bundle de produção em `frontend/dist/`.
- `npm run lint --prefix frontend` executa o ESLint.

## Estilo de Código e Convenções de Nomenclatura
Siga o estilo existente do projeto em vez de reformatar oportunisticamente. Em C#, use PascalCase para tipos e membros públicos, camelCase para variáveis/parâmetros locais, e uma classe por arquivo. Em React, arquivos usam PascalCase para componentes como `App.jsx`; mantenha hooks e helpers locais em camelCase. O frontend atualmente usa indentação de 2 espaços e o backend usa principalmente tabs; preserve o estilo do arquivo ao redor. Execute `npm run lint --prefix frontend` antes de submeter alterações no frontend.

## Diretrizes de Testes
Os testes do backend usam xUnit com Moq em `backend/Tests/Tests/`. Nomeie arquivos de teste com base no serviço testado e use nomes de método descritivos como `Should_Create_User_When_Email_Not_Exists`. Adicione ou atualize testes de comportamento da camada de serviço ao alterar regras de validação, autenticação ou persistência. Ainda não há setup de testes para frontend, então documente os passos de validação manual nos PRs de UI.

## Diretrizes de Commit e Pull Request
Commits recentes usam resumos curtos, em minúsculas e no imperativo, como `adding authorize`, `init front` e `feature get check in`. Prefira linhas de assunto concisas focadas em uma única mudança. PRs devem incluir um resumo claro, área afetada (`backend`, `frontend` ou ambos), issue vinculada (se houver), resultados de testes e screenshots ou payloads de API de exemplo para alterações de UI ou contrato.

## Dicas de Segurança e Configuração
A API lê configurações de SQLite e JWT de `backend/Controllers/appsettings.json`. Trate os segredos ali como apenas para desenvolvimento, evite commitar credenciais reais e não versione mudanças de banco local, a menos que o schema em si tenha mudado.

## Fluxo de Aprovação de Alterações
Antes de qualquer alteração em arquivos deste repositório, apresente de forma objetiva o que será modificado (plano curto e/ou diff proposto) e aguarde aprovação explícita do solicitante. Somente após essa validação as alterações devem ser aplicadas.
