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

        [Serializable]
        struct CorridorPoint
        {
            public int x;
            public int y;

            public CorridorPoint(int x, int y)
            {
                this.x = x; this.y = y;
            }
        }

        private CorridorPoint PopFromList(ref List<CorridorPoint> points)
        {
            int i = random.Next(points.Count);
            CorridorPoint output = points[i];
            points.RemoveAt(i);
            return output;
        }

        public void Generate(int numRooms, int corridorPasses)
        {
            int minW = 5;
            int maxW = 11;
            int minH = 5;
            int maxH = 8;

            int roomX;
            int roomY;

            int roomW;
            int roomH;
            // generate random places for rooms

            List<CorridorPoint> points = new List<CorridorPoint>();
            for (int i = 0; i < numRooms; i++)
            {
                bool isValid;
                do
                {
                    isValid = true;

                    roomX = random.Next(1, w);
                    roomY = random.Next(1, h);
                    roomW = random.Next(minW, maxW) + 2;
                    roomH = random.Next(minH, maxH) + 2;

                    for (int x = roomX; x < roomX + roomW; x++)
                    {
                        for (int y = roomY; y < roomY + roomH; y++)
                        {
                            try
                            {
                                if (Map[x, y] == 1)
                                {
                                    isValid = false;
                                    break;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (!isValid)
                        {
                            break;
                        }
                    }
                } while (!isValid);

                // check if there's no overlap

                points.Add(new CorridorPoint(roomX, roomY)); // those points will be used for generating corridors
                roomX -= roomW / 2;
                roomY -= roomH / 2;
                for (int x = roomX; x <= roomX + roomW; x++)
                {
                    for (int y = roomY; y <= roomY + roomH; y++)
                    {
                        if (y >= h || x >= w || y < 0 || x < 0) continue;
                        Map[x, y] = (x == roomX || x == roomX + roomW || y == roomY || y == roomY + roomH) ? 2 : 1;
                    }
                }
            }

            Console.WriteLine("generating corridors...");

            List<CorridorPoint> corridor = new List<CorridorPoint>();
            // generate corridors
            // start at a random room
            // pick a random room to go to
            // finish once all rooms have been visited
            for (int i = 0; i < corridorPasses; i++)
            {
                corridor = points;
                CorridorPoint start = PopFromList(ref corridor);

                CorridorPoint end;

                int currentX = start.x;
                int currentY = start.y;
                while (corridor.Count > 0)
                {
                    end = PopFromList(ref corridor);

                    int diffX = end.x - start.x;
                    int diffY = end.y - start.y;

                    int mode = random.Next(-1, 1);
                    for (int j = 0; j < 2; j++)
                    {
                        if (mode == -1) // horizontal first
                        {
                            int dir = Math.Sign(diffX);
                            for (int k = start.x; k != end.x; k += dir)
                            {
                                // place a floor tile
                                // if there's a wall, place a door
                                try
                                {
                                    if (Map[currentX, currentY] == 2)
                                    {
                                        if (Map[currentX + dir, currentY] != 2)
                                            Map[currentX, currentY] = 3;
                                        else
                                        {
                                            // avoid the wall
                                            if (Map[currentX, ++currentY + 1] != 2)
                                            {
                                                Map[currentX, currentY] = 2;
                                            }
                                            else if (Map[currentX, --currentY - 1] != 2)
                                            {
                                                currentY--;
                                                Map[currentX, currentY] = 2;
                                            }
                                        }
                                    }
                                    else Map[currentX, currentY] = 1;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    currentX--;
                                }
                                currentX = k;
                            }
                        }
                        if (mode == 0) // vertical first
                        {
                            int dir = Math.Sign(diffY);
                            for (int k = start.y; k != end.y; k += dir)
                            {
                                // place a floor tile
                                // if there's a wall, place a door

                                try
                                {
                                    if (Map[currentX, currentY] == 2)
                                    {
                                        if (Map[currentX, currentY + dir] != 2)
                                            Map[currentX, currentY] = 3;
                                        else
                                        {
                                            // avoid the wall
                                            if (Map[++currentX, currentY] != 2)
                                            {
                                                Map[currentX, currentY] = 2;
                                            }
                                            else if (Map[--currentX - 1, currentY - 1] != 2)
                                            {
                                                currentX--;
                                                Map[currentX, currentY] = 2;
                                            }
                                        }
                                    }
                                    else Map[currentX, currentY] = 1;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    currentY--;
                                }
                                currentY = k;
                            }
                        }
                        mode = ~mode;
                    }
                    start = end;
                }
            }

            
            // pass 1: fill the void with walls
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (Map[x, y] == 0 || (x == 0 || y == 0 || x == w - 1 || y == h - 1))
                    {
                        Map[x, y] = 2;
                    }
                }
            }

            Console.WriteLine("done!");
        }

    }
}