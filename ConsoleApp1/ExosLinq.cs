using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

        // À partir d'une structure de dossiers :

        // Objectif : Trouver tous les descendants (directs et indirects) d’un dossier donné (Id = 1) via LINQ + récursion.

        var folders = new List<Folder> {
            new() { Id = 1, ParentId = null, Name = "Root" },
            new() { Id = 2, ParentId = 1, Name = "Sub1" },
            new() { Id = 3, ParentId = 1, Name = "Sub2" },
            new() { Id = 4, ParentId = 2, Name = "Sub1.1" },
            new() { Id = 5, ParentId = 4, Name = "Sub1.1.1" },
            new() { Id = 6, ParentId = 3, Name = "Sub2.1" },
        };

        List<Folder> GetDescendants(List<Folder> all, int parentId)
        {
            return all
                .Where(f => f.ParentId == parentId)
                .SelectMany(f => new[] { f }.Concat(GetDescendants(all, f.Id)))
                .ToList();
        }

        var result = GetDescendants(folders, 1);

        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

    }

    #endregion

    #region Exo 3

    class Note
    {
        public string Student { get; set; }
        public string Subject { get; set; }
        public double Grade { get; set; }
    }


    public void ExoLinq3()
    {

        // Liste de notes par étudiant et matière.

        // Poids : Math = 2, autres = 1. Calculez les moyennes pondérées et n'affichez que la plus haute et la plus basse.

        var notes = new List<Note> {
            new() { Student = "Alice", Subject = "Maths", Grade = 19 },
            new() { Student = "Alice", Subject = "Histoire", Grade = 14 },
            new() { Student = "Bertrand", Subject = "Maths", Grade = 15 },
            new() { Student = "Bertrand", Subject = "Histoire", Grade = 17 },
            new() { Student = "Charles", Subject = "Maths", Grade = 10 },
            new() { Student = "Charles", Subject = "Histoire", Grade = 19 },
        };

        var averages = notes
            .GroupBy(n => n.Student)
            .Select(g =>
            {
                var weightedSum = g.Sum(n => n.Grade * (n.Subject == "Math" ? 2 : 1));
                var weightTotal = g.Sum(n => n.Subject == "Math" ? 2 : 1);
                return new
                {
                    Student = g.Key,
                    Average = weightedSum / weightTotal
                };
            });

        var result = averages
            .Where(avg =>
                avg.Average <= averages.Min(a => a.Average) ||  // je prends le min...
                avg.Average >= averages.Max(a => a.Average)     // ... ainsi que le max
             );

        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

    }

    #endregion

    #region Exo 4

    public void ExoLinq4()
    {

        // Regrouper des identifiants entiers consécutifs en plages continues.

        // Attendu :
        // [ { Start = 3, End = 5 }, { Start = 8, End = 9 }, { Start = 11, End = 14 } ]

        var ids = new List<int> { 3, 4, 5, 8, 9, 11, 12, 13, 14 };

        var result = ids
            .OrderBy(i => i)
            .Select((val, idx) => new { val, grp = val - idx })
            .GroupBy(x => x.grp)
            .Select(g => new { Start = g.First().val, End = g.Last().val })
            .ToList();

        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

    }

    #endregion

    #region Exo 5

    class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
    }

    class Project
    {
        public string Title { get; set; }
        public List<string> RequiredDepartments { get; set; }
    }

    public void ExoLinq5()
    {

        // Associer employés et projets selon correspondance de départements requis.

        var employees = new List<Employee> {
            new() { Name = "Anna", Department = "HR" },
            new() { Name = "Ben", Department = "IT" },
            new() { Name = "Cara", Department = "Finance" },
        };

        var projects = new List<Project> {
            new() { Title = "Migration", RequiredDepartments = new List<string> { "IT", "Finance" } },
            new() { Title = "Onboarding", RequiredDepartments = new List<string> { "HR" } },
        };

        // Attendu :
        // [
        //    { Title = "Migration", Employees = [ "Ben", "Cara" ] },
        //    { Title = "Onboarding", Employees = [ "Anna" ] }
        // ]

        var result = projects
            .Select(p => new {
                p.Title,
                Employees = employees
                    .Where(e => p.RequiredDepartments.Contains(e.Department))
                    .Select(e => e.Name)
                    .ToList()
            })
            .ToList();

        foreach (var item in result)
        {
            Console.WriteLine($"Title: {item.Title}, Employees: {string.Join(",", item.Employees)}");
        }
    }

    #endregion

    #region Exo 6

    public void ExoLinq6()
    {

        // À partir d’un document XML, retourner les ID de commandes dont le total d'achats > 1000

        //<Orders>
        //    <Order id="1">
        //		<Item name="Book" price="12.5"/>
        //		<Item name="Pen" price="1.5"/>
        //		<Item name="Printer" price="130"/>
        //    </Order>
        //    <Order id="2">
        //		<Item name="Laptop" price="2390"/>
        //		<Item name="Pen" price="1.5"/>
        //    </Order>
        //    <Order id="3">
        //		<Item name="Printer" price="130" quantity="9"/>
        //		<Item name="Pen" price="1.5" quantity="200"/>
        //    </Order>
        //</Orders>

        string path = "C:\\temp\\exo-linq-6_data.xml";
        XDocument doc = XDocument.Load(path);
        //var orders = doc.Root.Elements("Order");

        //var xml = XDocument.Parse(xmlString); ... ne pas oublier arobase+guillemet @"" pour le multi-ligne

        var result = doc.Descendants("Order")
            .Select(order => new {
                OrderId = (string)order.Attribute("id"),
                Total = order.Elements("Item").Sum(item => (double)item.Attribute("price") * ((int?)item.Attribute("quantity") ?? 1))
            })
            .Where(x => x.Total > 1000)
            .ToList();

        foreach (var item in result)
        {
            Console.WriteLine($"{item}");
        }
    }

    #endregion


}
