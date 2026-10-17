using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebSite.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
    public DbSet<AspNetCoreWebSite.Models.DiscussionForum> DiscussionForum { get; set; } = default!;
    }
}
