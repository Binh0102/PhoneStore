﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Application.Dto.User
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Remember {  get; set; }
    }
}
