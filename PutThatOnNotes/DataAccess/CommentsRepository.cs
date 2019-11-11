using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.Models;

namespace PutThatOnNotes.DataAccess
{
    public class CommentsRepository : BaseRepository<Comment>
    {
        public CommentsRepository(DbContextOptions<PutThatOnNotesDbContext> options) : base(options)
        {
        }
    }
}
