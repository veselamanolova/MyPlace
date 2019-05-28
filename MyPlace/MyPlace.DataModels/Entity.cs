﻿
namespace MyPlace.DataModels
{
    using System.Collections.Generic;

    public class Entity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<EntityCategory> EntityCategories { get; set; }

        public ICollection<UserEntity> UserEntities { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}