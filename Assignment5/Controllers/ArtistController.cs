using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Web.Mvc;
using Assignment5.Models.ViewModels.Album;
using Assignment5.Models.ViewModels.Artist;
using Assignment5.Models.ViewModels.MediaItem;

namespace Assignment5.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private Manager m = new Manager();

        // GET: Artist
        public ActionResult Index()
        { 
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var artist = m.ArtistGetById(id);

            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artist/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();

            var genreList = m.GenreGetAll();

            form.GenreList = new SelectList(genreList, "Name", "Name");

            return View(form);
        }

        // POST: Artist/Create
        [ValidateAntiForgeryToken]
        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ArtistAddFormViewModel newArtist)
        {
            if (ModelState.IsValid)
            {
                var newArtistAdded = m.ArtistAdd(newArtist);

                if (newArtistAdded == null)
                {
                    return View(newArtist);
                }
                else
                {
                    return RedirectToAction("Details", new { id = newArtistAdded.Id });
                }
            }

            return View(newArtist);
        }

        // GET: Artist/Edit/id
        [Authorize(Roles = "Executive")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArtistAddFormViewModel artistEdit = m.ArtistGetByIdForEdit(id);

            if (artistEdit == null)
            {
                return HttpNotFound();
            }

            var genreList = m.GenreGetAll();
            artistEdit.GenreList = new SelectList(genreList, "Name", "Name");

            return View(artistEdit);
        }

        // POST: Artist/Create
        [ValidateAntiForgeryToken]
        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Executive")]
        public ActionResult Edit(ArtistAddFormViewModel editArtist)
        {
            if (ModelState.IsValid)
            {
                var isArtistUpdated = m.ArtistEdit(editArtist);

                if (!isArtistUpdated)
                {
                    return View(editArtist);
                }
                else
                {
                    return RedirectToAction("Details", new { id = editArtist.Id });
                }
            }

            return View(editArtist);
        }

        // GET: Artist/Delete/5
        [Authorize(Roles = "Executive")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArtistBaseViewModel  artist = m.ArtistGetById(id);

            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Executive")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArtistBaseViewModel artistToBeDeleted = m.ArtistGetBaseInfoById(id.GetValueOrDefault());

            bool isDeleted = m.ArtistDelete(id.GetValueOrDefault());

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(artistToBeDeleted);
            }
        }


        [Authorize(Roles = "Coordinator")]
        [Route("Artist/{id}/addAlbum")]
        public ActionResult AddAlbum(int? id)
        {
            var form = new AlbumAddFormViewModel();

            var genreList = m.GenreGetAll();

            form.Artist = m.ArtistGetById(id);

            form.GenreList = new SelectList(genreList, "Name", "Name");
            return View(form);
        }

        [HttpPost, ValidateInput(false)]
        [Route("Artist/{id}/addAlbum")]
        [Authorize(Roles = "Coordinator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAlbum(AlbumAddFormViewModel newAlbum)
        {
            if (ModelState.IsValid)
            {
                AlbumAddFormViewModel newAlbumAdded = m.AlbumAdd(newAlbum);
                return RedirectToAction("Index");
            }
            else
            {
                var genreList = m.GenreGetAll();
                newAlbum.GenreList = new SelectList(genreList, "Name", "Name");

                return View(newAlbum);
            }
        }


        [Authorize(Roles = "Executive")]
        [Route("Artist/{id}/addMediaItem")]
        public ActionResult AddMediaItem(int? id)
        {
            var artist = m.ArtistGetById(id);

            var form = new MediaItemAddFormViewModel
            {
                ArtistId = artist.Id,
                ArtistName = artist.Name
            };

            return View(form);
        }

        [Authorize(Roles = "Executive")]
        [Route("Artist/{id}/addMediaItem")]
        [HttpPost]
        public ActionResult AddMediaItem(MediaItemAddViewModel newMedia)
        {
            if (!ModelState.IsValid)
            {
                return View(newMedia);
            }

            var addedMedia = m.MediaAdd(newMedia);

            if (addedMedia == null)
            {
                return View(newMedia);
            }
            else
            {
                return RedirectToAction("Details", new {id = newMedia.ArtistId});
            }
        }
    }
}
