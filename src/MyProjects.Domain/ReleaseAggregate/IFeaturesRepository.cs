using Ddd.Interfaces;

namespace MyReleases.Domain.ReleaseAggregate
{
    public interface IFeaturesRepository:IRepository<ReleaseFeature>
    {
        Task<List<ReleaseFeatureComment>> GetComments(string featureId);
        Task<ReleaseFeatureComment> GetComment(string featureId, string commentId);
        Task AddComment(ReleaseFeatureComment comment);
        Task DeleteComment(ReleaseFeatureComment comment);
        Task<List<ReleaseFeaturePhase>> GetPhases(string featureId);
        Task<ReleaseFeaturePhase> GetPhase(string featureId, string phaseId);
        Task AddPhase(ReleaseFeaturePhase phase);
        Task DeletePhase(ReleaseFeaturePhase phase);
        Task<List<ReleaseFeatureRollout>> GetRollouts(string featureId);
        Task<ReleaseFeatureRollout> GetRollout(string featureId, string rolloutId);
        Task AddRollout(ReleaseFeatureRollout rollout);
        Task DeleteRollout(ReleaseFeatureRollout rollout);
    }
}
