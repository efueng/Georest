﻿// <auto-generated />
using System;
using Georest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Georest.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190317233439_Migration2AddedForeignKeys")]
    partial class Migration2AddedForeignKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("Georest.Domain.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int?>("LabId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LabId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Georest.Domain.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("Georest.Domain.Models.InstructorResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int>("ExerciseId");

                    b.Property<int?>("InstructorLabId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("InstructorLabId");

                    b.ToTable("InstructorResponses");
                });

            modelBuilder.Entity("Georest.Domain.Models.Lab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsPublished");

                    b.Property<DateTime>("PublishedOn");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Lab");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Lab");
                });

            modelBuilder.Entity("Georest.Domain.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InstructorId");

                    b.Property<string>("SectionString")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Georest.Domain.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int>("SectionId");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Georest.Domain.Models.StudentLab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentLabs");
                });

            modelBuilder.Entity("Georest.Domain.Models.StudentResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("ExerciseId");

                    b.Property<int?>("StudentLabId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("StudentLabId");

                    b.ToTable("StudentResponses");
                });

            modelBuilder.Entity("Georest.Domain.Models.InstructorLab", b =>
                {
                    b.HasBaseType("Georest.Domain.Models.Lab");

                    b.Property<int>("InstructorId");

                    b.HasIndex("InstructorId");

                    b.HasDiscriminator().HasValue("InstructorLab");
                });

            modelBuilder.Entity("Georest.Domain.Models.Exercise", b =>
                {
                    b.HasOne("Georest.Domain.Models.Lab")
                        .WithMany("Exercises")
                        .HasForeignKey("LabId");
                });

            modelBuilder.Entity("Georest.Domain.Models.InstructorResponse", b =>
                {
                    b.HasOne("Georest.Domain.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Georest.Domain.Models.InstructorLab")
                        .WithMany("Responses")
                        .HasForeignKey("InstructorLabId");
                });

            modelBuilder.Entity("Georest.Domain.Models.Section", b =>
                {
                    b.HasOne("Georest.Domain.Models.Instructor", "Instructor")
                        .WithMany("Sections")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Georest.Domain.Models.Student", b =>
                {
                    b.HasOne("Georest.Domain.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Georest.Domain.Models.StudentLab", b =>
                {
                    b.HasOne("Georest.Domain.Models.Student", "Student")
                        .WithMany("Labs")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Georest.Domain.Models.StudentResponse", b =>
                {
                    b.HasOne("Georest.Domain.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Georest.Domain.Models.StudentLab")
                        .WithMany("Responses")
                        .HasForeignKey("StudentLabId");
                });

            modelBuilder.Entity("Georest.Domain.Models.InstructorLab", b =>
                {
                    b.HasOne("Georest.Domain.Models.Instructor", "Instructor")
                        .WithMany("Labs")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
