﻿// <auto-generated />
using System;
using ConnectDataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConnectDataBase.Migrations
{
    [DbContext(typeof(UsersDB))]
    partial class UsersDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConnectDataBase.Employers", b =>
                {
                    b.Property<int>("Employer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Employer_last_name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Employer_name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Employment_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Username")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Employer_id");

                    b.ToTable("Employers");

                    b.HasData(
                        new
                        {
                            Employer_id = 1,
                            Employer_last_name = "Heliak",
                            Employer_name = "Mikolaj",
                            Employment_date = new DateTime(2021, 2, 18, 21, 20, 48, 309, DateTimeKind.Local).AddTicks(1115),
                            Password = "123",
                            Username = "menager"
                        });
                });

            modelBuilder.Entity("ConnectDataBase.Items", b =>
                {
                    b.Property<int>("Item_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Item_Count")
                        .HasColumnType("int");

                    b.Property<string>("Item_name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Item_ID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ConnectDataBase.Performance", b =>
                {
                    b.Property<int>("Performance_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Performace_visit_cost")
                        .HasPrecision(10, 4)
                        .HasColumnType("decimal(10,4)");

                    b.Property<DateTime>("Performance_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Performance_name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Performance_id");

                    b.ToTable("Performance");
                });

            modelBuilder.Entity("ConnectDataBase.Performance_staff", b =>
                {
                    b.Property<int>("Performance_staff_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Employer_id")
                        .HasColumnType("int");

                    b.Property<int>("Performance_id")
                        .HasColumnType("int");

                    b.Property<int?>("Performance_id1")
                        .HasColumnType("int");

                    b.HasKey("Performance_staff_id");

                    b.HasIndex("Employer_id");

                    b.HasIndex("Performance_id1");

                    b.ToTable("Performance_staff");
                });

            modelBuilder.Entity("ItemsPerformance", b =>
                {
                    b.Property<int>("ItemsItem_ID")
                        .HasColumnType("int");

                    b.Property<int>("Performance_id")
                        .HasColumnType("int");

                    b.HasKey("ItemsItem_ID", "Performance_id");

                    b.HasIndex("Performance_id");

                    b.ToTable("ItemsPerformance");
                });

            modelBuilder.Entity("ConnectDataBase.Performance_staff", b =>
                {
                    b.HasOne("ConnectDataBase.Employers", "Employers")
                        .WithMany("Performance_staff")
                        .HasForeignKey("Employer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConnectDataBase.Performance", "Performance")
                        .WithMany("Performance_staff")
                        .HasForeignKey("Performance_id1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Employers");

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("ItemsPerformance", b =>
                {
                    b.HasOne("ConnectDataBase.Items", null)
                        .WithMany()
                        .HasForeignKey("ItemsItem_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConnectDataBase.Performance", null)
                        .WithMany()
                        .HasForeignKey("Performance_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConnectDataBase.Employers", b =>
                {
                    b.Navigation("Performance_staff");
                });

            modelBuilder.Entity("ConnectDataBase.Performance", b =>
                {
                    b.Navigation("Performance_staff");
                });
#pragma warning restore 612, 618
        }
    }
}
