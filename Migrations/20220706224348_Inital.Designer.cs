﻿// <auto-generated />
using System;
using Craftfest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Craftfest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220706224348_Inital")]
    partial class Inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Craftfest.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("RoundId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<int>("WinnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.HasIndex("TypeId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Craftfest.Models.GameType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableGames")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfTeams")
                        .HasColumnType("integer");

                    b.Property<string>("Rules")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GameTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableGames = 2,
                            Description = "Toss bean bags onto specialized boards.",
                            ImagePath = "images/cornhole.jpg",
                            Name = "Corn Hole",
                            NumberOfTeams = 2,
                            Rules = "<ul><li>Players on the same side will alternate throws</li><li>Individual points cancel each other out. Round score is determined by subtracting one score from the other</li><li>First team to 21 wins</li><li>To win, you must get exactly 21 to win. Overtime is a game to 11.</li><li>3 points = sand bag in the hole</li><li>1 point = sand bag on the board</li><li>A sandbag CANNOT hit the ground and bounce on the board. This is a zero point scenario</li><li>If you go over 21, the amount of points you go over will be subtracted from your score.</li></ul>"
                        },
                        new
                        {
                            Id = 2,
                            AvailableGames = 2,
                            Description = "Throw frisbees into trashcans.",
                            ImagePath = "images/kanjam.jpg",
                            Name = "Kan Jam",
                            NumberOfTeams = 2,
                            Rules = "<ul><li>Play to 21 points</li><li>If a team goes over 21 points, those extra points are subtracted from the team’s current score</li><li>When a team scores 3 points on both throws, each team member throws again.</li><li>3 points = slapped in on top</li><li>2 points = direct KamJam hit without teammate interference</li><li>1 point = disc is directed into the outside of the KamJam by teammate</li><li>Slot = Automatic win</li><li>Overtime is a game to 11</li></ul>"
                        },
                        new
                        {
                            Id = 3,
                            AvailableGames = 1,
                            Description = "Toss ping pong balls into solo cups.",
                            ImagePath = "images/beerpong.jpg",
                            Name = "Beer Pong",
                            NumberOfTeams = 2,
                            Rules = "<ul><li>The rules are simplistic to mimic basic beer pong</li><li>Both eye for an eye OR rock, paper, scissors can be used to determine who goes first</li><li>If both players make a cup on their turn, the balls are returned to shoot again</li><li>There is no heating up, on fire, “blowing” / “finger” the ball from the cup or lightning</li><li>Teams get 1 (re-rack) request per game and can only be used at the start of a teams turn.</li><li>There is no end game gentlemens</li><li>If a team sinks all cups, the opposing players have redemption (chance to shoot again). If one of these players makes a cup, that player is afforded another shot until they miss.</li><li>Sudden death setup is a 3 cup triangle.</li></ul>"
                        },
                        new
                        {
                            Id = 4,
                            AvailableGames = 1,
                            Description = "Race to the finish with your legs in a potato sack.",
                            ImagePath = "images/psr.jpg",
                            Name = "Potato Sack Race",
                            NumberOfTeams = 2,
                            Rules = "<ul><li>A player from both teams starts the race by placing both feet into the sack.</li><li>The first players will hop down to the marked location and head back to the starting point.</li><li>Players MUST Tag the hand of their teammates before getting out of the sack. The tagged in teammates will hop the course and the first team back to the finish line wins.</li><li>IMPORTANT: an officiator must be present at every race to confirm winner and enforce rules.</li></ul>"
                        },
                        new
                        {
                            Id = 5,
                            AvailableGames = 1,
                            Description = "Throw bolas around poles on specialized ladder structure.",
                            ImagePath = "images/laddergolf.jpg",
                            Name = "Ladder Golf",
                            NumberOfTeams = 2,
                            Rules = "<ul><li>establish throw line at 5 paces from the ladder</li><li>Game is to 21 exactly. If a player goes over 21, all points are discarded from that round.</li><li>Bolla on the bottom ladder = 1 point</li><li>Bolla on the middle ladder = 2 points</li><li>Bolla on the top ladder = 3 points</li><li>If a Bolla from both teams end up on the same ladder, those points cancel out. IMPORTANT: points ONLY cancel out on the same ladder. If team A’s Bolla hits the top ladder and Team B’s hits the middle ladder those points DO NOT cancel out</li><li>Bolas can be knocked off the ladder. Bola’s knocked off the ladder do not count towards end of round scoring.</li><li>One (1) bonus point is awarded if a player lands all 3 Bolla’s on any ladder during their turn.</li></ul>"
                        });
                });

            modelBuilder.Entity("Craftfest.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("Craftfest.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Texas",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 2,
                            Name = "Florida",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tennesse",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 4,
                            Name = "Maryland",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 5,
                            Name = "Arizona",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 6,
                            Name = "Hawaii",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 7,
                            Name = "Indiana",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 9,
                            Name = "California",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 10,
                            Name = "New Hampshire",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 11,
                            Name = "New Jersey",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 12,
                            Name = "Kentucky",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 13,
                            Name = "Louisiana",
                            Score = 0.0
                        },
                        new
                        {
                            Id = 14,
                            Name = "North Carolina",
                            Score = 0.0
                        });
                });

            modelBuilder.Entity("Craftfest.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TournamentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WinnerId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("GameTeam", b =>
                {
                    b.Property<int>("GamesId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamsId")
                        .HasColumnType("integer");

                    b.HasKey("GamesId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("GameTeam");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Craftfest.Models.Player", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasIndex("TeamId");

                    b.HasDiscriminator().HasValue("Player");
                });

            modelBuilder.Entity("Craftfest.Models.Game", b =>
                {
                    b.HasOne("Craftfest.Models.Round", null)
                        .WithMany("Games")
                        .HasForeignKey("RoundId");

                    b.HasOne("Craftfest.Models.GameType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Craftfest.Models.Round", b =>
                {
                    b.HasOne("Craftfest.Models.Tournament", null)
                        .WithMany("Rounds")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("Craftfest.Models.Tournament", b =>
                {
                    b.HasOne("Craftfest.Models.Team", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("GameTeam", b =>
                {
                    b.HasOne("Craftfest.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Craftfest.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Craftfest.Models.Player", b =>
                {
                    b.HasOne("Craftfest.Models.Team", null)
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Craftfest.Models.Round", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Craftfest.Models.Team", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Craftfest.Models.Tournament", b =>
                {
                    b.Navigation("Rounds");
                });
#pragma warning restore 612, 618
        }
    }
}
