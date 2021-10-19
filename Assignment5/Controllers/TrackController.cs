using System.Net;
using System.Web.Mvc;
using Assignment5.Models.ViewModels.Track;

namespace Assignment5.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private Manager m = new Manager();
        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var track = m.TrackGetById(id);

            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        [Route("clip/{id}")]
        public ActionResult GetClip(int? id)
        {
            var track = m.TrackClipGetByTrackId(id.GetValueOrDefault());

            if (track == null)
            {
                return HttpNotFound();
            }
            else
            {

                return (track.ClipContentType != null && track.ClipContent != null) 
                            ? File(track.ClipContent, track.ClipContentType) 
                            : null;
            }
        }


        // GET: Track/Edit/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrackClipEditFormViewModel trackEdit = m.TrackGetByIdForEdit(id);

            if (trackEdit == null)
            {
                return HttpNotFound();
            }

            return View(trackEdit);
        }

        // POST: Track/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Clerk")]
        public ActionResult Edit(TrackClipEditViewModel editTrack)
        {

            if (ModelState.IsValid)
            {
                var isTrackUpdated = m.TrackEdit(editTrack);

                if (!isTrackUpdated)
                {
                    return View(editTrack);
                }
                else
                {
                    return RedirectToAction("Details", new { id = editTrack.Id });
                }
            }

            return View(editTrack);
        }

        // GET: Track/Delete/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrackBaseViewModel track = m.TrackGetById(id);

            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Clerk")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrackBaseViewModel trackToBeDeleted = m.TrackGetById(id.GetValueOrDefault());

            bool isDeleted = m.TrackDelete(id.GetValueOrDefault());

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(trackToBeDeleted);
            }
        }
    }
}
