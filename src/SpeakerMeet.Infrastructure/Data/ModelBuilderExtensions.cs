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
                new Tag { Id = guid[4], Name = "Quiet Residential Area", IsActive = true, Created = DateTime.UtcNow, Updated =  DateTime.Now.AddHours(2)},
                new Tag { Id = guid[5], Name = "A little Noisier Residential Area", IsActive = true, Created = DateTime.UtcNow.AddHours(-3), Updated = DateTime.Now.AddHours(5)},
                new Tag { Id = guid[6], Name = "A very Noisy Community", IsActive = true, Created = DateTime.UtcNow.AddHours(6), Updated = DateTime.Now.AddHours(2) },
                new Tag { Id = guid[7], Name = "Quiet Industrial Nightmare", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.Now.AddHours(15) }
                    );
        }
    }
}
