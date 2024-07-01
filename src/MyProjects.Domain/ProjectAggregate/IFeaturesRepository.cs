using Ddd.Interfaces;

namespace MyProjects.Domain.ProjectAggregate
{
    public interface IFeaturesRepository:IRepository<ProjectFeature>
    {
        Task<List<ProjectFeatureComment>> GetComments(string featureId);
        Task<ProjectFeatureComment> GetComment(string featureId, string commentId);
        Task AddComment(ProjectFeatureComment comment);
        Task DeleteComment(ProjectFeatureComment comment);
        Task<List<ProjectFeaturePhase>> GetPhases(string featureId);
        Task<ProjectFeaturePhase> GetPhase(string featureId, string phaseId);
        Task AddPhase(ProjectFeaturePhase phase);
        Task DeletePhase(ProjectFeaturePhase phase);
        Task<List<ProjectFeatureRollout>> GetRollouts(string featureId);
        Task<ProjectFeatureRollout> GetRollout(string featureId, string rolloutId);
        Task AddRollout(ProjectFeatureRollout rollout);
        Task DeleteRollout(ProjectFeatureRollout rollout);
    }
}
