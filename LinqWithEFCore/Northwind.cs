using Microsoft.EntityFrameworkCore; // DbContext, DbSet<T>
using System.Diagnostics;

namespace Packt.Shared; // this manages the connection to the database

public class Northwind : DbContext { // these properties map to tables in the database
    public DbSet<Category>? Categories { get; set; } 
    public DbSet<Product>? Products { get; set; } 
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder) {
        string connection = "Data Source= DESKTOP-58E2NJ4\\CS10DOTNET6;" + "Initial Catalog=Northwind;" + "Integrated Security=true;" + "MultipleActiveResultSets=true;";
        optionsBuilder.UseSqlServer(connection);
    }
    protected override void OnModelCreating( ModelBuilder modelBuilder) { 
        modelBuilder.Entity<Product>() .Property(product => product.UnitPrice) .HasConversion<double>(); 
    }
}

