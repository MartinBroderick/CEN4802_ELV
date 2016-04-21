using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsideUITest.OutsideUI {
	class Display {

		private int floor;
		private int location;
		private Label display;

		public Display(int floor, int location, Label display) {
			this.floor = floor;
			this.location = location;
			this.display = display;
			this.display.Content = location;
		}

		public void updateDisplay( int floor) {
			this.location = floor;
			this.display.Content = floor;
		}

		public int getLocation() {
			return this.location;
		}

	}
}
