using EmployeeDataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;

namespace EmployeeDataAccess.Context
{
    internal class ProjektContext : DbContext
    {
        public ProjektContext()
        {
            bool created = Database.EnsureCreated();
            if (created)
            {
                Debug.WriteLine("Database created");
            }

        }
   
    }
}
