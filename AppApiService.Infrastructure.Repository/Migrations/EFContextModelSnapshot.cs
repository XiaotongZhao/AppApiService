﻿// <auto-generated />
using System;
using AppApiService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppApiService.Infrastructure.Repository.Migrations
{
    [DbContext(typeof(EFContext))]
    partial class EFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppApiService.Domain.DevOps.AgentServer.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConnect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Port")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.AgentServer.ServerUploadFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("FileContent")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServerUploadFiles");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.DeployPipeline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeployPipelineStatus")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PipelineId")
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DeployPipelines");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.DeployPipelineTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeployPipelineId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("OutputLog")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutputResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeployPipelineId");

                    b.ToTable("DeployPipelineTasks");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.Pipeline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pipelines");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.PipelineTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PipelineId")
                        .HasColumnType("int");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServerFileId")
                        .HasColumnType("int");

                    b.Property<int>("StepNo")
                        .HasColumnType("int");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PipelineId");

                    b.ToTable("PipelineTasks");
                });

            modelBuilder.Entity("AppApiService.Domain.DynamicRequestDataService.DataMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DataMapId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("MapName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MapType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentMapName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataMapId");

                    b.ToTable("DataMap");
                });

            modelBuilder.Entity("AppApiService.Domain.DynamicRequestDataService.DataValueMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DataMapId")
                        .HasColumnType("int");

                    b.Property<string>("DataMapValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifyOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DataMapId");

                    b.ToTable("DataValueMap");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.DeployPipelineTask", b =>
                {
                    b.HasOne("AppApiService.Domain.DevOps.ServiceTask.DeployPipeline", null)
                        .WithMany("DeployPipelineTasks")
                        .HasForeignKey("DeployPipelineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.PipelineTask", b =>
                {
                    b.HasOne("AppApiService.Domain.DevOps.ServiceTask.Pipeline", null)
                        .WithMany("Tasks")
                        .HasForeignKey("PipelineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppApiService.Domain.DynamicRequestDataService.DataMap", b =>
                {
                    b.HasOne("AppApiService.Domain.DynamicRequestDataService.DataMap", null)
                        .WithMany("ChildDataMaps")
                        .HasForeignKey("DataMapId");
                });

            modelBuilder.Entity("AppApiService.Domain.DynamicRequestDataService.DataValueMap", b =>
                {
                    b.HasOne("AppApiService.Domain.DynamicRequestDataService.DataMap", null)
                        .WithMany("ChildDataValueMaps")
                        .HasForeignKey("DataMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.DeployPipeline", b =>
                {
                    b.Navigation("DeployPipelineTasks");
                });

            modelBuilder.Entity("AppApiService.Domain.DevOps.ServiceTask.Pipeline", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("AppApiService.Domain.DynamicRequestDataService.DataMap", b =>
                {
                    b.Navigation("ChildDataMaps");

                    b.Navigation("ChildDataValueMaps");
                });
#pragma warning restore 612, 618
        }
    }
}
