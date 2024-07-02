using Ddd.Interfaces;
using MyProjects.Domain.ReleaseAggregate;

namespace MyReleases.Domain.ReleaseAggregate
{
    public interface IReleasesRepository : IRepository<Release>
    {
        Task<ReleaseFeature> GetFeature(string ReleaseId, string featureId);
        Task<IEnumerable<ReleaseFeature>> GetFeatures(string ReleaseId);
        Task AddFeature(ReleaseFeature feature);
        Task RemoveFeature(ReleaseFeature feature);

        Task<ReleaseComment> GetComment(string ReleaseId, string commentId);
        Task<IEnumerable<ReleaseComment>> GetComments(string ReleaseId);
        Task AddComment(ReleaseComment comment);
        Task RemoveComment(ReleaseComment comment);

        Task<ReleaseReference> GetReference(string ReleaseId, string referenceId);
        Task<IEnumerable<ReleaseReference>> GetReferences(string ReleaseId);
        Task AddReference(ReleaseReference reference);
        Task RemoveReference(ReleaseReference reference);
    }
}
