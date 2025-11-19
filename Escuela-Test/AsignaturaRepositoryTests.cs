using Escuela_Back.Models;
using Escuela_Back.Repositories;

namespace Escuela_Test
{
    public class AsignaturaRepositoryTests
    {
        private readonly MockJsonDataService _json;
        private readonly AsignaturaRepository _repo;
        private const string FileName = "asignaturas_data.json";

        public AsignaturaRepositoryTests()
        {
            _json = new MockJsonDataService();

            var initialData = new List<Asignatura>
            {
                new Asignatura
                {
                    Id = Guid.Parse("a466800b-d70f-4194-8ca0-df30a830a8e9"),
                    Nombre = "Matemáticas",
                    Descripcion = "Matematica"
                },
                new Asignatura
                {
                    Id = Guid.Parse("079993ea-d4c4-4d3e-9f08-191e8d67cb10"),
                    Nombre = "Historia",
                    Descripcion = "Historia Argentina"
                }
            };

            _json.Seed(FileName, initialData);

            _repo = new AsignaturaRepository(_json);
        }

        [Fact]
        public async Task DeberiaDevolverLista()
        {
            var lista = await _repo.GetAllAsync();

            Assert.NotNull(lista);
            Assert.Equal(2, lista.Count);
        }

        [Fact]
        public async Task DeberiaDevolverAsignatura()
        {
            var item = await _repo.GetByIdAsync(Guid.Parse("a466800b-d70f-4194-8ca0-df30a830a8e9"));

            Assert.NotNull(item);
            Assert.Equal("Matemáticas", item!.Nombre);
        }

        [Fact]
        public async Task DeberiaDevolverNull()
        {
            var item = await _repo.GetByIdAsync(Guid.NewGuid());
            Assert.Null(item);
        }

        [Fact]
        public async Task DeberiaAgregarNuevo()
        {
            var nuevo = new Asignatura
            {
                Nombre = "Geografía",
                Descripcion = "Geografía desc"
            };

            var creado = await _repo.AddAsync(nuevo);
            var lista = await _repo.GetAllAsync();

            Assert.NotNull(creado);
            Assert.NotEqual(Guid.Empty, creado.Id);
            Assert.Equal(3, lista.Count);
        }

        [Fact]
        public async Task DeberiaActualizar()
        {
            var id = Guid.Parse("079993ea-d4c4-4d3e-9f08-191e8d67cb10");

            var modelo = new Asignatura
            {
                Nombre = "Historia",
                Descripcion = "Historia desc"
            };

            var actualizado = await _repo.UpdateAsync(id, modelo);

            Assert.NotNull(actualizado);
            Assert.Equal("Historia", actualizado!.Nombre);
        }

        [Fact]
        public async Task DeberiaDevolverNullSinActualizar()
        {
            var actualizado = await _repo.UpdateAsync(Guid.NewGuid(), new Asignatura());
            Assert.Null(actualizado);
        }

        [Fact]
        public async Task DeberiaBorrar()
        {
            var id = Guid.Parse("a466800b-d70f-4194-8ca0-df30a830a8e9");

            var ok = await _repo.DeleteAsync(id);
            var lista = await _repo.GetAllAsync();

            Assert.True(ok);
            Assert.Single(lista);
        }

        [Fact]
        public async Task DeberiaDevolverFalse()
        {
            var ok = await _repo.DeleteAsync(Guid.NewGuid());
            Assert.False(ok);
        }
    }
}
