using AutoMapper;
using Ddd.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Database.Tables;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;

namespace Myreleases.Infrastructure.Repositories.releases
{
    public class ReleasesRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IReleasesRepository
    {
        public async Task<IEnumerable<ReleaseEntity>> GetAll(PaginationDto pagination)
        {
            var queryable = context.Releases.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(p => p.Title).Paginate(pagination).ToListAsync();

            var releases = mapper.Map<IEnumerable<ReleaseEntity>>(data);

            return releases;
        }

        public async Task<IEnumerable<ReleaseEntity>> GetByTitle(string title)
        {
            var releasesTable = await context.Releases.Where(x => x.Title.Contains(title)).OrderBy(p => p.Title).ToListAsync();

            var releasesDomain = mapper.Map<IEnumerable<ReleaseEntity>>(releasesTable);

            return releasesDomain;
        }

        public async Task<ReleaseEntity> GetById(string id)
        {
            var releaseTable = await context.Releases.FirstOrDefaultAsync(x => x.Id == id);

            var releaseDomain = mapper.Map<ReleaseEntity>(releaseTable);

            return releaseDomain;
        }

        public async Task<bool> Exists(string id)
        {
            return await context.Releases.AnyAsync(x => x.Id == id);
        }


        public async Task Create(ReleaseEntity release)
        {
            context.Add(release);
            await context.SaveChangesAsync();
        }


        public async Task Update(ReleaseEntity release)
        {
            context.Update(release);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            await context.Releases.Where(release => release.Id == id).ExecuteDeleteAsync();
        }

        public async Task<ReleaseFeature> GetFeature(string releaseId, string featureId)
        {
            var featureTable = await context.ReleaseFeatures.FirstOrDefaultAsync(x => x.ReleaseId == releaseId && x.FeatureId == featureId);

            var featureDomain = mapper.Map<ReleaseFeature>(featureTable);

            return featureDomain;
        }

        public async Task<IEnumerable<ReleaseFeature>> GetFeatures(string releaseId)
        {
            var featuresTable = await context.ReleaseFeatures.Where(x => x.ReleaseId == releaseId).ToListAsync();

            var featuresDomain = mapper.Map<IEnumerable<ReleaseFeature>>(featuresTable);

            return featuresDomain;
        }

        public async Task AddFeature(ReleaseFeature feature)
        {
            var featureTable = mapper.Map<ReleaseFeatureTable>(feature);

            context.Add(featureTable);
            
            await context.SaveChangesAsync();
        }

        public async Task RemoveFeature(ReleaseFeature feature)
        {
            var featureTable = mapper.Map<ReleaseFeatureTable>(feature);

            context.Remove(featureTable);

            await context.SaveChangesAsync();
        }

        public async Task<ReleaseComment> GetComment(string releaseId, string commentId)
        {
            var commentTable = await context.ReleaseComments.FirstOrDefaultAsync(x => x.ReleaseId == releaseId && x.CommentId == commentId);

            var commentDomain = mapper.Map<ReleaseComment>(commentTable);

            return commentDomain;
        }

        public async Task<IEnumerable<ReleaseComment>> GetComments(string releaseId)
        {
            var commentsTable = await context.ReleaseComments.Where(x => x.ReleaseId == releaseId).ToListAsync();

            var commentsDomain = mapper.Map<IEnumerable<ReleaseComment>>(commentsTable);

            return commentsDomain;
        }

        public async Task AddComment(ReleaseComment comment)
        {
            var commentTable = mapper.Map<ReleaseCommentTable>(comment);

            context.Add(commentTable);

            await context.SaveChangesAsync();
        }

        public async Task RemoveComment(ReleaseComment comment)
        {
            var commentTable = mapper.Map<ReleaseCommentTable>(comment);

            context.Remove(commentTable);

            await context.SaveChangesAsync();
        }

        public async Task<ReleaseReference> GetReference(string releaseId, string referenceId)
        {
            var referenceTable = await context.ReleaseReferences.FirstOrDefaultAsync(x => x.ReleaseId == releaseId && x.ReferenceId == referenceId);

            var referenceDomain = mapper.Map<ReleaseReference>(referenceTable);

            return referenceDomain;
        }

        public async Task<IEnumerable<ReleaseReference>> GetReferences(string releaseId)
        {
            var referencesTable = await context.ReleaseReferences.Where(x => x.ReleaseId == releaseId).ToListAsync();

            var referencesDomain = mapper.Map<IEnumerable<ReleaseReference>>(referencesTable);

            return referencesDomain;
        }

        public async Task AddReference(ReleaseReference reference)
        {
            var referenceTable = mapper.Map<ReleaseReferenceTable>(reference);

            context.Add(referenceTable);

            await context.SaveChangesAsync();
        }

        public async Task RemoveReference(ReleaseReference reference)
        {
            var referenceTable = mapper.Map<ReleaseReferenceTable>(reference);

            context.Remove(referenceTable);

            await context.SaveChangesAsync();
        }
    }
}
