using Microsoft.AspNetCore.Http;

namespace MyProjects.Shared.Infrastructure.FileStorage
{
    public interface IFileStorage
    {
        Task<string> Store(string container, IFormFile file);
        Task Delete(string path, string container);
        async Task<string> Edit(string? path, string container, IFormFile file) { 
            await Delete(path!, container);
            return await Store(container, file);
        }
    }
}
