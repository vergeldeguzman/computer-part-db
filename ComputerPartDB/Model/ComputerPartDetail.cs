﻿namespace ComputerParstDb
{
    public class ComputerPartDetail : ComputerPart
    {
        public ComputerPartDetail() {}

        public decimal? Price { get; set; }

        public string Location { get; set; }

        public string Remarks { get; set; }
    }
}
