using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Olis.Models;
using Xamarin.Forms;

namespace Olis
{
    public partial class CardView : StackLayout
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(CardView), default(string));
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
            BindableProperty.Create("Color", typeof(Color), typeof(CardView),
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
        public CardView(string type)
        {
            ItemsSource = new ObservableCollection<RecipeBody>();
            this.type = type;
            InitializeComponent();

            //itemLv.HeightRequest = 150;
            //if (itemLv.HeightRequest > 250)
            //{
            //    itemLv.HeightRequest = 250;
            //}
            BindingContext = this;
        }

        public CardView()
        {
            ItemsSource = new ObservableCollection<RecipeBody>();
            this.type = "ingredient";
            InitializeComponent();
            itemLv.HeightRequest = 150;
            BindingContext = this;
        }

        public void AddEntryList(List<RecipeBody> list)
        {
            foreach (RecipeBody rp in list)
            {
                ItemsSource.Add(rp);
            }
            var height = 0;
        
            if (type.Equals("ingredient"))
            {
                itemLv.HeightRequest = ItemsSource.Count * 40;
            }
            else
            {
                itemLv.HeightRequest = ItemsSource.Count * 150;
            }
            
        }
    }
}

