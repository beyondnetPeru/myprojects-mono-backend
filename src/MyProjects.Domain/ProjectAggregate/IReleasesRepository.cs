using Ddd.Interfaces;

namespace MyProjects.Domain.ProjectAggregate
{
    public interface IProjectsRepository : IRepository<ProjectEntity>
    {
        Task<ProjectFeature> GetFeature(string ProjectId, string featureId);
        Task<IEnumerable<ProjectFeature>> GetFeatures(string ProjectId);
        Task AddFeature(ProjectFeature feature);
        Task RemoveFeature(ProjectFeature feature);

        Task<ProjectComment> GetComment(string ProjectId, string commentId);
        Task<IEnumerable<ProjectComment>> GetComments(string ProjectId);
        Task AddComment(ProjectComment comment);
        Task RemoveComment(ProjectComment comment);

        Task<ProjectReference> GetReference(string ProjectId, string referenceId);
        Task<IEnumerable<ProjectReference>> GetReferences(string ProjectId);
        Task AddReference(ProjectReference reference);
        Task RemoveReference(ProjectReference reference);
    }
}
