using Escuela_Back.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Escuela_Test;

public class CursosTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CursosTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // --- GET /api/cursos ------------------------------------------------------

    [Fact]
    public async Task Get_DeberiaRetornarLista()
    {
        var response = await _client.GetAsync("/api/cursos");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var lista = JsonSerializer.Deserialize<List<Curso>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(lista);
        Assert.True(lista!.Count > 0);
    }

    // --- GET /api/cursos/{id} --------------------------------------------------

    [Fact]
    public async Task GetPorId_Existente_DeberiaRetornarOk()
    {
        var response = await _client.GetAsync("/api/cursos/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var curso = JsonSerializer.Deserialize<Curso>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(curso);
        Assert.Equal(Guid.Parse("6c421ee6-8b2f-4aae-b18e-4ad90f9aafff"), curso!.Id);
    }

    [Fact]
    public async Task GetPorId_Inexistente_DeberiaRetornar404()
    {
        var response = await _client.GetAsync("/api/cursos/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // --- POST /api/cursos ------------------------------------------------------

    [Fact]
    public async Task Post_DeberiaCrearCurso()
    {
        var nuevo = new Curso
        {
            Nombre = "Primaria 3",
            Color = "#4477AA",
            Icono = "📘",
            Asignaturas = new List<Guid>(),
            Estudiantes = new List<Guid>()
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(nuevo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/api/cursos", contenido);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var creado = JsonSerializer.Deserialize<Curso>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(creado);
        Assert.True(creado!.Id != Guid.Empty);
    }

    [Fact]
    public async Task Post_NombreInvalido_DeberiaRetornar400()
    {
        var modelo = new Curso
        {
            Nombre = "AB",
            Color = "#123456",
            Icono = "📘",
            Asignaturas = new List<Guid>(),
            Estudiantes = new List<Guid>()
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/cursos", contenido);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_ColorInvalido_DeberiaRetornar400()
    {
        var modelo = new Curso
        {
            Nombre = "Primaria 4",
            Color = "Azul",
            Icono = "📙"
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/cursos", contenido);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // --- PUT /api/cursos/{id} --------------------------------------------------

    [Fact]
    public async Task Put_DeberiaActualizar()
    {
        var modelo = new Curso
        {
            Nombre = "Primaria 1A",
            Color = "#336699",
            Icono = "📗"
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/api/cursos/1", contenido);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Put_Inexistente_DeberiaRetornar404()
    {
        var modelo = new Curso
        {
            Nombre = "XYZ",
            Color = "#112233",
            Icono = "📕"
        };

        var contenido = new StringContent(
            JsonSerializer.Serialize(modelo),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/api/cursos/9999", contenido);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // --- DELETE /api/cursos/{id} ----------------------------------------------

    [Fact]
    public async Task Delete_DeberiaEliminar()
    {
        var response = await _client.DeleteAsync("/api/cursos/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Inexistente_DeberiaRetornar404()
    {
        var response = await _client.DeleteAsync("/api/cursos/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // --- VINCULAR ASIGNATURA ---------------------------------------------------

    [Fact]
    public async Task VincularAsignatura_DeberiaRetornarOk()
    {
        var response = await _client.PostAsync("/api/cursos/1/asignaturas/1", null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task VincularAsignatura_Inexistente_DeberiaRetornar404()
    {
        var response = await _client.PostAsync("/api/cursos/999/asignaturas/5", null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // --- DESVINCULAR ASIGNATURA ------------------------------------------------

    [Fact]
    public async Task DesvincularAsignatura_DeberiaRetornarOk()
    {
        var response = await _client.DeleteAsync("/api/cursos/1/asignaturas/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // --- VINCULAR ESTUDIANTE ---------------------------------------------------

    [Fact]
    public async Task VincularEstudiante_DeberiaRetornarOk()
    {
        var response = await _client.PostAsync("/api/cursos/1/estudiantes/1", null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task VincularEstudiante_Inexistente_DeberiaRetornar404()
    {
        var resp = await _client.PostAsync("/api/cursos/777/estudiantes/888", null);

        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }

    // --- DESVINCULAR ESTUDIANTE -----------------------------------------------

    [Fact]
    public async Task DesvincularEstudiante_DeberiaRetornarOk()
    {
        var resp = await _client.DeleteAsync("/api/cursos/1/estudiantes/1");

        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }
}
