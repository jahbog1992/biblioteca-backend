namespace Biblioteca.DTO.Request
{
    public class PaginationDTO
    {
        private readonly int maxRecordsPerPage = 5;
        public int Page { get; set; } = 1;

        private int recordsPerPage;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set { recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value; }
        }
    }
}
