using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Models;

namespace MUN_Digital_Speaker.Data
{
    public class MUN_Digital_SpeakerContext : DbContext
    {
        public MUN_Digital_SpeakerContext (DbContextOptions<MUN_Digital_SpeakerContext> options)
            : base(options)
        {
        }

        public DbSet<MUN_Digital_Speaker.Models.Delegation> Delegation { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delegation>()
                .OwnsMany(d => d.amendments);
        }
    }
}
