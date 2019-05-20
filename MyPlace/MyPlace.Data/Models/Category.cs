﻿
namespace MyPlace.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<EntityCategory> EntityCategories { get; set; }
    }
}
