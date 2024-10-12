namespace TimeManager.Core;


public class Appointment 
{
    public int Id {get; init;}
    public DateTime Date {get; private set;}
    public string? Description {get; private set;}

    public Appointment(DateTime dateTime, string description)
    {
        if (dateTime == null)
            throw new ArgumentNullException("Cannot create an appointment without a date and a time");

        Date = dateTime;
        Description = description;
    }

    public override string ToString()
    {
        var detail = $"Appointment at: {Date}.";
        
        if (Description != null)
            detail += $" {Description}.";

        return detail;
    }
}
