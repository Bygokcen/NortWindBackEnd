using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class NorthwindContext:DbContext
{
        
        

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;database=Northwind;trusted_connection=false;User Id=sa;Password=gokcen1989;Persist Security Info=False;Encrypt=False");
    }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }
    
    public DbSet<User>? Users { get; set; }
    
    public DbSet<OperationClaim>? OperationClaims { get; set; }
    
    public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
}