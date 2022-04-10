﻿// <auto-generated />
using System;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommentManagement.Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(CommentContext))]
    [Migration("20220403100027_init_commentTable")]
    partial class init_commentTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CommentManagement.Domain.Comment.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CretionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long>("OwenerRecordId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("CommentManagement.Domain.Comment.Comment", b =>
                {
                    b.HasOne("CommentManagement.Domain.Comment.Comment", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });
#pragma warning restore 612, 618
        }
    }
}