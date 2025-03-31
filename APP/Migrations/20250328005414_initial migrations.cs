using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APP.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "Category", "Text" },
                values: new object[,]
                {
                    { 1, "positive_emotion", "happy" },
                    { 2, "positive_emotion", "joy" },
                    { 3, "positive_emotion", "love" },
                    { 4, "positive_emotion", "excited" },
                    { 5, "positive_emotion", "content" },
                    { 6, "positive_emotion", "pleased" },
                    { 7, "positive_emotion", "grateful" },
                    { 8, "positive_emotion", "hopeful" },
                    { 9, "positive_emotion", "proud" },
                    { 10, "positive_emotion", "amused" },
                    { 11, "positive_emotion", "cheerful" },
                    { 12, "positive_emotion", "delighted" },
                    { 13, "positive_emotion", "optimistic" },
                    { 14, "positive_emotion", "enthusiastic" },
                    { 15, "positive_emotion", "satisfied" },
                    { 16, "positive_emotion", "blissful" },
                    { 17, "positive_emotion", "ecstatic" },
                    { 18, "positive_emotion", "gleeful" },
                    { 19, "positive_emotion", "jubilant" },
                    { 20, "positive_emotion", "merry" },
                    { 21, "positive_emotion", "radiant" },
                    { 22, "positive_emotion", "thrilled" },
                    { 23, "positive_emotion", "upbeat" },
                    { 24, "positive_emotion", "vivacious" },
                    { 25, "positive_emotion", "zestful" },
                    { 26, "positive_emotion", "buoyant" },
                    { 27, "positive_emotion", "elated" },
                    { 28, "positive_emotion", "exhilarated" },
                    { 29, "positive_emotion", "lighthearted" },
                    { 30, "positive_emotion", "overjoyed" },
                    { 31, "positive_emotion", "rapturous" },
                    { 32, "positive_emotion", "triumphant" },
                    { 33, "positive_emotion", "euphoric" },
                    { 34, "positive_emotion", "exultant" },
                    { 35, "positive_emotion", "festive" },
                    { 36, "positive_emotion", "jolly" },
                    { 37, "positive_emotion", "jovial" },
                    { 38, "positive_emotion", "mirthful" },
                    { 39, "positive_emotion", "peppy" },
                    { 40, "positive_emotion", "perky" },
                    { 41, "positive_emotion", "playful" },
                    { 42, "positive_emotion", "sparkling" },
                    { 43, "positive_emotion", "sunny" },
                    { 44, "positive_emotion", "vibrant" },
                    { 45, "positive_emotion", "whimsical" },
                    { 46, "positive_emotion", "winsome" },
                    { 47, "positive_emotion", "zany" },
                    { 48, "positive_emotion", "carefree" },
                    { 49, "positive_emotion", "ebullient" },
                    { 50, "positive_emotion", "effervescent" },
                    { 51, "positive_emotion", "exuberant" },
                    { 52, "negative_emotion", "sad" },
                    { 53, "negative_emotion", "angry" },
                    { 54, "negative_emotion", "fear" },
                    { 55, "negative_emotion", "anxious" },
                    { 56, "negative_emotion", "depressed" },
                    { 57, "negative_emotion", "frustrated" },
                    { 58, "negative_emotion", "worried" },
                    { 59, "negative_emotion", "upset" },
                    { 60, "negative_emotion", "disappointed" },
                    { 61, "negative_emotion", "guilty" },
                    { 62, "negative_emotion", "ashamed" },
                    { 63, "negative_emotion", "lonely" },
                    { 64, "negative_emotion", "miserable" },
                    { 65, "negative_emotion", "gloomy" },
                    { 66, "negative_emotion", "desperate" },
                    { 67, "negative_emotion", "hopeless" },
                    { 68, "negative_emotion", "bitter" },
                    { 69, "negative_emotion", "resentful" },
                    { 70, "negative_emotion", "irritated" },
                    { 71, "negative_emotion", "enraged" },
                    { 72, "negative_emotion", "furious" },
                    { 73, "negative_emotion", "aggravated" },
                    { 74, "negative_emotion", "annoyed" },
                    { 75, "negative_emotion", "disgruntled" },
                    { 76, "negative_emotion", "displeased" },
                    { 77, "negative_emotion", "exasperated" },
                    { 78, "negative_emotion", "incensed" },
                    { 79, "negative_emotion", "indignant" },
                    { 80, "negative_emotion", "outraged" },
                    { 81, "negative_emotion", "vexed" },
                    { 82, "negative_emotion", "apprehensive" },
                    { 83, "negative_emotion", "dreadful" },
                    { 84, "negative_emotion", "frightened" },
                    { 85, "negative_emotion", "panicked" },
                    { 86, "negative_emotion", "petrified" },
                    { 87, "negative_emotion", "terrified" },
                    { 88, "negative_emotion", "alarmed" },
                    { 89, "negative_emotion", "shocked" },
                    { 90, "negative_emotion", "horrified" },
                    { 91, "negative_emotion", "dismayed" },
                    { 92, "negative_emotion", "distressed" },
                    { 93, "negative_emotion", "grieved" },
                    { 94, "negative_emotion", "heartbroken" },
                    { 95, "negative_emotion", "melancholy" },
                    { 96, "negative_emotion", "mournful" },
                    { 97, "negative_emotion", "sorrowful" },
                    { 98, "negative_emotion", "woeful" },
                    { 99, "negative_emotion", "despondent" },
                    { 100, "negative_emotion", "disheartened" },
                    { 101, "negative_emotion", "forlorn" },
                    { 102, "negative_emotion", "pessimistic" },
                    { 103, "social", "friend" },
                    { 104, "social", "family" },
                    { 105, "social", "team" },
                    { 106, "social", "community" },
                    { 107, "social", "partner" },
                    { 108, "social", "colleague" },
                    { 109, "social", "neighbor" },
                    { 110, "social", "acquaintance" },
                    { 111, "social", "ally" },
                    { 112, "social", "companion" },
                    { 113, "social", "confidant" },
                    { 114, "social", "mate" },
                    { 115, "social", "peer" },
                    { 116, "social", "supporter" },
                    { 117, "social", "advocate" },
                    { 118, "social", "backer" },
                    { 119, "social", "benefactor" },
                    { 120, "social", "comrade" },
                    { 121, "social", "crony" },
                    { 122, "social", "pal" },
                    { 123, "social", "associate" },
                    { 124, "social", "collaborator" },
                    { 125, "social", "co-worker" },
                    { 126, "social", "classmate" },
                    { 127, "social", "roommate" },
                    { 128, "social", "playmate" },
                    { 129, "social", "soulmate" },
                    { 130, "social", "spouse" },
                    { 131, "social", "sibling" },
                    { 132, "social", "parent" },
                    { 133, "social", "child" },
                    { 134, "social", "relative" },
                    { 135, "social", "kin" },
                    { 136, "social", "clan" },
                    { 137, "social", "tribe" },
                    { 138, "social", "group" },
                    { 139, "social", "club" },
                    { 140, "social", "society" },
                    { 141, "social", "organization" },
                    { 142, "social", "network" },
                    { 143, "social", "circle" },
                    { 144, "social", "crew" },
                    { 145, "social", "gang" },
                    { 146, "social", "posse" },
                    { 147, "social", "squad" },
                    { 148, "social", "unit" },
                    { 149, "social", "band" },
                    { 150, "social", "troop" },
                    { 151, "social", "assembly" },
                    { 152, "social", "congregation" },
                    { 153, "social", "gathering" },
                    { 154, "cognitive", "think" },
                    { 155, "cognitive", "know" },
                    { 156, "cognitive", "believe" },
                    { 157, "cognitive", "understand" },
                    { 158, "cognitive", "realize" },
                    { 159, "cognitive", "consider" },
                    { 160, "cognitive", "contemplate" },
                    { 161, "cognitive", "ponder" },
                    { 162, "cognitive", "reflect" },
                    { 163, "cognitive", "analyze" },
                    { 164, "cognitive", "evaluate" },
                    { 165, "cognitive", "assess" },
                    { 166, "cognitive", "judge" },
                    { 167, "cognitive", "decide" },
                    { 168, "cognitive", "conclude" },
                    { 169, "cognitive", "deduce" },
                    { 170, "cognitive", "infer" },
                    { 171, "cognitive", "reason" },
                    { 172, "cognitive", "rationalize" },
                    { 173, "cognitive", "speculate" },
                    { 174, "cognitive", "hypothesize" },
                    { 175, "cognitive", "theorize" },
                    { 176, "cognitive", "postulate" },
                    { 177, "cognitive", "conjecture" },
                    { 178, "cognitive", "surmise" },
                    { 179, "cognitive", "guess" },
                    { 180, "cognitive", "estimate" },
                    { 181, "cognitive", "calculate" },
                    { 182, "cognitive", "compute" },
                    { 183, "cognitive", "measure" },
                    { 184, "cognitive", "quantify" },
                    { 185, "cognitive", "qualify" },
                    { 186, "cognitive", "compare" },
                    { 187, "cognitive", "contrast" },
                    { 188, "cognitive", "differentiate" },
                    { 189, "cognitive", "distinguish" },
                    { 190, "cognitive", "identify" },
                    { 191, "cognitive", "recognize" },
                    { 192, "cognitive", "recall" },
                    { 193, "cognitive", "remember" },
                    { 194, "cognitive", "recollect" },
                    { 195, "cognitive", "retrieve" },
                    { 196, "cognitive", "forget" },
                    { 197, "cognitive", "ignore" },
                    { 198, "cognitive", "overlook" },
                    { 199, "cognitive", "neglect" },
                    { 200, "cognitive", "misunderstand" },
                    { 201, "cognitive", "confuse" },
                    { 202, "cognitive", "bewilder" },
                    { 203, "cognitive", "perplex" },
                    { 204, "cognitive", "puzzle" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_UserId",
                table: "JournalEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
