using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.IO;

namespace Escuela_Test
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectPath = ResolveProjectPath("Escuela-Back", "Escuela-Back.csproj");
            if (!string.IsNullOrEmpty(projectPath))
            {
                builder.UseContentRoot(projectPath);
            }
        }

        private static string ResolveProjectPath(string projectFolderName, string csprojFileName)
        {
            var baseDir = AppContext.BaseDirectory;
            var dir = new DirectoryInfo(baseDir);

            while (dir != null)
            {
                var candidateFolder = Path.Combine(dir.FullName, projectFolderName);
                var candidateCsproj = Path.Combine(candidateFolder, csprojFileName);
                if (File.Exists(candidateCsproj))
                {
                    return candidateFolder;
                }

                var csprojInCurrent = Path.Combine(dir.FullName, csprojFileName);
                if (File.Exists(csprojInCurrent))
                {
                    return dir.FullName;
                }

                dir = dir.Parent;
            }

            return string.Empty;
        }
    }
}