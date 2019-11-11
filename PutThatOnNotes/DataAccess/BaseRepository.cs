using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.Models;

namespace PutThatOnNotes.DataAccess
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected PutThatOnNotesDbContext _context;
        protected DbSet<T> DbSet { get; set; }

        public BaseRepository(DbContextOptions<PutThatOnNotesDbContext> options)
        {
            this._context = new PutThatOnNotesDbContext(options);
            this.DbSet = _context.Set<T>();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Delete(int id)
        {
            T model = Get(id);

            DbSet.Remove(model);

            _context.SaveChanges();
        }

        public void Save(T model)
        {
            if (model.Id == 0)
            {
                Create(model);
            }
            else
            {
                Update(model);
            }

            _context.SaveChanges();
        }

        private void Create(T model)
        {
            DbSet.Add(model);
        }

        private void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}
