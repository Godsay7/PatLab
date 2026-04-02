using System;
using System.Collections.Generic;
using PatLab1.Models.Animals;
using PatLab1.Models.Environments;
using PatLab1.Models.States;
using PatLab1.Models.Capabilities;
using PatLab1.Simulation;

namespace PatLab1.UI
{
    internal class Program
    {
        private static readonly TimeSimulations _clock = new TimeSimulations();
        private static readonly List<PatLab1.Models.Environments.Environment> _environments = new List<PatLab1.Models.Environments.Environment>();
        private static int _nextAnimalId = 1;

        private static void Main(string[] args)
        {
            InitializeEnvironments();

            bool exit = false;
            while (!exit)
            {
                ShowStatus();
                Console.WriteLine();
                Console.WriteLine("=== Animal Simulation ===");
                Console.WriteLine("Current time: Day {0}, Hour {1}", _clock.Day + 1, _clock.Hour);
                Console.WriteLine("1. Add animal to environment");
                Console.WriteLine("2. Feed an animal");
                Console.WriteLine("3. Clean after an animal / all in pet shop");
                Console.WriteLine("4. Make an animal act (run/fly/sing/walk/crawl)");
                Console.WriteLine("5. Advance time (hours)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");

                var input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        AddAnimalMenu();
                        break;
                    case "2":
                        FeedAnimalMenu();
                        break;
                    case "3":
                        CleanMenu();
                        break;
                    case "4":
                        ActionMenu();
                        break;
                    case "5":
                        AdvanceTimeMenu();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void InitializeEnvironments()
        {
            _environments.Add(new Home());
            _environments.Add(new PetShop());
            _environments.Add(new Nature());
        }

        private static void ShowStatus()
        {
            Console.WriteLine("=== Animals status ===");
            foreach (var env in _environments)
            {
                Console.WriteLine($"Environment: {env.Name} (animals: {env.ListOfAnimals.Count}/{env.MaxAnimals})");
                foreach (var a in env.ListOfAnimals)
                {
                    var happy = env is Nature ? "Happy (wild)" : a.IsHappy ? "Happy" : "Not happy";
                    Console.WriteLine(
                        $"  Id={a.id}, {a.NameAnimal}, State={a.State}, MealsToday={a.MealsToday}, " +
                        $"HoursSinceLastMeal={a.HourSinceLastMeal}, {happy}");
                }
            }
        }

        private static PatLab1.Models.Environments.Environment ChooseEnvironment()
        {
            for (int i = 0; i < _environments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_environments[i].Name}");
            }

            Console.Write("Choose environment: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index -= 1;
                if (index >= 0 && index < _environments.Count)
                    return _environments[index];
            }

            Console.WriteLine("Invalid environment.");
            return null;
        }

        private static Animal ChooseAnimal(PatLab1.Models.Environments.Environment env)
        {
            if (env == null || env.ListOfAnimals.Count == 0)
            {
                Console.WriteLine("No animals in this environment.");
                return null;
            }

            foreach (var a in env.ListOfAnimals)
            {
                Console.WriteLine($"Id={a.id}: {a.NameAnimal} (State={a.State})");
            }

            Console.Write("Enter animal id: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                foreach (var a in env.ListOfAnimals)
                {
                    if (a.id == id)
                        return a;
                }
            }

            Console.WriteLine("Animal not found.");
            return null;
        }

        private static void AddAnimalMenu()
        {
            var env = ChooseEnvironment();
            if (env == null)
                return;

            Console.WriteLine("Choose animal type:");
            Console.WriteLine("1. Cat");
            Console.WriteLine("2. Dog");
            Console.WriteLine("3. Parrot");
            Console.WriteLine("4. Spider");
            Console.Write("Type: ");

            Animal animal = null;
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    animal = new Cat();
                    break;
                case "2":
                    animal = new Dog();
                    break;
                case "3":
                    animal = new Parrot();
                    break;
                case "4":
                    animal = new Spider();
                    break;
                default:
                    Console.WriteLine("Unknown animal type.");
                    return;
            }

            animal.id = _nextAnimalId++;
            env.AddAnimal(animal);
            Console.WriteLine($"{animal.NameAnimal} added to {env.Name} with id={animal.id}.");
        }

        private static void FeedAnimalMenu()
        {
            var env = ChooseEnvironment();
            if (env == null)
                return;

            var animal = ChooseAnimal(env);
            if (animal == null)
                return;

            switch (env)
            {
                case Home home:
                    home.Feed(animal);
                    break;
                case PetShop shop:
                    shop.FeedOne(animal);
                    break;
                default:
                    animal.Eat();
                    break;
            }
        }

        private static void CleanMenu()
        {
            var env = ChooseEnvironment();
            if (env == null)
                return;

            if (env is PetShop shop)
            {
                Console.WriteLine("1. Clean one animal");
                Console.WriteLine("2. Clean all animals");
                Console.Write("Choose: ");
                var choice = Console.ReadLine();
                if (choice == "2")
                {
                    shop.CleanAll();
                }
                else
                {
                    var animal = ChooseAnimal(env);
                    if (animal != null)
                    {
                        shop.CleanOne(animal);
                    }
                }
            }
            else if (env is Home home)
            {
                var animal = ChooseAnimal(env);
                if (animal != null)
                {
                    home.Clean(animal);
                }
            }
            else
            {
                Console.WriteLine("Nature does not require cleaning.");
            }
        }

        private static void ActionMenu()
        {
            var env = ChooseEnvironment();
            if (env == null)
                return;

            var animal = ChooseAnimal(env);
            if (animal == null)
                return;

            if (animal.State == PhysicalStates.Dead)
            {
                Console.WriteLine("This animal is dead and cannot act.");
                return;
            }

            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Run (if supported)");
            Console.WriteLine("2. Fly (if supported)");
            Console.WriteLine("3. Sing (if supported)");
            Console.WriteLine("4. Walk");
            Console.WriteLine("5. Crawl");
            Console.Write("Action: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (animal is IRunnable runnable)
                        runnable.Run();
                    else
                        Console.WriteLine("This animal cannot run.");
                    break;
                case "2":
                    if (animal is IFlyable flyable)
                        flyable.Fly();
                    else
                        Console.WriteLine("This animal cannot fly.");
                    break;
                case "3":
                    if (animal is ISingable singable)
                        singable.Sing();
                    else
                        Console.WriteLine("This animal cannot sing.");
                    break;
                case "4":
                    animal.Walk();
                    break;
                case "5":
                    animal.Craw();
                    break;
                default:
                    Console.WriteLine("Unknown action.");
                    break;
            }
        }

        private static void AdvanceTimeMenu()
        {
            Console.Write("Enter number of hours to advance (4 hour maximum): ");
            if (!int.TryParse(Console.ReadLine(), out int hours) || hours <= 0 || hours > 4)
            {
                Console.WriteLine("Invalid number of hours.");
                return;
            }

            for (int i = 0; i < hours; i++)
            {
                int previousDay = _clock.Day;
                _clock.UpdateTime(1);
                bool isEndOfDay = _clock.Day > previousDay;

                foreach (var env in _environments)
                {
                    foreach (var a in env.ListOfAnimals)
                    {
                        a.PassTime(1, isEndOfDay);
                    }
                }
            }
        }
    }
}