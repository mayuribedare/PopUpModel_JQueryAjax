using Microsoft.EntityFrameworkCore;
using PopUpModel_JQueryAjax.Models;

namespace PopUpModel_JQueryAjax.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
