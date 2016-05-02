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
        public static string path = (System.AppDomain.CurrentDomain.BaseDirectory);

        public MainWindow()
        {
            InitializeComponent(); //initialize the GUI interface
            //var elevator = new List<Elevator>(); //create a temporary List item of the elevator systems current state
            //Test Values
            MainWindow main = this;
            welcome();

            string[] selections = { "1", "2", "3", "4", "5" };
            for (int i = 0; i < selections.Length; i++)
            {
                el1CallSelect.Items.Add(selections[i]);
                el2CallSelect.Items.Add(selections[i]);
                el3CallSelect.Items.Add(selections[i]);
                el4CallSelect.Items.Add(selections[i]);
                if (i < 4)
                {
                    comboBoxElevator.Items.Add(selections[i]);
                }
            }

            updateView(getStates()); //update the GUI
        }
        
        // **SIMULATION PURPOSES ONLY..**
        //internal string Status1
        //{
        //    get { return tabEl1Status.Content.ToString(); }
        //    set { Dispatcher.Invoke(new Action(() => { tabEl1Status.Content = value; })); }
        //}

        //internal string Status2
        //{
        //    get { return tabEl2Status.Content.ToString(); }
        //    set { Dispatcher.Invoke(new Action(() => { tabEl2Status.Content = value; })); }
        //}

        //internal string Status3
        //{
        //    get { return tabEl3Status.Content.ToString(); }
        //    set { Dispatcher.Invoke(new Action(() => { tabEl3Status.Content = value; })); }
        //}

        //internal string Status4
        //{
        //    get { return tabEl4Status.Content.ToString(); }
        //    set { Dispatcher.Invoke(new Action(() => { tabEl4Status.Content = value; })); }
        //}

        /// <summary>
        /// MASTER CLICK EVENTS
        /// </summary>

        private void masterUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxElevator.SelectedIndex != -1) {
                hardwareElevators[comboBoxElevator.SelectedIndex].in_Service = true;
                updateView(getStates());
            }
            else
            {
                MessageBox.Show("Select the elevator you want to lock.");
            }
        }

        private void masterLockElevator_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxElevator.SelectedIndex != -1)
            {
                hardwareElevators[comboBoxElevator.SelectedIndex].in_Service = false;
                updateView(getStates());
            }
            else
            {
                MessageBox.Show("Select the elevator you want to unlock.");
            }
        }
        
        private void masterEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < hardwareElevators.Length; i++)
            {
                hardwareElevators[i].in_Service = false;
            }
            updateView(getStates());
        }

        private void masterStartElevators_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < hardwareElevators.Length; i++)
            {
                hardwareElevators[i].in_Service = true;
            }
            updateView(getStates());
        }

        /// <summary>
        /// TAB 1 CLICK EVENTS
        /// </summary>
        private void tabOneUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].in_Service = true;
            updateView(getStates());
        }

        private void tabOneEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].in_Service = false;
            updateView(getStates());
        }

        private void tabOneCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el1CallSelect.SelectedIndex != -1) {
                hardwareElevators[0].MoveToFloor(el1CallSelect.SelectedIndex + 1);
                updateView(getStates());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 1.");
            }
        }

        private void tabOnePush1_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].MoveToFloor(1);
            updateView(getStates());
        }

        private void tabOnePush2_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].MoveToFloor(2);
            updateView(getStates());
        }

        private void tabOnePush3_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].MoveToFloor(3);
            updateView(getStates());
        }

        private void tabOnePush4_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].MoveToFloor(4);
            updateView(getStates());
        }

        private void tabOnePush5_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].MoveToFloor(5);
            updateView(getStates());
        }

        private void tabOneCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[0].DoorCloseRoutine();
            updateView(getStates());
        }

        private void tabOneOpenDoor_Click(object sender, RoutedEventArgs e)
        {

            String simulate = tabEl1Status.Content.ToString().Replace("Closed", "Open");
            tabEl1Status.Content = simulate;
            hardwareElevators[0].DoorOpenRoutine();            
            updateView(getStates());            
        }

        /// <summary>
        /// TAB 2 CLICK EVENTS
        /// </summary>
        private void tabTwoUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].in_Service = true;
            updateView(getStates());
        }

        private void tabTwoEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].in_Service = false;
            updateView(getStates());
        }

        private void tabTwoCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el2CallSelect.SelectedIndex != -1)
            {
                hardwareElevators[1].MoveToFloor(el2CallSelect.SelectedIndex + 1);
                updateView(getStates());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 2.");
            }

        }

        private void tabTwoPush1_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].MoveToFloor(1);
            updateView(getStates());
        }

        private void tabTwoPush2_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].MoveToFloor(2);
            updateView(getStates());
        }

        private void tabTwoPush3_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].MoveToFloor(3);
            updateView(getStates());
        }

        private void tabTwoPush4_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].MoveToFloor(4);
            updateView(getStates());
        }

        private void tabTwoPush5_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].MoveToFloor(5);
            updateView(getStates());
        }

        private void tabTwoCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[1].DoorCloseRoutine();
            updateView(getStates());
        }

        private void tabTwoOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            String simulate = tabEl2Status.Content.ToString().Replace("Closed", "Open");
            tabEl2Status.Content = simulate;
            hardwareElevators[1].DoorOpenRoutine();
            updateView(getStates());
        }

        /// <summary>
        /// TAB 3 CLICK EVENTS
        /// </summary>
        private void tabThreeUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].in_Service = true;
            updateView(getStates());
        }

        private void tabThreeEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].in_Service = false;
            updateView(getStates());
        }

        private void tabThreeCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el3CallSelect.SelectedIndex != -1)
            {
                hardwareElevators[2].MoveToFloor(el3CallSelect.SelectedIndex + 1);
                updateView(getStates());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 3.");
            }
        }

        private void tabThreePush1_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].MoveToFloor(1);
            updateView(getStates());
        }

        private void tabThreePush2_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].MoveToFloor(2);
            updateView(getStates());
        }

        private void tabThreePush3_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].MoveToFloor(3);
            updateView(getStates());
        }

        private void tabThreePush4_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].MoveToFloor(4);
            updateView(getStates());
        }

        private void tabThreePush5_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].MoveToFloor(5);
            updateView(getStates());
        }

        private void tabThreeCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[2].DoorCloseRoutine();
            updateView(getStates());
        }

        private void tabThreeOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            String simulate = tabEl3Status.Content.ToString().Replace("Closed", "Open");
            tabEl3Status.Content = simulate;
            hardwareElevators[2].DoorOpenRoutine();
            updateView(getStates());
        }

        /// <summary>
        /// TAB 4 CLICK EVENTS
        /// </summary>
        private void tabFourUnlockElevator_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].in_Service = true;
            updateView(getStates());
        }

        private void tabFourEmergencyStop_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].in_Service = false;
            updateView(getStates());
        }

        private void tabFourCallElevator_Click(object sender, RoutedEventArgs e)
        {
            if (el4CallSelect.SelectedIndex != -1)
            {
                hardwareElevators[3].MoveToFloor(el4CallSelect.SelectedIndex + 1);
                updateView(getStates());
            }
            else
            {
                MessageBox.Show("Please select floor where you want to call elevator 4.");
            }
        }

        private void tabFourPush1_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].MoveToFloor(1);
            updateView(getStates());
        }

        private void tabFourPush2_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].MoveToFloor(2);
            updateView(getStates());
        }

        private void tabFourPush3_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].MoveToFloor(3);
            updateView(getStates());
        }

        private void tabFourPush4_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].MoveToFloor(4);
            updateView(getStates());
        }

        private void tabFourPush5_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].MoveToFloor(5);
            updateView(getStates());
        }

        private void tabFourCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            hardwareElevators[3].DoorCloseRoutine();
            updateView(getStates());
        }

        private void tabFourOpenDoor_Click(object sender, RoutedEventArgs e)
        {

            String simulate = tabEl4Status.Content.ToString().Replace("Closed", "Open");
            tabEl4Status.Content = simulate;
            hardwareElevators[3].DoorOpenRoutine();
            updateView(getStates());
        }


        /// <summary>
        /// Static Method UpdateView
        /// </summary>


        public void updateView(int[][] vatorStates)
        {
            Image[] vatorFloor = { elevatorimage1, elevatorimage2, elevatorimage3, elevatorimage4 };
            Image[] vatorTabFloor = { elevatorimaget1, elevatorimaget2, elevatorimaget3, elevatorimaget4 };
            Image[] vator1Service = { el1statusfl1, el1statusfl2, el1statusfl3, el1statusfl4, el1statusfl5 };
            Image[] vator2Service = { el2statusfl1, el2statusfl2, el2statusfl3, el2statusfl4, el2statusfl5 };
            Image[] vator3Service = { el3statusfl1, el3statusfl2, el3statusfl3, el3statusfl4, el3statusfl5 };
            Image[] vator4Service = { el4statusfl1, el4statusfl2, el4statusfl3, el4statusfl4, el4statusfl5 };
            Image[] vator1TabService = { el1statusflt1, el1statusflt2, el1statusflt3, el1statusflt4, el1statusflt5 };
            Image[] vator2TabService = { el2statusflt1, el2statusflt2, el2statusflt3, el2statusflt4, el2statusflt5 };
            Image[] vator3TabService = { el3statusflt1, el3statusflt2, el3statusflt3, el3statusflt4, el3statusflt5 };
            Image[] vator4TabService = { el4statusflt1, el4statusflt2, el4statusflt3, el4statusflt4, el4statusflt5 };
            Image[][] vatorsTabService = { vator1TabService, vator2TabService, vator3TabService, vator4TabService };
            Image[][] vatorsService = { vator1Service, vator2Service, vator3Service, vator4Service };
            
            //World View Update Label Controls Main/Tabs          
                if (vatorStates[0][2] == 1)
                {
                    mainEl1Status.Content = "Status: On" + Environment.NewLine + "Current Floor: " + vatorStates[0][0];
                    if (vatorStates[0][1] == 0)
                    {
                        tabEl1Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" + 
                        Environment.NewLine + "Current Floor: " + vatorStates[0][0] + Environment.NewLine + "Door Status: Open";
                    }else if(vatorStates[0][1] == 1)
                    {
                        tabEl1Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[0][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                else {
                    mainEl1Status.Content = "Status: Off" + Environment.NewLine + "Current Floor: " + vatorStates[0][0];
                    if (vatorStates[0][1] == 0)
                    {
                        tabEl1Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[0][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[0][1] == 1)
                    {
                        tabEl1Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[0][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                if (vatorStates[1][2] == 1)
                {
                    mainEl2Status.Content = "Status: On" + Environment.NewLine + "Current Floor: " + vatorStates[1][0];
                    if (vatorStates[1][1] == 0)
                    {
                        tabEl2Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[1][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[1][1] == 1)
                    {
                        tabEl2Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[1][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                else {
                    mainEl2Status.Content = "Status: Off" + Environment.NewLine + "Current Floor: " + vatorStates[1][0];
                    if (vatorStates[1][1] == 0)
                    {
                        tabEl2Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[1][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[1][1] == 1)
                    {
                        tabEl2Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[1][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                if (vatorStates[2][2] == 1)
                {
                    mainEl3Status.Content = "Status: On" + Environment.NewLine + "Current Floor: " + vatorStates[2][0];
                    if (vatorStates[2][1] == 0)
                    {
                        tabEl3Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[2][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[2][1] == 1)
                    {
                        tabEl3Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[2][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                else
                {                
                    mainEl3Status.Content = "Status: Off" + Environment.NewLine + "Current Floor: " + vatorStates[2][0];
                    if (vatorStates[2][1] == 0)
                    {
                        tabEl3Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[2][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[2][1] == 1)
                    {
                        tabEl3Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[2][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                if (vatorStates[3][2] == 1)
                {
                    mainEl4Status.Content = "Status: On" + Environment.NewLine + "Current Floor: " + vatorStates[3][0];
                    if (vatorStates[3][1] == 0)
                    {
                        tabEl4Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[3][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[3][1] == 1)
                    {
                        tabEl4Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Active" +
                        Environment.NewLine + "Current Floor: " + vatorStates[3][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
                else {
                    mainEl4Status.Content = "Status: Off" + Environment.NewLine + "Current Floor: " + vatorStates[3][0];
                    if (vatorStates[3][1] == 0)
                    {
                        tabEl4Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[3][0] + Environment.NewLine + "Door Status: Open";
                    }
                    else if (vatorStates[3][1] == 1)
                    {
                        tabEl4Status.Content = "Elevator Data:" + Environment.NewLine + "Elevator Status: Inactive" +
                        Environment.NewLine + "Current Floor: " + vatorStates[3][0] + Environment.NewLine + "Door Status: Closed";
                    }
                }
 

            for  (int i = 0; i < vatorStates.Length; i++) {
                if (vatorStates[i][2] == 1)
                {
                    for (int j = 0; j < vatorsService[i].Length; j++)
                    {
                        vatorsService[i][j].Source = new BitmapImage(new Uri(path + "good.png"));
                        vatorsTabService[i][j].Source = new BitmapImage(new Uri(path + "good.png"));
                    }
                }
                else
                {
                    for (int j = 0; j < vatorsService[i].Length; j++)
                    {
                        vatorsService[i][j].Source = new BitmapImage(new Uri(path + "bad.png"));
                        vatorsTabService[i][j].Source = new BitmapImage(new Uri(path + "bad.png"));
                    }
                }

                vatorFloor[i].Source = new BitmapImage(new Uri(path + "floor" + vatorStates[i][0] + ".png"));
                vatorTabFloor[i].Source = new BitmapImage(new Uri(path + "floor" + vatorStates[i][0] + ".png"));
            }
        }
        
        // Begin Interior Elevator Panel


        /// <summary>
        /// when a floor is reached deactivate the button on the
        /// gui for that floor
        /// </summary>
        /// <param name="theFloorReached"></param>
        public void floorReached(RadioButton theFloorReached)
        {
            theFloorReached.IsChecked = false;
        }

        /// <summary>
        /// when the first floor is selected on the gui. light up the button
        /// and add it to the que
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            addFloorToQue(1);
        }

        /// <summary>
        /// when the second floor is selected on the gui. light up the button
        /// and add it to the que
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            addFloorToQue(2);
        }

        /// <summary>
        /// when the third floor is selected on the gui. light up the button
        /// and add it to the que
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb3_Checked(object sender, RoutedEventArgs e)
        {
            addFloorToQue(3);
        }

        /// <summary>
        /// when the fourth floor is selected on the gui. light up the button
        /// and add it to the que
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb4_Checked(object sender, RoutedEventArgs e)
        {
            addFloorToQue(4);
        }

        /// <summary>
        /// when the fith floor is selected on the gui. light up the button
        /// and add it to the que
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb5_Checked(object sender, RoutedEventArgs e)
        {
            addFloorToQue(5);
            
        }

        /// <summary>
        /// calls another class to add the selected floor the list of selected floors
        /// </summary>
        /// <param name="floorToAdd"></param>
        private async void addFloorToQue(int floorToAdd)
        {
            // notify Controller or worldView class to add floor to que
            // ** FOR SIMULATION ONLY **
            await Task.Delay(1000);
            hardwareElevators[cmbElevator.SelectedIndex].MoveToFloor(floorToAdd);
            rb1.IsChecked = rb2.IsChecked = rb3.IsChecked = rb4.IsChecked = rb5.IsChecked = false;

        }

        /// <summary>
        /// notify hardware to open door. deselects buttons after 1 second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void rb6_Checked(object sender, RoutedEventArgs e)
        {
           if (cmbElevator.SelectedIndex == 0)
            {
               String simulate = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl1Status.Content.ToString().Replace("Closed", "Open");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl1Status.Content = simulate;
            }
            else if (cmbElevator.SelectedIndex == 1)
            {
                String simulate = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl2Status.Content.ToString().Replace("Closed", "Open");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl2Status.Content = simulate;
            }
            else if (cmbElevator.SelectedIndex == 2)
            {
                String simulate = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl3Status.Content.ToString().Replace("Closed", "Open");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl3Status.Content = simulate;
            }
            else if (cmbElevator.SelectedIndex == 3)
            {
                String simulate = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl4Status.Content.ToString().Replace("Closed", "Open");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl4Status.Content = simulate;
            }
            hardwareElevators[cmbElevator.SelectedIndex].DoorOpenRoutine();
            await Task.Delay(1000);
            rb6.IsChecked = false;
        }

        /// <summary>
        /// notify hardware to close door. deselects buttons after 1 second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void rb7_Checked(object sender, RoutedEventArgs e)
        {
            hardwareElevators[cmbElevator.SelectedIndex].DoorCloseRoutine();
            await Task.Delay(1000);
            rb7.IsChecked = false;
        }

        /// <summary>
        /// notify world view that there is an issue. deselects buttons after 1 second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void rb8_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("If you have a real emergency call the police you idiot. Otherwise, sit tight and be patient.");
            await Task.Delay(1000);
            rb8.IsChecked = false;
        }       

        // End Interior Elevator Panel

        // Begin Exterior Elevator Panel

        Display[] displays = new Display[4];
        ButtonUI[] buttons = new ButtonUI[2];        

        public void updateDisplay(int displayNum, int floorNum)
        {
            this.displays[displayNum].updateDisplay(floorNum);
        }

        class Display
        {

            private int floor;
            private int location;
            private Label display;

            public Display(int floor, int location, Label display)
            {
                this.floor = floor;
                this.location = location;
                this.display = display;
                this.display.Content = location;
            }

            public void updateDisplay(int floor)
            {
                this.location = floor;
                this.display.Content = floor;
            }

            public int getLocation()
            {
                return this.location;
            }

        }

        class ButtonUI
        {

            string id;
            Button button;

            public ButtonUI(string id, Button button)
            {

                this.id = id;
                this.button = button;

            }

            private void lightButton()
            {
                if (this.button.Background == Brushes.Yellow)
                {
                    this.button.ClearValue(Control.BackgroundProperty);
                }
                else
                {
                    this.button.Background = Brushes.Yellow;
                }
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Background = Brushes.Yellow;
            string closestElevator = "someElevatorThatIsCloseButNoTacos";
            int marker = 5;
            foreach (Elevator el in hardwareElevators)
            {
                if (Math.Abs(cmbFloor.SelectedIndex - el.currentFloor) < marker)
                {
                    marker = Math.Abs(cmbFloor.SelectedIndex - el.currentFloor);
                    closestElevator = el.Name;
                }
            }

            foreach (Elevator el in hardwareElevators)
            {
                if(el.Name == closestElevator) {
                    el.MoveToFloor(cmbFloor.SelectedIndex + 1);
                }
            }
            await Task.Delay(1000);

            ((Button)sender).ClearValue(Control.BackgroundProperty);
        }

        // End Exterior Panel

        // Begin Sark Hardware Component
        static Elevator[] hardwareElevators = { new Elevator("One"), new Elevator("Two"), new Elevator("Three"), new Elevator("Four") };
        public int[][] getStates()
        {
            int[][] states = new int[hardwareElevators.Length][];
            for (int i = 0; i < hardwareElevators.Length; i++)
            {
                states[i] = new int[3];
                states[i][0] = hardwareElevators[i].currentFloor;
                states[i][1] = hardwareElevators[i].doorState;
                states[i][2] = Convert.ToInt32(hardwareElevators[i].inService);
            }
            return states;
        }

        internal class Elevator
        {

            Random rnd = new Random();            

            private const int max_Capacity = 2000;
            private int current_Capacity;
            private string _Name, _Status;
            private int current_Floor;
            private int? next_Floor, last_Floor;
            public int doorState;
            public bool door_Clear, in_Service, _Occupied;

            public Elevator()
            {
                Name = "De-Fault";
                inService = true;
                currentCapacity = rnd.Next(50, 600);
                currentFloor = 1;
                nextFloor = 1;
                lastFloor = 1;
                doorState = 1;
                doorClear = true;
            }

            public Elevator(string name, int currentfloor)
            {
                Name = name;
                inService = true;
                currentCapacity = rnd.Next(50, 600);
                currentFloor = currentfloor;
                nextFloor = 1;
                lastFloor = 1;
                doorState = 1;
                doorClear = true;
            }

            public Elevator(string name)
            {
                Name = name;
                inService = true;
                currentCapacity = rnd.Next(50, 600);
                currentFloor = 1;
                nextFloor = 1;
                lastFloor = 1;
                doorState = 1;
                doorClear = true;
            }

            public int maxCapacity
            {
                get { return max_Capacity; }
            }

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            public string Status
            {
                get { return _Status; }
                set { _Status = value; }
            }

            public int currentFloor
            {
                get { return current_Floor; }
                set { current_Floor = value; }
            }

            public int? nextFloor
            {
                get { return next_Floor; }
                set { next_Floor = value; }
            }

            public int? lastFloor
            {
                get { return last_Floor; }
                set { last_Floor = value; }
            }

            public bool doorClear
            {
                get { return door_Clear; }
                set { door_Clear = value; }
            }

            public bool inService
            {
                get { return in_Service; }
                set { in_Service = value; }
            }

            public bool Occupied
            {
                get { return _Occupied; }
                set { _Occupied = value; }
            }

            public int currentCapacity
            {
                get { return current_Capacity; }
                set { current_Capacity = value; }
            }

            public void MoveToFloor(int nextfloor)
            {

                if (inService == false)
                {
                    MainWindow.sysMessage(this.Name + ": Elevator not in Service");
                }
                else if (currentFloor == nextfloor)
                {
                    MainWindow.sysMessage(this.Name + ":Elevator is already on the correct floor");
                }
                else
                {
                    nextFloor = nextfloor;
                    DoorOpenRoutine();
                    ElevatorMove();
                }
            }            

            private void IsOccupied()
            {
                if (currentCapacity >= 0)
                {
                    Occupied = true;
                }
                else
                {
                    Occupied = false;
                }
            }
            
            public async void DoorOpenRoutine()
            {
                IsOccupied();

                if (doorState != 0)
                {
                    if (doorState == 2)
                    {
                        MainWindow.sysMessage(this.Name + ":Elevator Door is open");
                        if (currentCapacity > maxCapacity)
                        {
                            currentCapacity = rnd.Next(50, 600);
                        }

                        // subtract from the elevator
                        currentCapacity = -rnd.Next(0, currentCapacity);
                        //  add people to the
                        currentCapacity = +rnd.Next(50, 600);
                        
                        DoorCloseRoutine();
                    }
                    else
                    {
                        MainWindow.sysMessage(this.Name + ":Elevator Door is opening");
                        DoorState(2);
                        await Task.Delay(1000);
                        MainWindow.sysMessage(this.Name + ":Elevator Door is open");

                        if (currentCapacity > maxCapacity)
                        {
                            currentCapacity = rnd.Next(50, 600);
                        }

                        // subtract from the elevator
                        currentCapacity = -rnd.Next(0, currentCapacity);
                        //  add people to the
                        currentCapacity = +rnd.Next(50, 600);
                        await Task.Delay(1000);
                        DoorCloseRoutine();
                    }

                }
                
                //access the MainWindow GUI directly...
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl1Status.Content = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl1Status.Content.ToString().Replace("Open", "Closed");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl2Status.Content = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl2Status.Content.ToString().Replace("Open", "Closed");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl3Status.Content = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl3Status.Content.ToString().Replace("Open", "Closed");
                ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl4Status.Content = ((MainWindow)System.Windows.Application.Current.MainWindow).tabEl4Status.Content.ToString().Replace("Open", "Closed");

                //send event to subscribed class...which is the MainWindow
                //main.Status1 = main.Status1.Replace("Open", "Closed");
                //main.Status2 = main.Status2.Replace("Open", "Closed");
                //main.Status3 = main.Status3.Replace("Open", "Closed");
                //main.Status4 = main.Status4.Replace("Open", "Closed");

            }

            public bool DoorClearCheck()
            {
                int tempSensor = rnd.Next(1, 10);
                if (tempSensor <= 9)
                {
                    return doorClear = true;
                }
                else
                {
                    return doorClear = false;
                }

            }

            public async void DoorCloseRoutine()
            {

                if (doorState != 0)
                {

                    MainWindow.sysMessage(this.Name + ":Elevator Door is Closing");
                    

                    if (DoorClearCheck() == true)
                    {
                        DoorState(1);
                        MainWindow.sysMessage(this.Name + ":Elevator Door is Closed");
                        
                    }
                    await Task.Delay(1000);
                    if (DoorClearCheck() == false)
                    {
                        DoorOpenRoutine();
                    }

                }
                IsOccupied();
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
                        MainWindow.sysMessage(this.Name + ":Door is locked.");

                        doorState = State;
                        break;

                    case 1: // Closed
                        MainWindow.sysMessage(this.Name + ":Door is closed.");

                        doorState = State;
                        break;

                    case 2: // Open
                        MainWindow.sysMessage(this.Name + ":Door Is Open.");

                        doorState = State;
                        break;

                    case 3: // Failed
                        MainWindow.sysMessage(this.Name + ":Door has Failed");

                        inService = false;
                        doorState = State;
                        break;

                    default: // 
                        MainWindow.sysMessage(this.Name + ":Critical Logic Error Occured, Closing Door");
                        
                        DoorState(1);
                        MoveToFloor(0);
                        ServiceToggle();
                        break;
                }
                return doorState;
            }

            public void ServiceToggle()
            {
                if (inService == true)
                {
                    inService = false;
                }
                else
                {
                    inService = true;
                }
            }

            private void FloorTime(int crrntFlr, int? nxtFlr)
            {
                if (nxtFlr == null)
                {
                    nxtFlr = 0;
                }

                int temp = (int)nxtFlr - crrntFlr;
                temp = Math.Abs(temp * 2);
            }

            private async void ElevatorMove()
            {
                this.lastFloor = this.currentFloor;

                sysMessage(this.Name + ":Elevator moving to " + nextFloor.ToString());
                await Task.Delay(1000);
                FloorTime(currentFloor, nextFloor);
                sysMessage(this.Name + ":Elevator is at floor " + nextFloor.ToString());
                await Task.Delay(1000);
                this.currentFloor = (int)nextFloor;
                sysMessage(this.Name + ":Elevator is Waiting at floor " + nextFloor.ToString());
                updateDisplays();
            }
        }

        // End Sark Hardware Component
      
        // Output to pseudo terminal
        public static void sysMessage(string message)
        {
            
            if (((MainWindow)System.Windows.Application.Current.MainWindow).consoleOutput.Items.Count > 15)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).consoleOutput.Items.RemoveAt(0);
            }
            ((MainWindow)System.Windows.Application.Current.MainWindow).consoleOutput.Items.Add(message);
        }

        public static void updateDisplays()
        {
            int[][] states = ((MainWindow)System.Windows.Application.Current.MainWindow).getStates();
            // Reset all to gray brackground
            ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor1.Background = Brushes.LightGray;
            ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor2.Background = Brushes.LightGray;
            ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor3.Background = Brushes.LightGray;
            ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor4.Background = Brushes.LightGray;
            ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor5.Background = Brushes.LightGray;


            int floor = states[((MainWindow)System.Windows.Application.Current.MainWindow).cmbElevator.SelectedIndex][0];
            if (floor == 1)
                ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor1.Background = Brushes.Yellow;
            else if (floor == 2)
                ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor2.Background = Brushes.Yellow;
            else if (floor == 3)
                ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor3.Background = Brushes.Yellow;
            else if (floor == 4)
                ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor4.Background = Brushes.Yellow;
            else if (floor == 5)
                ((MainWindow)System.Windows.Application.Current.MainWindow).onFloor5.Background = Brushes.Yellow;


            ((MainWindow)System.Windows.Application.Current.MainWindow).display0.Content = states[0][0];
            ((MainWindow)System.Windows.Application.Current.MainWindow).display1.Content = states[1][0];
            ((MainWindow)System.Windows.Application.Current.MainWindow).display2.Content = states[2][0];
            ((MainWindow)System.Windows.Application.Current.MainWindow).display3.Content = states[3][0];

            ((MainWindow)System.Windows.Application.Current.MainWindow).updateView(((MainWindow)System.Windows.Application.Current.MainWindow).getStates());
        }

        private void cmbElevator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                updateDisplays();
            } catch (Exception exc)
            {
                Console.Write(exc.ToString());
            }
        }

        private async void welcome()
        {
            sysMessage("Initializing Sark Hardware Components...");
            await Task.Delay(200);
            sysMessage("Loading SkyNet...");
            await Task.Delay(1000);
            sysMessage("Constructing Master Control Program...");
            await Task.Delay(1000);
            sysMessage("Instantiating Tacos for fun...");
            await Task.Delay(1000);
            for (int i = 0; i < 15; i++)
            {
                sysMessage("  Taco " + i + ": -- Generating taco shell");
                await Task.Delay(100);
                sysMessage("  Taco " + i + ": -- Generating taco meat filling");
                await Task.Delay(100);
                sysMessage("  Taco " + i + ": -- Generating taco toppings");
                await Task.Delay(100);
                sysMessage("  Taco " + i + ": -- Taco successfully instantiated!");
            }
            await Task.Delay(1000);
            sysMessage("Taco overload! Critical error!");
            sysMessage("Too many tacos! System crashing in...");
            for (int i = 20; i > 0; i--)
            {
                sysMessage("" + i);
                await Task.Delay(i * 5);
            }
            sysMessage("Elevators are online and operational.");
        }
    }
}
