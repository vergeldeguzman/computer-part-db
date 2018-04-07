namespace Services.DTO
{
    public class ComputerPartDetailDTO : ComputerPartDTO
    {
        public ComputerPartDetailDTO() {}

        public string PartType { get; set; }

        public decimal? Price { get; set; }

        public string Location { get; set; }

        public string Remarks { get; set; }
    }
}
