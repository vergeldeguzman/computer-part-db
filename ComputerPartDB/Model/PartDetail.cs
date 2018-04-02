namespace ComputerPartDb
{
    public class PartDetail : Part
    {
        public PartDetail() {}

        public string PartType { get; set; }

        public decimal? Price { get; set; }

        public string Location { get; set; }

        public string Remarks { get; set; }
    }
}
