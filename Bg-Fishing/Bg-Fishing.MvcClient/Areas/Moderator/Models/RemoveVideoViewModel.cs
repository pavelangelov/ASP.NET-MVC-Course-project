using Bg_Fishing.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class RemoveVideoViewModel
    {
        public IEnumerable<GalleryDTO>  Galleries { get; set; }

        [Display(Name = "Изберете категория от която да премахнете видео")]
        public IEnumerable<SelectListItem> GalleriesSelect
        {
            get
            {
                var defaultItem = Enumerable.Repeat<SelectListItem>(new SelectListItem{ Text = "-----" }, 1);
                if (this.Galleries != null)
                {
                    var galleriesList = this.Galleries.Select(g => new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.GalleryId
                    });

                    return defaultItem.Concat(galleriesList);
                }

                return defaultItem;
            }
        }
    }
}