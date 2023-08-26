﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LytelburghApi.Migrations
{
    [DbContext(typeof(ForthbroughtFadungFramework))]
    partial class ForthbroughtFadungFrameworkModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Begetter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Age")
                        .HasColumnType("REAL");

                    b.Property<string>("GivenName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Begetters");
                });

            modelBuilder.Entity("Thinkback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BegetterId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Thought")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BegetterId");

                    b.ToTable("Thinkbacks");
                });

            modelBuilder.Entity("Thinkback", b =>
                {
                    b.HasOne("Begetter", null)
                        .WithMany("Thinkbacks")
                        .HasForeignKey("BegetterId");
                });

            modelBuilder.Entity("Begetter", b =>
                {
                    b.Navigation("Thinkbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
