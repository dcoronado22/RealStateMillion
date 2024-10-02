namespace RealEstateMillion.Domain.Entities
{
    public class PropertyFilter
    {
        public string? City { get; set; }
        public string? State { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Year { get; set; }
    }
}