using System.Net;
using System.Web.Mvc;
using Assignment5.Models;
using Assignment5.Models.ViewModels.Album;
using Assignment5.Models.ViewModels.Track;

namespace Assignment5.Controllers
{

    [Authorize]
    public class AlbumController : Controller
    {
        private Manager m = new Manager();

        // GET: Album
        public ActionResult Index()
        {

            return View(m.AlbumGetAll());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var album = m.AlbumGetById(id);
            
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Artist/Edit/id
        [Authorize(Roles = "Coordinator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AlbumAddFormViewModel albumEdit = m.AlbumGetByIdForEdit(id);

            if (albumEdit == null)
            {
                return HttpNotFound();
            }

            var genreList = m.GenreGetAll();
            albumEdit.GenreList = new SelectList(genreList, "Name", "Name");

            return View(albumEdit);
        }

        // POST: Album/Create
        [ValidateAntiForgeryToken]
        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Coordinator")]
        public ActionResult Edit(AlbumAddFormViewModel editAlbum)
        {
            if (ModelState.IsValid)
            {
                var isAlbumUpdated = m.AlbumEdit(editAlbum);

                if (!isAlbumUpdated)
                {
                    return View(editAlbum);
                }
                else
                {
                    return RedirectToAction("Details", new { id = editAlbum.Id });
                }
            }

            return View(editAlbum);
        }

        // GET: Album/Delete/5
        [Authorize(Roles = "Coordinator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AlbumBaseViewModel album = m.AlbumGetById(id);

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinator")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AlbumBaseViewModel albumToBeDeleted = m.AlbumGetById(id.GetValueOrDefault());

            bool isDeleted = m.AlbumDelete(id.GetValueOrDefault());

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(albumToBeDeleted);
            }
        }

        [Authorize(Roles = "Clerk")]
        [HttpGet]
        [Route("Album/{id}/addTrack")]
        public ActionResult AddTrack(int? id)
        {
            var form = new TrackAddFormViewModel();

            var genreList = m.GenreGetAll();
            form.Album = m.AlbumGetById(id);

            form.GenreList = new SelectList(genreList, "Name", "Name");

            return View(form);
        }

        [HttpPost]
        [Authorize(Roles = "Clerk")]
        [Route("Album/{id}/addTrack")]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrack(TrackAddViewModel newTrack)
        {
            if (ModelState.IsValid)
            {
                TrackAddFormViewModel newTrackAdded = m.TrackAddWithClip(newTrack);
                return RedirectToAction("Index");
            }
            else
            {
                var genreList = m.GenreGetAll();

                newTrack.GenreList = new SelectList(genreList, "Name", "Name");

                return View(newTrack);
            }
            
        }
    }
}
