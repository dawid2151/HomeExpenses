using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using HomeExpenses.DTO;

namespace HomeExpenses.DTO
{
    public class HomeExpensesContext : DbContext
    {
        public DbSet<ReceiptDTO> Receipts { get; set; }
        public DbSet<StoreDTO> Stores { get; set; }
        public DbSet<ProductDTO> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=HomeExpenses;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReceiptDTO>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Store);
            });
            builder.Entity<StoreDTO>(entity =>
            {
                entity.HasKey(e => e.NIP);
                entity.HasMany(e => e.Receipts);
            });
            builder.Entity<ProductDTO>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Receipt);
            });
        }
    }
}
