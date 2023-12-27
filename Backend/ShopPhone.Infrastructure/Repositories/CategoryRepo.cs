﻿using ShopPhone.Domain.Entities;
using ShopPhone.Domain.Repositories;
using ShopPhone.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Infrastructure.Repositories
{
    public class CategoryRepo:Repo<Category>,ICategoryRepo
    {
        public CategoryRepo(ShopDbContext context) : base(context) { }
    }
}
