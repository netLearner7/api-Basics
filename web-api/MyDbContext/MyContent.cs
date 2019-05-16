using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.api.Entites;
using web.api.Entites.Configuration;

namespace web.api.MyDbContext
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
           : base(options)
        {
            //如果有当前context连接的数据库则什么都不发生，没有则创建这个数据库
            Database.EnsureCreated();
            //代码更新数据库，用这玩意更新之后没法使用nuget的migration命令创建，视情况而定
            //Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
        }
    }
}
