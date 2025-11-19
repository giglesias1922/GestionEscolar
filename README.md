Sistema de Gestión Escolar

Front: Blazor WebAssembly 9 + MudBlazor
Back: .NET 9 Web API (Controllers, Services, Repositories)
Tests: xUnit + Mock de persistencia JSON

Solution
│
├── Escuela_Front/            → Blazor WebAssembly 9 + MudBlazor
│
├── Escuela_Back/             → API .NET 9
│   ├── Controllers/          → Controladores REST
│   ├── Services/             → Lógica de negocio
│   ├── Repositories/         → Acceso a datos (JSON)
│   ├── Interfaces/           → Contratos (DI)
│   └── Models/               → Entidades del dominio
│
└── Escuela_Test/             → Pruebas unitarias (xUnit)