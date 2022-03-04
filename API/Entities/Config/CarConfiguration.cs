using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Entities.Config
{
    public class CarConfiguration //: //IEntityTypeConfiguration<Car>
    {
        // public void Configure(EntityTypeBuilder<Car> builder)
        // {
        //     builder.Property(p => p.Id).IsRequired();
        //     builder.Property(p => p.Brand).IsRequired();
        //     builder.Property(p => p.Model).IsRequired();
        //     builder.Property(p => p.RegistrationNumber).IsRequired();
        //     builder.Property(p => p.MeterStatus).IsRequired();
        //     builder.Property(p => p.VAT).IsRequired();
        //     builder.HasOne(p => p.AppUser).WithOne().OnDelete(DeleteBehavior.Cascade);
        //     builder.HasOne(c => c.CarInsurance).WithOne().OnDelete(DeleteBehavior.Cascade);
        //     builder.HasOne(t => t.TechnicalExamination).WithOne().OnDelete(DeleteBehavior.Cascade);
        //     builder.HasMany(s => s.Services).WithOne().OnDelete(DeleteBehavior.Cascade);
        // }
    }
}