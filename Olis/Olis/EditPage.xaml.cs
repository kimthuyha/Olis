using System;
using System.Collections.Generic;
using Olis.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.IO;
using System.Linq;
using FFImageLoading.Cache;
using FFImageLoading.Forms;

namespace Olis
{
    public partial class EditPage : ContentPage
    {
        public EditPageViewModel mod;
        byte[] imageBytes;
        public EditPage(int recipeId)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            double Width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            double Height = Width / 2.0;
            InitializeComponent();
            BindingContext = mod = new EditPageViewModel(recipeId);
            for (int i = 0; i <= 20; i++)
            {
                servingPicker.Items.Add(i.ToString());
            }
            servingPicker.SelectedIndex = mod.Recipe.Servings;
            ingredientCard.AddEntryList(mod.getIngredientList());
            directionCard.AddEntryList(mod.getDirectionList());
            image.WidthRequest = Width;
            image.HeightRequest = Height;
            recipeName.WidthRequest = Height;
            CachedImage.InvalidateCache(image.Source, CacheType.All, true);
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
            header.SetRButtonClicked(SaveRecipe);
            header.SetLButtonClicked(GoBack);
        }

        async void SelectImage(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    imageBytes = ms.ToArray();
                }
                image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            
        }

        async void GoBack(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void CalculateNutrition(System.Object sender, System.EventArgs e)
        {
            mod.CalculateNutrition(new List<RecipeBody>(ingredientCard.ItemsSource), recipeName.Text,
                Int32.Parse(servingPicker.SelectedItem.ToString()));
        }

        async void SaveRecipe(System.Object sender, System.EventArgs e)
        {
            if (mod.SaveRecipe(new List<RecipeBody>(ingredientCard.ItemsSource),
                new List<RecipeBody>(directionCard.ItemsSource))
            && mod.SaveImage(imageBytes))
            await Navigation.PushModalAsync(new ViewPage(mod.Recipe.Id));
        }

        async void DeleteRecipe(System.Object sender, System.EventArgs e)
        {
            mod.DeleteCurrentRecipe();
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}

