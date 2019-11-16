using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.Models;
using System.Collections.Generic;
using System.Linq;

namespace PutThatOnNotes.DataAccess
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly PutThatOnNotesDbContext _putThatOnNotesDbContext;
        protected DbSet<T> DbSet { get; set; }

        public BaseRepository(DbContextOptions<PutThatOnNotesDbContext> options)
        {
            _putThatOnNotesDbContext = new PutThatOnNotesDbContext(options);
            DbSet = _putThatOnNotesDbContext.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Delete(int id)
        {
            T model = Get(id);

            DbSet.Remove(model);

            _putThatOnNotesDbContext.SaveChanges();
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

            _putThatOnNotesDbContext.SaveChanges();
        }

        private void Create(T model)
        {
            DbSet.Add(model);
        }

        private void Update(T model)
        {
            _putThatOnNotesDbContext.Entry(model).State = EntityState.Modified;
        }
    }
}
