﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P3.Data;

namespace P3.Migrations
{
    [DbContext(typeof(P3Referential))]
    [Migration("20190128010341_MakeOrderLineForeignKeysCascaseDelete")]
    partial class MakeOrderLineForeignKeysCascaseDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("P3.Models.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("P3.Models.Entities.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .HasName("IX_OrderLineEntity_OrderEntityId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("P3.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Details");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("P3.Models.Entities.OrderLine", b =>
                {
                    b.HasOne("P3.Models.Entities.Order", "Order")
                        .WithMany("OrderLine")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderLineEntity_OrderEntity_OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P3.Models.Entities.Product", "Product")
                        .WithMany("OrderLine")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__OrderLine__Produ__52593CB8")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
