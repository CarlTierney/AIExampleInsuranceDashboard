﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(InsuranceDbContext))]
    [Migration("20250423230059_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Agent", b =>
                {
                    b.Property<long>("AgentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AgentId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AgentId");

                    b.ToTable("Agents", (string)null);
                });

            modelBuilder.Entity("Domain.Claim", b =>
                {
                    b.Property<long>("ClaimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ClaimId"));

                    b.Property<decimal?>("ApprovedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClaimAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ClaimNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ClaimStatusId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateFiled")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PolicyId")
                        .HasColumnType("bigint");

                    b.HasKey("ClaimId");

                    b.HasIndex("ClaimStatusId");

                    b.HasIndex("PolicyId");

                    b.ToTable("Claims", (string)null);
                });

            modelBuilder.Entity("Domain.ClaimAssignment", b =>
                {
                    b.Property<long>("ClaimAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ClaimAssignmentId"));

                    b.Property<long>("AgentId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ClaimId")
                        .HasColumnType("bigint");

                    b.HasKey("ClaimAssignmentId");

                    b.HasIndex("AgentId");

                    b.HasIndex("ClaimId");

                    b.ToTable("ClaimAssignments", (string)null);
                });

            modelBuilder.Entity("Domain.ClaimStatus", b =>
                {
                    b.Property<long>("ClaimStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ClaimStatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClaimStatusId");

                    b.ToTable("ClaimStatuses", (string)null);
                });

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CustomerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Domain.Payment", b =>
                {
                    b.Property<long>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("ClaimId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("ClaimId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("Domain.Policy", b =>
                {
                    b.Property<long>("PolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PolicyId"));

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PremiumAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PolicyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Policies", (string)null);
                });

            modelBuilder.Entity("Domain.Claim", b =>
                {
                    b.HasOne("Domain.ClaimStatus", "ClaimStatus")
                        .WithMany("Claims")
                        .HasForeignKey("ClaimStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Policy", "Policy")
                        .WithMany("Claims")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClaimStatus");

                    b.Navigation("Policy");
                });

            modelBuilder.Entity("Domain.ClaimAssignment", b =>
                {
                    b.HasOne("Domain.Agent", "Agent")
                        .WithMany("ClaimAssignments")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Claim", "Claim")
                        .WithMany("ClaimAssignments")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Claim");
                });

            modelBuilder.Entity("Domain.Payment", b =>
                {
                    b.HasOne("Domain.Claim", "Claim")
                        .WithMany("Payments")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");
                });

            modelBuilder.Entity("Domain.Policy", b =>
                {
                    b.HasOne("Domain.Customer", "Customer")
                        .WithMany("Policies")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.Agent", b =>
                {
                    b.Navigation("ClaimAssignments");
                });

            modelBuilder.Entity("Domain.Claim", b =>
                {
                    b.Navigation("ClaimAssignments");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.ClaimStatus", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Navigation("Policies");
                });

            modelBuilder.Entity("Domain.Policy", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
