﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PIOAccount.Models;

namespace PIOAccount.Classes
{
    public class CommonHelper
    {
        public DbConnection ObjDBConnection = new DbConnection();

    }
}