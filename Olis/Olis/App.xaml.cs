using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Olis.Models;
using System.Windows.Input;

[assembly: ExportFont("FontAwesome.otf", Alias = "FontAwesome")]
[assembly: ExportFont("RalewayReg.ttf", Alias = "RalewayReg")]
[assembly: ExportFont("RalewaySemi.ttf", Alias = "RalewaySemi")]
[assembly: ExportFont("RalewayBold.ttf", Alias = "RalewayBold")]

namespace Olis
{
    public partial class App : Application
    {
        public App()
        {
            DB.OpenConnection();
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}