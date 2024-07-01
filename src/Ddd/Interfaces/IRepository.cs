namespace Ddd.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Pagination pagination);
        Task<T> GetById(string id);
        Task<bool> Exists(string id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(string id);
    }
}
