﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QueuingSystemsModel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainViewModel viewModel = new MainViewModel();
            MainWindow view = new MainWindow() { DataContext = viewModel };
            view.Show();
            base.OnStartup(e);
        }
    }
}
