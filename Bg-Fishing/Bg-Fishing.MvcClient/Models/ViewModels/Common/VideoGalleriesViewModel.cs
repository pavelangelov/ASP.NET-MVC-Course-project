﻿using Bg_Fishing.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Models.ViewModels.Common
{
    public class VideoGalleriesViewModel
    {
        private IEnumerable<GalleryDTO> galleries;

        public string GalleryName { get; set; }

        [Display(Name = "Избери галерия")]
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
                    Value = g.GalleryId
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

        public void SetGalleries(IEnumerable<GalleryDTO> galleriesDTO)
        {
            this.galleries = galleriesDTO;
        }
    }
}