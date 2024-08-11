using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructuer.Configration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel { Id = 1, Name = "Hotel A" },
                new Hotel { Id = 2, Name = "Hotel B" },
                new Hotel { Id = 3, Name = "Hotel C" },
                new Hotel { Id = 4, Name = "Hotel D" },
                new Hotel { Id = 5, Name = "Hotel E" }
            );
        }

    }
}
