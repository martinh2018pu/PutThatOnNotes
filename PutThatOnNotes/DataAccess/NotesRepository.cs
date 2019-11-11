using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.Models;

namespace PutThatOnNotes.DataAccess
{
    public class NotesRepository : BaseRepository<Note>
    {
        public NotesRepository(DbContextOptions<PutThatOnNotesDbContext> options) : base(options)
        {
        }
    }
}
