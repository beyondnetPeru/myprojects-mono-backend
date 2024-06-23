namespace Ddd.Dtos
{
    public class PaginationDto
    {
        public int Page { get; private set; }
        private int recordsPerPage = 10;
        private readonly int recordsPerPageMax = 30;

        public PaginationDto(int page)
        {
            Page = page;            
        }

        public int RecordsPerPage
        {
            get => recordsPerPage;
            set => recordsPerPage = value > recordsPerPageMax ? recordsPerPageMax : value;
        }
    }  
}
