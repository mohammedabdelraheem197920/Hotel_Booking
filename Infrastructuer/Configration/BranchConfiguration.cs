﻿using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructuer.Configration
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasData(
                new Branch { Id = 1, Country = "Country1", State = "State1", City = "City1", Street = "Street1", PostalCode = "12345", HotelID = 1 },
                new Branch { Id = 2, Country = "Country1", State = "State1", City = "City2", Street = "Street2", PostalCode = "12346", HotelID = 1 },
                new Branch { Id = 3, Country = "Country1", State = "State1", City = "City3", Street = "Street3", PostalCode = "12347", HotelID = 1 },

                new Branch { Id = 4, Country = "Country2", State = "State2", City = "City1", Street = "Street4", PostalCode = "22345", HotelID = 2 },
                new Branch { Id = 5, Country = "Country2", State = "State2", City = "City2", Street = "Street5", PostalCode = "22346", HotelID = 2 },
                new Branch { Id = 6, Country = "Country2", State = "State2", City = "City3", Street = "Street6", PostalCode = "22347", HotelID = 2 },

                new Branch { Id = 7, Country = "Country3", State = "State3", City = "City1", Street = "Street7", PostalCode = "32345", HotelID = 3 },
                new Branch { Id = 8, Country = "Country3", State = "State3", City = "City2", Street = "Street8", PostalCode = "32346", HotelID = 3 },
                new Branch { Id = 9, Country = "Country3", State = "State3", City = "City3", Street = "Street9", PostalCode = "32347", HotelID = 3 },

                new Branch { Id = 10, Country = "Country4", State = "State4", City = "City1", Street = "Street10", PostalCode = "42345", HotelID = 4 },
                new Branch { Id = 11, Country = "Country4", State = "State4", City = "City2", Street = "Street11", PostalCode = "42346", HotelID = 4 },
                new Branch { Id = 12, Country = "Country4", State = "State4", City = "City3", Street = "Street12", PostalCode = "42347", HotelID = 4 },

                new Branch { Id = 13, Country = "Country5", State = "State5", City = "City1", Street = "Street13", PostalCode = "52345", HotelID = 5 },
                new Branch { Id = 14, Country = "Country5", State = "State5", City = "City2", Street = "Street14", PostalCode = "52346", HotelID = 5 },
                new Branch { Id = 15, Country = "Country5", State = "State5", City = "City3", Street = "Street15", PostalCode = "52347", HotelID = 5 }
            );
        }
    }
}
