using Roguelike.Core.Map;
using Roguelike.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Core
{
    public class MapManager
    {
        GameManager _gm;
        public MapGenerator generator;

        public int[,] Map;

        public MapManager(GameManager gm)
        {
            _gm = gm;
            generator = new(_gm.MapWidth, _gm.MapHeight);
            Map = generator.Map;
        }

        
    }
}
