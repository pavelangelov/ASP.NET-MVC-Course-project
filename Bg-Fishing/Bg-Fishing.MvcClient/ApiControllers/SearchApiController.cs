using System.Web.Http;

using Bg_Fishing.MvcClient.WebApiModels;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class SearchApiController : ApiController
    {
        private ILakeService lakeService;

        public SearchApiController(ILakeService lakeService)
        {
            Utils.Validator.ValidateForNull(lakeService, paramName: "lakeService");

            this.lakeService = lakeService;
        }
        
        [HttpPost]
        public IHttpActionResult Lakes(SearchModel model)
        {
            if (model.Name == null || model.Name.Length < 3)
            {
                return NotFound();
            }

            var lakes = this.lakeService.FindByLocation(model.Name);
            if (lakes == null)
            {
                return NotFound();
            }

            return Ok(lakes);
        }
    }
}
