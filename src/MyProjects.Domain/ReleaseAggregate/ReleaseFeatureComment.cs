namespace MyProjects.Domain.ReleaseAggregate
{
    public  class ReleaseFeatureComment
    {
        public ReleaseFeatureCommentName Comment { get; set; } 
        public ReleaseFeatureCommentAuthor Author { get; set; }
        public ReleaseFeatureCommentDate Date { get; set; }
    }
}
