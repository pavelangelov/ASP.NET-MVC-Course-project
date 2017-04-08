namespace Bg_Fishing.Data
{
    using Models;
    using Models.Comments;
    using Models.Galleries;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FishingContext : DbContext, IDatabaseContext
    {
        public FishingContext()
            : base("DefaultConnection")
        {
        }
        
        public virtual IDbSet<Fish> Fish { get; set; }
        public virtual IDbSet<Lake> Lakes { get; set; }
        public virtual IDbSet<Location> Locations { get; set; }
        public virtual IDbSet<Image> Images { get; set; }
        public virtual IDbSet<Video> Videos { get; set; }
        public virtual IDbSet<ImageGallery> ImageGalleries { get; set; }
        public virtual IDbSet<VideoGallery> VideoGalleries { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<News> News { get; set; }
        public virtual IDbSet<NewsComment> NewsComments { get; set; }
        public virtual IDbSet<InnerComment> InnerComments { get; set; }

        public int Save()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}