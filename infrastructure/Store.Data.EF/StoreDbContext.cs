using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Data.EF
{
    public class StoreDbContext : DbContext
    {
        private static readonly ValueComparer DictionaryComparer =
                      new ValueComparer<Dictionary<string, string>>(
                          (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
                           dictionary => dictionary.Aggregate(
                               0,
                               (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
                            )
                      );

        public DbSet<BookDto> Books { get; set; }

        public DbSet<OrderDto> Orders { get; set; }

        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildBooks(modelBuilder);
            BuildOrers(modelBuilder);
            BuildOrderItems(modelBuilder);
        }

        private static void BuildOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Price)
                      .HasColumnName("money");

                action.HasOne(dto => dto.Order)
                      .WithMany(dto => dto.Items)
                      .IsRequired();
            });
        }

        private static void BuildOrers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                      .HasMaxLength(20);

                action.Property(dto => dto.DeliveryUniqueCode)
                      .HasMaxLength(40);

                action.Property(dto => dto.DeliveryPrice)
                      .HasColumnType("money");

                action.Property(dto => dto.PaymentServiceName)
                      .HasMaxLength(40);

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

        private static void BuildBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDto>(action =>
            {
                action.Property(dto => dto.Isbn)
                      .HasMaxLength(17)
                      .IsRequired();

                action.Property(dto => dto.Title)
                      .IsRequired();

                action.Property(dto => dto.Price)
                      .HasColumnType("money");

                action.HasData(
                       new BookDto
                       {
                           Id = 1,
                           Isbn = "ISBN01235437898",
                           Author = "D. Knuth",
                           Title = "Art Of Programming",
                           Description = "This volume begins with...",
                           Price = 7.19m
                       },

                       new BookDto
                       {

                           Id = 2,
                           Isbn = "ISBN01235437854",
                           Author = "M. Fowler",
                           Title = "Refactoring",
                           Description = "As the application of object...",
                           Price = 12.45m
                       },

                       new BookDto
                       {

                           Id = 3,
                           Isbn = "ISBN01235439863",
                           Author = "B. W. Kernighan, D. M. Ritchie",
                           Title = "C Programming Language",
                           Description = "Known as the bible of C...",
                           Price = 14.98m
                       });
            });
        }
    }
}
