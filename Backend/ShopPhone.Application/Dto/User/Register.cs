﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Application.Dto.User
{
    public class Register
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}
