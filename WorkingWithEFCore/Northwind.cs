using Microsoft.EntityFrameworkCore; // DbContext, DbContextOptionsBuilder
using System.Diagnostics;
using static System.Console; 

namespace Packt.Shared;

// this manages the connection to the database

public class Northwind : DbContext{
    // these properties map to tables in the database
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

        optionsBuilder.UseLazyLoadingProxies();

        if (ProjectConstants.DatabaseProvider == "SQLite") {
            string path = Path.Combine(Environment.CurrentDirectory, "Northwind.db");
            WriteLine($"Using {path} database file."); 
            optionsBuilder.UseSqlite($"Filename={path}");
        }
        else {
            string connection = "Data Source= DESKTOP-58E2NJ4\\CS10DOTNET6;" + "Initial Catalog=Northwind;" + "Integrated Security=true;" + "MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        // example of using Fluent API instead of attributes to limit the length of a category name to 15
        modelBuilder.Entity<Category>() .Property(category => category.CategoryName) .IsRequired() .HasMaxLength(15); // NOT NULL 
        if (ProjectConstants.DatabaseProvider == "SQLite") { 
            // added to "fix" the lack of decimal support in SQLite
            modelBuilder.Entity<Product>() .Property(product => product.Cost) .HasConversion<double>(); 
        }
        // global filter to remove discontinued products
        modelBuilder.Entity<Product>() .HasQueryFilter(p => !p.Discontinued);
    } 
}


