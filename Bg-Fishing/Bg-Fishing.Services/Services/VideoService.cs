﻿using Bg_Fishing.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Data;
using Bg_Fishing.Utils;
using Bg_Fishing.DTOs;

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

        public string GetGalleryNameById(string galleryId)
        {
            var gallery =  this.dbContext.VideoGalleries.FirstOrDefault( g => g.Id == galleryId);

            return gallery != null ? gallery.Name : null;
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
