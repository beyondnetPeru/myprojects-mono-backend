using AutoMapper;
using Ddd;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ProjectAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Database.Tables;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;

namespace MyProjects.Infrastructure.Repositories.Projects
{
    public class ProjectsRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IProjectsRepository
    {
        public async Task<IEnumerable<ProjectEntity>> GetAll(Pagination pagination)
        {
            var queryable = context.Projects.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(p => p.Title).Paginate(pagination).ToListAsync();

            var Projects = mapper.Map<IEnumerable<ProjectEntity>>(data);

            return Projects;
        }

        public async Task<IEnumerable<ProjectEntity>> GetByTitle(string title)
        {
            var ProjectsTable = await context.Projects.Where(x => x.Title.Contains(title)).OrderBy(p => p.Title).ToListAsync();

            var ProjectsDomain = mapper.Map<IEnumerable<ProjectEntity>>(ProjectsTable);

            return ProjectsDomain;
        }

        public async Task<ProjectEntity> GetById(string id)
        {
            var ProjectTable = await context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            var ProjectDomain = mapper.Map<ProjectEntity>(ProjectTable);

            return ProjectDomain;
        }

        public async Task<bool> Exists(string id)
        {
            return await context.Projects.AnyAsync(x => x.Id == id);
        }


        public async Task Create(ProjectEntity Project)
        {
            context.Add(Project);
            await context.SaveChangesAsync();
        }


        public async Task Update(ProjectEntity Project)
        {
            context.Update(Project);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            await context.Projects.Where(Project => Project.Id == id).ExecuteDeleteAsync();
        }

        public async Task<ProjectFeature> GetFeature(string ProjectId, string featureId)
        {
            var featureTable = await context.ProjectFeatures.FirstOrDefaultAsync(x => x.ProjectId == ProjectId && x.FeatureId == featureId);

            var featureDomain = mapper.Map<ProjectFeature>(featureTable);

            return featureDomain;
        }

        public async Task<IEnumerable<ProjectFeature>> GetFeatures(string ProjectId)
        {
            var featuresTable = await context.ProjectFeatures.Where(x => x.ProjectId == ProjectId).ToListAsync();

            var featuresDomain = mapper.Map<IEnumerable<ProjectFeature>>(featuresTable);

            return featuresDomain;
        }

        public async Task AddFeature(ProjectFeature feature)
        {
            var featureTable = mapper.Map<ProjectFeatureTable>(feature);

            context.Add(featureTable);
            
            await context.SaveChangesAsync();
        }

        public async Task RemoveFeature(ProjectFeature feature)
        {
            var featureTable = mapper.Map<ProjectFeatureTable>(feature);

            context.Remove(featureTable);

            await context.SaveChangesAsync();
        }

        public async Task<ProjectComment> GetComment(string ProjectId, string commentId)
        {
            var commentTable = await context.ProjectComments.FirstOrDefaultAsync(x => x.ProjectId == ProjectId && x.CommentId == commentId);

            var commentDomain = mapper.Map<ProjectComment>(commentTable);

            return commentDomain;
        }

        public async Task<IEnumerable<ProjectComment>> GetComments(string ProjectId)
        {
            var commentsTable = await context.ProjectComments.Where(x => x.ProjectId == ProjectId).ToListAsync();

            var commentsDomain = mapper.Map<IEnumerable<ProjectComment>>(commentsTable);

            return commentsDomain;
        }

        public async Task AddComment(ProjectComment comment)
        {
            var commentTable = mapper.Map<ProjectCommentTable>(comment);

            context.Add(commentTable);

            await context.SaveChangesAsync();
        }

        public async Task RemoveComment(ProjectComment comment)
        {
            var commentTable = mapper.Map<ProjectCommentTable>(comment);

            context.Remove(commentTable);

            await context.SaveChangesAsync();
        }

        public async Task<ProjectReference> GetReference(string ProjectId, string referenceId)
        {
            var referenceTable = await context.ProjectReferences.FirstOrDefaultAsync(x => x.ProjectId == ProjectId && x.ReferenceId == referenceId);

            var referenceDomain = mapper.Map<ProjectReference>(referenceTable);

            return referenceDomain;
        }

        public async Task<IEnumerable<ProjectReference>> GetReferences(string ProjectId)
        {
            var referencesTable = await context.ProjectReferences.Where(x => x.ProjectId == ProjectId).ToListAsync();

            var referencesDomain = mapper.Map<IEnumerable<ProjectReference>>(referencesTable);

            return referencesDomain;
        }

        public async Task AddReference(ProjectReference reference)
        {
            var referenceTable = mapper.Map<ProjectReferenceTable>(reference);

            context.Add(referenceTable);

            await context.SaveChangesAsync();
        }

        public async Task RemoveReference(ProjectReference reference)
        {
            var referenceTable = mapper.Map<ProjectReferenceTable>(reference);

            context.Remove(referenceTable);

            await context.SaveChangesAsync();
        }
    }
}
