﻿using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Venues
{
    public interface IVenueService
    {
        Venue CreateVenue(INewVenue newVenue);
    }
}