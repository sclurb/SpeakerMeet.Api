using System;
using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {

        private static readonly Guid[] guid = new Guid[]
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Community>().HasData(
                new Community { Id = new Guid("725C6768-7EAB-43B0-AA39-86F15E97824A"), Name = "Sample Community", Slug = "sample-community", Location = "Tampa, FL", Description = "Sample Community", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.UtcNow}
            );

            modelBuilder.Entity<Conference>().HasData(
                new Community { Id = new Guid("BE0CBB60-FF0B-4E47-8E37-8F024AF1A5D2"), Name = "Sample Conference", Slug = "sample-conference", Location = "New York, NY", Description = "Sample Conference", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
            );

            modelBuilder.Entity<Speaker>().HasData(
                new Speaker { Id = new Guid("AA21D845-0562-4714-8DE0-04B4971C702B"), Name = "Sample Speaker", Slug = "sample-speaker", Location = "Louisville, KY", Description = "Sample Speaker", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
            );
            modelBuilder.Entity<SocialPlatform>().HasData(
                new SocialPlatform { Id = guid[0], Name = "Twitter" },
                new SocialPlatform { Id = guid[1], Name = "Linked In" },
                new SocialPlatform { Id = guid[2], Name = "GitHub" },
                new SocialPlatform { Id = guid[3], Name = "Palm Harbor Technical Resources" }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = guid[4], Name = "Quiet Residential Area", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.Now.AddHours(2) },
                new Tag { Id = guid[5], Name = "A little Noisier Residential Area", IsActive = true, Created = DateTime.UtcNow.AddHours(-3), Updated = DateTime.Now.AddHours(5) },
                new Tag { Id = guid[6], Name = "A very Noisy Community", IsActive = true, Created = DateTime.UtcNow.AddHours(6), Updated = DateTime.Now.AddHours(2) },
                new Tag { Id = guid[7], Name = "Quiet Industrial Nightmare", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.Now.AddHours(15) }
            );
            modelBuilder.Entity<Community>().HasData(
                new Community { Id = new Guid("DC22E451-A6AA-4276-9B01-5B343229B964"), Name = "Freddy's Trailer Park", Slug = "Freddy'd Trailer Park", Location = "Pasco County", Description = "A trailer park with people in need of help", IsActive = true, UpdatedBy = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), Created = DateTime.UtcNow, Updated = DateTime.UtcNow },
                new Community { Id = new Guid("75387FB2-8DE4-48D2-90AE-82C35B3AC1B4"), Name = "Beacon Groves", Slug = "Simple Subdivision", Location = "Palm Harbor, Fl", Description = "Single family homes", IsActive = true, UpdatedBy = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), Created = DateTime.UtcNow, Updated = DateTime.UtcNow },
                new Community { Id = new Guid("6BDA0880-156E-498C-BE15-AD5CD04758FD"), Name = "Emerald Isle", Slug = "Beautiful Place", Location = "Cearwater, Fl", Description = "Single family homes", IsActive = true, UpdatedBy = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), Created = DateTime.UtcNow, Updated = DateTime.UtcNow },
                new Community { Id = new Guid("D6697E38-9C52-4941-90BA-CD393D74A9CC"), Name = "La Villa Strangiata", Slug = "Funky Toon", Location = "Lackowanna, FL", Description = "Strange cozy place in the swamp", IsActive = true, UpdatedBy = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
                );
            //modelBuilder.Entity<CommunityTag>().HasData(
            //    new CommunityTags { Id = guid[8], TagId = new Guid("9d7aa94d-0aec-4989-9a22-ba9ca2ee6139"), CommunityId = new Guid("dc22e451-a6aa-4276-9b01-5b343229b964") },
            //    new CommunityTags { Id = guid[9], TagId = new Guid("b305c1d9-a7f5-4be5-8eaf-fd0ec3bf543b"), CommunityId = new Guid("75387fb2-8de4-48d2-90ae-82c35b3ac1b4") }
            //    );
        }
    }
}
