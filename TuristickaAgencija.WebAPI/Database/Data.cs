using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.WebAPI.Database
{
    public class Data
    {
        public static void Seed(TuristickaAgencijaContext context)
        {
            context.Database.Migrate();

            //add init
        }
    }
}
