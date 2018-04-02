namespace ComputerPartDb
{
    using System.Data.Entity;

    public partial class ComputerPartContext : DbContext
    {
        public ComputerPartContext()
            : base("name=ComputerPartContext")
        {
        }

        public virtual DbSet<ComputerPart> ComputerParts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComputerPart>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerPart>()
                .Property(e => e.Condition)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerPart>()
                .Property(e => e.PartType)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerPart>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<ComputerPart>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ComputerPart>()
                .Property(e => e.Remarks)
                .IsUnicode(false);
        }
    }
}
