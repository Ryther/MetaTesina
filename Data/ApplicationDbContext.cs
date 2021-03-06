﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MetaTesina.Models;

namespace MetaTesina.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<MetaTesina.Models.Article> Article { get; set; }

        public DbSet<MetaTesina.Models.Category> Category { get; set; }

        public DbSet<MetaTesina.Models.Asset> Asset { get; set; }

        public DbSet<MetaTesina.Models.AssetAttribute> AssetAttribute { get; set; }

        public DbSet<MetaTesina.Models.AssetType> AssetType { get; set; }
    }
}
