using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextEF : DbContext
    {
        private IConfiguration _config;
        
        public DataContextEF(IConfiguration config)
        {
                _config = config;
        }

        public DbSet<Computer>? Computer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  // the onconfiguring method is called when DBContext is created
        {
            
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"), optionsBuilder => optionsBuilder.EnableRetryOnFailure()); // EnableRetryOnFailure() attempt again if our first connection doesn't suceed
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            // modelBuilder.Entity<Computer>()
            // .ToTable("Computer", "TutorialAppSchema"); // Telling EF explicitly where to look for Table in the schema
            // .ToTable("TableName", "SchemaName");

            modelBuilder.Entity<Computer>()
                //.HasNoKey();
                .HasKey(c => c.ComputerId);


        } 
        
    }
}