using TP2Console.Models.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TP2Console.Models;
using System.Collections.ObjectModel;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello, World!");
            using(var ctx = new FilmsDBContext())
            {
                //Requête Select
                /Film titanic = ctx.Films.First(f => f.Nom.Contains("Titanic"));

                // Modification de l'entité (dans le contexte seulement)
                titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;

                // Sauvegarde du contexte => Application de la modification dans la db
                int nbChanges = ctx.SaveChanges();
                Console.WriteLine("Nombre de record updated : " + nbChanges.ToString());

                Console.WriteLine("----------------Chargement explicite----------------------");
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                //Chargement des films dans categorieAction
                ctx.Entry(categorieAction).Collection(c => c.Films).Load();
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction.Films)
                {
                    Console.WriteLine(film.Nom);
                }

                Console.WriteLine("----------------Chargement hâtif----------------------");

                //Chargement de la catégorie Action et des films de cette catégorie
                Categorie categorieAction2 = ctx.Categories
                .Include(c => c.Films)
                .First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction2.Nom);
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction2.Films)
                {
                    Console.WriteLine(film.Nom);
                }

                Console.WriteLine("----------------Lazy loading----------------------");

                //Chargement de la catégorie Action
                Categorie categorieAction3 = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction3.Nom);
                Console.WriteLine("Films : ");
                Chargement des films de la catégorie Action.
                foreach (var film in categorieAction3.Films) // lazy loading initiated
                {
                     Console.WriteLine(film.Nom);
                }
            }*/

            Test();

            Exo2q2();
            Console.ReadKey();

            Exo2q3();
            Console.ReadKey();

            Exo2q4();
            Console.ReadKey();

            Exo2q5();
            Console.ReadKey();

            Exo2q6();
            Console.ReadKey();

            Exo2q7();
            Console.ReadKey();

            Exo2q8();
            Console.ReadKey();

            Exo2q9();
            Console.ReadKey();

            Exo3q1();
            Console.ReadKey();

            Exo3q2();
            Console.ReadKey();

            Exo3q3();
            Console.ReadKey();

            Exo3q4();
            Console.ReadKey();

            Exo3q5();
            Console.ReadKey();
        }

        static void Test()
        {
            var ctx = new FilmsDBContext();
            var utilisateur = ctx.Utilisateurs.FirstOrDefault(u => u.Id == ctx.Avis.First(a => a.Note == ctx.Avis.Max(f => f.Note)).Utilisateur);
            Console.WriteLine(utilisateur);
        }

        static void Exo2q2()
        {
            var ctx = new FilmsDBContext();
            ctx.Utilisateurs.ToList().ForEach(x => Console.WriteLine(x.Email));
        }

        static void Exo2q3()
        {
            var ctx = new FilmsDBContext();
            ctx.Utilisateurs.OrderBy(x => x.Login).ToList().ForEach(x => Console.WriteLine(x));
        }

        static void Exo2q4()
        {
            var ctx = new FilmsDBContext();
            var category = ctx.Categories.First(c => c.Nom == "Action");
            ctx.Entry(category).Collection(c => c.Films).Load();
            foreach (var film in category.Films)
            {
                Console.WriteLine(film);
            }
        }

        static void Exo2q5()
        {
            var ctx = new FilmsDBContext();
            Console.WriteLine(ctx.Categories.Count());
        }
        
        static void Exo2q6()
        {
            var ctx = new FilmsDBContext();
            Console.WriteLine(ctx.Avis.Min(a => a.Note));
        }
        
        static void Exo2q7()
        {
            var ctx = new FilmsDBContext();
            ctx.Films.Where(f => f.Nom.ToLower().StartsWith("le")).ToList().ForEach(x => Console.WriteLine(x));
        }
        
        static void Exo2q8()
        {
            var ctx = new FilmsDBContext();
            var film = ctx.Films.First(f => f.Nom.ToLower() == "pulp fiction");
            ctx.Entry(film).Collection(c => c.Avis).Load();
            Console.WriteLine(film.Avis.Average(a => a.Note));
        }
        
        static void Exo2q9()
        {
            var ctx = new FilmsDBContext();
            var utilisateur = ctx.Utilisateurs.FirstOrDefault(u => u.Id == ctx.Avis.First(a => a.Note == ctx.Avis.Max(f => f.Note)).Utilisateur);
            Console.WriteLine(utilisateur);
        }

        static void Exo3q1()
        {
            var ctx = new FilmsDBContext();
            Utilisateur user = new Utilisateur
            {
                Id = 999,
                Login = "Baptiste",
                Email = "fake@email.com",
                Pwd = "12345"
            };
            ctx.Utilisateurs.Add(user);
            ctx.SaveChanges();
        }

        static void Exo3q2()
        {
            var ctx = new FilmsDBContext();
            Categorie categorie = ctx.Categories.First(c => c.Nom == "Drame");
            Film film = ctx.Films.First(f => f.Nom.ToLower() == "l'armee des douze singes");

            film.Categorie = categorie.Id;
            film.Description = "J'me souviens plus bien mais je crois que Bruce Willis voyage dans le temps.";

            ctx.SaveChanges();
        }

        static void Exo3q3()
        {
            var ctx = new FilmsDBContext();
            Film film = ctx.Films.First(f => f.Nom.ToLower() == "l'armee des douze singes");
            ctx.Entry(film).Collection(c => c.Avis).Load();
            ctx.Avis.RemoveRange(film.Avis);
            ctx.Films.Remove(film);
            ctx.SaveChanges();
        }

        static void Exo3q4()
        {
            var ctx = new FilmsDBContext();
            Film film = ctx.Films.First(f => f.Nom.ToLower() == "pulp fiction");
            Utilisateur user = ctx.Utilisateurs.First(u => u.Id == 999);
            Avi avi = new Avi
            {
                Film = film.Id,
                Note = 1,
                Avis = "Mon film pref parce que Tim Roth, Uma Turman, Samuel L. Jackson et Travolta",
                Utilisateur = user.Id
            };

            ctx.Entry(film).Collection(f => f.Avis).Load();
            ctx.Avis.Add(avi);

            ctx.SaveChanges();
        }

        static void Exo3q5()
        {
            var ctx = new FilmsDBContext();
            Categorie categorie = ctx.Categories.First(c => c.Nom.ToLower() == "drame");
            Film f1 = new Film
            {
                Description = "Je savais pas quels films étaient en base et flemme de chercher",
                Nom = "Comedie francaise naze avec Christian Clavier 4",
                Categorie = categorie.Id
            };

            Film f2 = new Film
            {
                Description = "Sûrement mon deuxième film préféré",
                Nom = "Samuraï Cop",
                Categorie = categorie.Id
            };

            ctx.Films.AddRange(new List<Film>{ f1, f2});
            ctx.SaveChanges();
        }
    }
}