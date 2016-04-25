using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using WorldView.InteriorElevator;

namespace WorldView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// 
    /// 
    /// 
    /// --Image and Label Names--
    /// MAINVIEW Components
    /// Elevator Image names: elevatorimage1, elevatorimage2, elevatorimage3, elevatorimage4
    /// Elevator Status Images: el + elevator# + statusfl + floornumber#  elevator 1 floor 5 = el1statusfl5
    /// Elevator Side Status Label names: mainEl1Status, mainEl2Status, mainEl3Status, mainEl4Status
    /// TABVIEW
    /// Includes Side Labels ^
    /// Elevator Image names: elevatorimaget1, elevatorimaget2, elevatorimaget3, elevatorimaget4
    /// Elevator Status Images: el + elevator# + statusflt + floornumber#  elevator 1 floor 5 = el1statusflt5
    /// Individual Tab Status label names: tabEl1Status, tabEl2Status, tabEl3Status, tabEl4Status
    /// IMAGE NAMES:
    /// bad.png - starting graphic - red circle
    /// good.png - initial notification from controll received - display updated - green circle
    /// locked.png - floor or elevator locked status - gold key
    /// (floor images) - indicate position(current floor) of elevator 
    /// floor1.png
    /// floor2.png
    /// floor3.png
    /// floor4.png
    /// floor5.png
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Elevator> elevator = new List<Elevator>();

        public static Controller controller;
        public static int floorSelection = 0;
        public static int elevatorSelection = 0;
        public static int el1Call = 0;
        public static int el2Call = 0;
        public static int el3Call = 0;
        public static int el4Call = 0;
        public static string path = (System.AppDomain.CurrentDomain.BaseDirectory);


        public MainWindow()
        {
            
            InitializeComponent(); //initialize the GUI interface
            //var elevator = new List<Elevator>(); //create a temporary List item of the elevator systems current state
            InteriorElevator.UserControl1 intEle = new InteriorElevator.UserControl1();
            contentcontrol1.Content = intEle;
            ExteriorElevator.OutsidePanel extEle1 = new ExteriorElevator.OutsidePanel();
            contentcontrol2.Content = extEle1;
            ExteriorElevator.OutsidePanel extEle2 = new ExteriorElevator.OutsidePanel();
            contentcontrol3.Content = extEle2;
            ExteriorElevator.OutsidePanel extEle3 = new ExteriorElevator.OutsidePanel();
            contentcontrol4.Content = extEle3;
            ExteriorElevator.OutsidePanel extEle4 = new ExteriorElevator.OutsidePanel();
            contentcontrol5.Content = extEle4;
            ExteriorElevator.OutsidePanel extEle5 = new ExteriorElevator.OutsidePanel();
            contentcontrol6.Content = extEle5;

            //Test Values
            for (int i = 1; i < 5; i++)
            {
                elevator.Add(new Elevator() { ElevatorID = i, Status = true, FloorLock1 = false, FloorLock2 = false, FloorLock3 = false, FloorLock4 = false, FloorLock5 = false, CurrentFloor = 1, DoorOpen = false, Queue1 = 0, Queue2 = 0, Queue3 = 0, Queue4 = 0, Queue5 = 0, TripCount = 0, FloorsTraveled = 0, EmergencyStop = false });
            }
            string[] selections = { "1", "2", "3", "4", "5" };
            for (int i = 0; i < selections.Length; i++)
            {
                comboBoxFloor.Items.Add(selections[i]);
                el1CallSelect.Items.Add(selections[i]);
                el2CallSelect.Items.Add(selections[i]);
                el3CallSelect.Items.Add(selections[i]);
                el4CallSelect.Items.Add(selections[i]);
                if (i < 4)
                {
                    comboBoxElevator.Items.Add(selections[i]);
                }
            }
            controller = new Controller(elevator);

            updateView(controller.getState()); //update the GUI
        }     

        /// <summary>
        /// MASTER CLICK EVENTS
        /// </summary>
        
        private void masterUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            if (elevatorSelection != 0) { 
                controller.unlockElevator(elevatorSelection);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Select the elevator you want to lock.");
            }
        }

        private void masterLockElevator_Click(object sender, RoutedEventArgs e)
        {
            if (elevatorSelection != 0)
            {
                controller.lockElevator(elevatorSelection);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Select the elevator you want to unlock.");
            }
        }

        private void masterCloseFloor_Click(object sender, RoutedEventArgs e)
        {
            if (floorSelection != 0)
            {
                controller.closeFloor(floorSelection);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Please select the floor you want to close.");
            }
        }

        private void masterOpenFloor_Click(object sender, RoutedEventArgs e)
        {
            if (floorSelection != 0)
            {
                controller.openFloor(floorSelection);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Please select the floor you want to open.");
            }
        }

        private void masterEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            controller.emergencyStop(0);
            updateView(controller.getState());
        }

        private void masterStartElevators_Click(object sender, RoutedEventArgs e)
        {
            controller.startElevators();
            updateView(controller.getState());
        }

        /// <summary>
        /// TAB 1 CLICK EVENTS
        /// </summary>
        private void tabOneUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.unlockElevator(1);
            updateView(controller.getState());
        }

        private void tabOneLockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.lockElevator(1);
            updateView(controller.getState());
        }

        private void tabOneEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            controller.emergencyStop(1);
            updateView(controller.getState());
        }

        private void tabOneCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el1Call != 0) {
                controller.callElevatorToFloor(1, el1Call);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 1.");
            }
        }

        private void tabOnePush1_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(1, 1);
            updateView(controller.getState());
        }

        private void tabOnePush2_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(1, 2);
            updateView(controller.getState());
        }

        private void tabOnePush3_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(1, 3);
            updateView(controller.getState());
        }

        private void tabOnePush4_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(1, 4);
            updateView(controller.getState());
        }

        private void tabOnePush5_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(1, 5);
            updateView(controller.getState());
        }

        private void tabOneCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.closeDoor(1);
            updateView(controller.getState());
        }

        private void tabOneOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.openDoor(1);
            updateView(controller.getState());
        }

        /// <summary>
        /// TAB 2 CLICK EVENTS
        /// </summary>
        private void tabTwoUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.unlockElevator(2);
            updateView(controller.getState());
        }

        private void tabTwoLockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.lockElevator(2);
            updateView(controller.getState());
        }

        private void tabTwoEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            controller.emergencyStop(2);
            updateView(controller.getState());
        }

        private void tabTwoCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el2Call != 0)
            {
                controller.callElevatorToFloor(2, el2Call);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 2.");
            }
            
        }

        private void tabTwoPush1_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(2, 1);
            updateView(controller.getState());
        }

        private void tabTwoPush2_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(2, 2);
            updateView(controller.getState());
        }

        private void tabTwoPush3_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(2, 3);
            updateView(controller.getState());
        }

        private void tabTwoPush4_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(2, 4);
            updateView(controller.getState());
        }

        private void tabTwoPush5_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(2, 5);
            updateView(controller.getState());
        }

        private void tabTwoCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.closeDoor(2);
            updateView(controller.getState());
        }

        private void tabTwoOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.openDoor(2);
            updateView(controller.getState());
        }

        /// <summary>
        /// TAB 3 CLICK EVENTS
        /// </summary>
        private void tabThreeUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.unlockElevator(3);
            updateView(controller.getState());
        }

        private void tabThreeLockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.lockElevator(3);
            updateView(controller.getState());
        }

        private void tabThreeEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            controller.emergencyStop(3);
            updateView(controller.getState());
        }

        private void tabThreeCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el3Call != 0)
            {
                controller.callElevatorToFloor(3, el3Call);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 3.");
            }
        }

        private void tabThreePush1_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(3, 1);
            updateView(controller.getState());
        }

        private void tabThreePush2_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(3, 2);
            updateView(controller.getState());
        }

        private void tabThreePush3_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(3, 3);
            updateView(controller.getState());
        }

        private void tabThreePush4_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(3, 4);
            updateView(controller.getState());
        }

        private void tabThreePush5_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(3, 5);
            updateView(controller.getState());
        }

        private void tabThreeCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.closeDoor(3);
            updateView(controller.getState());
        }

        private void tabThreeOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.openDoor(3);
            updateView(controller.getState());
        }

        /// <summary>
        /// TAB 4 CLICK EVENTS
        /// </summary>
        private void tabFourUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.unlockElevator(4);
            updateView(controller.getState());
        }

        private void tabFourLockElevator_Click(object sender, RoutedEventArgs e)
        {
            controller.lockElevator(4);
            updateView(controller.getState());
        }

        private void tabFourEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            controller.emergencyStop(4);
            updateView(controller.getState());
        }

        private void tabFourCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el4Call != 0)
            {
                controller.callElevatorToFloor(4, el4Call);
                updateView(controller.getState());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 4.");
            }
        }

        private void tabFourPush1_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(4, 1);
            updateView(controller.getState());
        }

        private void tabFourPush2_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(4, 2);
            updateView(controller.getState());
        }

        private void tabFourPush3_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(4, 3);
            updateView(controller.getState());
        }

        private void tabFourPush4_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(4, 4);
            updateView(controller.getState());
        }

        private void tabFourPush5_Click(object sender, RoutedEventArgs e)
        {
            controller.pushButton(4, 5);
            updateView(controller.getState());
        }

        private void tabFourCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.closeDoor(4);
            updateView(controller.getState());
        }

        private void tabFourOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            controller.openDoor(4);
            updateView(controller.getState());
        }


        /// <summary>
        /// Static Method UpdateView
        /// </summary>
        

        public void updateView(List<Elevator> temp)
        {

            foreach (Elevator row in temp)
            {
                switch (row.ElevatorID) {
                    case 1:
                        string locked = "Yes";
                        if (!row.FloorLock1 || !row.FloorLock2 || !row.FloorLock3 || !row.FloorLock4 || !row.FloorLock5)
                        {
                            locked = "No";
                        }
                        mainEl1Status.Content = "Status: " + row.Status +"\nElevator Locked: " + locked;
                        if (row.FloorLock1) {
                            el1statusfl1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el1statusfl1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status) {
                            el1statusfl1.Source = new BitmapImage(new Uri(path +  "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el1statusfl2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el1statusfl2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusfl2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el1statusfl3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el1statusfl3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusfl3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el1statusfl4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el1statusfl4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusfl4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el1statusfl5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el1statusfl5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusfl5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }


                        if (row.FloorLock1)
                        {
                            el1statusflt1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el1statusflt1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusflt1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el1statusflt2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el1statusflt2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusflt2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el1statusflt3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el1statusflt3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusflt3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el1statusflt4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el1statusflt4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusflt4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el1statusflt5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el1statusflt5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el1statusflt5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        switch (row.CurrentFloor)
                        {
                            case 1:
                                elevatorimage1.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                elevatorimaget1.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                break;
                            case 2:
                                elevatorimage1.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                elevatorimaget1.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                break;
                            case 3:
                                elevatorimage1.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                elevatorimaget1.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                break;
                            case 4:
                                elevatorimage1.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                elevatorimaget1.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                break;
                            case 5:
                                elevatorimage1.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                elevatorimaget1.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                break;
                        }
                        tabEl1Status.Content = "Elevator Data:\nElevator Status: " + row.Status + "\nNumber of Trips this session: " + row.TripCount + "\nFloors traveled: " + row.FloorsTraveled + "\nDoor open: " + row.DoorOpen;
                        break;
                    case 2:
                        locked = "Yes";
                        if (!row.FloorLock1 || !row.FloorLock2 || !row.FloorLock3 || !row.FloorLock4 || !row.FloorLock5)
                        {
                            locked = "No";
                        }
                        mainEl2Status.Content = "Status: " + row.Status + "\nElevator Locked: " + locked;
                        if (row.FloorLock1)
                        {
                            el2statusfl1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el2statusfl1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusfl1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el2statusfl2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el2statusfl2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusfl2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el2statusfl3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el2statusfl3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusfl3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el2statusfl4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el2statusfl4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusfl4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el2statusfl5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el2statusfl5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusfl5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }


                        if (row.FloorLock1)
                        {
                            el2statusflt1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el2statusflt1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusflt1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el2statusflt2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el2statusflt2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusflt2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el2statusflt3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el2statusflt3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusflt3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el2statusflt4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el2statusflt4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusflt4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el2statusflt5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el2statusflt5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el2statusflt5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        switch (row.CurrentFloor)
                        {
                            case 1:
                                elevatorimage2.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                elevatorimaget2.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                break;
                            case 2:
                                elevatorimage2.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                elevatorimaget2.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                break;
                            case 3:
                                elevatorimage2.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                elevatorimaget2.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                break;
                            case 4:
                                elevatorimage2.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                elevatorimaget2.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                break;
                            case 5:
                                elevatorimage2.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                elevatorimaget2.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                break;
                        }
                        tabEl2Status.Content = "Elevator Data:\nElevator Status: " + row.Status + "\nNumber of Trips this session: " + row.TripCount + "\nFloors traveled: " + row.FloorsTraveled + "\nDoor open: " + row.DoorOpen;
                        break;
                    case 3:
                        locked = "Yes";
                        if (!row.FloorLock1 || !row.FloorLock2 || !row.FloorLock3 || !row.FloorLock4 || !row.FloorLock5)
                        {
                            locked = "No";
                        }
                        mainEl3Status1.Content = "Status: " + row.Status + "\nElevator Locked: " + locked;
                        if (row.FloorLock1)
                        {
                            el3statusfl1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el3statusfl1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusfl1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el3statusfl2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el3statusfl2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusfl2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el3statusfl3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el3statusfl3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusfl3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el3statusfl4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el3statusfl4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusfl4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el3statusfl5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el3statusfl5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusfl5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }


                        if (row.FloorLock1)
                        {
                            el3statusflt1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el3statusflt1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusflt1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el3statusflt2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el3statusflt2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusflt2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el3statusflt3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el3statusflt3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusflt3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el3statusflt4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el3statusflt4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusflt4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el3statusflt5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el3statusflt5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el3statusflt5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        switch (row.CurrentFloor)
                        {
                            case 1:
                                elevatorimage3.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                elevatorimaget3.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                break;
                            case 2:
                                elevatorimage3.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                elevatorimaget3.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                break;
                            case 3:
                                elevatorimage3.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                elevatorimaget3.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                break;
                            case 4:
                                elevatorimage3.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                elevatorimaget3.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                break;
                            case 5:
                                elevatorimage3.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                elevatorimaget3.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                break;
                        }
                        tabEl3Status.Content = "Elevator Data:\nElevator Status: " + row.Status + "\nNumber of Trips this session: " + row.TripCount + "\nFloors traveled: " + row.FloorsTraveled + "\nDoor open: " + row.DoorOpen;
                        break;
                    case 4:
                        locked = "Yes";
                        if (!row.FloorLock1 || !row.FloorLock2 || !row.FloorLock3 || !row.FloorLock4 || !row.FloorLock5)
                        {
                            locked = "No";
                        }
                        mainEl4Status1.Content = "Status: " + row.Status + "\nElevator Locked: " + locked;
                        if (row.FloorLock1)
                        {
                            el4statusfl1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el4statusfl1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusfl1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el4statusfl2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el4statusfl2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusfl2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el4statusfl3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el4statusfl3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusfl3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el4statusfl4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el4statusfl4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusfl4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el4statusfl5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el4statusfl5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusfl5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }


                        if (row.FloorLock1)
                        {
                            el4statusflt1.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock1)
                        {
                            el4statusflt1.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusflt1.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock2)
                        {
                            el4statusflt2.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock2)
                        {
                            el4statusflt2.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusflt2.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock3)
                        {
                            el4statusflt3.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock3)
                        {
                            el4statusflt3.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusflt3.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock4)
                        {
                            el4statusflt4.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock4)
                        {
                            el4statusflt4.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusflt4.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        if (row.FloorLock5)
                        {
                            el4statusflt5.Source = new BitmapImage(new Uri(path + "locked.png"));
                        }
                        if (!row.FloorLock5)
                        {
                            el4statusflt5.Source = new BitmapImage(new Uri(path + "good.png"));
                        }
                        if (!row.Status)
                        {
                            el4statusflt5.Source = new BitmapImage(new Uri(path + "bad.png"));
                        }
                        switch (row.CurrentFloor)
                        {
                            case 1:
                                elevatorimage4.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                elevatorimaget4.Source = new BitmapImage(new Uri(path + "floor1.png"));
                                break;
                            case 2:
                                elevatorimage4.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                elevatorimaget4.Source = new BitmapImage(new Uri(path + "floor2.png"));
                                break;
                            case 3:
                                elevatorimage4.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                elevatorimaget4.Source = new BitmapImage(new Uri(path + "floor3.png"));
                                break;
                            case 4:
                                elevatorimage4.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                elevatorimaget4.Source = new BitmapImage(new Uri(path + "floor4.png"));
                                break;
                            case 5:
                                elevatorimage4.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                elevatorimaget4.Source = new BitmapImage(new Uri(path + "floor5.png"));
                                break;
                        }
                        tabEl1Status1.Content = "Elevator Data:\nElevator Status: " + row.Status + "\nNumber of Trips this session: " + row.TripCount + "\nFloors traveled: " + row.FloorsTraveled + "\nDoor open: " + row.DoorOpen;
                        break;
                }
            }
        }

        private void comboBox_ElevatorSelection(object sender, SelectionChangedEventArgs e)
        {
            elevatorSelection = Int32.Parse("" + comboBoxElevator.SelectedValue);
        }

        private void comboBox_FloorSelection(object sender, SelectionChangedEventArgs e)
        {
            floorSelection = Int32.Parse("" + comboBoxFloor.SelectedValue);
        }

        private void el1CallSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            el1Call = Int32.Parse("" + el1CallSelect.SelectedValue);
        }

        private void el2CallSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            el2Call = Int32.Parse("" + el2CallSelect.SelectedValue);
        }

        private void el3CallSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            el3Call = Int32.Parse("" + el3CallSelect.SelectedValue);
        }

        private void el4CallSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            el4Call = Int32.Parse("" + el4CallSelect.SelectedValue);
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }



    //Test Controller
    public class Controller
    {
        public event EventHandler<Window> ElevatorInfo;

        private static List<Elevator> elevators = new List<Elevator>(); //copy to pass

        public Controller(List<Elevator> temp) //pass initial test variables
        {
            elevators = temp;
        }

        public List<Elevator> getState()
        {
            return elevators;
        }

        internal void callElevatorToFloor(int elevatorid, int floor)
        {
            closeDoor(elevatorid);
            foreach (Elevator row in elevators)
            {
                if (row.ElevatorID == elevatorid)
                {
                    row.FloorsTraveled += Math.Abs(row.CurrentFloor - floor);
                    row.TripCount = row.TripCount + 1;
                    row.CurrentFloor = floor;
                }
            }
        }

        internal void addQueue(List<Elevator> elevators)
        {

        }

        internal void processQueue(List<Elevator> elevators)
        {
            foreach (Elevator row in elevators)
            {
                row.Queue1 = row.Queue2;
                row.Queue2 = row.Queue3;

            }
               
        }

        internal void closeDoor(int elevatorid)
        {
            Console.WriteLine();
            foreach (Elevator row in elevators)
            {
                if (row.ElevatorID == elevatorid)
                {
                    row.DoorOpen = false;
                }
            }
        }

        internal void closeFloor(int floorSelection)
        {            
            foreach (Elevator row in elevators)
            {
                switch (floorSelection)
                {                    
                    case 1:
                        row.FloorLock1 = true;
                        break;
                    case 2:
                        row.FloorLock2 = true;
                        break;
                    case 3:
                        row.FloorLock3 = true;
                        break;
                    case 4:
                        row.FloorLock4 = true;
                        break;
                    case 5:
                        row.FloorLock5 = true;
                        break;
                }

            }
        }

        internal void emergencyStop(int elevatorid)
        {
            if (elevatorid == 0)
            {
                foreach (Elevator row in elevators)
                {
                    row.Status = false;
                    row.EmergencyStop = true;
                }
            }
            else {
                foreach (Elevator row in elevators)
                {
                    if (row.ElevatorID == elevatorid)
                    {
                        row.Status = false;
                        row.EmergencyStop = true;
                    }
                }
            }
        }

        internal void lockElevator(int elevatorid)
        {
            foreach (Elevator row in elevators)
            {
                if (row.ElevatorID == elevatorid)
                {
                    row.FloorLock1 = row.FloorLock2 = row.FloorLock3 = row.FloorLock4 = row.FloorLock5 = true;
                }
            }
        }

        internal void openDoor(int elevatorid)
        {
            foreach (Elevator row in elevators)
            {
                if (row.ElevatorID == elevatorid)
                {
                    row.DoorOpen = true;
                }
            }
        }

        internal void openFloor(int floorSelection)
        {
            foreach (Elevator row in elevators)
            {
                switch (floorSelection)
                {
                    case 1:
                        row.FloorLock1 = false;
                        break;
                    case 2:
                        row.FloorLock2 = false;
                        break;
                    case 3:
                        row.FloorLock3 = false;
                        break;
                    case 4:
                        row.FloorLock4 = false;
                        break;
                    case 5:
                        row.FloorLock5 = false;
                        break;
                }                
            }
        }

        internal void pushButton(int elevatorid, int floor)
        {
            closeDoor(elevatorid);
            callElevatorToFloor(elevatorid, floor);
        }

        internal void startElevators()
        {
            foreach (Elevator row in elevators)
            {
                row.Status = true;
                row.EmergencyStop = false;
            }
        }

        public void unlockElevator(int elevatorSelection)
        {            
            foreach (Elevator row in elevators)
            {
                if (row.ElevatorID == elevatorSelection)
                {
                    row.FloorLock1 = row.FloorLock2 = row.FloorLock3 = row.FloorLock4 = row.FloorLock5 = false;
                }
            }
        }
    }

    public class Elevator
    {
        public int ElevatorID { get; set; }
        public Boolean Status { get; set; }
        public Boolean FloorLock1 { get; set;  }
        public Boolean FloorLock2 { get; set; }
        public Boolean FloorLock3 { get; set; }
        public Boolean FloorLock4 { get; set; }
        public Boolean FloorLock5 { get; set; }
        public int CurrentFloor { get; set; }
        public Boolean DoorOpen { get; set; }
        public int Queue1 { get; set; }
        public int Queue2 { get; set; }
        public int Queue3 { get; set; }
        public int Queue4 { get; set; }
        public int Queue5 { get; set; }
        public int TripCount { get; set; }
        public int FloorsTraveled { get; set;  }
        public Boolean EmergencyStop { get; set; }
    }
    
}
