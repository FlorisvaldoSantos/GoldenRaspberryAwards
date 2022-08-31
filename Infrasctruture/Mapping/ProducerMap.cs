using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctruture.Mapping
{
    public class ProducerMap : IEntityTypeConfiguration<ProducerDTO>
    {
        public void Configure(EntityTypeBuilder<ProducerDTO> builder)
        {
            builder.ToTable("Producer");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Producer)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Producer")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Interval)
               .IsRequired()
               .HasColumnName("Interval")
               .HasColumnType("INTEGER");

            builder.Property(prop => prop.PreviousWin)
                .IsRequired()
                .HasColumnName("PreviousWin")
                .HasColumnType("INTEGER");

            builder.Property(prop => prop.FollowingWin)
                .IsRequired()
                .HasColumnName("FollowingWin")
                .HasColumnType("INTEGER");

            builder.Property(prop => prop.FollowingWin)
                .IsRequired()
                .HasColumnName("CreatAt")
                .HasColumnType("DateTime");
        }
    }
}
