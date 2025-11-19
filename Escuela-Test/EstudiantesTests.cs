using Escuela_Back.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Escuela_Test;

public class EstudiantesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EstudiantesTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // --- GET /api/estudiantes -----------------------------------------------

    [Fact]
    public async Task Get_DeberiaRetornarLista()
    {
        var response = await _client.GetAsync("/api/estudiantes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var lista = JsonSerializer.Deserialize<List<Estudiante>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(lista);
        Assert.True(lista!.Count > 0);
    }

    // --- GET /api/estudiantes/{id} -------------------------------------------

    [Fact]
    public async Task GetPorId_Existente_DeberiaRetornarOk()
    {
        var response = await _client.GetAsync("/api/estudiantes/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var item = JsonSerializer.Deserialize<Estudiante>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(item);
        Assert.Equal(Guid.Parse("a7795a61-d07f-4e77-b7c4-42a134485732"), item!.Id);
    }

    [Fact]
    public async Task GetPorId_Inexistente_DeberiaRetornar404()
    {
        var response = await _client.GetAsync("/api/estudiantes/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // --- POST /api/estudiantes ------------------------------------------------

    [Fact]
    public async Task Post_DeberiaCrearEstudiante()
    {
        var nuevo = new Estudiante
        {
            NombreCompleto = "Pedro Alvarez",
            Edad = 11
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(nuevo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/api/estudiantes", contenido);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var creado = JsonSerializer.Deserialize<Estudiante>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(creado);
        Assert.True(creado!.Id != Guid.Empty);
    }

    [Fact]
    public async Task Post_NombreCorto_DeberiaRetornar400()
    {
        var modelo = new Estudiante
        {
            NombreCompleto = "AB",
            Edad = 10
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/api/estudiantes", contenido);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_EdadInvalida_DeberiaRetornar400()
    {
        var modelo = new Estudiante
        {
            NombreCompleto = "Juan Perez",
            Edad = 3
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/api/estudiantes", contenido);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // --- PUT /api/estudiantes/{id} -------------------------------------------

    [Fact]
    public async Task Put_DeberiaActualizar()
    {
        var modelo = new Estudiante
        {
            NombreCompleto = "Ana Gomez",
            Edad = 12
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/api/estudiantes/1", contenido);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Put_Inexistente_DeberiaRetornar404()
    {
        var modelo = new Estudiante
        {
            NombreCompleto = "Test Inexistente",
            Edad = 10
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/api/estudiantes/9999", contenido);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // --- DELETE /api/estudiantes/{id} ----------------------------------------

    [Fact]
    public async Task Delete_DeberiaEliminar()
    {
        var response = await _client.DeleteAsync("/api/estudiantes/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Inexistente_DeberiaRetornar404()
    {
        var response = await _client.DeleteAsync("/api/estudiantes/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
