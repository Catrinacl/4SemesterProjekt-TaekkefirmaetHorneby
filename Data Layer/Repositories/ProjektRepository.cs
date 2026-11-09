using Data_Layer.Context;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Repositories
{
    public class ProjektRepository
    {
        public static IProjekt GetProjekt(int id)
        {
            using (ProjektContext context = new ProjektContext)
            {
                //TODO
                //IProjekt projekt = 
            }
        } 
    }
}
