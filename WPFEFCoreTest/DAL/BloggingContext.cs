using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFEFCoreTest.Models;

namespace WPFEFCoreTest.DAL
{
    class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Visual Studio 2015 | 使用Visual Studio创建的LocalDb 12 实例
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp;Trusted_Connection=True;");

            // Visual Studio 2013 | 使用Visual Studio创建的LocalDb 11 实例
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Database=EFGetStarted.ConsoleApp;Trusted_Connection=True;");

            // Visual Studio 2012 | 使用Visual Studio创建的SQL Express实例
            // optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EFGetStarted.ConsoleApp;Trusted_Connection=True;");

            optionsBuilder.UseSqlite("Filename=./attcloud.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  配置Blog.Url为Required
            modelBuilder.Entity<Blog>().Property(b => b.Url).IsRequired();
        }

    }
}
