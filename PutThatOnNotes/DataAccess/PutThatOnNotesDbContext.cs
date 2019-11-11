using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PutThatOnNotes.DataAccess
{
    public class PutThatOnNotesDbContext : DbContext
    {
        public PutThatOnNotesDbContext(DbContextOptions<PutThatOnNotesDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }


        //
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        //
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
