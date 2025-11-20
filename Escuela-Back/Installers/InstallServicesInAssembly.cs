using Escuela_Back.Interfaces;
using Escuela_Back.Repositories;
using Escuela_Back.Services;

namespace Escuela_Back.Installers
{
    public static class InstallerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void InstallServicesInAssembly(this IServiceCollection services)
        {
            services.AddSingleton<IJsonDataService, JsonDataService>();
            services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
            services.AddScoped<IEstudianteRepository, EstudianteRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ICursoLogic, CursoLogic>();
            services.AddScoped<IAsignaturaService, AsignaturaService>();
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<IEstudianteService, EstudianteService>();
        }
    }
}
