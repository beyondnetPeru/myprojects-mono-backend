namespace MyProjects.Shared.Application.Pagination
{
    public class PaginationDto
    {
        public int Page { get; set; }
        private int recordsPerPage = 10;
        private readonly int recordsPerPageMax = 30;

        public int RecordsPerPage
        {
            get => recordsPerPage;
            set => recordsPerPage = value > recordsPerPageMax ? recordsPerPageMax : value;
        }

    }
}
