using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Models;

namespace CodecoolMaterialAPI.DAL.DbInitializers
{
    public static class CodecoolMaterialDbInitializer
    {
        public static void Initialize(CodecoolMaterialDbContext context)
        {
            // Look for any Author.
            if (context.Authors.Any())
            {
                return;   // DB has been seeded
            }

            var eduMaterialTypes = new EduMaterialType[]
            {
                new EduMaterialType { Name = "Video Tutorial", Definition = "Video tutorial is a video material that focuses mostly on guiding step-by-step in dedicated topic" },
                new EduMaterialType { Name = "Online Tutorial", Definition = "Online tutorial is a material that focuses mostly on guiding step-by-step in dedicated topic" },
                new EduMaterialType { Name = "Presentation", Definition = "" },
                new EduMaterialType { Name = "Project Requirments", Definition = "" }
            };

            foreach (var eduMaterialType in eduMaterialTypes)
            {
                context.EduMaterialTypes.Add(eduMaterialType);
            }
            context.SaveChanges();

            var authors = new Author[]
            {
                new Author { Name = "Dominik Starzyk", Description = "Codecool's Mentor of Motorola Backend Academy" },
                new Author { Name = "Les Jackson", Description = "Youtuber" },
                new Author { Name = "Tim Corey", Description = "Youtuber" },
                new Author { Name = "Microsoft", Description = "Microsoft documentation" },
            };

            foreach (var author in authors)
            {
                context.Authors.Add(author);
            }
            context.SaveChanges();

            var eduMaterialNavPoints = new EduMaterialNavPoint[]
            {
                new EduMaterialNavPoint { Title = ".NET Core 3.1 MVC REST API - Full Course", 
                    Description = "In this full course, we show you how to build a full REST API using .NET Core 3.1. We’ll employ MVC, REST, the Repository Pattern, Dependency Injection, Entity Framework, Data Transfer Objects, (DTOs), AutoMapper to provide 6 API endpoints that will allow you to Create, Read Update and Delete resources.",
                    Location = "https://www.youtube.com/watch?v=fmvcAzHpsk8&ab_channel=LesJackson",
                    PublishDate = DateTime.Parse("2020-04-22"),
                    AuthorID = authors[1].ID,
                    EduMaterialTypeID = eduMaterialTypes[0].ID,
                    },
                new EduMaterialNavPoint { Title = "Introduction to ASP.NET Core MVC in C# plus LOTS of Tips",
                    Description = "In this video, I am going to walk you through how MVC is set up, how authentication works, and how it is different from the .NET Framework version of MVC.",
                    Location = "https://www.youtube.com/watch?v=1ck9LIBxO14&ab_channel=IAmTimCorey",
                    PublishDate = DateTime.Parse("2020-06-08"),
                    AuthorID = authors[2].ID,
                    EduMaterialTypeID = eduMaterialTypes[0].ID,
                    },
                new EduMaterialNavPoint { Title = "Motorola 2ND Backend exam",
                    Description = "Project requirments of 2ND motorola backend exam",
                    Location = "Codecool’s library at Ślusarska 9",
                    PublishDate = DateTime.Parse("2021-04-27"),
                    AuthorID = authors[0].ID,
                    EduMaterialTypeID = eduMaterialTypes[3].ID,
                    },
                new EduMaterialNavPoint { Title = "Async/Await Presentation",
                    Description = "Presentation about Async/Await in ASP.NET",
                    Location = "https://docs.google.com/presentation/d/1Ty5mZNkzoQxilUnfFpNRWU8Wa-UfVVrp/edit#slide=id.p1",
                    PublishDate = DateTime.Parse("2021-04-06"),
                    AuthorID = authors[0].ID,
                    EduMaterialTypeID = eduMaterialTypes[2].ID,
                    },
                new EduMaterialNavPoint { Title = "Tutorial: Get Started with Entity Framework 6 Code First using MVC 5",
                    Description = "In this series of tutorials, you learn how to build an ASP.NET MVC 5 application that uses Entity Framework 6 for data access. This tutorial uses the Code First workflow.",
                    Location = "https://docs.microsoft.com/pl-pl/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application",
                    PublishDate = DateTime.Parse("2019-01-22"),
                    AuthorID = authors[3].ID,
                    EduMaterialTypeID = eduMaterialTypes[1].ID,
                    },
                new EduMaterialNavPoint { Title = "Tutorial: Get started with EF Database First using MVC 5",
                    Description = "This tutorial shows how to start with an existing database and quickly create a web application that enables users to interact with the data.",
                    Location = "https://docs.microsoft.com/pl-pl/aspnet/mvc/overview/getting-started/database-first-development/setting-up-database",
                    PublishDate = DateTime.Parse("2019-01-15"),
                    AuthorID = authors[3].ID,
                    EduMaterialTypeID = eduMaterialTypes[1].ID,
                    },
            };

            foreach (var eduMaterialNavPoint in eduMaterialNavPoints)
            {
                context.EduMaterialNavPoints.Add(eduMaterialNavPoint);
            }
            context.SaveChanges();

            var reviews = new Review[]
            {
                new Review { Rate = 9, Comment = "Great tutorial about Entity Framework!",
                    EduMaterialNavPointID = eduMaterialNavPoints[4].ID},
                new Review { Rate = 10, Comment = "Awesome tutorial! Thanks Microsoft!",
                    EduMaterialNavPointID = eduMaterialNavPoints[4].ID},
                new Review { Rate = 5, Comment = "I don't like this tutorial, I don't understand anything!",
                    EduMaterialNavPointID = eduMaterialNavPoints[5].ID},
                new Review { Rate = 1, Comment = "Poor tutorial! I learnt NOTHING!",
                    EduMaterialNavPointID = eduMaterialNavPoints[5].ID},
                new Review { Rate = 8, Comment = "Great complementaty tutorial about API! :)",
                    EduMaterialNavPointID = eduMaterialNavPoints[0].ID},
                new Review { Rate = 7, Comment = "I like this guy!",
                    EduMaterialNavPointID = eduMaterialNavPoints[0].ID},
                new Review { Rate = 9, Comment = "Nice introduction to MVC",
                    EduMaterialNavPointID = eduMaterialNavPoints[1].ID},
                new Review { Rate = 8, Comment = "I like this guy!",
                    EduMaterialNavPointID = eduMaterialNavPoints[1].ID},
                new Review { Rate = 9, Comment = "Nice introduction to MVC",
                    EduMaterialNavPointID = eduMaterialNavPoints[1].ID},
                new Review { Rate = 0, Comment = "I like this mentor! But not this exam! :( :P",
                    EduMaterialNavPointID = eduMaterialNavPoints[2].ID},
                new Review { Rate = 5, Comment = "It's hard but I should pass this exam! xD",
                    EduMaterialNavPointID = eduMaterialNavPoints[2].ID},
                new Review { Rate = 7, Comment = "Good presentation about async/await!",
                    EduMaterialNavPointID = eduMaterialNavPoints[3].ID},
                new Review { Rate = 8, Comment = "And now I understand async/await! Great job Dominik!",
                    EduMaterialNavPointID = eduMaterialNavPoints[3].ID},
            };

            foreach (var review in reviews)
            {
                context.Reviews.Add(review);
            }
            context.SaveChanges();
        }
    }
}
