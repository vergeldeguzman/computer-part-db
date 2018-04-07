namespace Services.Providers
{
    using System.Data.Entity;

    public partial class ComputerPartsContext : DbContext
    {
        public ComputerPartsContext()
            : base("name=ComputerPartsContext")
        {
        }

        public virtual DbSet<ComputerParts> ComputerParts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComputerParts>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerParts>()
                .Property(e => e.Condition)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerParts>()
                .Property(e => e.PartType)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerParts>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerParts>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ComputerParts>()
                .Property(e => e.Remarks)
                .IsUnicode(false);
        }
    }
}
