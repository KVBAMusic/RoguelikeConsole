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

        public readonly int MapWidth = 80;
        public readonly int MapHeight = 24;
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
            mapManager.generator.Generate(170);

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

        public void Loop()
        {
            renderer.Display();
            logger.Display();
            inputHandler.WaitForInput();
            
            // here would be a tick of other entities

            // --------------------------------------
            Loop();
        }
    }
}