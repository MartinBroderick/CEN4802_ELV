using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Timers;

namespace OutsideUITest.OutsideUI {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class OutsidePanel : UserControl {

		OutsideMainPanel mainPanel;
		Random random = new Random();
		Button lastButtonPushed;

		int floor = 3;

		public OutsidePanel() {
			InitializeComponent();
			
			Display[] displays = new Display[4];
			ButtonUI[] buttons = new ButtonUI[2];


			displays[0] = new Display(floor, random.Next(1, 6), display0);
			displays[1] = new Display(floor, random.Next(1, 6), display1);
			displays[2] = new Display(floor, random.Next(1, 6), display2);
			displays[3] = new Display(floor, random.Next(1, 6), display3);

			buttons[0] = new ButtonUI("up", upButton);
			buttons[1] = new ButtonUI("down", downButton);

			mainPanel = new OutsideMainPanel(floor, displays, buttons);

		}

		private void Button_Click(object sender, RoutedEventArgs e) {

			Button btn = sender as Button;

			illuminateButton(btn);

			lastButtonPushed = btn;

			//testing

			//updateDisplay(3, 2, 1);

			//updateDisplay(2000, btn);

			//main class request call;




		}

		private void illuminateButton(Button btn) {
			if (btn.Background == Brushes.Yellow) {
				btn.ClearValue(Control.BackgroundProperty);
			}
			else {
				btn.Background = Brushes.Yellow;
			}
		}

		private void updateDisplay(int selFloor, int selElevator, int floorUpdate) { //async

			//for testing
			//int timeToDelay = 2000;

			int adjustedFloor = selFloor - 1;
			int adjustedElevator = selElevator - 1;
			Display display = this.mainPanel.displays[adjustedElevator];

			/*int position = display.getLocation();
			if (floor > position) {
				while (floor > position) {
					await Task.Delay(timeToDelay);
					position += 1;
					display.updateDisplay(position);
				}

			}
			else if (floor < position) {
				while (floor < position) {
					await Task.Delay(timeToDelay);
					position -= 1;
					display.updateDisplay(position);
				}
			}*/

			display.updateDisplay(floorUpdate);

			if (selFloor == floorUpdate) {
				illuminateButton(lastButtonPushed);
				MessageBox.Show("Elevator " + (selFloor + 1) + " has arrived at the floor.");
			}

		}
	}
}
