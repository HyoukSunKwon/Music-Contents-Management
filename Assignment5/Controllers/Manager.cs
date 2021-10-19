using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;
using System.Security.Claims;
using System.Web.Mvc;
using Assignment5.Models.ViewModels.Album;
using Assignment5.Models.ViewModels.Artist;
using Assignment5.Models.ViewModels.MediaItem;
using Assignment5.Models.ViewModels.Track;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistAddFormViewModel, Artist>();
                cfg.CreateMap<Artist, ArtistAddFormViewModel>();
                cfg.CreateMap<Artist, ArtistBaseModel>();
                cfg.CreateMap<ArtistBaseModel, Artist>();

                cfg.CreateMap<Album, AlbumAddFormViewModel>();
                cfg.CreateMap<AlbumAddFormViewModel, Album>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseModel>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackClipViewModel>();
                cfg.CreateMap<Track, TrackClipEditViewModel>();
                cfg.CreateMap<Track, TrackClipEditFormViewModel>();
                cfg.CreateMap<Track, TrackAddFormViewModel>();

                cfg.CreateMap<MediaItemAddViewModel, MediaItem>();
                cfg.CreateMap<MediaItem, MediaItemAddViewModel>();
                cfg.CreateMap<MediaItem, MediaItemContentViewModel>();
                cfg.CreateMap<MediaItem, MediaItemViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()






        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "http://www.billboard.com/files/styles/article_main_image/public/media/Adele-2015-close-up-XL_Columbia-billboard-650.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(g => g.Name));
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistWithDetailViewModel ArtistGetById(int? id)
        {
            var artist = ds.Artists
                                .Include(a => a.MediaItems)
                                .Include(i => i.Albums)
                                .FirstOrDefault(a => a.Id == id);

            return artist == null ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(artist);
        }

        public ArtistBaseViewModel ArtistGetBaseInfoById(int? id)
        {
            var artist = ds.Artists
                .FirstOrDefault(a => a.Id == id);

            return artist == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        public ArtistAddFormViewModel ArtistGetByIdForEdit(int? id)
        {
            var artist = ds.Artists.FirstOrDefault(a => a.Id == id);
            return artist == null ? null : mapper.Map<Artist, ArtistAddFormViewModel>(artist);
        }

        public IEnumerable<ApplicationUser> UserGetExecutives()
        {
            return ds.Users;
        }

        public ArtistAddFormViewModel ArtistAdd(ArtistAddFormViewModel newArtist)
        {
            var addedArtist = ds.Artists.Add(mapper.Map<ArtistAddFormViewModel, Artist>(newArtist));
            addedArtist.Executive = User.Name;
            ds.SaveChanges();

            return addedArtist == null ? null : mapper.Map<Artist, ArtistAddFormViewModel>(addedArtist);
        }

        public bool ArtistEdit(ArtistAddFormViewModel artist)
        {
            bool updated = false;
            var artistfound = ds.Artists.Find(artist.Id);

            if (artistfound != null)
            {
                ds.Entry(artistfound).CurrentValues.SetValues(artist);
                var saved = ds.SaveChanges();

                updated = saved > 0;
            }

            return updated;
        }

        public bool ArtistDelete(int id)
        {
            var artistToDelete = ds.Artists.Find(id);

            if (artistToDelete == null)
            {
                return false;
            }
            else
            {
                ds.Artists.Remove(artistToDelete);
                var count = ds.SaveChanges();

                return count > 0;
            }
        }

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(a => a.Name));
        }

        public AlbumBaseViewModel AlbumGetById(int? id)
        {
            var album = ds.Albums.FirstOrDefault(a => a.Id == id);

            return mapper.Map<Album, AlbumBaseViewModel>(album);
        }

        public AlbumAddFormViewModel AlbumGetByIdForEdit(int? id)
        {
            var album = ds.Albums.FirstOrDefault(a => a.Id == id);

            return mapper.Map<Album, AlbumAddFormViewModel>(album);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(t => t.Genre)
                .ThenBy(t => t.Name));
        }

        public AlbumAddFormViewModel AlbumAdd(AlbumAddFormViewModel newAlbum)
        {
            var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddFormViewModel, Album>(newAlbum));

            addedAlbum.Artists.Add(ds.Artists.Find(newAlbum.Artist.Id));
            addedAlbum.Coordinator = User.Name;

            ds.SaveChanges();

            return addedAlbum == null ? null : mapper.Map<Album, AlbumAddFormViewModel>(addedAlbum);
        }

        public bool AlbumEdit(AlbumAddFormViewModel editAlbum)
        {
            bool updated = false;
            var albumFound = ds.Albums.Find(editAlbum.Id);

            if (albumFound != null)
            {
                ds.Entry(albumFound).CurrentValues.SetValues(editAlbum);
                var saved = ds.SaveChanges();

                updated = saved > 0;
            }

            return updated;
        }

        public bool AlbumDelete(int id)
        {
            var albumToBeDeleted = ds.Albums.Find(id);

            if (albumToBeDeleted == null)
            {
                return false;
            }
            else
            {
                ds.Albums.Remove(albumToBeDeleted);
                var count = ds.SaveChanges();

                return count > 0;
            }
        }

        public TrackWithDetailViewModel TrackGetById(int? id)
        {
            var track = ds.Tracks.Include(i => i.Albums).FirstOrDefault(a => a.Id == id);

            return track == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(track);
        }


        public TrackClipEditFormViewModel TrackGetByIdForEdit(int? id)
        {
            var track = ds.Tracks.Include(i => i.Albums).FirstOrDefault(a => a.Id == id);

            return track == null ? null : mapper.Map<Track, TrackClipEditFormViewModel>(track);
        }

        public TrackAddFormViewModel TrackAdd(TrackAddFormViewModel newTrack)
        {
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddFormViewModel, Track>(newTrack));

            var album = ds.Albums.Find(newTrack.Album.Id);
            addedTrack.Albums.Add(album);

            ds.SaveChanges();

            return addedTrack == null ? null : mapper.Map<Track, TrackAddFormViewModel>(addedTrack);
        }

        public TrackAddFormViewModel TrackAddWithClip(TrackAddViewModel newTrack)
        {
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

            byte[] clipBytes = new byte[newTrack.SampleClip.ContentLength];
            newTrack.SampleClip.InputStream.Read(clipBytes, 0, newTrack.SampleClip.ContentLength);

            addedTrack.ClipContent = clipBytes;
            addedTrack.ClipContentType = newTrack.SampleClip.ContentType;

            var album = ds.Albums.Find(newTrack.Album.Id);
            addedTrack.Albums.Add(album);
            addedTrack.Clerk = User.Name;

            ds.SaveChanges();

            return addedTrack == null ? null : mapper.Map<Track, TrackAddFormViewModel>(addedTrack);
        }

        public bool TrackEdit(TrackClipEditViewModel editTrack)
        {
            bool updated = false;
            var trackFound = ds.Tracks.Find(editTrack.Id);

            if (trackFound != null)
            {
                byte[] clipBytes = new byte[editTrack.SampleClip.ContentLength];
                editTrack.SampleClip.InputStream.Read(clipBytes, 0, editTrack.SampleClip.ContentLength);

                trackFound.ClipContent = clipBytes;
                trackFound.ClipContentType = editTrack.SampleClip.ContentType;

                var saved = ds.SaveChanges();

                updated = saved > 0;
            }

            return updated;
        }

        public bool TrackDelete(int id)
        {
            var trackToBeDeleted = ds.Tracks.Find(id);

            if (trackToBeDeleted == null)
            {
                return false;
            }
            else
            {
                ds.Tracks.Remove(trackToBeDeleted);
                var count = ds.SaveChanges();

                return count > 0;
            }
        }

        public TrackClipViewModel TrackClipGetByTrackId(int? id)
        {
            var track = ds.Tracks.Find(id);

            return (track == null) ? null : mapper.Map<Track, TrackClipViewModel>(track);
        }

        public MediaItemAddViewModel MediaAdd(MediaItemAddViewModel newMedia)
        {
            var addedMedia = new MediaItem();
            ds.MediaItems.Add(addedMedia);

            byte[] mediaBytes = new byte[newMedia.ContentInfo.ContentLength];

            newMedia.ContentInfo.InputStream.Read(mediaBytes, 0, newMedia.ContentInfo.ContentLength);

            addedMedia.Content = mediaBytes;
            addedMedia.ContentType = newMedia.ContentInfo.ContentType;
            addedMedia.Caption = newMedia.Caption;

            var artist = ds.Artists.Find(newMedia.ArtistId);
            addedMedia.Artist = artist;


            ds.SaveChanges();

            return addedMedia == null ? null : mapper.Map<MediaItem, MediaItemAddViewModel>(addedMedia);
        }

        public MediaItemViewModel MediaItemGetById(int id)
        {
            var mediaItem = ds.MediaItems.Find(id);

            return (mediaItem == null) ? null : mapper.Map<MediaItem, MediaItemViewModel>(mediaItem);
        }


    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}