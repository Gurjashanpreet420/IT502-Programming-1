/* 
 * Project Name: LANGHAM Hotel Management System
 * Author Name: [Your Name]
 * Date: [Current Date]
 * Application Purpose: Console-based hotel room management with file I/O operations
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNo { get; set; }
        public bool IsAllocated { get; set; }
    }

    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
    }

    // Custom Class - RoomAllocation
    public class RoomAllocation
    {
        public int AllocatedRoomNo { get; set; }
        public Customer AllocatedCustomer { get; set; }
    }

    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static List<Room> listofRooms = new List<Room>();
        public static List<RoomAllocation> listOfRoomAllocations = new List<RoomAllocation>();
        public static string filePath;

        // Main function
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "lhms_studentid.txt");

            char ans;

            do
            {
                Console.Clear();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("                 LANGHAM HOTEL MANAGEMENT SYSTEM                  ");
                Console.WriteLine("                            MENU                                 ");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("0. Backup Room Allocations");
                Console.WriteLine("9. Exit");
                Console.WriteLine("***********************************************************************************");
                Console.Write("Enter Your Choice Number Here:");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddRooms();
                            break;
                        case 2:
                            DisplayRooms();
                            break;
                        case 3:
                            AllocateRoom();
                            break;
                        case 4:
                            DeAllocateRoom();
                            break;
                        case 5:
                            DisplayRoomAllocations();
                            break;
                        case 6:
                            Console.WriteLine("\nBilling Feature is Under Construction and will be added soon...!!!");
                            break;
                        case 7:
                            SaveRoomAllocationsToFile();
                            break;
                        case 8:
                            ShowRoomAllocationsFromFile();
                            break;
                        case 0:
                            BackupRoomAllocations();
                            break;
                        case 9:
                            Console.WriteLine("\nThank you for using LANGHAM Hotel Management System!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\nInvalid choice! Please select a valid option.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nUnhandled Exception: System.FormatException: Input string was not in a correct format.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"\nUnhandled Exception: System.InvalidOperationException: {ex.Message}");
                }

                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }

        // Task 2a: Add Rooms Function
        static void AddRooms()
        {
            try
            {
                Console.Write("\nPlease Enter the Total Number of Rooms in the Hotel:");
                int totalRooms = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < totalRooms; i++)
                {
                    Console.Write($"Please enter the Room Number:");
                    int roomNo = Convert.ToInt32(Console.ReadLine());

                    Room room = new Room
                    {
                        RoomNo = roomNo,
                        IsAllocated = false
                    };
                    listofRooms.Add(room);
                }

                Console.WriteLine($"\n{totalRooms} Rooms added successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nUnhandled Exception: System.FormatException: Input string was not in a correct format.");
            }
        }

        // Task 2a: Display Rooms Function
        static void DisplayRooms()
        {
            if (listofRooms.Count == 0)
            {
                Console.WriteLine("\nNo rooms available. Please add rooms first.");
                return;
            }

            Console.WriteLine("\n--- Room List ---");
            Console.WriteLine("Room No\t\tStatus");
            Console.WriteLine("----------------------------");

            foreach (var room in listofRooms)
            {
                string status = room.IsAllocated ? "Allocated" : "Available";
                Console.WriteLine($"{room.RoomNo}\t\t{status}");
            }
        }

        // Task 2a: Allocate Room Function
        static void AllocateRoom()
        {
            try
            {
                Console.Write("\nPlease Enter the Room No:");
                int roomNo = Convert.ToInt32(Console.ReadLine());

                Room room = listofRooms.FirstOrDefault(r => r.RoomNo == roomNo);

                if (room == null)
                {
                    throw new InvalidOperationException("Sequence contains no matching element");
                }

                if (room.IsAllocated)
                {
                    Console.WriteLine($"\nRoom {roomNo} is already allocated!");
                    return;
                }

                Console.Write("Please Enter Customer Number:");
                int customerNo = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please Enter Customer Name:");
                string customerName = Console.ReadLine();

                Customer customer = new Customer
                {
                    CustomerNo = customerNo,
                    CustomerName = customerName
                };

                RoomAllocation allocation = new RoomAllocation
                {
                    AllocatedRoomNo = roomNo,
                    AllocatedCustomer = customer
                };

                listOfRoomAllocations.Add(allocation);
                room.IsAllocated = true;

                Console.WriteLine($"\nRoom {roomNo} allocated to {customerName} successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nUnhandled Exception: System.FormatException: Input string was not in a correct format.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nUnhandled Exception: System.InvalidOperationException: Sequence contains no matching element");
            }
        }

        // Task 2a: De-Allocate Room Function
        static void DeAllocateRoom()
        {
            try
            {
                Console.Write("\nPlease Enter the Room No to De-Allocate:");
                int roomNo = Convert.ToInt32(Console.ReadLine());

                Room room = listofRooms.FirstOrDefault(r => r.RoomNo == roomNo);

                if (room == null)
                {
                    throw new InvalidOperationException("Sequence contains no matching element");
                }

                if (!room.IsAllocated)
                {
                    Console.WriteLine($"\nRoom {roomNo} is not allocated!");
                    return;
                }

                RoomAllocation allocation = listOfRoomAllocations.FirstOrDefault(a => a.AllocatedRoomNo == roomNo);

                if (allocation != null)
                {
                    listOfRoomAllocations.Remove(allocation);
                    room.IsAllocated = false;
                    Console.WriteLine($"\nRoom {roomNo} de-allocated successfully!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nUnhandled Exception: System.FormatException: Input string was not in a correct format.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nUnhandled Exception: System.InvalidOperationException: Sequence contains no matching element");
            }
        }

        // Task 2a: Display Room Allocations Function
        static void DisplayRoomAllocations()
        {
            if (listOfRoomAllocations.Count == 0)
            {
                Console.WriteLine("\nNo room allocations found.");
                return;
            }

            Console.WriteLine("\n--- Room Allocation Details ---");
            Console.WriteLine("Room No\t\tCustomer No\tCustomer Name");
            Console.WriteLine("------------------------------------------------");

            foreach (var allocation in listOfRoomAllocations)
            {
                Console.WriteLine($"{allocation.AllocatedRoomNo}\t\t{allocation.AllocatedCustomer.CustomerNo}\t\t{allocation.AllocatedCustomer.CustomerName}");
            }
        }

        // Task 2b: Save Room Allocations To File
        static void SaveRoomAllocationsToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine($"=== LANGHAM Hotel Room Allocations ===");
                    writer.WriteLine($"Timestamp: {DateTime.Now}");
                    writer.WriteLine($"==========================================");
                    writer.WriteLine("Room No\tCustomer No\tCustomer Name");
                    writer.WriteLine("------------------------------------------");

                    foreach (var allocation in listOfRoomAllocations)
                    {
                        writer.WriteLine($"{allocation.AllocatedRoomNo}\t{allocation.AllocatedCustomer.CustomerNo}\t\t{allocation.AllocatedCustomer.CustomerName}");
                    }

                    writer.WriteLine($"==========================================");
                    writer.WriteLine($"Total Allocations: {listOfRoomAllocations.Count}");
                }

                Console.WriteLine($"\nRoom allocations saved to file successfully!");
                Console.WriteLine($"File Location: {filePath}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nUnhandled Exception: System.ArgumentException: Stream was not writable.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving to file: {ex.Message}");
            }
        }

        // Task 2b: Show Room Allocations From File
        static void ShowRoomAllocationsFromFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Could not find file '{filePath}'");
                }

                Console.WriteLine("\n--- Reading from File ---");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\nUnhandled Exception: System.IO.FileNotFoundException: Could not find file '{filePath}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError reading file: {ex.Message}");
            }
        }

        // Task 2b: Backup Room Allocations
        static void BackupRoomAllocations()
        {
            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string backupFilePath = Path.Combine(folderPath, "lhms_studentid_backup.txt");

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Could not find file '{filePath}'");
                }

                // Read content from original file
                string content = File.ReadAllText(filePath);

                // Append to backup file
                using (StreamWriter writer = new StreamWriter(backupFilePath, true))
                {
                    writer.WriteLine($"\n--- Backup Created: {DateTime.Now} ---");
                    writer.WriteLine(content);
                }

                // Delete content from original file
                File.WriteAllText(filePath, string.Empty);

                Console.WriteLine("\nBackup created successfully!");
                Console.WriteLine($"Backup Location: {backupFilePath}");
                Console.WriteLine($"Original file '{filePath}' has been cleared.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\nUnhandled Exception: System.IO.FileNotFoundException: Could not find file '{filePath}'");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nUnhandled Exception: System.ArgumentException: Stream was not writable.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError during backup: {ex.Message}");
            }
        }
    }
}