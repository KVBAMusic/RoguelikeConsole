using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Core.Map
{
    public class MapGenerator
    {
        Random random = new();
        // 0 - empty/invisible
        // 1 - floor
        // 2 - wall
        // 3 - door

        public int[,] Map;

        private readonly int w;
        private readonly int h;

        public MapGenerator(int w, int h)
        {
            Map = new int[w,h];
            this.w = w;
            this.h = h;

            for(int i = 0; i < w; i++)
            {
                for(int j = 0; j < h; j++)
                {
                    Map[i, j] = 0;
                }
            }
        }

        public void Generate(int numIterations)
        {
            int minW = 3;
            int maxW = 11;
            int minH = 3;
            int maxH = 8;

            int roomX;
            int roomY;

            int roomW;
            int roomH;

            // pass 0: generate rooms
            // TODO: find better alogrithm
            for(int i = 0; i < numIterations; i++)
            {
                // pick a random point on map
                roomX = Math.Clamp(random.Next(w) - 1, 0, w);
                roomY = Math.Clamp(random.Next(h) - 1, 0, h);

                // create a room 
                roomW = random.Next(minW, maxW) + 2;
                roomH = random.Next(minH, maxH) + 2;
                for(int x = roomX; x <= roomX + roomH; x++)
                {
                    for (int y = roomY; y <= roomY + roomH; y++)
                    {
                        if (y >= h || x >= w) continue;
                        Map[x, y] = (x == roomX || x == roomX + roomW - 1|| y == roomY || y == roomY + roomH - 1) ? 2 : 1;
                    }
                }

                
            }
            
            // pass 1: outer walls, filling the void with floor, generating doors
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    // place walls around the map
                    if (x == 0 || y == 0 || x == w - 1 || y == h - 1)
                    {
                        Map[x, y] = 2;
                    }
                    else
                    {
                        switch (Map[x, y])
                        {
                            default:
                                break;
                            case 0: // empty space: fill with floor
                                Map[x, y] = 1;
                                break;
                            case 2: // wall: check if there are 2 neighbouring floor tiles - if there are, place a door with probability
                                if ((Map[x, y + 1] == 1 && Map[x, y - 1] == 1) && (Map[x + 1, y] == 2 && Map[x - 1, y] == 2)
                                 || (Map[x, y + 1] == 2 && Map[x, y - 1] == 2) && (Map[x + 1, y] == 1 && Map[x - 1, y] == 1))
                                {
                                    if (random.Next(12) == 0)
                                    {
                                        Map[x, y] = 3;
                                    }
                                }
                                break;
                        }

                    }
                }
            }

            // check if wall is touching 2 different floor tiles and place a door
        }

    }
}