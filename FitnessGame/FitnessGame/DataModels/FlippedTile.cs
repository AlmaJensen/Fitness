using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessGame.DataModels
{
    public class FlippedTile : RealmObject
    {
        public string TileID { get; set; }
        public string AssociatedImage { get; set; }
    }
}
