namespace ProvaPub.Dtos
{
    public class QueryStringParameters
    {
        const int maxPageSize = 10;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 50;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
    public class ApiResponse<TData>
    {
        public TData Data { get; private set; }

        public ApiResponse(TData data)
            => Data = data;
    }
}
