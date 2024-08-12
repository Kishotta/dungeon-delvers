﻿// <auto-generated />
using System;
using DungeonDelvers.Modules.Monsters.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DungeonDelvers.Modules.Monsters.Infrastructure.Database.Migrations
{
    [DbContext(typeof(MonstersDbContext))]
    [Migration("20240812230207_AddMonsterSizeTypeAlignmentAndArmorClass")]
    partial class AddMonsterSizeTypeAlignmentAndArmorClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("monsters")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DungeonDelvers.Common.Infrastructure.Auditing.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("text")
                        .HasColumnName("affected_columns");

                    b.Property<string>("NewValues")
                        .HasColumnType("text")
                        .HasColumnName("new_values");

                    b.Property<DateTime>("OccurredAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_at_utc");

                    b.Property<string>("OldValues")
                        .HasColumnType("text")
                        .HasColumnName("old_values");

                    b.Property<string>("PrimaryKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("primary_key");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("table_name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_audit_logs");

                    b.ToTable("audit_logs", "monsters");
                });

            modelBuilder.Entity("DungeonDelvers.Common.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_at_utc");

                    b.Property<DateTime?>("ProcessedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_at_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages", "monsters");
                });

            modelBuilder.Entity("DungeonDelvers.Common.Infrastructure.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("InboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("inbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("InboxMessageId", "Name")
                        .HasName("pk_inbox_message_consumers");

                    b.ToTable("inbox_message_consumers", "monsters");
                });

            modelBuilder.Entity("DungeonDelvers.Common.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_at_utc");

                    b.Property<DateTime?>("ProcessedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_at_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", "monsters");
                });

            modelBuilder.Entity("DungeonDelvers.Common.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("OutboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("outbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("OutboxMessageId", "Name")
                        .HasName("pk_outbox_message_consumers");

                    b.ToTable("outbox_message_consumers", "monsters");
                });

            modelBuilder.Entity("DungeonDelvers.Modules.Monsters.Domain.Monsters.Monster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Alignment")
                        .HasColumnType("integer")
                        .HasColumnName("alignment");

                    b.Property<int>("ArmorClass")
                        .HasColumnType("integer")
                        .HasColumnName("armor_class");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<bool>("Official")
                        .HasColumnType("boolean")
                        .HasColumnName("official");

                    b.Property<int>("Size")
                        .HasColumnType("integer")
                        .HasColumnName("size");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_monsters");

                    b.ToTable("monsters", "monsters");
                });

            modelBuilder.Entity("DungeonDelvers.Modules.Monsters.Domain.Monsters.Monster", b =>
                {
                    b.OwnsOne("DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings.ChallengeRating", "ChallengeRating", b1 =>
                        {
                            b1.Property<Guid>("MonsterId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<float>("Value")
                                .HasColumnType("real")
                                .HasColumnName("challenge_rating");

                            b1.HasKey("MonsterId");

                            b1.ToTable("monsters", "monsters");

                            b1.WithOwner()
                                .HasForeignKey("MonsterId")
                                .HasConstraintName("fk_monsters_monsters_id");
                        });

                    b.OwnsOne("DungeonDelvers.Modules.Monsters.Domain.DiceExpressions.DiceExpression", "HitPoints", b1 =>
                        {
                            b1.Property<Guid>("MonsterId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<int>("Average")
                                .HasColumnType("integer")
                                .HasColumnName("hit_points_average");

                            b1.Property<int>("DiceCount")
                                .HasColumnType("integer")
                                .HasColumnName("hit_points_dice_count");

                            b1.Property<int>("DiceType")
                                .HasColumnType("integer")
                                .HasColumnName("hit_points_dice_type");

                            b1.Property<string>("Expression")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("hit_points_expression");

                            b1.Property<int>("Maximum")
                                .HasColumnType("integer")
                                .HasColumnName("hit_points_maximum");

                            b1.Property<int>("Minimum")
                                .HasColumnType("integer")
                                .HasColumnName("hit_points_minimum");

                            b1.Property<int>("Modifier")
                                .HasColumnType("integer")
                                .HasColumnName("hit_points_modifier");

                            b1.HasKey("MonsterId");

                            b1.ToTable("monsters", "monsters");

                            b1.WithOwner()
                                .HasForeignKey("MonsterId")
                                .HasConstraintName("fk_monsters_monsters_id");
                        });

                    b.Navigation("ChallengeRating")
                        .IsRequired();

                    b.Navigation("HitPoints")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
