// See https://aka.ms/new-console-template for more information

using TimeManager.Core;

var timeManager = new Manager();

while(true)
{
    Console.WriteLine("Menu");
    Console.WriteLine("1. Get all appointments");
    Console.WriteLine("2. Create new appointment");
    
    var option = int.Parse(Console.ReadLine());

    if (option == 1)
    {
        timeManager.GetAllAppointments();
    }else 
    {
        if (option == 2)
        {
            var (whenn, description) = PrintCreateAppointmentMenu();
            var result = timeManager.Appoint(whenn, description);
            PrintResult(result);
        }
    }
}

(DateTime, string?) PrintCreateAppointmentMenu()
{
    string whenn = string.Empty;
    string description = string.Empty;

    Console.WriteLine("When do you want to set the appointment?");
   
    whenn = Console.ReadLine();

    var ok = DateTime.TryParse(whenn, out var whenWillHappen);

    if(!ok)
    {
        Console.WriteLine("Not able to parse date time string read from cli");
        throw new ArgumentNullException("Could not parse the given date time");
    }

    Console.WriteLine("What is the appointment about");
    description = Console.ReadLine();
    
    return (whenWillHappen, description);
}

void PrintResult(Result result)
{
    if (result.Ok)
    {
        Console.WriteLine("the appointment was successfully created");
    }

    foreach (var det in result.Details)
    {
         Console.WriteLine(det);
    } 
}

void PrintAppointments(IEnumerable<Appointment> appointments)
{
    foreach (var appointment in appointments)
    {
        Console.WriteLine(appointment);   
    }
}
