using System;
using System.Xml.Linq;
using SQLite;
namespace Olis.Models
{
    [Table("RecipeBody")]
    public class RecipeBody
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("recipe_id")]
        public int RecipeId { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("description")]
        public string Description { get; set; }
        public RecipeBody()
        {
        }

        public override string ToString()
        {
            return Id + " " + RecipeId + " " + Type + " " + Description;
        }
    }
}

