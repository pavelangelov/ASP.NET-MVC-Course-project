using Bg_Fishing.DTOs;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class AddVideoViewModel
    {
        private IEnumerable<GalleryDTO> galleryNames;

        [Required]
        [Display(Name = ViewModelsDisplayNames.VideoUrl_DisplayName)]
        public string VideoUrl { get; set; }

        [Required]
        [Display(Name = ViewModelsDisplayNames.VideoTitle_DisplayName)]
        public string VideoTitle { get; set; }

        [Display(Name = ViewModelsDisplayNames.CategoryId_DisplayName)]
        public string GalleryId { get; set; }

        [Display(Name = ViewModelsDisplayNames.NewGalleryName_DisplayName)]
        public string NewGalleryName { get; set; }

        public IEnumerable<SelectListItem> GalleryNames
        {
            get
            {
                if (this.galleryNames == null)
                {
                    return null;
                }

                var allNames = this.galleryNames.Select(n => new SelectListItem
                {
                    Value = n.GalleryId,
                    Text = n.Name
                });

                return this.DefaultGalleryName.Concat(allNames);
            }
        }

        public IEnumerable<SelectListItem> DefaultGalleryName
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "",
                    Text = "------"
                }, count: 1);
            }
        }

        public void SetNames(IEnumerable<GalleryDTO> names)
        {
            this.galleryNames = names;
        }
    }
}