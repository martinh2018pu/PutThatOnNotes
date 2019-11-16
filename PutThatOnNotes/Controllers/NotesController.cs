using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PutThatOnNotes.DataAccess;
using PutThatOnNotes.Models;
using System;

namespace PutThatOnNotes.Controllers
{
    public class NotesController : Controller
    {
        private static int _currentNoteId;
        private static DateTime _currentNoteCreationDate;
        private static DateTime _currentNoteLastModificationDate;

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

            RememberOriginalValuesOfReadonlyNoteProps(model);

            return View(model);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            var modelToCreate = new Note();

            RememberOriginalValuesOfReadonlyNoteProps(modelToCreate);

            return View(modelToCreate);
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  // TODO: Vas is das? Check if it is setuped and used at all. 
        public ActionResult Create(Note model)
        {
            try
            {
                TryUpdateModelAsync(model);

                SetOriginalValuesOfReadonlyNoteModelProps(model);
                MarkValidValidationStateForReadonlyNoteProps(model);

                if (ModelState.IsValid)
                {
                    var now = DateTime.Now;
                    model.CreationDate = now;
                    model.LastModificationDate = now;

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

            RememberOriginalValuesOfReadonlyNoteProps(modelToUpdate);

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

                SetOriginalValuesOfReadonlyNoteModelProps(model);
                MarkValidValidationStateForReadonlyNoteProps(model);

                if (ModelState.IsValid)
                {
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
            
            RememberOriginalValuesOfReadonlyNoteProps(modelToDelete);

            return View(modelToDelete);
        }

        // POST: Notes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Note model)
        {
            try
            {
                // Check ModelState IsValid? Conversation: - No, just check the Id? - No, even the Id is not needed (Id will be hidden). - Yes it is hidden, but value from html is not and can be violated/changed.

                // this maybe triggers validation and also get the html form values even that I have change them here in the back-end (So, put it first Always!).
                //TryValidateModel(model);

                // this should be cleared (for certain keys) if validating those keys is not needed.
                //if (ModelState.IsValid) { }


                SetOriginalValuesOfReadonlyNoteModelProps(model);

                _notesRepository.Delete(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        /// <summary>
        /// Marks as valid validation state of note Id/CreationDate/LastModificationDate keys, in order to handle not legal changes to them made in value attribute of hidden html tag. 
        /// </summary>
        /// <param name="model">Current Note entity model.</param>
        private void MarkValidValidationStateForReadonlyNoteProps(Note model)
        {
            ModelState.ClearValidationState(nameof(model.Id));
            ModelState.ClearValidationState(nameof(model.CreationDate));
            ModelState.ClearValidationState(nameof(model.LastModificationDate));

            ModelState.MarkFieldValid(nameof(model.Id));
            ModelState.MarkFieldValid(nameof(model.CreationDate));
            ModelState.MarkFieldValid(nameof(model.LastModificationDate));
        }

        /// <summary>
        /// RememberValuesOfReadonlyNoteProps in static fields in order to handles not legal changing of note Id/CreationDate in value attribute of hidden html tag. 
        /// 
        /// </summary>
        /// <param name="model">Current Note entity model.</param>
        private void RememberOriginalValuesOfReadonlyNoteProps(Note model)
        {
            _currentNoteId = model.Id;
            _currentNoteCreationDate = model.CreationDate;
            // not needed to remember this for now (but left it for consistency). Now we only are assigning it to DateTime.Now and we don't use its past (original) value. 
            _currentNoteLastModificationDate = model.LastModificationDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Current Note entity model.</param>
        private void SetOriginalValuesOfReadonlyNoteModelProps(Note model)
        {
            model.Id = _currentNoteId;
            model.CreationDate = _currentNoteCreationDate;
            // not needed to remember this for now (but left it for consistency). Now we only are assigning it to DateTime.Now and we don't use its past (original) value. 
            model.LastModificationDate = _currentNoteLastModificationDate;
        }
    }
}