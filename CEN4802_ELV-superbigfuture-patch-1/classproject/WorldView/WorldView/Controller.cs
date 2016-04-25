using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace Master_Control_Program
{
    class Program
    {
        /*static void Main(string[] args)
        {
            Elevator elwood = new Elevator();
            //elwood.openElevator();
            Console.WriteLine(elwood);
            elwood.setDoorState("opened");
            Console.Write("\nWhich Floor? : ");
            elwood.setDoorState("closed");
            string x = Console.ReadLine();
            int floor;
            bool isValid = int.TryParse(x, out floor);
            while (floor != -1)
            {
                elwood.gotoFloor(floor);
                Console.WriteLine(elwood.getElevatorState());
                //Console.WriteLine("\npress enter to goto next floor...");
                Console.ReadLine();
                Console.WriteLine(elwood);
                Console.Write("\nWhich Floor? (enter -1 to quit): ");
                x = Console.ReadLine();
                elwood.setDoorClear(true);
                elwood.closeElevator();
                isValid = int.TryParse(x, out floor);
            }
        }*/

        public class Elevator
        {
            private int MaxCapacity;
            private string Name;
            private int CurrentFloor;
            private Nullable<int> NextFloor, LastFloor;
            private int DoorState;
            private bool DoorClear, InService, Occupied;
            private List<Passenger> Passengers = new List<Passenger>();

            //default contructor
            public Elevator()
            {
                Name = "Elwood the Elevator";
                InService = true;
                MaxCapacity = 6;
                NextFloor = null;
                LastFloor = null;
                DoorState = 0;
                DoorClear = false;
            }

            //constructor public
            public Elevator(string elevatorName, int currentFloor)
            {
                Name = elevatorName;
                CurrentFloor = currentFloor;
                NextFloor = null;
                LastFloor = null;
                DoorState = 0;
                DoorClear = false;
                InService = true;
            }

            //contructor
            private Elevator(string elevatorName, int currentFloor, Nullable<int> nextFloor,
            Nullable<int> lastFloor, int doorState, bool doorClear, bool inService)
            {
                Name = elevatorName;
                CurrentFloor = currentFloor;
                NextFloor = nextFloor;
                LastFloor = lastFloor;
                DoorState = doorState;
                DoorClear = doorClear;
                InService = inService;
            }

            public string getName()
            {
                return Name;
            }

            public void setName(string name)
            {
                Name = name;
            }

            public int getCurrentFloor()
            {
                return CurrentFloor;
            }

            public void setCurrentFloor(int currentFloor)
            {
                CurrentFloor = currentFloor;
            }

            public int getNextFloor()
            {
                return (int)NextFloor;
            }

            public void setNextFloor(int nextFloor)
            {
                NextFloor = nextFloor;
            }

            public int getLastFloor()
            {
                return (int)LastFloor;
            }

            public void setLastFloor(int lastFloor)
            {
                LastFloor = lastFloor;
            }

            public bool getDoorClear()
            {
                return DoorClear;
            }

            public void setDoorClear(bool doorClear)
            {
                DoorClear = doorClear;
            }

            public bool getInService()
            {
                return InService;
            }

            public int getDoorState()
            {
                return DoorState;
            }

            public void setDoorState(int doorState)
            {
                if (doorState >= 0 && doorState <= 4)
                {
                    DoorState = doorState;
                }
            }

            //toString()
            public override string ToString()
            {
                string s = "";
                s = "Name: " + Name + "\nInService: " + InService + "\nCurrentFloor: " + CurrentFloor
                + "\nNextFloor: " + NextFloor + "\nLastFloor: " + LastFloor
                + "\nDoorState: " + showDoorState() + "\nDoorClear: " + DoorClear;
                return s;
            }

            // setting the door state 
            public void setDoorState(string doorState)
            {
                switch (doorState)
                {
                    case "closed":
                        DoorState = 0;
                        break;
                    case "closing":
                        DoorState = 3;
                        break;
                    case "opened":
                        DoorState = 2;
                        break;
                    case "opening":
                        DoorState = 1;
                        break;
                    default:
                        break;
                }
            }
            //display door state 
            public string showDoorState()
            {
                string s = "";
                if (DoorState == 0)
                { s = "DoorClosed"; }
                if (DoorState == 1)
                { s = "DoorOpening"; }
                if (DoorState == 2)
                { s = "DoorOpen"; }
                if (DoorState == 3)
                { s = "DoorClosing"; }
                return s;
            }

            public async void gotoFloor(int nextFloor)
            {
                if (getDoorState() == 0)
                {
                    NextFloor = nextFloor;
                    await Task.Delay(ETA());
                    openElevator();
                    arrivedAtFloor();
                }

            }

            public void arrivedAtFloor()
            {
                Console.WriteLine("you have arrived");
                LastFloor = CurrentFloor;
                CurrentFloor = (int)NextFloor;
                NextFloor = null;
            }

            public int ETA()
            {
                int t = 0;
                t = (int)NextFloor - CurrentFloor;
                t *= 7000;
                if (t < 0)
                {
                    t *= -1;
                }
                return t;
            }

            //this tells you if the elevator is headed up or down. 
            public string getElevatorState()
            {
                string s = "Arrived_At_Floor";
                if (NextFloor != null)
                {
                    if ((CurrentFloor - (int)NextFloor) < 0)
                    { s = "Going_UP"; }
                    if ((CurrentFloor - (int)NextFloor) > 0)
                    { s = "Going_DOWN"; }
                    if ((CurrentFloor - (int)NextFloor) == 0)
                    { s = "you're already there..."; }
                }
                return s;
            }


            public async void openElevator()
            {
                if (DoorState == 0 || DoorClear == true)
                {
                    DoorState = 1;
                    Console.WriteLine("the door is opening. ");
                    await Task.Delay(3000);
                    DoorState = 2;
                    Console.WriteLine("the door is opened. ");
                }
            }

            public async void closeElevator()
            {
                if (DoorState == 2 && DoorClear == true)
                {
                    DoorState = 3;
                    Console.WriteLine("the door is closing. ");
                    await Task.Delay(3000);
                    DoorState = 0;
                    Console.WriteLine("the door is closed. ");
                }
            }
        }
        /// <>
        /// this is the end of the elevator class
        /// </>
        class Passenger
        {
            private string Name;
            private int CurrentFloor, NextFloor;
            private int TimePickUp, TimeDropOff, TimeWait;

            public Passenger(string name, int currentFloor, int nextFloor)
            {
                Name = name;
                CurrentFloor = currentFloor;
                NextFloor = nextFloor;
            }

            //methods get/set
            public string getName()
            {
                return Name;
            }
            public int getCurrentFloor()
            {
                return CurrentFloor;
            }
            public int getNextFloor()
            {
                return NextFloor;
            }
        }

        class Scheduler
        {
            List<Elevator> Elevator = new List<Elevator>();
            List<Passenger> Passengers = new List<Passenger>();
            List<Passenger> Requests = new List<Passenger>();

            int floor, lobby, top;

            public delegate bool gotoFloorAndWait(int nextFloor);
        }

        public bool gotoFloorAndWait(int nextFloor)
        { return true; }

        class Buttons { }
        class Display { }
    }
}