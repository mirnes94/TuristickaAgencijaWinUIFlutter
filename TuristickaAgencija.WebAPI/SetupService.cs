using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Services.Database;

namespace TuristickaAgencija.WebAPI
{
    public class SetupService
    {
        public static void Init(TuristickaAgencijaContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
