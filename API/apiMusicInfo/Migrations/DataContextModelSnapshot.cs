﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using apiMusicInfo.Data;

#nullable disable

namespace apiMusicInfo.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BandMusician", b =>
                {
                    b.Property<string>("BandsName")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("MusiciansName")
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("BandsName", "MusiciansName");

                    b.HasIndex("MusiciansName");

                    b.ToTable("BandMusician");
                });

            modelBuilder.Entity("ExtensionSong", b =>
                {
                    b.Property<string>("ExtensionsName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SongsUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExtensionsName", "SongsUID");

                    b.HasIndex("SongsUID");

                    b.ToTable("ExtensionSong");
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.Property<Guid>("SongsUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PlaylistsDispositiu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlaylistsPlaylistName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SongsUID", "PlaylistsDispositiu", "PlaylistsPlaylistName");

                    b.HasIndex("PlaylistsDispositiu", "PlaylistsPlaylistName");

                    b.ToTable("PlaylistSong");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Album", b =>
                {
                    b.Property<string>("AlbumName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<Guid>("SongUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlbumName", "Year", "SongUID");

                    b.HasIndex("SongUID");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Band", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Genre")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Origin")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Name");

                    b.ToTable("Band");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Extension", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("Name");

                    b.ToTable("Extensions");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Instrument", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Musician", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.HasIndex("Name");

                    b.ToTable("Musician");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Play", b =>
                {
                    b.Property<Guid>("SongUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MusicianName")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("InstrumentName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bandname")
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("SongUID", "MusicianName", "InstrumentName", "Bandname");

                    b.HasIndex("Bandname");

                    b.HasIndex("InstrumentName");

                    b.HasIndex("MusicianName");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Playlist", b =>
                {
                    b.Property<string>("Dispositiu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlaylistName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Dispositiu", "PlaylistName");

                    b.HasIndex("Dispositiu");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Song", b =>
                {
                    b.Property<Guid>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OriginalSongUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UID");

                    b.HasIndex("OriginalSongUID");

                    b.HasIndex("UID");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("BandMusician", b =>
                {
                    b.HasOne("apiMusicInfo.Models.Band", null)
                        .WithMany()
                        .HasForeignKey("BandsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiMusicInfo.Models.Musician", null)
                        .WithMany()
                        .HasForeignKey("MusiciansName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExtensionSong", b =>
                {
                    b.HasOne("apiMusicInfo.Models.Extension", null)
                        .WithMany()
                        .HasForeignKey("ExtensionsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiMusicInfo.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaylistSong", b =>
                {
                    b.HasOne("apiMusicInfo.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiMusicInfo.Models.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistsDispositiu", "PlaylistsPlaylistName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("apiMusicInfo.Models.Album", b =>
                {
                    b.HasOne("apiMusicInfo.Models.Song", "Song")
                        .WithMany("Albums")
                        .HasForeignKey("SongUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Song");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Play", b =>
                {
                    b.HasOne("apiMusicInfo.Models.Band", "Band")
                        .WithMany("Plays")
                        .HasForeignKey("Bandname")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiMusicInfo.Models.Instrument", "Instrument")
                        .WithMany("Plays")
                        .HasForeignKey("InstrumentName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiMusicInfo.Models.Musician", "Musician")
                        .WithMany("Plays")
                        .HasForeignKey("MusicianName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiMusicInfo.Models.Song", "Song")
                        .WithMany("Plays")
                        .HasForeignKey("SongUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Band");

                    b.Navigation("Instrument");

                    b.Navigation("Musician");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Song", b =>
                {
                    b.HasOne("apiMusicInfo.Models.Song", "OriginalSong")
                        .WithMany("DerivedVersions")
                        .HasForeignKey("OriginalSongUID");

                    b.Navigation("OriginalSong");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Band", b =>
                {
                    b.Navigation("Plays");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Instrument", b =>
                {
                    b.Navigation("Plays");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Musician", b =>
                {
                    b.Navigation("Plays");
                });

            modelBuilder.Entity("apiMusicInfo.Models.Song", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("DerivedVersions");

                    b.Navigation("Plays");
                });
#pragma warning restore 612, 618
        }
    }
}
