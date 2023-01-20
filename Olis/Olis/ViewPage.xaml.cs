using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Olis
{
    public partial class ViewPage : ContentPage
    {
        public ViewPageViewModel mod;
        public ViewPage(int recipeId)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            double Width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            double Height = Width / 2.0;
            InitializeComponent();
            BindingContext = mod = new ViewPageViewModel(recipeId);
            //for (int i = 0; i <= 20; i++)
            //{
            //    servingPicker.Items.Add(i.ToString());
            //}
            image.WidthRequest = Width;
            image.HeightRequest = Height;
            refresh();
            header.SetRButtonClicked(OpenAddPage);
            header.SetLButtonClicked(GoBack);
        }


        async void OpenAddPage(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new EditPage(mod.Recipe.Id));
        }
        async void GoBack(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        private void refresh()
        {
            ingredientCard.ItemsSource.Clear();
            directionCard.ItemsSource.Clear();
            ingredientCard.AddEntryList(mod.getIngredientList());
            directionCard.AddEntryList(mod.getDirectionList());
            if (mod.Recipe.PicturePath.Contains("defaultImage"))
            {
                image.Source = "defaultImage";
            }
            else
            {
                image.Source = ImageSource.FromFile(
                System.IO.Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), mod.Recipe.PicturePath));
            }
        }

        protected override void OnAppearing()
        {
            refresh();
            base.OnAppearing();
        }
    }
}

