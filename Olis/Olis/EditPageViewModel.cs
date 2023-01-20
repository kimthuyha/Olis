using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Olis.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using static SQLite.SQLite3;

namespace Olis
{
    public class NutritionReq
    {
        public string title;
        public List<string> ingredients;
        public int servings;
    }
    
    public class EditPageViewModel : ViewModelBase
    {
        public bool HasBackButton { get; set; }
        public string RButtonText { get; set; }
        public Recipe Recipe { get; set; }
        //private List<RecipeBody> currentIngredients { get; set; }
        //private List<RecipeBody> currentDirections { get; set; }
        public List<Nutrition> Nutritions { get; set; }
        HttpClient client = new HttpClient();
        public EditPageViewModel(int recipeId)
        {
            HasBackButton = true;
            RButtonText = "Done";
            Recipe = getRecipe(recipeId);
            //currentIngredients = getIngredientList();
            //currentDirections = getDirectionList();
            Nutritions = getNutritionList();
        }

        //private getDefaultImage()
        //{
        //    string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Images");
        //    string filePath = System.IO.Path.Combine(folderPath,"defaultImage.png");

        //}
        private Recipe getRecipe(int recipeId)
        {
            var recipe = DB.conn.Table<Recipe>().
                Where(a => a.Id == recipeId).FirstOrDefault();
            return recipe == null ?
                new Recipe { Id = recipeId, Servings = 1,
                    PicturePath = "defaultImage.jpg", Name = "New Recipe"} :
                recipe;
        }

        public int GetServings()
        {
            return Recipe.Servings;
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
            if (Recipe.Id != -1)
            {
                return DB.conn.Table<Nutrition>().
                Where(a => a.RecipeId == Recipe.Id).OrderBy(a => a.Id).
                ToList();
            }
            else
            {
                var temp = new List<Nutrition>();
                string[] nutritionNames = { "Calories", "Fat", "Saturated Fat",
                    "Trans Fat", "Cholesterol", "Sodium", "Carbohydrates", "Fiber",
                    "Sugar", "Protein","Vitamin D", "Calcium", "Iron", "Potassium"};
                string[] nutritionUnits = { "kcal", "g", "g", "g", "mg", "mg",
                    "g", "g", "g", "g","g", "g", "g", "g"};
                for (int i = 0; i < nutritionNames.Length; i++)
                {
                    temp.Add(new Nutrition
                    {
                        Amount = 0,
                        Name = nutritionNames[i],
                        Unit = nutritionUnits[i]
                    });
                }
                return temp;
            }
            
        }



        public async void CalculateNutrition (List<RecipeBody> ingredients, string title, int servings)
        {
            var uri = "https://api.spoonacular.com/recipes/analyze?includeNutrition=true&apiKey=1ac8081af2604ebcbb2cdf50252e549c";
            var nutritionNameSet = new HashSet<string>();
            NutritionReq reqObject = new NutritionReq
            {
                title = title,
                servings = servings,
                ingredients = (from p in ingredients
                               select p.Description).ToList(),

            };
            string req = JsonConvert.SerializeObject(reqObject);
            var content = new StringContent(req, UnicodeEncoding.UTF8, "application/json");
            JObject resObj = null;
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);

                var result = await response.Content.ReadAsStringAsync();
                resObj = JObject.Parse(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
                Environment.Exit(0);
            }
            List<JToken> jTokens = resObj["nutrition"]["nutrients"].ToList();
            Dictionary<string, Nutrition> allNutritions = new Dictionary<string, Nutrition>();
            foreach (JToken jToken in jTokens)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                var searchResult = jToken.ToObject<Nutrition>();
                allNutritions.Add(searchResult.Name, searchResult);
            }
            for (int i = 0; i < Nutritions.Count; i++)
            {
                var name = Nutritions[i].Name;

                if (allNutritions.ContainsKey(name)) {
                    Nutritions[i] = allNutritions[name];
                } else
                {
                    Nutritions[i].Amount = 0;
                }
            }
            
            OnPropertyChanged("Nutritions");

        }

        public bool SaveImage(byte[] img)
        {
            if (img == null)
            {
                return true;
            }
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Images");
            string filePath = System.IO.Path.Combine(folderPath, Recipe.Id.ToString() + ".png");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Write(img, 0, img.Length);
            }
            Recipe.PicturePath = System.IO.Path.Combine("Images",Recipe.Id.ToString() + ".png");
            DB.conn.Update(Recipe);
            return true;
        }
        public bool SaveRecipe(List<RecipeBody> ingredients,
            List<RecipeBody> directions)
        {
            if (Recipe.Id == -1)
            {
                DB.conn.Insert(Recipe);
            }
            else
            {
                DB.conn.Update(Recipe);
            }
            ingredients.ForEach(ingr => ingr.RecipeId = Recipe.Id);
            directions.ForEach(dir => dir.RecipeId = Recipe.Id);
            Nutritions.ForEach(nut => nut.RecipeId = Recipe.Id);
            DB.conn.Table<RecipeBody>().Delete(x => x.RecipeId == Recipe.Id);
            DB.conn.Table<Nutrition>().Delete(x => x.RecipeId == Recipe.Id);
            DB.conn.InsertAll(ingredients);
            DB.conn.InsertAll(directions);
            DB.conn.InsertAll(Nutritions);
            return true;
        }

        internal void DeleteCurrentRecipe()
        {
            DB.conn.Table<RecipeBody>().Delete(x => x.RecipeId == Recipe.Id);
            DB.conn.Table<Nutrition>().Delete(x => x.RecipeId == Recipe.Id);
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Images");
            string filePath = System.IO.Path.Combine(folderPath, Recipe.Id.ToString() + ".png");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            DB.conn.Table<Recipe>().Delete(x => x.Id == Recipe.Id);
        }
    }
}

