using Microsoft.EntityFrameworkCore;
using API.Entities;


namespace API.Data
{
    public class DataContext : DbContext
    {

 public DataContext(DbContextOptions options) : base(options)
        {
        }

     public DbSet<AppUser> Users{ get; private set; } 
     public DbSet<Complaint> Complaints{ get; private set; } 
    //  public DbSet<ComplaintsComment> ComplaintsComments{ get; private set; } 
     public DbSet<Department> Departments{ get; private set; } 
     public DbSet<Role> Roles{ get; private set; } 
     public DbSet<ComplaintStatus> ComplaintStatuses{ get; private set; } 

     // not needed here 
     public DbSet<ComplaintsComment> ComplaintsComments{ get; private set; } 

    }
 
}