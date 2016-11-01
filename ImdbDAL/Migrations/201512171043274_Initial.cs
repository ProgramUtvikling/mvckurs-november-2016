namespace ImdbDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Author = c.String(maxLength: 100),
                        Headline = c.String(maxLength: 100),
                        Body = c.String(maxLength: 4000),
                        Movie_MovieId = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId)
                .Index(t => t.Movie_MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.String(nullable: false, maxLength: 30),
                        Title = c.String(maxLength: 100),
                        OriginalTitle = c.String(maxLength: 100),
                        Description = c.String(),
                        ProductionYear = c.String(maxLength: 4),
                        RunningLength = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Movie_MovieId = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId)
                .Index(t => t.Movie_MovieId);
            
            CreateTable(
                "dbo.MovieActor",
                c => new
                    {
                        Movie_MovieId = c.String(nullable: false, maxLength: 30),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Person_PersonId })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_PersonId, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.MovieDirector",
                c => new
                    {
                        Movie_MovieId = c.String(nullable: false, maxLength: 30),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Person_PersonId })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_PersonId, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.MovieProducer",
                c => new
                    {
                        Movie_MovieId = c.String(nullable: false, maxLength: 30),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Person_PersonId })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_PersonId, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Person_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieProducer", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.MovieProducer", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.MovieDirector", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.MovieDirector", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Comments", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieActor", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.MovieActor", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.MovieProducer", new[] { "Person_PersonId" });
            DropIndex("dbo.MovieProducer", new[] { "Movie_MovieId" });
            DropIndex("dbo.MovieDirector", new[] { "Person_PersonId" });
            DropIndex("dbo.MovieDirector", new[] { "Movie_MovieId" });
            DropIndex("dbo.MovieActor", new[] { "Person_PersonId" });
            DropIndex("dbo.MovieActor", new[] { "Movie_MovieId" });
            DropIndex("dbo.Votes", new[] { "Movie_MovieId" });
            DropIndex("dbo.Movies", new[] { "GenreId" });
            DropIndex("dbo.Comments", new[] { "Movie_MovieId" });
            DropTable("dbo.MovieProducer");
            DropTable("dbo.MovieDirector");
            DropTable("dbo.MovieActor");
            DropTable("dbo.Votes");
            DropTable("dbo.Genres");
            DropTable("dbo.People");
            DropTable("dbo.Movies");
            DropTable("dbo.Comments");
        }
    }
}
