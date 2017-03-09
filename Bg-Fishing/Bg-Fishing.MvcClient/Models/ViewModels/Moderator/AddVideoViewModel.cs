using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Models.ViewModels.Moderator
{
    public class AddVideoViewModel
    {
        private IEnumerable<string> galleryNames;

        [Required]
        [Display(Name = "Линк към видеото")]
        public string VideoUrl { get; set; }

        [Required]
        [Display(Name = "Заглавие на видеото")]
        public string VideoTitle { get; set; }

        [Display(Name = "Избери категория")]
        public string GalleryName { get; set; }

        [Display(Name = "Създай нова категотия")]
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
                    Value = n,
                    Text = n
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

        public void SetNames(IEnumerable<string> names)
        {
            this.galleryNames = names;
        }
    }
}