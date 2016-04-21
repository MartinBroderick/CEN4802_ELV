using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OutsideUITest.OutsideUI {
	class ButtonUI {

		string id;
		Button button;

		public ButtonUI(string id, Button button) {

			this.id = id;
			this.button = button;

		}

		private void lightButton() {
			if (this.button.Background == Brushes.Yellow) {
				this.button.ClearValue(Control.BackgroundProperty);
			}
			else {
				this.button.Background = Brushes.Yellow;
			}
		}

	}
}
