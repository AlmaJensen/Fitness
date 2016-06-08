using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessGame.DataModels
{
    public class StoreOptions
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime WhenPurchased { get; set; }
        public DateTime WhenAvailable { get; set; }
        public DateTime WhenNoLongerAvailable { get; set; }
        public double Cost { get; set; }
        public bool CostsRubies { get; set; }
        public bool Purchased { get; set; }
    }
}
