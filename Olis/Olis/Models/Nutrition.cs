using System;
using Newtonsoft.Json;
using SQLite;

namespace Olis.Models
{
    using System;
    using SQLite;

    namespace Olis.Models
    {
        [Table("recipe")]
        public class Recipe
        {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int Id { get; set; }
            [Column("name")]
            public string Name { get; set; }
            [Column("picture_path")]
            public string PicturePath { get; set; }
            [Column("servings")]
            public int Servings { get; set; }

            public Recipe()
            {
            }

            public override string ToString()
            {
                return Id + " " + Name + " " + PicturePath + " " + Servings;
            }
        }
    }


    [Table("Nutrition")]
    public class Nutrition
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("recipe_id")]
        public int RecipeId { get; set; }

        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        [Column("amount")]
        public double Amount { get; set; }

        [JsonProperty("percentOfDailyNeeds")]
        [Column("percent")]
        public double Percent { get; set; }

        [JsonProperty("unit")]
        [Column("unit")]
        public string Unit { get; set; }

        public Nutrition()
        {
        }

        public override string ToString()
        {
            return Id + " " + RecipeId + " " + Name + " " + Amount;
        }
    }
}

