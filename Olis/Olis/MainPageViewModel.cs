using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Olis.Models;
using System.Collections.Generic;

namespace Olis
{
	public class MainPageViewModel : ViewModelBase
	{
        public bool HasBackButton { get; set; }
        public string RButtonText { get; set; }
        public ICommand RButtonCommand { get; private set; }
		public MainPageViewModel()
		{
			HasBackButton = false;
			RButtonText = "Add";
		}

		public List<Recipe> GetRecipes()
		{
			return DB.conn.Table<Recipe>().ToList();
		}

        public List<Recipe> SearchRecipes(string text)
        {
			var l = DB.conn.Table<Recipe>().ToList();
			text = text.ToLower();
			List<Recipe> result = new List<Recipe>();
			foreach (var recipe in l)
			{
				if (recipe.Name.ToLower().Contains(text))
				{
					result.Add(recipe);
				}
			}
			return result;
        }
    }
}

