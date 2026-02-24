namespace Customer_Service.Helpers
{
    public class PagedResponse<T> : ApiResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            Success = true;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }

}
