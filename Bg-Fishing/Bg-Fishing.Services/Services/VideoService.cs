﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.DTOs;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Services
{
    public class VideoService : IVideoService
    {
        private IDatabaseContext dbContext;

        public VideoService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, "dbContext");

            this.dbContext = dbContext;
        }

        public VideoDTO GetVideoById(string id)
        {
            var video = this.dbContext.Videos.FirstOrDefault(v => v.Id == id);

            if (video != null)
            {
                return new VideoDTO
                {
                    Id = video.Id,
                    Title = video.Title,
                    Url = video.Url
                };
            }

            return null;
        }

        public string GetGalleryNameById(string galleryId)
        {
            var gallery =  this.dbContext.VideoGalleries.FirstOrDefault( g => g.Id == galleryId);

            return gallery != null ? gallery.Name : null;
        }

        public IEnumerable<VideoDTO> GetVideosFromGallery(string galleryId)
        {
            var gallery = this.dbContext.VideoGalleries.Include(g => g.Videos).FirstOrDefault(g => g.Id == galleryId);

            if (gallery != null)
            {
                return gallery.Videos.Select(v => new VideoDTO
                {
                    Title = v.Title,
                    Url = v.Url,
                    Id = v.Id
                });
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<GalleryDTO> GetAll()
        {
            var galleries = this.dbContext.VideoGalleries;
            if (galleries != null)
            {
                var galleryDTOs = galleries.Select(g => new GalleryDTO
                {
                    GalleryId = g.Id,
                    Name = g.Name
                });

                return galleryDTOs;
            }

            return null;
        }

        public void AddVideoToGallery(string galleryName, Video video)
        {
            var gallery = this.dbContext.VideoGalleries.FirstOrDefault(g => g.Name == galleryName);

            if (gallery != null)
            {
                gallery.Videos.Add(video);
            }
            else
            {
                gallery = new VideoGallery(galleryName);
                gallery.Videos.Add(video);
                this.dbContext.VideoGalleries.Add(gallery);
            }
        }

        public bool RemoveVideoFromGallery(string galleryName, string videoId)
        {
            var gallery = this.dbContext.VideoGalleries.FirstOrDefault(g => g.Name == galleryName);
            if (gallery == null)
            {
                return false;
            }

            var video = gallery.Videos.FirstOrDefault(v => v.Id == videoId);
            if (video != null)
            {
                gallery.Videos.Remove(video);
                return true;
            }

            return false;
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
