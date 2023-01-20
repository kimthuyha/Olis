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

