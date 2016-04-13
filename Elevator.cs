using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//
namespace Sark___Hardware_Component
{
    public class Elevator
    {
        //Event delegate for a progress bar
        internal delegate void SetProgressDelegate(int progress);
        internal event SetProgressDelegate SetProgress;

        Random rnd = new Random();

        private const int max_Capacity = 2000;
        private int current_Capacity;
        private string _Name, _Status;
        private int current_Floor;
        private int? next_Floor, last_Floor;
        private int door_State;
        private bool door_Clear, in_Service, _Occupied;

        public Elevator(){
            Name = "De-Fault";
            inService = true;
            currentCapacity = rnd.Next(50, 600);
            currentFloor = 1;
            nextFloor = null;
            lastFloor = null;
            doorState = 0;
            doorClear = false;
        }

        public Elevator(string name, int currentfloor)
        {
            Name = name;
            inService = true;
            currentCapacity = rnd.Next(50, 600);
            currentFloor = currentfloor;
            nextFloor = null;
            lastFloor = null;
            doorState = 0;
            doorClear = false;
        }

        public Elevator(string name)
        {
            Name = name;
            inService = true;
            currentCapacity = rnd.Next(50, 600);
            currentFloor = 1;
            nextFloor = null;
            lastFloor = null;
            doorState = 0;
            doorClear = false;
        }

        public int maxCapacity {
            get { return max_Capacity;}
        }

        public string Name {
            get { return _Name;}
            set { _Name = value ;}
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public int currentFloor {
            get { return current_Floor;}
            set { current_Floor = value ;}
        }

        public int? nextFloor {
            get { return next_Floor;}
            set { next_Floor = value ;}
        }

        public int? lastFloor {
            get { return last_Floor; }
            set { last_Floor = value ;}
        }

        public int doorState {
            get { return door_State; }
            set { door_State = value ;}
        }

        public bool doorClear {
            get { return door_Clear; }
            set { door_Clear = value ;}
        }

        public bool inService {
            get { return in_Service; }
            set { in_Service = value ;}
        }

        public bool Occupied {
            get { return _Occupied; }
            set { _Occupied = value ;}
        }

        public int currentCapacity {
            get { return current_Capacity; }
            set { current_Capacity = value ;}
        }

        public void MoveToFloor(int nextfloor)
        {
            if (inService == false)
            {
                Status = "Elevator not in Service";
            }
            else if (currentFloor == nextfloor)
            {
                Status = "Elevator is already on the correct floor";
            }
            else
            {
                nextFloor = nextfloor;
                DoorOpenRoutine();
                ElevatorMove();
                
            }
        }

        private void DoorOpenRoutine()
        {
            if (doorState != 0)
            {
                if (currentCapacity >= 0)
                {
                    Occupied = true;
                }
                else
                {
                    Occupied = false;
                }        
                
                Status = "Elevator Door is opening";
                Timer(4);
                doorState = 2;
                Status = "Elevator Door is open";

                // subtract from the elevator
                currentCapacity = -rnd.Next(50, currentCapacity);


                //  add people to the
                currentCapacity = +rnd.Next(50, 600);
            }

        }

        //Door State will slide from 0 - 3
        //Where:
        //0 = Locked
        //1 = Closed
        //2 = Open
        //3 = Failed
        private int DoorState(int State)
        {
            switch (State)
            {
                case 0: // Locked 
                    Status = "Door is locked.";
                    Timer(1);
                    
                    doorState = State;
                    break;

                case 1: // Closed
                    Status = "Door is closed.";
                    Timer(2);
                    
                    doorState = State;
                    break;

                case 2: // Open
                    Status = "Door Is Open.";
                    Timer(2);
                    
                    doorState = State;
                    break;

                case 3: // Failed
                    Status = "Door has Failed";
                    Timer(10);
                    
                    inService = false;
                    doorState = State;
                    break;

                default: // 
                    Status = "Critical Logic Error Occured, Closing Door";
                    Timer(0);
                    
                    DoorState(1);
                    MoveToFloor(0);
                    ServiceToggle();
                    break;
            }
            return doorState;
        }

        public void ServiceToggle()
        {
            if(inService == true)
            {
                inService = false;
            }
            else
            {
                inService = true;
            }
        }

        private void Timer(int multiplier)
        {
            int interval = 100 / multiplier;
            int temp = interval;
            for (int i = 0; i < multiplier; i++)
            {
                System.Threading.Thread.Sleep(500);
                SetProgress(temp);
                temp = temp + interval;
            }
        }
    
        private void FloorTime (int crrntFlr, int? nxtFlr)
        {
            if (nxtFlr == null)
            {
                nxtFlr = 0;
            }

            int temp = (int)nxtFlr - crrntFlr;
            temp = Math.Abs(temp);
            this.Timer(temp);
        }

        private void ElevatorMove ()
        {
            this.lastFloor = this.currentFloor;
            Status = "Elevator moving to " + nextFloor.ToString();
            FloorTime(currentFloor, nextFloor);
            Status = "Elevator is at floor " + nextFloor.ToString();
            this.currentFloor = (int)nextFloor;
        }

    }
    /// <>
    /// this is the end of the elevator class
    /// </>
}