using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class RemoveVideoViewModel
    {
        public IEnumerable<VideoGalleryModel>  Galleries { get; set; }

        [Display(Name = ViewModelsDisplayNames.GalleriesSelect_DisplayName)]
        public IEnumerable<SelectListItem> GalleriesSelect
        {
            get
            {
                var defaultItem = Enumerable.Repeat<SelectListItem>(new SelectListItem{ Text = "-----", Value = "" }, 1);
                if (this.Galleries != null)
                {
                    var galleriesList = this.Galleries.Select(g => new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.Id
                    });

                    return defaultItem.Concat(galleriesList);
                }

                return defaultItem;
            }
        }
    }
}