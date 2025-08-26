using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HBLoggingService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Callsign = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StationName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    GridSquare = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    County = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CountyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Dxcc = table.Column<int>(type: "integer", nullable: false),
                    ProKey = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatorProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qsos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Call = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    MyCall = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    QsoDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ContestId = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Freq = table.Column<decimal>(type: "numeric", nullable: false),
                    Band = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    RstSent = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    RstRcvd = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Dxcc = table.Column<int>(type: "integer", nullable: false),
                    BackedUp = table.Column<bool>(type: "boolean", nullable: false),
                    BackupDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qsos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Dtg = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TotalErrors = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallBookConfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Host = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    ApiKey = table.Column<string>(type: "text", nullable: true),
                    LogConfigId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallBookConfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallBookConfs_LogConfig_LogConfigId",
                        column: x => x.LogConfigId,
                        principalTable: "LogConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DxClusterConfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Host = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false, defaultValue: 7300),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    LogConfigId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DxClusterConfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DxClusterConfs_LogConfig_LogConfigId",
                        column: x => x.LogConfigId,
                        principalTable: "LogConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RigCtlConfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Host = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    TunerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LogConfigId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RigCtlConfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RigCtlConfs_LogConfig_LogConfigId",
                        column: x => x.LogConfigId,
                        principalTable: "LogConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CallSigns",
                columns: table => new
                {
                    Call = table.Column<string>(type: "text", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    OperatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Class = table.Column<string>(type: "text", nullable: false),
                    OperatorProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallSigns", x => x.Call);
                    table.ForeignKey(
                        name: "FK_CallSigns_OperatorProfiles_OperatorProfileId",
                        column: x => x.OperatorProfileId,
                        principalTable: "OperatorProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QsoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QsoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FieldValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QsoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QsoDetails_Qsos_QsoId",
                        column: x => x.QsoId,
                        principalTable: "Qsos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QsoQslInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QslService = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    QslSent = table.Column<bool>(type: "boolean", nullable: false),
                    QslReceived = table.Column<bool>(type: "boolean", nullable: false),
                    QsoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QsoQslInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QsoQslInfos_Qsos_QsoId",
                        column: x => x.QsoId,
                        principalTable: "Qsos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CallBookConfs_LogConfigId",
                table: "CallBookConfs",
                column: "LogConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_CallSigns_OperatorProfileId",
                table: "CallSigns",
                column: "OperatorProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DxClusterConfs_LogConfigId",
                table: "DxClusterConfs",
                column: "LogConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_QsoDetails_QsoId",
                table: "QsoDetails",
                column: "QsoId");

            migrationBuilder.CreateIndex(
                name: "IX_QsoQslInfos_QslService",
                table: "QsoQslInfos",
                column: "QslService");

            migrationBuilder.CreateIndex(
                name: "IX_QsoQslInfos_QsoId",
                table: "QsoQslInfos",
                column: "QsoId");

            migrationBuilder.CreateIndex(
                name: "IX_Qsos_Band",
                table: "Qsos",
                column: "Band");

            migrationBuilder.CreateIndex(
                name: "IX_Qsos_Call",
                table: "Qsos",
                column: "Call");

            migrationBuilder.CreateIndex(
                name: "IX_Qsos_Dxcc",
                table: "Qsos",
                column: "Dxcc");

            migrationBuilder.CreateIndex(
                name: "IX_RigCtlConfs_LogConfigId",
                table: "RigCtlConfs",
                column: "LogConfigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallBookConfs");

            migrationBuilder.DropTable(
                name: "CallSigns");

            migrationBuilder.DropTable(
                name: "DxClusterConfs");

            migrationBuilder.DropTable(
                name: "QsoDetails");

            migrationBuilder.DropTable(
                name: "QsoQslInfos");

            migrationBuilder.DropTable(
                name: "RigCtlConfs");

            migrationBuilder.DropTable(
                name: "ServerLogs");

            migrationBuilder.DropTable(
                name: "OperatorProfiles");

            migrationBuilder.DropTable(
                name: "Qsos");

            migrationBuilder.DropTable(
                name: "LogConfig");
        }
    }
}
