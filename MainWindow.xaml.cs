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

namespace TestTrainingProgram{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        public MainWindow() {
            InitializeComponent();

            //=============mainStyle================
            mainLayoutStart.BorderBrush = SpecialColor.mainBlue();
            mainLayoutResults.BorderBrush = SpecialColor.mainBlue();
            mainLayoutExit.BorderBrush = SpecialColor.mainBlue();
            
            //Настройка размеров всего приложения
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

            //placeholders
            Placeholder.add(lastName, "Фамилия");
            Placeholder.add(firstName, "Имя");
            Placeholder.add(secondtName, "Отчество");
            Placeholder.add(group, "Группа");
            //=============mainStyle================
        }

        //=======================navBar=======================
        //navBarButton1 ' ТЕОРЕТИЧЕСКИЙ МАТЕРИАЛ
        //-----hoverEffect
        private void navBarButton1_MouseEnter(object sender, MouseEventArgs e){
            navBarButton1.Background = SpecialColor.mainBackHover();
        }
        private void navBarButton1_MouseLeave(object sender, MouseEventArgs e){
            navBarButton1.Background = SpecialColor.mainBack();
        }
        //-----hoverEffect

        //navBarButton2 ' СПРАВКА
        //-----hoverEffect
        private void navBarButton2_MouseEnter(object sender, MouseEventArgs e){
            navBarButton2.Background = SpecialColor.mainBackHover();
        }
        private void navBarButton2_MouseLeave(object sender, MouseEventArgs e){
            navBarButton2.Background = SpecialColor.mainBack();
        }
        //-----hoverEffect

        //navBarButton3 ' О ПРОГРАММЕ
        //-----hoverEffect
        private void navBarButton3_MouseEnter(object sender, MouseEventArgs e){
            navBarButton3.Background = SpecialColor.mainBackHover();
        }
        private void navBarButton3_MouseLeave(object sender, MouseEventArgs e){
            navBarButton3.Background = SpecialColor.mainBack();
        }
        //-----hoverEffect
        //=======================navBar=======================

        //=======================mainLayout=======================
        //mainLayoutStart
        //-----hoverEffect
        private void mainLayoutStart_MouseEnter(object sender, MouseEventArgs e){
            mainLayoutStart.Background = SpecialColor.mainBlue();
            mainLayoutStart.Foreground = SpecialColor.white();
        }
        private void mainLayoutStart_MouseLeave(object sender, MouseEventArgs e) {
            mainLayoutStart.Background = SpecialColor.mainBack();
            mainLayoutStart.Foreground = SpecialColor.mainBlue();
        }
        //-----hoverEffect

        //mainLayoutResults
        //-----hoverEffect
        private void mainLayoutResults_MouseEnter(object sender, MouseEventArgs e){
            mainLayoutResults.Background = SpecialColor.mainBlue();
            mainLayoutResults.Foreground = SpecialColor.white();
        }
        private void mainLayoutResults_MouseLeave(object sender, MouseEventArgs e){
            mainLayoutResults.Background = SpecialColor.mainBack();
            mainLayoutResults.Foreground = SpecialColor.mainBlue();
        }
        //-----hoverEffect

        //mainLayoutExit
        //-----hoverEffect
        private void mainLayoutExit_MouseEnter(object sender, MouseEventArgs e){
            mainLayoutExit.Background = SpecialColor.mainBlue();
            mainLayoutExit.Foreground = SpecialColor.white();
        }
        private void mainLayoutExit_MouseLeave(object sender, MouseEventArgs e){
            mainLayoutExit.Background = SpecialColor.mainBack();
            mainLayoutExit.Foreground = SpecialColor.mainBlue();
        }
        //-----hoverEffect

        //recordBlock
        //-----hoverEffect
        private void recordBlock_MouseEnter(object sender, MouseEventArgs e){
            recordLabel.Foreground = SpecialColor.white();
            recordBlock.Background = SpecialColor.mainBlue();
        }
        private void recordBlock_MouseLeave(object sender, MouseEventArgs e){
            recordLabel.Foreground = SpecialColor.mainBlue();
            recordBlock.Background = SpecialColor.transparent();
        }
        //-----hoverEffect

        //------saveData
        //-----hoverEffect
        private void saveData_MouseEnter(object sender, MouseEventArgs e){
            saveData.Background = SpecialColor.mainBackHover();
        }
        private void saveData_MouseLeave(object sender, MouseEventArgs e){
            saveData.Background = SpecialColor.mainBack();
        }
        //-----hoverEffect


        //-----ClickEvent
        private void mainLayoutExit_Click(object sender, RoutedEventArgs e){
            this.Close();
        }
        //-----ClickEvent

        //=======================mainLayout=======================
    }
}
