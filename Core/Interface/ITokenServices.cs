﻿using Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface ITokenServices
    {
        string CreateToken(AppUser user);
    }
}
