using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>
            {
                new Difficulty {Id = Guid.Parse("b5767e41-a322-4a3a-9e45-9be5b889fb51"), Name = "Easy"},
                new Difficulty {Id = Guid.Parse("0215db79-fab9-4f35-bb0e-d1024a495603"), Name = "Moderate"},
                new Difficulty {Id = Guid.Parse("6fd20390-1c46-4521-998e-59f6d5deb7dd"), Name = "Hard"}
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
            {
                new Region
                {
                    Id= Guid.Parse("767d81d0-b660-4318-9e18-8b36e21c7912"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl=null
                },
                new Region
                {
                    Id= Guid.Parse("ae091cb5-4549-4939-a0f4-8cb5138aef0c"),
                    Name = "Nothland",
                    Code = "NTL",
                    RegionImageUrl=null
                },
                new Region
                {
                    Id= Guid.Parse("ebaf65d4-1751-46b9-a3c9-b96ecfbb1b2a"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl=null
                },
                new Region
                {
                    Id= Guid.Parse("5bfb536b-b3ac-435e-99e0-246bdfb2e98f"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl=null
                },
                new Region
                {
                    Id= Guid.Parse("aedc2547-5877-4324-9663-8c3cddccfbaf"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl=null
                },
                new Region
                {
                    Id= Guid.Parse("d8822eb4-6db0-4419-a067-4865a509eb5a"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl=null
                },

            };
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
