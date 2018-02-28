namespace ComputerPartsInventory
{
    class PartDetail : Part
    {
        public PartDetail() {}

        public string PartType { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Remarks { get; set; }

        public int Add()
        {
            return DbUtils.InsertPart(Description, Condition, PartType, Location, Price, Remarks);
        }
        public void Read(int id)
        {
            PartDetail detail = DbUtils.SelectPartDetail(id);
            Id = id;
            Description = detail.Description;
            Condition = detail.Condition;
            PartType = detail.PartType;
            Price = detail.Price;
            Location = detail.Location;
            Remarks = detail.Remarks;
        }
        public void Update()
        {
            DbUtils.UpdatePart(Id, Description, Condition, PartType, Location, Price, Remarks);
        }
    }
}
