PatLab1 is a small .net console application built as a lab-style animal simulation. The entry point is under PatLab1/UI/Program.cs, which drives a text menu: list status, add animals to an environment, feed them,
clean (including “all in pet shop”), trigger behaviours (run, fly, sing, walk, crawl), and advance simulated time by hours.
Domain model. Concrete animals include Dog, Cat, Parrot, and Spider. They are meant to inherit a shared Animal base (name, id, meals, time since last meal, happiness, physical state, etc.) 
and to implement capability interfaces in Models/Capabilities: for example IRunnable, IFlyable, ISingable, IWalkable, ICrawlable.
Behaviours often check hunger (e.g. if more than eight hours since the last meal, the animal falls back to walking or refuses the action), matching the semantics hinted in PhysicalStates 
(Full, HungryTired, Dead) in Models/States/PhysicalStates.cs.
Simulation. TimeSimulations (Simulation/TimeSimulation.cs) tracks hour and day and rolls hours into days with a 24-hour clock.
Environments. The UI is written for three places—Home, PetShop, and Nature—with caps on how many animals each holds and slightly different rules (e.g. happiness shown as “wild” in nature in the status screen).
In the snapshot reflected by your git status, the Animal base class and environment types are removed from disk while Program.cs still references them, so the design is clear from the UI and concrete animals,
but the tree may not build until those types are restored. Solution layout. PatLab1.sln includes the main project and a ClassLibrary1 project whose reference is commented out in PatLab1.csproj; 
ClassLibrary1 is effectively a placeholder.
Overall, the project is a teaching-oriented OOP exercise: interfaces for abilities, inheritance for species, environments, and a simple time-driven life cycle (feeding, cleaning, acting, 
advancing time)—exposed through an interactive console frontend.
