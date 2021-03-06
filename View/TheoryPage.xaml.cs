﻿using System;
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

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для TheoryPage.xaml
    /// </summary>
    public partial class TheoryPage : UserControl {
        public TheoryPage(ContentControl headerControl) {
            InitializeComponent();

            this.headerControl = headerControl;

            //вешаем события на клик
            foreach (Button el in navButtons.Children) {
                el.Click += Button_Click;
            }
        }
        //возвращает на предыдущую страницу
        private ContentControl headerControl;
        private void onMainPage_Click(object sender, RoutedEventArgs e) {
            this.headerControl.IsEnabled = false;
            this.headerControl.Opacity = 0;
            this.headerControl.IsHitTestVisible = false;
        }

        //private static bool firstLoaded = true;
        //добавление 10 пикселей сверху/снизу для элементов меню
        //private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            //if (firstLoaded == true) {
            //    firstLoaded = false;
            //    foreach (UIElement el in navButtons.Children) {
            //        (el as Button).Height = (el as Button).ActualHeight + 20;
            //    }
            //}
        //}

        //скролл до выбранного элемента
        private void Button_Click(object sender, RoutedEventArgs e) {
            string titleName = (sender as Button).Content.ToString();

            double heightCount = 0;
            foreach (UIElement el in spravkaContent.Children) {
                if (el is Label && (el as Label).Content.ToString() == titleName) {
                    break;
                }
                if (el is Label) 
                    heightCount += (el as Label).ActualHeight + 35;
                else if (el is Border)
                    heightCount += (el as Border).ActualHeight + 30;
                else
                    heightCount += (el as TextBlock).ActualHeight;
            }

            spravkaContentScroll.ScrollToVerticalOffset(heightCount);
        }
    }
}
