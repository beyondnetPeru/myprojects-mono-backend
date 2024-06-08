using MyProjects.Shared.Application.Pagination;

namespace MyProjects.Shared.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(PaginationDto pagination);
        Task<T> GetById(string id);
        Task<bool> Exists(string id);
        Task Create(T project);
        Task Update(T project);
        Task Delete(string id);
    }
}
