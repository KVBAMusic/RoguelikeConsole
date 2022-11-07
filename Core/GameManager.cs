using Roguelike.Entities;
using ConsoleApp1.Core;
using Roguelike.UI;

namespace Roguelike.Core
{
    public class GameManager
    {
        EntityManager entityManager;
        MapManager mapManager;
        MapRenderer renderer;
        InputHandler inputHandler;
        ActionLogger logger;

        Random random = new Random();
        public PlayerEntity Player { get; set; }

        public EntityManager EntityManager => entityManager;
        public MapManager MapManager => mapManager;
        public MapRenderer Renderer => renderer;
        public InputHandler InputHandler => inputHandler;

        public readonly int MapWidth = 60;
        public readonly int MapHeight = 40;
        public GameManager()
        {
            entityManager = new(this);
            mapManager = new(this);
            renderer = new(this);
            inputHandler = new(this);
            logger = new();
        }

        public void Start()
        {

            Console.WriteLine("generating...");
            mapManager.generator.Generate(15, 1);

            // spawn player
            int playerX, playerY;
            do
            {
                playerX = random.Next(MapWidth);
                playerY = random.Next(MapHeight);
            } while (mapManager.Map[playerX, playerY] != 1);

            EntityStats playerStats = new("you", "\u001b[101m \u001b[0m", 20, 1, 4, 1);
            PlayerEntity player = new(playerStats);
            player.Spawn(mapManager.Map);

            entityManager.Spawn(player);

            Player = entityManager.GetPlayer();
            entityManager.SpawnRandomEnemies(5, 10);
            renderer.Display();
            Loop();
        }

        public void TryMoveEntity(object? sender, MoveEventArgs e)
        {
            int dx = e.deltaX;
            int dy = e.deltaY;

            int px = e.Entity.PosX;
            int py = e.Entity.PosY;

            if (mapManager.Map[px + dx, py + dy] != 2)
            {
                e.Entity.MoveSuccessful(dx, dy);
            }
        }

        public void Loop()
        {
            renderer.Display();
            Console.Write(Player.GetCurrentStats());
            logger.Display();
            inputHandler.WaitForInput();
            
            // here would be a tick of other entities
            foreach (var e in entityManager.Entities)
            {
                e.OnMove += TryMoveEntity;
                int[] md;
                if (e is PlayerEntity)
                {
                    md = inputHandler.playerMoveDelta;
                }
                else
                {
                    md = random.Next(4) switch
                    {
                        0 => new int[] {  1, 0 },
                        1 => new int[] { -1, 0 },
                        2 => new int[] { 0,  1 },
                        3 => new int[] { 0, -1 },
                        _ => new int[] { 0,  0 }
                    };
                }
                e.Move(md[0], md[1]);
                e.Tick();
                e.OnMove -= TryMoveEntity;
            }
            // --------------------------------------
            Loop();
        }
    }
}