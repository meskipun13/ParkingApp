namespace ParkingApp
{
    public enum VehicleType
    {
        Mobil,
        Motor
    }

    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string Colour { get; set; }
        public VehicleType Type { get; set; }

        public Vehicle(string registrationNumber, string colour, VehicleType type)
        {
            RegistrationNumber = registrationNumber;
            Colour = colour;
            Type = type;
        }
    }
}