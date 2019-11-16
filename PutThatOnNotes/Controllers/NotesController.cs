using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.DataAccess;
using PutThatOnNotes.Models;
using System;

namespace PutThatOnNotes.Controllers
{
    public class NotesController : Controller
    {
        private readonly NotesRepository _notesRepository;

        public NotesController(DbContextOptions<PutThatOnNotesDbContext> options)
        {
            _notesRepository = new NotesRepository(options);
        }

        // GET: Notes
        public ActionResult Index()
        {
            var notes = _notesRepository.GetAll();
            return View(notes);
        }

        // GET: Notes/Details/5
        public ActionResult Details(int id)
        {
            var model = _notesRepository.Get(id);
            return View(model);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            var modelToCreate = new Note();
            return View(modelToCreate);
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  // TODO: What is that? Check if it is setuped and used at all. 
        public ActionResult Create(Note model)
        {
            try
            {
                TryUpdateModelAsync(model);

                if (ModelState.IsValid)
                {
                    var now = DateTime.Now;
                    model.CreationDate = now;
                    model.LastModificationDate = now;

                    // Clarify* here doing model.Id=0; , because _notesRepository.Save(model); does: if (model.Id == 0){Create(model);} else{Update(model);} .
                    model.Id = 0;

                    _notesRepository.Save(model);

                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            var modelToUpdate = _notesRepository.Get(id);
            return View(modelToUpdate);
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note model)
        {
            try
            {
                TryUpdateModelAsync(model);

                if (ModelState.IsValid)
                {
                    model.CreationDate = _notesRepository.Get(model.Id).CreationDate;
                    model.LastModificationDate = DateTime.Now;

                    _notesRepository.Save(model);

                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int id)
        {
            var modelToDelete = _notesRepository.Get(id);
            return View(modelToDelete);
        }

        // POST: Notes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Note model)
        {
            try
            {
                // Conversation: Check ModelState IsValid? - No, just check the Id? - No, even the Id is not needed (Id will be hidden). - Yes it is hidden, but value from html is not and can be violated/changed.

                // Conversation: this maybe triggers validation and also get the html form values even that I have change them here in the back-end (So, put it first Always!).
                //TryValidateModel(model);

                // Conversation: this should be cleared (for certain keys) if validating those keys is not needed. - NOPE! Just remove the props/keys from Views (hidden TOO !!!)
                //if (ModelState.IsValid) { }


                _notesRepository.Delete(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}