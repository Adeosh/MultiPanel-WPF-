using Microsoft.EntityFrameworkCore.Infrastructure;
using MultiPanel_WPF_.Core;
using System;
using System.Windows;

namespace MultiPanel_WPF_
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();
        }
    }
}
