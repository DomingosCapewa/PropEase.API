# PropEase.API

API REST simples para gerenciar imóveis (casas e apartamentos) e proprietários, implementando os pilares de POO (abstração, encapsulamento, herança e polimorfismo). Persistência com Entity Framework Core + SQLite.

## Requisitos

- .NET SDK 9.0
- Windows PowerShell (ou shell equivalente)

## Configuração

A connection string já está configurada para SQLite no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=C:\\Desafio_Pooo\\PropEase.API\\propease.db"
  }
}
```

Se a connection string estiver vazia, o aplicativo usa um fallback automático para `Data Source=<ContentRoot>/propease.db`.

## Executar

No diretório do projeto:

```powershell
# Compilar
dotnet build

# Executar (aplica migrações automaticamente)
dotnet run
```

Aplicação ficará disponível em:

- API: http://localhost:5150
- Swagger: http://localhost:5150/swagger

## Estrutura principal

- `Models/Imovel.cs` (abstrata), `Models/Casa.cs`, `Models/Apartamento.cs`, `Models/Proprietario.cs`
- `Data/AppDbContext.cs` (EF Core, herança TPH via discriminador `TipoImovel`)
- `Controllers/ImovelController.cs`, `Controllers/ProprietariosController.cs`
- `Services/ImovelService.cs`, `Services/ProprietarioService.cs`
- `Migrations/*` (SQLite)

## Endpoints

### Proprietários

- `GET /api/proprietarios` — lista proprietários
- `GET /api/proprietarios/{id}` — detalha um proprietário
- `POST /api/proprietarios` — cria proprietário
  - Exemplo body:
    ```json
    {
      "nome": "Maria Silva",
      "telefone": "(11) 99999-0000",
      "cpf": "123.456.789-00"
    }
    ```
- `PUT /api/proprietarios/{id}` — atualiza
- `DELETE /api/proprietarios/{id}` — remove

### Imóveis

- `GET /api/imoveis` — lista imóveis (inclui proprietário)
- `GET /api/imoveis/{id}` — detalha um imóvel
- `POST /api/imoveis` — cria imóvel
  - Body (DTO):
    ```json
    {
      "tipo": "Casa", // "Casa" ou "Apartamento"
      "endereco": "Rua A, 123",
      "numero": 10,
      "proprietarioId": 1,
      "alugado": false
    }
    ```
- `POST /api/imoveis/alugar/{id}` — marca como alugado
- `PUT /api/imoveis/disponibilizar/{id}` — marca como disponível
- `DELETE /api/imoveis/{id}` — remove
- `GET /api/imoveis/{id}/calcular-aluguel?periodo=30` — calcula valor total do aluguel no período

## Pilares de POO atendidos

- Abstração: `Imovel` define interface comum; subclasses implementam diferenças.
- Encapsulamento: métodos controlam estado (`Alugar`, `Disponibilizar`, `CalcularAluguel`), e propriedades são mapeadas para persistência.
- Herança: `Casa` e `Apartamento` herdam de `Imovel`.
- Polimorfismo: mensagens/valores de aluguel sobrescritos nas subclasses.

## Dicas e troubleshooting

- Erro "no such table": garanta que as migrações foram aplicadas (o app faz `Database.Migrate()` na inicialização). Se necessário, apague o arquivo `propease.db` e rode `dotnet run` novamente.
- Connection string: verifique os arquivos `appsettings.json` e `appsettings.Development.json`. O log de inicialização mostra a "Connection String (efetiva)" usada.
- Porta: configurada em `Properties/launchSettings.json` (http://localhost:5150).

## Próximos passos (sugestões)

- DTOs de saída para evitar expor entidades diretamente
- Validações com FluentValidation
- Tratamento global de erros (ProblemDetails)
- Autenticação/autorização básica
