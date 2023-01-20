using System;
using Xamarin.Forms;
using Olis.Models;

namespace Olis
{
    public class RecipeBodyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IngredientTemplate { get; set; }
        public DataTemplate DirectionTemplate { get; set; }
        public RecipeBodyTemplateSelector()
        {
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((RecipeBody)item).Type.Equals("ingredient") ? IngredientTemplate : DirectionTemplate;
        }
    }
}

