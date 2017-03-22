using Bg_Fishing.DTOs.LakeDTOs;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class SearchController : ApiController
    {
        private ILakeService lakeService;

        public SearchController(ILakeService lakeService)
        {
            Validator.ValidateForNull(lakeService, paramName: "lakeService");

            this.lakeService = lakeService;
        }

        public IEnumerable<LakeDTO> Get()
        {
            var lakes = this.lakeService.GetAll();

            return lakes;
        }
        
        [HttpPost]
        public IHttpActionResult Lakes(GetResult model)
        {
            var lakes = this.lakeService.FindByLocation(model.Name);
            if (lakes == null)
            {
                return NotFound();
            }

            return Ok(lakes);
        }
    }

    public class GetResult
    {
        public string Name { get; set; }
    }
}
