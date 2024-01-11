using Microsoft.EntityFrameworkCore;
using WebAPIOrders.Core.Entities;

namespace WebAPIOrders.Repository
{
	public class ApplicationContext: DbContext
	{
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }

		public ApplicationContext(DbContextOptions options) : base(options) { }
		public ApplicationContext() { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Order>().HasIndex(ord => ord.Number).IsUnique();

			modelBuilder.Entity<Order>().ToTable("Orders");
			modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
			modelBuilder.Entity<OrderItem>().ToTable(tb => tb.HasTrigger("UpdateTotalAmount"));
		}

	}
}