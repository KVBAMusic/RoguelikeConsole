using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Roguelike.Entities;

namespace Roguelike.Core
{
    public class MapRenderer
    {
        GameManager _gm;
        // 0 - empty/invisible
        // 1 - floor
        // 2 - wall
        // 3 - door
        // and yes i know those are ANSI strings
        public readonly string[] mapChars = { " ", "\u001b[33m.\u001b[0m", "#", "\u001b[30m\u001b[43m!\u001b[0m" };
        public readonly string[] mapMemoryChars = { " ", "\u001b[90m.\u001b[0m", "\u001b[90m#\u001b[0m", "\u001b[30m\u001b[100m!\u001b[0m" };

        int[,] MapVisible;
        int[,] MapMemory;

        const double DEG2RAD = Math.PI / 180.0;

        public MapRenderer(GameManager gm)
        {
            _gm = gm;
            MapVisible = new int[_gm.MapWidth, _gm.MapHeight];
            MapMemory = new int[_gm.MapWidth, _gm.MapHeight];
        }

        public void Display()
        {
            int[,] Map = _gm.MapManager.Map;
            Console.Clear();
            MapVisible = new int[_gm.MapWidth, _gm.MapHeight];

            // calculate the visibility
            // send a number of rays all around the player with a max length of 10
            // repeat until the ray either hits a wall or has reached max distance:
            // - go forward by 1 unit
            // - save the rounded coordinates of the cell in MapVisible array
            // to be optimised probably

            // send rays from the position of player

            double angle = 0f;
            int numRays = 180;
            double rayForwardStep = 1;
            for (int i = 0; i < numRays; i++)
            {
                double rayX = _gm.Player.PosX;
                double rayY = _gm.Player.PosY;

                // angle will be counted counter-clockwise and 0 deg is facing downwards (pos Y)
                // probably doesnt matter but idcs
                double directionX = Math.Sin(angle * DEG2RAD);
                double directionY = Math.Cos(angle * DEG2RAD);

                // for debug
                // MapVisible = Map;

                //repeat for 12 steps in the specified direction
                for (int j = 0; j < 12; j++)
                {
                    int roundedRayX = (int)Math.Clamp(Math.Round(rayX), 0, _gm.MapWidth);
                    int roundedRayY = (int)Math.Clamp(Math.Round(rayY), 0, _gm.MapHeight);
                    MapVisible[roundedRayX, roundedRayY] = Map[roundedRayX, roundedRayY];
                    MapMemory[roundedRayX, roundedRayY] = Map[roundedRayX, roundedRayY];
                    int current = Map[roundedRayX, roundedRayY];
                    if (current == 2 || (current == 3 && !_gm.EntityManager.GetEntityAtPosition(roundedRayX, roundedRayY, out _))) break;
                    rayX += directionX * rayForwardStep;
                    rayY += directionY * rayForwardStep;
                }

                angle += 360.0 / numRays;
            }

            for (int y = 0; y < _gm.MapHeight; y++)
            {
                for (int x = 0; x < _gm.MapWidth; x++)
                {
                    if (MapVisible[x, y] != 0)
                    {
                        if (_gm.EntityManager.GetEntityAtPosition(x, y, out var entity))
                        {
                            // VS warns that this entity may be of value null
                            // this bit of code will run if there's an entity at position (x, y)
                            // if there's no entity, this bit will be ignored
                            Console.Write((entity).MapDisplay);
                        }
                        else Console.Write(mapChars[MapVisible[x, y]]); 
                    }
                    else Console.Write(mapMemoryChars[MapMemory[x, y]]);
                }
                Console.Write('\n');
            }
        }
    }
}
