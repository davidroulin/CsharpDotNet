using System;
using System.Collections.Generic;
using System.Linq;

partial class Program
{

    #region Exo 1

    class LogEntry
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; } // "LOGIN", "ACTION", "LOGOUT"
    }

    public void ExoLinq1()
    {

        // On dispose d'une liste de logs d’événements utilisateurs :

        

        // Objectif : Trouver les utilisateurs ayant au moins une session dans laquelle il y a plus de 5 événements "ACTION" entre un LOGIN et un LOGOUT.



        var logs = new List<LogEntry> {
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:00"), EventType = "LOGIN" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:01"), EventType = "ACTION" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:02"), EventType = "ACTION" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:03"), EventType = "ACTION" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:04"), EventType = "ACTION" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:05"), EventType = "ACTION" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:06"), EventType = "ACTION" },
            new() { UserId = "u1", SessionId = "s1", Timestamp = DateTime.Parse("2024-01-01 09:10"), EventType = "LOGOUT" },

            new() { UserId = "u2", SessionId = "s2", Timestamp = DateTime.Parse("2024-01-01 10:00"), EventType = "LOGIN" },
            new() { UserId = "u2", SessionId = "s2", Timestamp = DateTime.Parse("2024-01-01 10:01"), EventType = "ACTION" },
            new() { UserId = "u2", SessionId = "s2", Timestamp = DateTime.Parse("2024-01-01 10:05"), EventType = "LOGOUT" },
        };


        var result = logs
            .GroupBy(l => l.SessionId)
            .Where(session =>
            {
                var orderedEvents = session.OrderBy(e => e.Timestamp).ToList();
                var loginIndex = orderedEvents.FindIndex(e => e.EventType == "LOGIN");
                var logoutIndex = orderedEvents.FindLastIndex(e => e.EventType == "LOGOUT");

                if (loginIndex == -1 || logoutIndex == -1 || logoutIndex <= loginIndex)
                    return false;

                var actionsBetween = orderedEvents
                    .Skip(loginIndex + 1)
                    .Take(logoutIndex - loginIndex - 1)
                    .Count(e => e.EventType == "ACTION");

                return actionsBetween > 5;
            })
            .Select(g => g.First().UserId)
            .Distinct()
            .ToList();

        // cf aussi SkipWhile / Skip / TakeWhile (solution de Victor)

        // solution Frederic :
        // un gros Where initial complexe qui filtre les lignes situees entre un LOGIN et un LOGOUT
        //      (dont le timestamp est superieur au timestamp du dernier LOGIN avant lui-meme 🥵
        //      ET inferieur au timestamp du premier LOGOUT apres lui-meme 🥵🥵🥵🥵)
        // puis regrouper par user id
        // ce qui permet de compter
        // et donc de filtrer > 5
        // et enfin on extrait l'user id

        foreach (var user in result)
        {
            Console.WriteLine(user);
        }

    }

    #endregion

    #region Exo 2

    class Folder
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public void ExoLinq2()
    {

        var folders = new List<Folder> {
            new() { Id = 1, ParentId = null, Name = "Root" },
            new() { Id = 2, ParentId = 1, Name = "Sub1" },
            new() { Id = 3, ParentId = 1, Name = "Sub2" },
            new() { Id = 4, ParentId = 2, Name = "Sub1.1" },
            new() { Id = 5, ParentId = 4, Name = "Sub1.1.1" },
            new() { Id = 6, ParentId = 3, Name = "Sub2.1" },
        };

       

    }

    #endregion

    #region Exo 3



    //public void ExoLinqN()
    //{



    //}

    #endregion

    #region Exo N...



    //public void ExoLinqN()
    //{



    //}

    #endregion

    #region Exo N...



    //public void ExoLinqN()
    //{



    //}

    #endregion

    #region Exo N...



    //public void ExoLinqN()
    //{



    //}

    #endregion


}
