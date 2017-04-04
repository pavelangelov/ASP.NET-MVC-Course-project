using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Utils;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.MvcClient.Models.ViewModels.Common
{
    public class VideoGalleriesViewModel
    {
        private IEnumerable<VideoGalleryModel> galleries;

        public string GalleryName { get; set; }

        [Display(Name = ViewModelsDisplayNames.GalleriesDisplayName)]
        public IEnumerable<SelectListItem> Galleries
        {
            get
            {
                if (this.galleries == null)
                {
                    return this.Default;
                }

                var all = this.galleries.Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id
                });

                return this.Default.Concat(all);
            }
        }

        public IEnumerable<SelectListItem> Default
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem { Value = "", Text = "-----" }, count: 1);
            }
        }

        public void SetGalleries(IEnumerable<VideoGalleryModel> galleriesDTO)
        {
            this.galleries = galleriesDTO;
        }
    }
}