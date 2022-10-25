using System;
using System.Linq;
using System.Net.Http.Headers;
using Roguelike.Entities;
using Roguelike.Registry;

namespace Roguelike.Core
{

    public class EntityManager
    {
        GameManager _gm;
        Random _random;

        EntityRegistry registry;

        int tick = 0;

        public List<BaseEntity> Entities { get; private set; }
        public EntityManager(GameManager gm)
        {
            _gm = gm;
            Entities = new List<BaseEntity>();
            _random = new Random();
            registry = new EntityRegistry();
        }

        public void Spawn(BaseEntity entity)
        {
            Entities.Add(entity);
        }

        public PlayerEntity GetPlayer()
        {
            foreach (var e in Entities)
            {
                if (e is PlayerEntity)
                {
                    return (PlayerEntity)e;
                }
            }
            return null;
        }

        public void KillEntity(BaseEntity entity)
        {
            // find an entity with matching id
            // remove it from the list
        }

        public bool GetEntityAtPosition(int x, int y, out BaseEntity? entity)
        {
            foreach(var e in Entities)
            {
                if (e.PosX == x && e.PosY == y)
                {
                    entity = e;
                    return true;
                }
            }
            entity = null;
            return false;
        }

        private void SpawnEnemy()
        {   
            EnemyEntity e = (EnemyEntity)registry.Enemies[_random.Next(registry.Enemies.Count)];
            Entities.Add(e);
            e.Spawn(_gm.MapManager.Map);
        }

        public void SpawnRandomEnemies(int min, int max)
        {
            int x = _random.Next(min, max + 1);
            for (int i = 0; i < x; i++)
            {
                SpawnEnemy();
            }
        }

        public void Tick()
        {
            tick++;
            if (tick >= 30)
            {
                tick = 0;
                if (Entities.Count < 3)
                {
                    int n = _random.Next(2, 5);
                    for (int i = 0; i < n; i++)
                    {
                        SpawnEnemy();
                    }
                    // spawn like 2 - 4 new enemies
                }
            }
        }
    }
}

