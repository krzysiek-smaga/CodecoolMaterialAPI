using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodecoolMaterialAPI.DAL.Models;

namespace CodecoolMaterialAPI.DAL.Data
{
    public class CodecoolMaterialDbContext : DbContext
    {
        public CodecoolMaterialDbContext(DbContextOptions<CodecoolMaterialDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<EduMaterialNavPoint> EduMaterialNavPoints { get; set; }
        public DbSet<EduMaterialType> EduMaterialTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<EduMaterialNavPoint>().ToTable("EduMaterialNavPoint");
            modelBuilder.Entity<EduMaterialType>().ToTable("EduMaterialType");
            modelBuilder.Entity<Review>().ToTable("Review");
        }
    }
}
