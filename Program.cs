namespace TeamsLektion3;

// Tre tips:
// För vanlig data: lägg upp ett hierarki med klasser genom inheritance och polymorphism
// För logik : lägg upp interfaces med metoder
// Fokusera på inheritance och polymorphism

class Program
{
    static void Main(string[] args)
    {
        // 1. Inheritance
        // protected
        // base
        // symbolen ':'
        // virtual
        // abstract
        // interface

        // 2. Polymorphism
        // abstract, interface
        // override

        // 3. Encapsulation
        // getters och setters (men försök undvik setters)

        // 4. Abstraction


        List<Person> people = new List<Person>();

        Employee adam = new Employee();
        Employee bengt = new Employee();
        Customer olof = new Customer();
        Customer ben = new Customer();
        Administrator erik = new Administrator();

        try
        {
            adam.GoToRoom("food");
            olof.GoToRoom("entertainment");
            bengt.GoToRoom("staff");
            ben.GoToRoom("staff");

            erik.GoToRoom("moneyStorage");
            adam.GoToRoom("moneyStorage");
            ben.GoToRoom("moneyStorage");
        }
        catch (Exception)
        {

        }

        people.Add(adam);
        people.Add(bengt);
        people.Add(olof);
        people.Add(ben);
        people.Add(erik);

        int count = 0;
        foreach (Person person in people) {
            // Kolla om "person" är en "Employee"
            if (person is Employee) {
                // Nu vet vi att "person" är en "Employee" så vi kan casta till "Employee"
                Employee employee = (Employee) person;
                Console.WriteLine(employee.EmployeeId);
                count++;
            }

            //if (person.IsAdmin()) {
            //    count++;
            //}
        }
        

        Console.WriteLine($"Employees {count}");
    }

    public static void PrintName(Person person)
    {
        Console.WriteLine(person.Name);
    }
}

// Interfaces är exakt samma som abstrakta klasser förutom att de inte kan spara någon data.
interface IDatabase
{
    void Connect();
    string GetData();
}
 
// Abstract metod kräver abstract klass.
// Man kan inte skapa instanser av abstrakta klasser. Använd det för att sätta begränsningar (se "Person" klassen för exempel).
abstract class Database
{

    // Anledningar till abstract:
    // 1. Vi har ingen generell implementation; alla databaser hämtar data på olika sätt
    // 2. Alla databaser MÅSTE kunna hämta data, och därför tvingar vi dem att implementera denna metod.
    public abstract void Connect();
    public abstract string GetData();
}

class Product { }

class FoodProduct : Product { }

class Städmedel : Product { }

class ToolProduct : Product { }

// Vi kan ärva från en klass och flera interfaces, men inte flera klasser samtidigt.
class Vindruvor : FoodProduct, IDatabase
{
    public void Connect()
    {
        throw new NotImplementedException();
    }

    public string GetData()
    {
        throw new NotImplementedException();
    }
}

// Vi gör Person 'abstract' eftersom det inte kan finnas en "Person" som bara är en "Person". Det måste vara antingen en "Customer" eller en "Employee".
abstract class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    // Vi gör CurrentRoom till en egen variabel så att vi kan ha en getter för att hämta data, men också så att vi kan ändra på variabeln inom klassen.
    // Vi gör variabeln protected så att vi kan komma åt den i subklasser, exempelvis "Administrator".
    protected string _currentRoom;
    public string CurrentRoom
    {
        get { return _currentRoom; }
    }

    public virtual bool IsAdmin() {
        return false;
    }

    // Varje "Person" skall kunna gå till ett rum, men endast "Administrator" kan gå till moneyStorage. Därför sätter vi en gräns i implementationen här.
    // Vi gör metden virtual eftersom logiken för de flesta klasser är samma.
    // Om någon klass behöver ändra på beteendet så kan de överskriva metoden (override). Se "Administrator" och "Customer" för exempel.
    public virtual void GoToRoom(string room)
    {
        if (room == "moneyStorage")
        {
            throw new UnauthorizedAccessException("Nobody can go to the money room.");
        }

        this._currentRoom = room;
    }
}

class Customer : Person
{
    public List<string> PurchaseHistory { get; set; }
    public List<string> Cart { get; set; }

    // Customers skall inte kunna gå till "staff" rummet, och därför överskriver vi metoden och lägger till den begränsningen.
    // Annars så anroppar vi "Person" GoToRoom med "base.GoToRoom(room)" eftersom det finns lite mer logik vi måste köra (som att ändra på _currentRoom variabeln exempelvis).
    public override void GoToRoom(string room)
    {
        if (room == "staff")
        {
            throw new UnauthorizedAccessException("Customers cannot go to the staff room.");
        }

        // Base refererar till objektet men i bas-klassens scope, i detta fallet "Person" eftersom "Customer" ärver från "Person".
        base.GoToRoom(room);
    }
}

class Employee : Person
{
    public int EmployeeId { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

class Manager : Employee
{
    public string Branch { get; set; }
}

class Administrator : Manager
{
    // Admins skall kunna gå till alla rum, så därför överskriver vi metoden utan begränsningar.
    public override void GoToRoom(string room)
    {
        this._currentRoom = room;
    }

    public override bool IsAdmin()
    {
        return true;
    }
}

class Owner : Administrator { }

class GroceryShop { }

class Storage { }
