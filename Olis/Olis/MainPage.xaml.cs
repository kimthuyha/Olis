using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Olis.Models;

namespace Olis
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel mod;
        public MainPage()
        {
            mod = new MainPageViewModel();
            InitializeComponent();
            BindingContext = mod;
            header.SetRButtonClicked(
                async (System.Object sender, System.EventArgs e) =>
                {
                    await Navigation.PushModalAsync(new EditPage(-1));
                });
            loadRecipes(mod.GetRecipes());
        }

        private void loadRecipes(List<Recipe> l)
        {
            catalog.Children.Clear();
            Color c1 = Color.FromHex("#DE803E");
            Color c2 = Color.FromHex("#2E876B");
            for (int i = 0; i < l.Count / 2 + 1; i++)
            {
                
                for (int j = 0; j < 2; j++)
                {
                    if (i * 2 + j >= l.Count) break;
                    if ((i + j) % 2 == 0)
                    {
                        catalog.Children.Add(new RecipeViewCell(l[i * 2 + j], c1),j, i);
                    }
                    else
                    {
                        catalog.Children.Add(new RecipeViewCell(l[i * 2 + j], c2), j, i);
                    }
                   
                }
                
                
            }
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            loadRecipes(mod.SearchRecipes(searchBar.Text));
        }
    }
}

