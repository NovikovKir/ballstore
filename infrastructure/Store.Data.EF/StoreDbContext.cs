using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    public class StoreDbContext : DbContext
    {
        public DbSet<BallDto> Balls { get; set; }

        public DbSet<OrderDto> Orders { get; set; }

        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options) 
        {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildBalls(modelBuilder);
            BuildOrders(modelBuilder);
            BuildOrderItems(modelBuilder);
        }

        private static void BuildBalls(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BallDto>(action =>
            {
                action.Property(dto => dto.Brand)
                      .IsRequired(false);
                action.Property(dto => dto.Model)
                      .IsRequired(false);
                action.Property(dto => dto.Name)
                      .IsRequired(false);
                action.Property(dto => dto.Price)
                      .HasColumnType("money");
                action.HasData(
                    new BallDto
                    {
                        Id = 1, 
                        Name = "Волейбольный мяч", 
                        Brand = "DEMIX", 
                        Model = "VLPU440 Super Touch", 
                        Description = "Волейбольный мяч Demix для тренировок и игр в зале и на открытых площадках.", 
                        Price = 1999m,
                    },
                    new BallDto
                    {
                        Id = 2,
                        Name = "Волейбольный мяч",
                        Brand = "TORRES",
                        Model = "BM850 V42325",
                        Description = "Волейбольный мяч BM850 — это классическая модель волейбольного мяча, самая универсальная из всего модельного ряда мячей Torres.",
                        Price = 3150m,
                    },
                    new BallDto
                    {
                        Id = 3,
                        Name = "Волейбольный мяч",
                        Brand = "MIKASA",
                        Model = "V330W",
                        Description = "Волейбольный мяч для тренировок и любительских соревнований от Mikasa.",
                        Price = 7999m
                    },
                    new BallDto
                    {
                        Id = 4,
                        Name = "Волейбольный мяч",
                        Brand = "MIKASA",
                        Model = "V200W",
                        Description = "Официальный игровой мяч Mikasa предназначен для проведения соревнований самого высокого уровня.",
                        Price = 14999m
                    }
                );
            });
        }

        private static void BuildOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                      .HasMaxLength(20)
                      .IsRequired(false);
                action.Property(dto => dto.DeliveryUniqueCode)
                      .HasMaxLength(40)
                      .IsRequired(false);
                action.Property(dto => dto.DeliveryPrice)
                      .HasColumnType("money");
                action.Property(dto => dto.PaymentServiceName)
                      .HasMaxLength(40)
                      .IsRequired(false);

                action.Property(dto => dto.DeliveryParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);

                action.Property(dto => dto.PaymentParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);
            });
        }

        private static readonly ValueComparer DictionaryComparer =
            new ValueComparer<Dictionary<string, string>>(
                (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
                dictionary => dictionary.Aggregate(
                    0,
                    (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
                )
            );

        private void BuildOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Price)
                      .HasColumnType("money");
                action.HasOne(dto => dto.Order)
                      .WithMany(dto => dto.Items)
                      .IsRequired(false);
            });
        }
    }
}
