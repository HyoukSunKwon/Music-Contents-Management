using System.Web.Mvc;
using Microsoft.Win32;

namespace Assignment5.Controllers
{
    public class MediaItemController : Controller
    {
        private Manager m = new Manager();

        [Route("Media/{id}")]
        public ActionResult GetMediaItem(int? id)
        {
            var mediaItem = m.MediaItemGetById(id.GetValueOrDefault());

            if (mediaItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(mediaItem.Content, mediaItem.ContentType);
            }
        }

        [Route("Media/{id}/Download")]
        public ActionResult DownloadMediaItem(int? id)
        {
            var mediaItem = m.MediaItemGetById(id.GetValueOrDefault());

            if (mediaItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Get file extension, assumes the web server is Microsoft IIS based
                // Must get the extension from the Registry (which is a key-value storage structure for configuration settings, for the Windows operating system and apps that opt to use the Registry)

                // Working variables
                string extension;
                RegistryKey key;
                object value;

                // Open the Registry, attempt to locate the key
                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mediaItem.ContentType, false);
                // Attempt to read the value of the key
                value = (key == null) ? null : key.GetValue("Extension", null);
                // Build/create the file extension string
                extension = (value == null) ? string.Empty : value.ToString();

                // Create a new Content-Disposition header
                var cd = new System.Net.Mime.ContentDisposition
                {
                    // Assemble the file name + extension
                    FileName = $"Document-{mediaItem.StringId}{extension}",
                    // Force the media item to be saved (not viewed)
                    Inline = false
                };
                // Add the header to the response
                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(mediaItem.Content, mediaItem.ContentType);
            }
        }
    }
}
