using System;
using System.Collections;
using Xamarin.Forms;
using Olis.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Olis
{
    public partial class EditCardView : StackLayout
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(EditCardView), default(string));
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }
        
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(Color), typeof(EditCardView),
                default(Color));


        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public ObservableCollection<RecipeBody> ItemsSource { get; set; }

        private string type;
        public EditCardView(string type)
        {
            ItemsSource = new ObservableCollection<RecipeBody>();
            this.type = type;
            InitializeComponent();
            itemLv.HeightRequest = ItemsSource.Count * 45;
            BindingContext = this;
        }

        public EditCardView()
        {
            ItemsSource = new ObservableCollection<RecipeBody>();
            this.type = "ingredient";
            InitializeComponent();
            itemLv.HeightRequest = ItemsSource.Count * 45;
            BindingContext = this;
        }

        public void AddEntryList(List<RecipeBody> list)
        {
            foreach (RecipeBody rp in list)
            {
                ItemsSource.Add(rp);
            }
            
            itemLv.HeightRequest = ItemsSource.Count * 45;
        }

        public void AddEntry(object sender, System.EventArgs e)
        {
            ItemsSource.Add(new RecipeBody { Type = type });
            itemLv.HeightRequest = ItemsSource.Count * 45;
        }

        public void RemoveEntry(object sender, System.EventArgs e)
        {
            if (ItemsSource.Count > 0)
            {
                ItemsSource.Remove((RecipeBody)itemLv.SelectedItem);
                itemLv.HeightRequest = ItemsSource.Count * 45;
            }
        }
    }
}

