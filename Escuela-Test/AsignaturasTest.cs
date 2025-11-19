using Escuela_Back.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Escuela_Test
{
    public class AsignaturasTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AsignaturasTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_DeberiaTraerLista()
        {
            var response = await _client.GetAsync("/api/asignaturas");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Asignatura>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(lista);
            Assert.True(lista!.Count > 0);
        }

        [Fact]
        public async Task GetPorId_DeberiaRetornarElemento()
        {
            var response = await _client.GetAsync("/api/asignaturas/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<Asignatura>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(item);
            Assert.Equal(Guid.Parse("a466800b-d70f-4194-8ca0-df30a830a8e9"), item!.Id);
        }

        [Fact]
        public async Task GetPorId_Inexistente_DeberiaRetornar404()
        {
            var response = await _client.GetAsync("/api/asignaturas/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_DeberiaCrearAsignatura()
        {
            var nuevo = new Asignatura
            {
                Nombre = "Geografía",
                Descripcion = "Contenido general"
            };

            var contenido = new StringContent(
                JsonSerializer.Serialize(nuevo),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/api/asignaturas", contenido);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var creado = JsonSerializer.Deserialize<Asignatura>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(creado);
            Assert.True(creado!.Id != Guid.Empty);
        }

        [Fact]
        public async Task Post_NombreInvalido_DeberiaRetornar400()
        {
            var modelo = new Asignatura
            {
                Nombre = "AB",
                Descripcion = "Prueba"
            };

            var contenido = new StringContent(
                JsonSerializer.Serialize(modelo),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/api/asignaturas", contenido);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_DeberiaActualizarElemento()
        {
            var editado = new Asignatura
            {
                Nombre = "Historia Argentina",
                Descripcion = "Actualización"
            };

            var contenido = new StringContent(
                JsonSerializer.Serialize(editado),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PutAsync("/api/asignaturas/1", contenido);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Put_Inexistente_DeberiaRetornar404()
        {
            var editado = new Asignatura
            {
                Nombre = "Test",
                Descripcion = "Prueba"
            };

            var contenido = new StringContent(
                JsonSerializer.Serialize(editado),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PutAsync("/api/asignaturas/999", contenido);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task Delete_DeberiaEliminar()
        {
            var response = await _client.DeleteAsync("/api/asignaturas/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Inexistente_DeberiaRetornar404()
        {
            var response = await _client.DeleteAsync("/api/asignaturas/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}
