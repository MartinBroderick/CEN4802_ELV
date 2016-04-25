using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldView.ExteriorElevator
{
	class OutsideMainPanel {

		public Display[] displays = new Display[4];
		public ButtonUI[] buttons = new ButtonUI[2];
		private int floor;

		public OutsideMainPanel(int floor, Display[] displays, ButtonUI[] buttons) {
			this.floor = floor;
			this.displays = displays;
			this.buttons = buttons;
		}

		public void updateDisplay(int displayNum, int floorNum) {
			this.displays[displayNum].updateDisplay(floorNum);
		}

	}
}
