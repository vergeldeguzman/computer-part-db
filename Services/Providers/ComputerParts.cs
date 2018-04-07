namespace Services.Providers
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ComputerParts")]
    public partial class ComputerParts
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Condition { get; set; }

        [Required]
        [StringLength(20)]
        public string PartType { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        public decimal? Price { get; set; }

        [Column(TypeName = "text")]
        public string Remarks { get; set; }
    }
}
