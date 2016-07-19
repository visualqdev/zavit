﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues;

namespace zavit.Web.Api.Controllers
{
    public class VenuesController : ApiController
    {
        const string PostRoute = "VenuesPost";

        readonly IVenueDtoService _venueDtoService;

        public VenuesController(IVenueDtoService venueDtoService)
        {
            _venueDtoService = venueDtoService;
        }

        [HttpPost]
        [Route("~/api/places/{placeid}/venues", Name = PostRoute)]
        public async Task<IHttpActionResult> Post(VenueDto venueDto, string placeId)
        {
            var venue = await _venueDtoService.AddVenue(venueDto, placeId);
            return CreatedAtRoute(CommonRoutes.Default, new { controller = "venues", id = venue.Id }, venue);
        }

        public VenueDto Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}