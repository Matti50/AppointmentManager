namespace TimeManager.Core;

using Microsoft.Data.Sqlite;
using Dapper;

public class Manager
{

    public Result Appoint(DateTime dateTime, string? description)
    {
        if (dateTime == null)
        {
            throw new ArgumentNullException("The date cannot be null");
        }
        
        var result = new Result();
        
        try 
        {
            using var connection = new SqliteConnection("./database.db");

            connection.Open();

            var sql = "insert into appointments (date_time, description) values(@date, @desc)";

            var parameters = new {date = dateTime, desc = description};

            var rowsAffected = connection.Execute(sql, parameters);

            if (rowsAffected == 1)
            {
                result.Ok = true; 

                result.Details.Add(CreateDefaultDetail(dateTime, description));
            }else
            {
                if (rowsAffected > 1)
                {
                    result.Ok = true;
                    result.Details.Add(CreateDefaultDetail(dateTime, description));
                    result.Details.Add("There was more than one apointment created. Be careful");
                }else 
                {
                    result.Ok = false;
                    result.Details.Add("No appointments created");
                }     
            }
        }catch(Exception ex)
        {
            result.Ok = false;
            result.Details.Add("There was an error when inserting the appointment into the database. Exception attached in this response");
            result.Details.Add($"Exception: {ex}");
        }

        return result;
    }
    
    public List<Appointment> GetAllAppointments()
    {
        using var connection = new SqliteConnection("./database.db");

        connection.Open();

        var sql = "select id as Id, date_time as Date, description as Description from appointments";
        
        var appointments = connection.Query<Appointment>(sql).ToList();

        connection.Close();

        return appointments;
    }

    private string CreateDefaultDetail(DateTime dateTime, string description)
    {
        var detail = $"Appointment created at {dateTime}.";
        if (description != null) detail += $" {description}";

        return detail;
    }
    
}

public struct Result 
{
    public bool Ok {get; set;}
    public List<string> Details {get; set;}
}
