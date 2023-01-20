using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Olis.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Olis
{
    public partial class RecipeViewCell : Frame
    {
        private Recipe recipe;
        public RecipeViewCell(Recipe r, Color c)
        {
            InitializeComponent();
            this.recipe = r;
            recipeName.Text = r.Name;
            stack.BorderColor = Color.FromHex("#E5E4E2");
            if (recipe.PicturePath.Contains("defaultImage"))
            {
                image.Source = "defaultImage";
            }
            else
            {
                image.Source = ImageSource.FromFile(
                System.IO.Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), recipe.PicturePath));
            }
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (System.Object sender, System.EventArgs e) =>
            {
                await Navigation.PushModalAsync(new ViewPage(recipe.Id));
            };
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            stack.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}

