using Ddd.Interfaces;

namespace MyProjects.Domain.ReleaseAggregate
{
    public interface IReleasesRepository : IRepository<ReleaseEntity>
    {
        Task<ReleaseFeature> GetFeature(string releaseId, string featureId);
        Task<IEnumerable<ReleaseFeature>> GetFeatures(string releaseId);
        Task AddFeature(ReleaseFeature feature);
        Task RemoveFeature(ReleaseFeature feature);

        Task<ReleaseComment> GetComment(string releaseId, string commentId);
        Task<IEnumerable<ReleaseComment>> GetComments(string releaseId);
        Task AddComment(ReleaseComment comment);
        Task RemoveComment(ReleaseComment comment);

        Task<ReleaseReference> GetReference(string releaseId, string referenceId);
        Task<IEnumerable<ReleaseReference>> GetReferences(string releaseId);
        Task AddReference(ReleaseReference reference);
        Task RemoveReference(ReleaseReference reference);
    }
}
