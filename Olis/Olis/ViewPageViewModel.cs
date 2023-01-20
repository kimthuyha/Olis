using System;
using Olis.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Olis
{
    public class ViewPageViewModel
    {
        public bool HasBackButton { get; set; }
        public string RButtonText { get; set; }
        public Recipe Recipe { get; set; }
        public List<Nutrition> Nutritions { get; set; }
        public ViewPageViewModel(int recipeId)
        {
            HasBackButton = true;
            RButtonText = "Edit";
            Recipe = getRecipe(recipeId);
            Nutritions = getNutritionList();
        }

        private Recipe getRecipe(int recipeId)
        {
            return DB.conn.Table<Recipe>().
                Where(a => a.Id == recipeId).First();
        }

        public List<RecipeBody> getIngredientList()
        {
            return DB.conn.Table<RecipeBody>().
                Where(a => a.Type.Equals("ingredient") && a.RecipeId == Recipe.Id).
                OrderBy(a => a.Id).ToList();
        }

        public List<RecipeBody> getDirectionList()
        {
            return DB.conn.Table<RecipeBody>().
                Where(a => a.Type.Equals("direction") && a.RecipeId == Recipe.Id).
                OrderBy(a => a.Id).ToList();
        }

        public List<Nutrition> getNutritionList()
        {
            return DB.conn.Table<Nutrition>().
                Where(a => a.RecipeId == Recipe.Id).OrderBy(a => a.Id).
                ToList();

        }
    }
}

