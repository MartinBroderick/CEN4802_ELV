//  Project:    Inside Elevator GUI
//  Authors:    Christopher Fernandez / Dustin Ellsworth
//  Date:       3/7/2016

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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
        private async void addFloorToQue( int floorToAdd )
        {
            // notify Controller or worldView class to add floor to que
                                                                                // ** FOR SIMULATION ONLY **
                                                                                await Task.Delay(3000);
                                                                                runIt(floorToAdd);
                                                                                // ** FOR SIMULATION ONLY **
        }

        /// <summary>
        /// notify hardware to open door. deselects buttons after 1 second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void rb6_Checked(object sender, RoutedEventArgs e)
        {
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
            await Task.Delay(1000);
            rb8.IsChecked = false;
        }

        /// <summary>
        /// gets the current floor from hardware/controller/worldview
        /// and lights up its corresponding button in the gui
        /// </summary>
        /// <param name="currentFloor"></param>
        private void getCurrentFloor(Button currentFloor)
        {
            // reset the color of the last selected floor back to grey
            onFloor1.Background = Brushes.LightGray;
            onFloor2.Background = Brushes.LightGray;
            onFloor3.Background = Brushes.LightGray;
            onFloor4.Background = Brushes.LightGray;
            onFloor5.Background = Brushes.LightGray;
            // set the button for the floor passed in to grey
            currentFloor.Background = Brushes.CornflowerBlue;

                                                                                // ** FOR SIMULATION ONLY **
                                                                                if (currentFloor == onFloor1) {
                                                                                    floorReached(rb1);
                                                                                }
                                                                                else if (currentFloor == onFloor2)
                                                                                {
                                                                                    floorReached(rb2);
                                                                                }
                                                                                else if (currentFloor == onFloor3)
                                                                                {
                                                                                    floorReached(rb3);
                                                                                }
                                                                                else if (currentFloor == onFloor4)
                                                                                {
                                                                                    floorReached(rb4);
                                                                                }
                                                                                else if (currentFloor == onFloor5)
                                                                                {
                                                                                    floorReached(rb5);
                                                                                }
                                                                                // ** FOR SIMULATION ONLY **
        }
                                                                                // ** FOR SIMULATION ONLY **
                                                                                public void runIt(int num)
                                                                                {
                                                                                    if (num == 1)
                                                                                    {
                                                                                        getCurrentFloor(onFloor1);
                                                                                    }
                                                                                    else if (num == 2)
                                                                                    {
                                                                                        getCurrentFloor(onFloor2);
                                                                                    }
                                                                                    else if (num == 3)
                                                                                    {
                                                                                        getCurrentFloor(onFloor3);
                                                                                    }
                                                                                    else if (num == 4)
                                                                                    {
                                                                                        getCurrentFloor(onFloor4);
                                                                                    }
                                                                                    else if (num == 5)
                                                                                    {
                                                                                        getCurrentFloor(onFloor5);
                                                                                    }
                                                                                }
                                                                                // ** FOR SIMULATION ONLY **
    }
}
