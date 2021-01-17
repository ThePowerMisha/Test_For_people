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
    /// Логика взаимодействия для aboutProgramm.xaml
    /// </summary>
    public partial class aboutProgramm : UserControl {
        public aboutProgramm(ContentControl headerControl) {
            InitializeComponent();

            this.headerControl = headerControl;
        }
        private ContentControl headerControl;
        private void onMainPage_Click(object sender, RoutedEventArgs e) {
            this.headerControl.IsEnabled = false;
            this.headerControl.Opacity = 0;
            this.headerControl.IsHitTestVisible = false;
        }
    }
}
