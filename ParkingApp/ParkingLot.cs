namespace ParkingApp
{
    public class ParkingLot
    {
        private readonly Dictionary<int, Vehicle> _slots;

        public ParkingLot(int capacity)
        {
            _slots = new Dictionary<int, Vehicle>(capacity);
            for (int i = 1; i <= capacity; i++)
            {
                _slots[i] = null; // Initialize slots as empty
            }
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            int slot = _slots.FirstOrDefault(s => s.Value == null).Key;
            if (slot == 0)
            {
                Console.WriteLine("Sorry, parking lot is full");
            }
            else
            {
                _slots[slot] = vehicle;
                Console.WriteLine($"Allocated slot number: {slot}");
            }
        }

        public void LeaveSlot(int slotNumber)
        {
            if (_slots.ContainsKey(slotNumber) && _slots[slotNumber] != null)
            {
                _slots[slotNumber] = null;
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                Console.WriteLine("Slot not found or already empty");
            }
        }

        public void Status()
        {
            Console.WriteLine("Slot\tNo.\t\tType\tRegistration No\tColour");
            foreach (var slot in _slots.Where(s => s.Value != null))
            {
                Console.WriteLine($"{slot.Key}\t{slot.Value.RegistrationNumber}\t{slot.Value.Type}\t{slot.Value.Colour}");
            }
        }

        public void ReportByType(VehicleType type)
        {
            int count = _slots.Values.Count(v => v != null && v.Type == type);
            Console.WriteLine(count);
        }

        public void ReportByColour(string colour)
        {
            var results = _slots.Where(s => s.Value != null && s.Value.Colour.ToLower() == colour.ToLower())
                                .Select(s => s.Value.RegistrationNumber);
            Console.WriteLine(string.Join(", ", results));
        }

        public void ReportByPlate(bool isOdd)
        {
            var results = _slots.Where(s => s.Value != null &&
                (isOdd ? IsOddPlate(s.Value.RegistrationNumber) : !IsOddPlate(s.Value.RegistrationNumber)))
                .Select(s => s.Value.RegistrationNumber);
            Console.WriteLine(string.Join(", ", results));
        }

        private bool IsOddPlate(string regNumber)
        {
            string lastDigit = regNumber.Split('-')[1].Last().ToString();
            return int.TryParse(lastDigit, out int digit) && digit % 2 != 0;
        }
    }
}