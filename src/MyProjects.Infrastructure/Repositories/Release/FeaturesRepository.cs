using AutoMapper;
using Ddd;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ProjectAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Database.Tables;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;

namespace MyProjects.Infrastructure.Repositories.Project
{
    public class FeaturesRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IFeaturesRepository
    {
        public async Task AddComment(ProjectFeatureComment comment)
        {
            var commentTable = mapper.Map<FeatureCommentTable>(comment);

            await context.FeatureComments.AddAsync(commentTable);

            await context.SaveChangesAsync();
        }
            
        public async Task AddPhase(ProjectFeaturePhase phase)
        {
            var phaseTable = mapper.Map<FeaturePhaseTable>(phase);

            await context.FeaturePhases.AddAsync(phaseTable);

            await context.SaveChangesAsync();
        }

        public async Task AddRollout(ProjectFeatureRollout rollout)
        {
            var rolloutTable = mapper.Map<FeatureRolloutTable>(rollout);

            await context.FeatureRollouts.AddAsync(rolloutTable);

            await context.SaveChangesAsync();
        }

        public async Task Create(ProjectFeature item)
        {
            var featureTable = mapper.Map<FeatureTable>(item);

            await context.Features.AddAsync(featureTable);

            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var featureTable = await context.Features.FirstOrDefaultAsync(x => x.Id == id);

            context.Features.Remove(featureTable);

            await context.SaveChangesAsync();
        }

        public async Task DeleteComment(ProjectFeatureComment comment)
        {
            var commentTable = mapper.Map<FeatureCommentTable>(comment);

            context.FeatureComments.Remove(commentTable);

            await context.SaveChangesAsync();
        }

        public async Task DeletePhase(ProjectFeaturePhase phase)
        {
            var phaseTable = mapper.Map<FeaturePhaseTable>(phase);

            context.FeaturePhases.Remove(phaseTable);

            await context.SaveChangesAsync();
        }

        public async Task DeleteRollout(ProjectFeatureRollout rollout)
        {
            var rolloutTable = mapper.Map<FeatureRolloutTable>(rollout);

            context.FeatureRollouts.Remove(rolloutTable);

            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string id)
        {
            return await context.Features.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ProjectFeature>> GetAll(Pagination pagination)
        {
            var queryable = context.Features.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(p => p.Name).Paginate(pagination).ToListAsync();

            var features = mapper.Map<IEnumerable<ProjectFeature>>(data);

            return features;
        }

        public async Task<ProjectFeature> GetById(string id)
        {
            var featureTable = await context.Features.FirstOrDefaultAsync(x => x.Id == id);

            var featureDomain = mapper.Map<ProjectFeature>(featureTable);

            return featureDomain;
        }

        public async Task<ProjectFeatureComment> GetComment(string featureId, string commentId)
        {
            var commentTable = await context.FeatureComments.FirstOrDefaultAsync(x => x.FeatureId == featureId && x.CommentId == commentId);

            var commentDomain = mapper.Map<ProjectFeatureComment>(commentTable);

            return commentDomain;
        }

        public async Task<List<ProjectFeatureComment>> GetComments(string featureId)
        {
            var commentsTable = await context.FeatureComments.Where(x => x.FeatureId == featureId).ToListAsync();

            var commentsDomain = mapper.Map<List<ProjectFeatureComment>>(commentsTable);

            return commentsDomain;
        }

        public async Task<ProjectFeaturePhase> GetPhase(string featureId, string phaseId)
        {
            var phaseTable = await context.FeaturePhases.FirstOrDefaultAsync(x => x.FeatureId == featureId && x.PhaseId == phaseId);

            var phaseDomain = mapper.Map<ProjectFeaturePhase>(phaseTable);

            return phaseDomain;
        }

        public async Task<List<ProjectFeaturePhase>> GetPhases(string featureId)
        {
            var phasesTable = await context.FeaturePhases.Where(x => x.FeatureId == featureId).ToListAsync();

            var phasesDomain = mapper.Map<List<ProjectFeaturePhase>>(phasesTable);

            return phasesDomain;
        }

        public async Task<ProjectFeatureRollout> GetRollout(string featureId, string rolloutId)
        {
            var rolloutTable = await context.FeatureRollouts.FirstOrDefaultAsync(x => x.FeatureId == featureId && x.RolloutId == rolloutId);

            var rolloutDomain = mapper.Map<ProjectFeatureRollout>(rolloutTable);

            return rolloutDomain;
        }

        public async Task<List<ProjectFeatureRollout>> GetRollouts(string featureId)
        {
            var rolloutsTable = await context.FeatureRollouts.Where(x => x.FeatureId == featureId).ToListAsync();

            var rolloutsDomain = mapper.Map<List<ProjectFeatureRollout>>(rolloutsTable);

            return rolloutsDomain;
        }

        public async Task Update(ProjectFeature item)
        {
            var featureTable = mapper.Map<ProjectFeature>(item);

            context.Update(featureTable);

            await context.SaveChangesAsync();
        }
    }
}
