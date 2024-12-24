using System;
using System.Collections.Generic;
using System.Linq;

class ParkingLot
{
    private int capacity;
    private Dictionary<int, Vehicle> slots;
    private HashSet<string> registrationNumbers;

    public ParkingLot(int capacity)
    {
        this.capacity = capacity;
        slots = new Dictionary<int, Vehicle>();
        registrationNumbers = new HashSet<string>();
        for (int i = 1; i <= capacity; i++)
        {
            slots[i] = null;
        }
    }

    public void Park(string registrationNumber, string color, string type)
    {
        if (registrationNumbers.Contains(registrationNumber))
        {
            Console.WriteLine($"Vehicle with registration number {registrationNumber} is already parked.");
            return;
        }

        var slot = slots.FirstOrDefault(s => s.Value == null);
        if (slot.Key == 0)
        {
            Console.WriteLine("Sorry, parking lot is full");
            return;
        }

        Vehicle vehicle = new Vehicle(registrationNumber, color, type);
        slots[slot.Key] = vehicle;
        registrationNumbers.Add(registrationNumber);
        Console.WriteLine($"Allocated slot number: {slot.Key}");
    }

    public void Leave(int slotNumber)
    {
        if (slots.ContainsKey(slotNumber) && slots[slotNumber] != null)
        {
            registrationNumbers.Remove(slots[slotNumber].RegistrationNumber);
            slots[slotNumber] = null;
            Console.WriteLine($"Slot number {slotNumber} is free");
        }
        else
        {
            Console.WriteLine("Slot is already empty or invalid.");
        }
    }

    public void Status()
    {
        Console.WriteLine("Slot\tNo.\t\tType\tRegistration No\tColour");
        foreach (var slot in slots.Where(s => s.Value != null))
        {
            Console.WriteLine($"{slot.Key}\t{slot.Value.RegistrationNumber}\t{slot.Value.Type}\t{slot.Value.Color}");
        }
    }

    public void CountByType(string type)
    {
        int count = slots.Values.Count(v => v != null && v.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine(count);
    }

    public void RegistrationNumbersByPlate(string plateType)
    {
        bool isEven = plateType.Equals("even", StringComparison.OrdinalIgnoreCase);
        var result = slots.Values
            .Where(v => v != null && ((int)Char.GetNumericValue(v.RegistrationNumber.Last()) % 2 == 0) == isEven)
            .Select(v => v.RegistrationNumber);

        Console.WriteLine(string.Join(", ", result));
    }

    public void VehiclesByColor(string color)
    {
        var result = slots.Values.Where(v => v != null && v.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine(string.Join(", ", result.Select(v => v.RegistrationNumber)));
    }

    public void SlotsByColor(string color)
    {
        var result = slots.Where(s => s.Value != null && s.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine(string.Join(", ", result.Select(r => r.Key)));
    }

    public void SlotByRegistrationNumber(string registrationNumber)
    {
        var slot = slots.FirstOrDefault(s => s.Value != null && s.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        if (slot.Value != null)
            Console.WriteLine(slot.Key);
        else
            Console.WriteLine("Not found");
    }
}

class Vehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; }
    public string Type { get; }

    public Vehicle(string registrationNumber, string color, string type)
    {
        RegistrationNumber = registrationNumber;
        Color = color;
        Type = type;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ParkingLot parkingLot = null;
        while (true)
        {
            string command = Console.ReadLine();
            string[] inputs = command.Split(' ');
            switch (inputs[0])
            {
                case "create_parking_lot":
                    parkingLot = new ParkingLot(int.Parse(inputs[1]));
                    Console.WriteLine($"Created a parking lot with {inputs[1]} slots");
                    break;
                case "park":
                    parkingLot.Park(inputs[1], inputs[2], inputs[3]);
                    break;
                case "leave":
                    parkingLot.Leave(int.Parse(inputs[1]));
                    break;
                case "status":
                    parkingLot.Status();
                    break;
                case "type_of_vehicles":
                    parkingLot.CountByType(inputs[1]);
                    break;
                case "registration_numbers_for_vehicles_with_ood_plate":
                    parkingLot.RegistrationNumbersByPlate("odd");
                    break;
                case "registration_numbers_for_vehicles_with_event_plate":
                    parkingLot.RegistrationNumbersByPlate("even");
                    break;
                case "registration_numbers_for_vehicles_with_colour":
                    parkingLot.VehiclesByColor(inputs[1]);
                    break;
                case "slot_numbers_for_vehicles_with_colour":
                    parkingLot.SlotsByColor(inputs[1]);
                    break;
                case "slot_number_for_registration_number":
                    parkingLot.SlotByRegistrationNumber(inputs[1]);
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }
}
