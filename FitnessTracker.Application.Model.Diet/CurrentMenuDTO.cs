using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Diet
{
    public class CurrentMenuDTO
    {     
        public string Id { get; set; }
        public int ItemID { get; set; }
        public double Serving { get; set; }
    }
}
