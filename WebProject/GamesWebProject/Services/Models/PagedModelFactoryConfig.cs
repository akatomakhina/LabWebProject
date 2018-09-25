namespace EntertainmentPortal.Web.Services.Sports.Models
{
    public class PagedModelFactoryConfig
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public string PageParameterValue { get; set; }
        public string RouteName { get; set; }
    }
}