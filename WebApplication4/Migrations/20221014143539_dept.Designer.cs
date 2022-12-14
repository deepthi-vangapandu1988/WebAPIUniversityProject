// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication4.Data;

#nullable disable

namespace WebApplication4.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    [Migration("20221014143539_dept")]
    partial class dept
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication4.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentName = "ECE",
                            Location = "HYD"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentName = "CSE",
                            Location = "HYD"
                        });
                });

            modelBuilder.Entity("WebApplication4.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "HYD",
                            DepartmentId = 1,
                            Email = "Student1@gmail.com",
                            Name = "Student 1"
                        },
                        new
                        {
                            Id = 2,
                            Address = "HYD",
                            DepartmentId = 1,
                            Email = "Student2@gmail.com",
                            Name = "Student 2"
                        },
                        new
                        {
                            Id = 3,
                            Address = "HYD",
                            DepartmentId = 1,
                            Email = "Student2@gmail.com",
                            Name = "Student 3"
                        },
                        new
                        {
                            Id = 4,
                            Address = "HYD",
                            DepartmentId = 2,
                            Email = "Student3@gmail.com",
                            Name = "Student 4"
                        });
                });

            modelBuilder.Entity("WebApplication4.Data.Student", b =>
                {
                    b.HasOne("WebApplication4.Data.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("WebApplication4.Data.Department", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
