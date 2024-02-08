namespace Application.Dtos
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string ManufacturerEmail { get; set; }
        public string ManufacturerPhone { get; set; }
        public DateTime ProduceDate { get; set; }

    }
}
