using AutoMapper;
using Ddd.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Database.Tables;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;

namespace MyProjects.Infrastructure.Repositories.Release
{
    public class FeaturesRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IFeaturesRepository
    {
        public async Task AddComment(ReleaseFeatureComment comment)
        {
            var commentTable = mapper.Map<FeatureCommentTable>(comment);

            await context.FeatureComments.AddAsync(commentTable);

            await context.SaveChangesAsync();
        }
            
        public async Task AddPhase(ReleaseFeaturePhase phase)
        {
            var phaseTable = mapper.Map<FeaturePhaseTable>(phase);

            await context.FeaturePhases.AddAsync(phaseTable);

            await context.SaveChangesAsync();
        }

        public async Task AddRollout(ReleaseFeatureRollout rollout)
        {
            var rolloutTable = mapper.Map<FeatureRolloutTable>(rollout);

            await context.FeatureRollouts.AddAsync(rolloutTable);

            await context.SaveChangesAsync();
        }

        public async Task Create(ReleaseFeature item)
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

        public async Task DeleteComment(ReleaseFeatureComment comment)
        {
            var commentTable = mapper.Map<FeatureCommentTable>(comment);

            context.FeatureComments.Remove(commentTable);

            await context.SaveChangesAsync();
        }

        public async Task DeletePhase(ReleaseFeaturePhase phase)
        {
            var phaseTable = mapper.Map<FeaturePhaseTable>(phase);

            context.FeaturePhases.Remove(phaseTable);

            await context.SaveChangesAsync();
        }

        public async Task DeleteRollout(ReleaseFeatureRollout rollout)
        {
            var rolloutTable = mapper.Map<FeatureRolloutTable>(rollout);

            context.FeatureRollouts.Remove(rolloutTable);

            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string id)
        {
            return await context.Features.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ReleaseFeature>> GetAll(PaginationDto pagination)
        {
            var queryable = context.Features.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(p => p.Name).Paginate(pagination).ToListAsync();

            var features = mapper.Map<IEnumerable<ReleaseFeature>>(data);

            return features;
        }

        public async Task<ReleaseFeature> GetById(string id)
        {
            var featureTable = await context.Features.FirstOrDefaultAsync(x => x.Id == id);

            var featureDomain = mapper.Map<ReleaseFeature>(featureTable);

            return featureDomain;
        }

        public async Task<ReleaseFeatureComment> GetComment(string featureId, string commentId)
        {
            var commentTable = await context.FeatureComments.FirstOrDefaultAsync(x => x.FeatureId == featureId && x.CommentId == commentId);

            var commentDomain = mapper.Map<ReleaseFeatureComment>(commentTable);

            return commentDomain;
        }

        public async Task<List<ReleaseFeatureComment>> GetComments(string featureId)
        {
            var commentsTable = await context.FeatureComments.Where(x => x.FeatureId == featureId).ToListAsync();

            var commentsDomain = mapper.Map<List<ReleaseFeatureComment>>(commentsTable);

            return commentsDomain;
        }

        public async Task<ReleaseFeaturePhase> GetPhase(string featureId, string phaseId)
        {
            var phaseTable = await context.FeaturePhases.FirstOrDefaultAsync(x => x.FeatureId == featureId && x.PhaseId == phaseId);

            var phaseDomain = mapper.Map<ReleaseFeaturePhase>(phaseTable);

            return phaseDomain;
        }

        public async Task<List<ReleaseFeaturePhase>> GetPhases(string featureId)
        {
            var phasesTable = await context.FeaturePhases.Where(x => x.FeatureId == featureId).ToListAsync();

            var phasesDomain = mapper.Map<List<ReleaseFeaturePhase>>(phasesTable);

            return phasesDomain;
        }

        public async Task<ReleaseFeatureRollout> GetRollout(string featureId, string rolloutId)
        {
            var rolloutTable = await context.FeatureRollouts.FirstOrDefaultAsync(x => x.FeatureId == featureId && x.RolloutId == rolloutId);

            var rolloutDomain = mapper.Map<ReleaseFeatureRollout>(rolloutTable);

            return rolloutDomain;
        }

        public async Task<List<ReleaseFeatureRollout>> GetRollouts(string featureId)
        {
            var rolloutsTable = await context.FeatureRollouts.Where(x => x.FeatureId == featureId).ToListAsync();

            var rolloutsDomain = mapper.Map<List<ReleaseFeatureRollout>>(rolloutsTable);

            return rolloutsDomain;
        }

        public async Task Update(ReleaseFeature item)
        {
            var featureTable = mapper.Map<ReleaseFeature>(item);

            context.Update(featureTable);

            await context.SaveChangesAsync();
        }
    }
}
