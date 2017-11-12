using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Test.Models;
using WebApi_Test.Repository;

namespace WebApi_Test.Controllers
{
    public class BeerController : ApiController
    {
        private BeerRepository beerRepository;

        public BeerController()
        {
            this.beerRepository = new BeerRepository();

        }

        [AcceptVerbs("GET", "SEARCH")]
        public IHttpActionResult Search([FromBody]string query)
        {
            if (String.IsNullOrEmpty(query))
                return Content(HttpStatusCode.BadRequest, "No search parameters");

            IEnumerable<Beer> beers = beerRepository.GetByName(query);

            if (beers != null && beers.Any())
                return Ok(beers);
            else
                return NotFound();

        }

        [AcceptVerbs("GET", "SEARCH")]
        public IHttpActionResult GetAll()
        {
            return Ok(beerRepository.GetAll());
        }

        [AcceptVerbs("POST", "PUT", "PATCH")]
        public IHttpActionResult Add([FromBody]Beer beer)
        {
            if (Beer.IsValid(beer) && beerRepository.Add(beer))
                return Ok();
            else
                return Content(HttpStatusCode.BadRequest, "");

        }

        [AcceptVerbs("POST", "PUT", "PATCH")]
        public IHttpActionResult Update([FromBody]Beer beer)
        {
            if (Beer.IsValid(beer))
            {
                Beer updatedBeer = beerRepository.Update(beer);

                if (updatedBeer != null)
                    return Ok(updatedBeer);
                else
                    return Content(HttpStatusCode.BadRequest, "");
            }
            else
                return Content(HttpStatusCode.BadRequest, "");
        }
    }
}
