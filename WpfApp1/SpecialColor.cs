using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1{
    public class SpecialColor{
        //fields
        private static Color Transparent = Color.FromArgb(0, 0, 0, 0);
        private static Color MainBack = Color.FromRgb(239, 239, 239);
        private static Color MainBackHover = Color.FromRgb(228, 228, 228);
        private static Color MainBlue = Color.FromRgb(62, 148, 209);
        private static Color Red = Color.FromRgb(255, 64, 64);
        private static Color Green = Color.FromRgb(54, 214, 149);
        private static Color White = Color.FromRgb(255, 255, 255);
        private static Color Black = Color.FromRgb(0, 0, 0);
        private static Color Placeholder = Color.FromArgb(100, 0, 0, 0);

        //getters
        //return color from format 'Color.FromRgb'
        public static SolidColorBrush getColor(Color color){
            return new SolidColorBrush(color);
        }
        //return Transparent
        public static SolidColorBrush transparent(){
            return new SolidColorBrush(Transparent);
        }
        //return MainBack
        public static SolidColorBrush mainBack(){
            return new SolidColorBrush(MainBack);
        }
        //return MainBackHover
        public static SolidColorBrush mainBackHover(){
            return new SolidColorBrush(MainBackHover);
        }
        //return MainBlue
        public static SolidColorBrush mainBlue(){
            return new SolidColorBrush(MainBlue);
        }
        //return Red
        public static SolidColorBrush red(){
            return new SolidColorBrush(Red);
        }
        //return Green
        public static SolidColorBrush green(){
            return new SolidColorBrush(Green);
        }
        //return White
        public static SolidColorBrush white(){
            return new SolidColorBrush(White);
        }
        //return Black
        public static SolidColorBrush black(){
            return new SolidColorBrush(Black);
        }
        //return Placeholder
        public static SolidColorBrush placeholder(){
            return new SolidColorBrush(Placeholder);
        }
    }
}
