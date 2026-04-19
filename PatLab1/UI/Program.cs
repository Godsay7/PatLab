using System;
using System.Collections.Generic;
using PatLab1.Models.Animals;
using PatLab1.Models.Environments;
using PatLab1.Models.States;
using PatLab1.Models.Capabilities;
using PatLab1.Simulation;
using PatLab1.Models.Factories;

namespace PatLab1.UI
{
    internal class Program
    {
        private static readonly TimeSimulations _clock = new TimeSimulations();
        private static readonly List<Environments> _environments = new List<Environments>();

        private static void Main(string[] args)
        {
            InitializeEnvironments();

            bool exit = false;
            while (!exit)
            {
                ShowStatus();
                Console.WriteLine();
                Console.WriteLine("=== Animal Simulation ===");
                Console.WriteLine("Current time: Day {0}, Hour {1}", _clock.Day, _clock.Hour + 1);
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
            Home myHome = new Home();
            myHome.NotificationRaised += PrintSimulationMessage;
            _environments.Add(myHome);

            PetShop myPetShop = new PetShop();
            myPetShop.NotificationRaised += PrintSimulationMessage;
            _environments.Add(myPetShop);

            Nature myNature = new Nature();
            myNature.NotificationRaised += PrintSimulationMessage;
            _environments.Add(myNature);
        }

        private static void ShowStatus()
        {
            Console.WriteLine("=== Animals status ===");
            foreach (var env in _environments)
            {
                Console.WriteLine($"Environment: {env.Name} (animals: {env.ListOfAnimals.Count}/{env.MaxAnimals})");
                foreach (var a in env.ListOfAnimals)
                {
                    var happy = env is Nature ? "Happy (wild)" : a.IsHappy ? "Happy" : "Sad";
                    Console.WriteLine(
                        $"  Id={a.id}, {a.NameAnimal}, State={a.State}, MealsToday={a.MealsToday}, " +
                        $"HoursSinceLastMeal={a.HourSinceLastMeal}, {happy}");
                }
            }
        }

        private static Environments ChooseEnvironment()
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

        private static Animal ChooseAnimal(Environments env)
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
            
            var input = Console.ReadLine();
            Animal animal = AnimalFactory.CreateAnimal(input);

            if (animal == null)
            {
                Console.WriteLine("Unknown animal type.");
            }
            else
            {
                env.AddAnimal(animal);
                Console.WriteLine($"{animal.NameAnimal} added to {env.Name} with id={animal.id}.");
            }
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
                    if (home.Feed(animal))
                    {
                        Console.WriteLine($"{animal.NameAnimal} has eaten.");
                    }
                    else
                    {
                        Console.WriteLine($"{animal.NameAnimal}, ID:{animal.id} is not hungry.");
                    }
                    break;
                case PetShop shop:
                    if (shop.FeedOne(animal))
                    {
                        Console.WriteLine($"{animal.NameAnimal} has eaten.");
                    }
                    else
                    {
                        Console.WriteLine($"{animal.NameAnimal}, ID:{animal.id} is not hungry.");
                    }
                    break;
                default:
                    if (animal.Eat())
                    {
                        Console.WriteLine($"{animal.NameAnimal} has eaten.");
                    }
                    else
                    {
                        Console.WriteLine($"{animal.NameAnimal}, ID:{animal.id} is not hungry.");
                    }
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
            Console.Write("Action: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (animal is IFastMovable fastmovable)
                        if (animal.State == PhysicalStates.HungryTired)
                        {
                            Console.WriteLine("The animal wants to eat and can't move fast.");
                        }
                        else
                            fastmovable.MoveFast();
                    else
                        Console.WriteLine("This animal cannot move fast.");
                    break;
                case "2":
                    if (animal is IFlyable flyable)
                        if (animal.State == PhysicalStates.HungryTired)
                        {
                            Console.WriteLine("The animal wants to eat and can't flying.");
                        }
                        else
                            flyable.Fly(); 
                    else
                        Console.WriteLine("This animal cannot fly.");
                    break;
                case "3":
                    if (animal is ISoundable singable)
                        if (animal.State == PhysicalStates.HungryTired)
                        {
                            Console.WriteLine("The animal wants to eat and can't singing.");
                        }
                        else
                            singable.MakeSound();
                    else
                        Console.WriteLine("This animal cannot make sound.");
                    break;
                case "4":
                    if (animal is IMovable movable)
                        movable.Move();
                    else
                        Console.WriteLine("This animal cannot move.");
                    break;
                default:
                    Console.WriteLine("Unknown action.");
                    break;
            }
        }

        private static void AdvanceTimeMenu()
        {
            Console.Write("Enter number of hours to advance (8 hour maximum): ");
            if (!int.TryParse(Console.ReadLine(), out int hours) || hours <= 0 || hours > 8)
            {
                Console.WriteLine("Invalid number of hours.");
                return;
            }

            int previousDay = _clock.Day;
            _clock.UpdateTime(hours);
            bool isEndOfDay = _clock.Day > previousDay;
            
            foreach (var env in _environments)
            {
                foreach (var a in env.ListOfAnimals.ToList())
                {
                    a.PassTime(hours, isEndOfDay);
                }
            }
        }
        static void PrintSimulationMessage(string message)
        {
            Console.WriteLine($"{message}");
        }
    }
}