using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using Olis.Models;
using System.Collections.ObjectModel;

namespace Olis
{
    public partial class NutritionLabel : Frame
    {
        public ObservableCollection<Nutrition> ItemsSource { get; set; }
        public NutritionLabel()
        {
            ItemsSource = new ObservableCollection<Nutrition>();
            ItemsSource.Add(new Nutrition { Name = "Calories" });
            InitializeComponent();
            //BindingContext = this;
        }

        public void Refresh(List<Nutrition> list)
        {
            ItemsSource.Clear();
            foreach (Nutrition nut in list)
            {
                ItemsSource.Add(nut);
            }
        }
    }
}

