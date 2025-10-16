using System.Reflection;
using TVShowTracker.API.Models;

namespace TVShowTracker.API.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.TVShows.Any())
                return;

            //Create actors
            var actors = new List<Actor>
            {
                //Breaking Bad
                new Actor { Name = "Bryan Cranston", Bio="Walter White", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Bryan_Cranston_2022_%283x4_cropped%29.jpg/250px-Bryan_Cranston_2022_%283x4_cropped%29.jpg" },
                new Actor { Name = "Aaron Paul", Bio="Jesse Pinkman", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Aaron_Paul_-_AMC_The_Grove_-_Ash.jpg/250px-Aaron_Paul_-_AMC_The_Grove_-_Ash.jpg" },
                new Actor { Name = "Anna Gunn", Bio="Skyler White", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Anna_Gunn_by_Gage_Skidmore_3.jpg/250px-Anna_Gunn_by_Gage_Skidmore_3.jpg" },
                //Game of Thrones
                new Actor { Name = "Emilia Clarke", Bio="Daenerys Targaryen", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Emilia_Clarke_at_the_2023_Harper%27s_Bazaar_Women_of_the_Year_Awards.jpg/250px-Emilia_Clarke_at_the_2023_Harper%27s_Bazaar_Women_of_the_Year_Awards.jpg" },
                new Actor { Name = "Kit Harington", Bio="Jon Snow", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Kit_harrington_by_sachyn_mital_%28cropped_2%29.jpg/250px-Kit_harrington_by_sachyn_mital_%28cropped_2%29.jpg" },
                new Actor { Name = "Peter Dinklage", Bio="Tyrion Lannister", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/5/5f/Peter_Dinklage_by_Gage_Skidmore_2.jpg/250px-Peter_Dinklage_by_Gage_Skidmore_2.jpg" },
                //Stranger Things
                new Actor { Name = "Millie Bobby Brown", Bio="Eleven", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Millie_Bobby_Brown_-_MBB_-_Portrait_1_-_SFM5_-_July_10%2C_2022_at_Stranger_Fan_Meet_5_People_Convention_%28cropped%29.jpg/250px-Millie_Bobby_Brown_-_MBB_-_Portrait_1_-_SFM5_-_July_10%2C_2022_at_Stranger_Fan_Meet_5_People_Convention_%28cropped%29.jpg" },
                new Actor { Name = "David Harbour", Bio="Jim Hopper", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7b/David_Harbour_by_Gage_Skidmore_2.jpg/250px-David_Harbour_by_Gage_Skidmore_2.jpg" },
                new Actor { Name = "Finn Wolfhard", Bio="Mike Wheeler", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/Finn_Wolfhard_by_Gage_Skidmore_2.jpg/250px-Finn_Wolfhard_by_Gage_Skidmore_2.jpg" },
                //The Office
                new Actor { Name = "Steve Carell", Bio="Michael Scott", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/e/e8/Steve_Carell_-_The_40-Year-Old-Virgin.jpg/250px-Steve_Carell_-_The_40-Year-Old-Virgin.jpg" },
                new Actor { Name = "John Krasinski", Bio="Jim Halpert", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/John_Krasinski_2022.jpg/250px-John_Krasinski_2022.jpg" },
                new Actor { Name = "Jenna Fischer", Bio="Pam Beesly", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Jenna_Fischer_May08_cropped.jpg/250px-Jenna_Fischer_May08_cropped.jpg" },
                //Friends
                new Actor { Name = "Jennifer Aniston", Bio="Rachel Green", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/1/16/JenniferAnistonHWoFFeb2012.jpg/250px-JenniferAnistonHWoFFeb2012.jpg" },
                new Actor { Name = "Courteney Cox", Bio="Monica Geller", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/0/0f/CourteneyCoxFeb09.jpg" },
                new Actor { Name = "Matthew Perry", Bio="Chandler Bing", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/c/cd/Matthew_Perry_in_support_of_Awareness_on_Drug_Courts_and_Reduced_Substance_Abuse.jpg/250px-Matthew_Perry_in_support_of_Awareness_on_Drug_Courts_and_Reduced_Substance_Abuse.jpg" },
                //The Last of Us
                new Actor { Name = "Pedro Pascal", Bio = "Joel Miller in The Last of Us and Javier Peña in Narcos", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Pedro_Pascal_at_the_2025_Cannes_Film_Festival_05.jpg/250px-Pedro_Pascal_at_the_2025_Cannes_Film_Festival_05.jpg" },
                new Actor { Name = "Bella Ramsey", Bio = "Ellie Williams", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Bella_Ramsey_SXSW_2025_%28crop%29.jpg/250px-Bella_Ramsey_SXSW_2025_%28crop%29.jpg" },
                new Actor { Name = "Isabela Merced", Bio = "Dina Ackermann", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Isabela_Merced%2C_actor%2C_at_SXSW_2025_03_%28cropped%29.jpg/250px-Isabela_Merced%2C_actor%2C_at_SXSW_2025_03_%28cropped%29.jpg" },
                //The Witcher
                new Actor { Name = "Henry Cavill", Bio = "Geralt of Rivia", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/Henry_Cavill_%2848417913146%29_%28cropped%29.jpg/250px-Henry_Cavill_%2848417913146%29_%28cropped%29.jpg" },
                new Actor { Name = "Anya Chalotra", Bio = "Yennefer", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Anya_Chalotra_by_Gage_Skidmore.jpg/250px-Anya_Chalotra_by_Gage_Skidmore.jpg" },
                new Actor { Name = "Freya Allan", Bio = "Ciri", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Actress_Freya_Allan_in_2024_%2853704843534%29.jpg/250px-Actress_Freya_Allan_in_2024_%2853704843534%29.jpg" },
                //Peaky Blinders
                new Actor { Name = "Cillian Murphy", Bio = "Thomas Shelby", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/CillianMurphy-TIFF2025-01-Cropped_%28cropped%29.png/250px-CillianMurphy-TIFF2025-01-Cropped_%28cropped%29.png" },
                new Actor { Name = "Tom Hardy", Bio = "Alfie Solomons", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Tom_Hardy_by_Gage_Skidmore_in_2018.jpg/250px-Tom_Hardy_by_Gage_Skidmore_in_2018.jpg" },
                new Actor { Name = "Paul Anderson", Bio = "Arthur Shelby", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d2/Paul_Thomas_Anderson_2022_%282%29_%28cropped%29.jpg" },
                //Westworld
                new Actor { Name = "Evan Rachel Wood", Bio = "Dolores Abernathy", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Evan_Rachel_Wood_%283x4_cropped%29.jpg/250px-Evan_Rachel_Wood_%283x4_cropped%29.jpg" },
                new Actor { Name = "Anthony Hopkins", Bio = "Robert Ford", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/AnthonyHopkins10TIFF.jpg/250px-AnthonyHopkins10TIFF.jpg" },
                new Actor { Name = "Jeffrey Wright", Bio = "Bernard Lowe", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Jeffrey_Wright-7706.jpg/250px-Jeffrey_Wright-7706.jpg" },
                //Narcos
                new Actor { Name = "Wagner Moura", Bio = "Pablo Escobar", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/Wagner_Moura_at_2025_Cannes_Red_Carpet_for_O_Agente_Secreto_2.jpg/250px-Wagner_Moura_at_2025_Cannes_Red_Carpet_for_O_Agente_Secreto_2.jpg" },
                new Actor { Name = "Boyd Holbrook", Bio = "Steve Murphy", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/Boyd_Holbrook_%2840061613100%29.jpg/250px-Boyd_Holbrook_%2840061613100%29.jpg" },
                //Mr. Robot
                new Actor { Name = "Rami Malek", Bio = "Elliot Alderson", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Rami_Malek_in_2015_%282%29_%28cropped%29.jpg/250px-Rami_Malek_in_2015_%282%29_%28cropped%29.jpg" },
                new Actor { Name = "Christian Slater", Bio = "Mr. Robot", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Christian_Slater_by_Gage_Skidmore.jpg/250px-Christian_Slater_by_Gage_Skidmore.jpg" },
                new Actor { Name = "Carly Chaikin", Bio = "Darlene Alderson", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/Carly_Chaikin_2012.jpg_%28cropped%2Benhanced%29.jpg/250px-Carly_Chaikin_2012.jpg_%28cropped%2Benhanced%29.jpg" }
            };

            context.Actors.AddRange(actors);
            context.SaveChanges();

            //Create TV shows with episodes
            var shows = new List<TVShow>
            {
                new TVShow {
                    Title="Breaking Bad",
                    Description="A chemistry teacher becomes a meth producer.",
                    Genre="Crime", Type="Drama",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMzU5ZGYzNmQtMTdhYy00OGRiLTg0NmQtYjVjNzliZTg1ZGE4XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    ReleaseDate=new DateTime(2008,1,20),
                    Episodes=new List<Episode>{
                        new Episode{Title="Pilot",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2008,1,20)},
                        new Episode{Title="Cat's in the Bag...",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2008,1,27)},
                        new Episode{Title="…And the Bag's in the River",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2008,2,10)}
                    }
                },
                new TVShow {
                    Title="Game of Thrones",
                    Description="Nine noble families fight for control over the lands of Westeros.",
                    Genre="Fantasy", Type="Drama",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTNhMDJmNmYtNDQ5OS00ODdlLWE0ZDAtZTgyYTIwNDY3OTU3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    ReleaseDate=new DateTime(2011,4,17),
                    Episodes=new List<Episode>{
                        new Episode{Title="Winter Is Coming",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2011,4,17)},
                        new Episode{Title="The Kingsroad",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2011,4,24)},
                        new Episode{Title="Lord Snow",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2011,5,1)}
                    }
                },
                new TVShow {
                    Title="Stranger Things",
                    Description="Mysteries in a small town unfold.",
                    Genre="Sci-Fi", Type="Drama",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjg2NmM0MTEtYWY2Yy00NmFlLTllNTMtMjVkZjEwMGVlNzdjXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    ReleaseDate=new DateTime(2016,7,15),
                    Episodes=new List<Episode>{
                        new Episode{Title="The Vanishing of Will Byers",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2016,7,15)},
                        new Episode{Title="The Weirdo on Maple Street",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2016,7,15)},
                        new Episode{Title="Holly, Jolly",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2016,7,15)}
                    }
                },
                new TVShow {
                    Title="The Office",
                    Description="A mockumentary on a group of typical office workers.",
                    Genre="Comedy", Type="Sitcom",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BZjQwYzBlYzUtZjhhOS00ZDQ0LWE0NzAtYTk4MjgzZTNkZWEzXkEyXkFqcGc@._V1_.jpg",
                    ReleaseDate=new DateTime(2005,3,24),
                    Episodes=new List<Episode>{
                        new Episode{Title="Pilot",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2005,3,24)},
                        new Episode{Title="Diversity Day",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2005,3,29)},
                        new Episode{Title="Health Care",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2005,4,5)}
                    }
                },
                new TVShow {
                    Title="Friends",
                    Description="Follows the personal and professional lives of six twenty to thirty-something-year-old friends.",
                    Genre="Comedy", Type="Sitcom",
                    ReleaseDate=new DateTime(1994,9,22),
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BOTU2YmM5ZjctOGVlMC00YTczLTljM2MtYjhlNGI5YWMyZjFkXkEyXkFqcGc@._V1_.jpg",
                    Episodes=new List<Episode>{
                        new Episode{Title="The Pilot",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(1994,9,22)},
                        new Episode{Title="The One with the Sonogram at the End",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(1994,9,29)},
                        new Episode{Title="The One with the Thumb",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(1994,10,6)}
                    }
                },
                 new TVShow {
                    Title="The Last of Us",
                    Description="Survivors navigate a post-apocalyptic world ravaged by a fungal infection.",
                    Genre="Drama", Type="Adventure",
                    ImageUrl="https://m.media-amazon.com/images/M/MV5BYWI3ODJlMzktY2U5NC00ZjdlLWE1MGItNWQxZDk3NWNjN2RhXkEyXkFqcGc@._V1_.jpg",
                    ReleaseDate=new DateTime(2023,1,15),
                    Episodes=new List<Episode>{
                        new Episode{Title="When You're Lost in the Darkness",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2023,1,15)},
                        new Episode{Title="Infected",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2023,1,22)},
                        new Episode{Title="Long, Long Time",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2023,1,29)}
                    }
                },
                 new TVShow {
                    Title="The Witcher",
                    Description="A monster hunter struggles to find his place in a world where people often prove more wicked than beasts.",
                    Genre="Fantasy", Type="Action",
                    ImageUrl="https://m.media-amazon.com/images/M/MV5BOTQzMzNmMzUtODgwNS00YTdhLTg5N2MtOWU1YTc4YWY3NjRlXkEyXkFqcGc@._V1_.jpg",
                    ReleaseDate=new DateTime(2019,12,20),
                    Episodes=new List<Episode>{
                        new Episode{Title="The End's Beginning",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2019,12,20)},
                        new Episode{Title="Four Marks",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2019,12,20)},
                        new Episode{Title="Betrayer Moon",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2019,12,20)}
                    }
                },
                 new TVShow {
                    Title="Peaky Blinders",
                    Description="A gangster family epic set in 1919 Birmingham, England.",
                    Genre="Crime", Type="Drama",
                    ImageUrl="https://m.media-amazon.com/images/M/MV5BOGM0NGY3ZmItOGE2ZC00OWIxLTk0N2EtZWY4Yzg3ZDlhNGI3XkEyXkFqcGc@._V1_.jpg",
                    ReleaseDate=new DateTime(2013,9,12),
                    Episodes=new List<Episode>{
                        new Episode{Title="Episode 1",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2013,9,12)},
                        new Episode{Title="Episode 2",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2013,9,19)},
                        new Episode{Title="Episode 3",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2013,9,26)}
                    }
                },
                  new TVShow {
                    Title="Westworld",
                    Description="A futuristic theme park allows guests to live without limits — until the hosts begin to awaken.",
                    Genre="Sci-Fi", Type="Drama",
                    ImageUrl="https://m.media-amazon.com/images/M/MV5BMjM2MTA5NjIwNV5BMl5BanBnXkFtZTgwNjI2OTMxNTM@._V1_FMjpg_UX1000_.jpg",
                    ReleaseDate=new DateTime(2016,10,2),
                    Episodes=new List<Episode>{
                        new Episode{Title="The Original",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2016,10,2)},
                        new Episode{Title="Chestnut",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2016,10,9)},
                        new Episode{Title="The Stray",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2016,10,16)}
                    }
                },
                  new TVShow {
                    Title="Narcos",
                    Description="A chronicled look at the criminal exploits of Colombian drug lord Pablo Escobar.",
                    Genre="Crime", Type="Drama",
                    ImageUrl="https://m.media-amazon.com/images/M/MV5BNzQwOTcwMzIwN15BMl5BanBnXkFtZTgwMjYxMTA0NjE@._V1_FMjpg_UX1000_.jpg",
                    ReleaseDate=new DateTime(2015,8,28),
                    Episodes=new List<Episode>{
                        new Episode{Title="Descenso",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2015,8,28)},
                        new Episode{Title="The Sword of Simón Bolívar",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2015,8,28)},
                        new Episode{Title="The Men of Always",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2015,8,28)}
                    }
                },
                  new TVShow {
                    Title="Mr. Robot",
                    Description="A young programmer is recruited by a mysterious anarchist to destroy corporate America.",
                    Genre="Thriller", Type="Drama",
                    ImageUrl="https://m.media-amazon.com/images/M/MV5BOTg4NTBiZDAtZTc0YS00NzZlLTg4Y2ItNGQ3M2ZlMDM5MWQzXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                    ReleaseDate=new DateTime(2015,6,24),
                    Episodes=new List<Episode>{
                        new Episode{Title="eps1.0_hellofriend.mov",SeasonNumber=1,EpisodeNumber=1,ReleaseDate=new DateTime(2015,6,24)},
                        new Episode{Title="eps1.1_ones-and-zer0es.mpeg",SeasonNumber=1,EpisodeNumber=2,ReleaseDate=new DateTime(2015,7,1)},
                        new Episode{Title="eps1.2_d3bug.mkv",SeasonNumber=1,EpisodeNumber=3,ReleaseDate=new DateTime(2015,7,8)}
                    }
                }
            };

            context.TVShows.AddRange(shows);
            context.SaveChanges();

            // ----------------- Associate Actors with Series (ShowActor) -----------------
            var showActorMappings = new List<ShowActor>
            {
                //Breaking Bad
                new ShowActor{TVShowId=shows[0].Id, ActorId=actors[0].Id},
                new ShowActor{TVShowId=shows[0].Id, ActorId=actors[1].Id},
                new ShowActor{TVShowId=shows[0].Id, ActorId=actors[2].Id},
                //Game of Thrones
                new ShowActor{TVShowId=shows[1].Id, ActorId=actors[3].Id},
                new ShowActor{TVShowId=shows[1].Id, ActorId=actors[4].Id},
                new ShowActor{TVShowId=shows[1].Id, ActorId=actors[5].Id},
                //Stranger Things
                new ShowActor{TVShowId=shows[2].Id, ActorId=actors[6].Id},
                new ShowActor{TVShowId=shows[2].Id, ActorId=actors[7].Id},
                new ShowActor{TVShowId=shows[2].Id, ActorId=actors[8].Id},
                //The Office
                new ShowActor{TVShowId=shows[3].Id, ActorId=actors[9].Id},
                new ShowActor{TVShowId=shows[3].Id, ActorId=actors[10].Id},
                new ShowActor{TVShowId=shows[3].Id, ActorId=actors[11].Id},
                //Friends
                new ShowActor{TVShowId=shows[4].Id, ActorId=actors[12].Id},
                new ShowActor{TVShowId=shows[4].Id, ActorId=actors[13].Id},
                new ShowActor{TVShowId=shows[4].Id, ActorId=actors[14].Id},
                //The Last of Us
                new ShowActor{TVShowId=shows[5].Id, ActorId=actors[15].Id},
                new ShowActor{TVShowId=shows[5].Id, ActorId=actors[16].Id},
                new ShowActor{TVShowId=shows[5].Id, ActorId=actors[17].Id},
                //The Witcher
                new ShowActor{TVShowId=shows[6].Id, ActorId=actors[18].Id},
                new ShowActor{TVShowId=shows[6].Id, ActorId=actors[19].Id},
                new ShowActor{TVShowId=shows[6].Id, ActorId=actors[20].Id},
                //Peaky Blinders
                new ShowActor{TVShowId=shows[7].Id, ActorId=actors[21].Id},
                new ShowActor{TVShowId=shows[7].Id, ActorId=actors[22].Id},
                new ShowActor{TVShowId=shows[7].Id, ActorId=actors[23].Id},
                //Westworld
                new ShowActor{TVShowId=shows[8].Id, ActorId=actors[24].Id},
                new ShowActor{TVShowId=shows[8].Id, ActorId=actors[25].Id},
                new ShowActor{TVShowId=shows[8].Id, ActorId=actors[26].Id},
                //Narcos
                new ShowActor{TVShowId=shows[9].Id, ActorId=actors[15].Id},
                new ShowActor{TVShowId=shows[9].Id, ActorId=actors[27].Id},
                new ShowActor{TVShowId=shows[9].Id, ActorId=actors[28].Id},
                //Mr. Robot
                new ShowActor{TVShowId=shows[10].Id, ActorId=actors[29].Id},
                new ShowActor{TVShowId=shows[10].Id, ActorId=actors[30].Id},
                new ShowActor{TVShowId=shows[10].Id, ActorId=actors[31].Id}
            };

            context.ShowActors.AddRange(showActorMappings);
            context.SaveChanges();
        }
    }
}
