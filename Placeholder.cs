using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1 {
    public static class Placeholder {
        public static void add(this TextBox textbox, string placeholder) {
            //base styles
            textbox.Text = placeholder;
            textbox.Foreground = SpecialColor.placeholder();

            //effect on focus (toggle placeholder)
            textbox.GotFocus += (s, e) => {
                if (textbox.Text == placeholder) {
                    placeholderChanger(textbox, SpecialColor.black(), "");
                }
            };
            textbox.LostFocus += (s, e) => {
                if (textbox.Text == "") {
                    placeholderChanger(textbox, SpecialColor.placeholder(), placeholder);
                }
            };
        }

        //add color styles for placeholder
        private static void placeholderChanger(TextBox textbox, SolidColorBrush color, string text) {
            textbox.Foreground = color;
            textbox.Text = text;
        }
    }
}
