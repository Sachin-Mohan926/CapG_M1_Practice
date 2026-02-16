using System;
using System.Collections.Generic;

public class Patient
{
    private int _id;
    private string _name;
    private int _age;
    private string _condition;

    public List<string> MedicalHistory { get; set; }

    public int Id
    {
        get { return _id; }
        private set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    public string Condition
    {
        get { return _condition; }
        set { _condition = value; }
    }

    public Patient(int id, string name, int age, string condition)
    {
        _id = id;
        _name = name;
        _age = age;
        _condition = condition;
        MedicalHistory = new List<string>();
    }
}


public class HospitalManager
{
    private Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
    private Queue<Patient> _appointmentQueue = new Queue<Patient>();

    public bool RegisterPatient(int id, string name, int age, string condition)
    {
        if (_patients.ContainsKey(id))
        {
            Console.WriteLine("Patient with this ID already exists.");
            return false;
        }

        Patient patient = new Patient(id, name, age, condition);
        _patients.Add(id, patient);
        return true;
    }

    public bool ScheduleAppointment(int patientId)
    {
        if (!_patients.ContainsKey(patientId))
        {
            Console.WriteLine("Patient not found.");
            return false;
        }

        _appointmentQueue.Enqueue(_patients[patientId]);
        return true;
    }

    public Patient ProcessNextAppointment()
    {
        if (_appointmentQueue.Count == 0)
        {
            Console.WriteLine("No appointments in queue.");
            return null;
        }

        return _appointmentQueue.Dequeue();
    }

    public List<Patient> FindPatientsByCondition(string condition)
    {
        return _patients.Values
                        .Where(p => p.Condition.Equals(condition, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }
}

class Program
{
    public static void Main()
    {
        HospitalManager manager = new HospitalManager();

        manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
        manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");

        manager.ScheduleAppointment(1);
        manager.ScheduleAppointment(2);

        Patient nextPatient = manager.ProcessNextAppointment();

        if (nextPatient != null)
            Console.WriteLine(nextPatient.Name);   // John Doe

        var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
        Console.WriteLine(diabeticPatients.Count);   // 1
    }
}


