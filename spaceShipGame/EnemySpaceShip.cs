using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spaceShipGame
{
    public class EnemySpaceShip
    {
        public EnemySpaceShip()
        {
        }
        List<EnemySpaceShip> enemySpaceShips = new();
        private int _id { get; set; }
        private int _posX { get; set; }
        private EnemySpaceShip(int id, int posX)
        {
            _posX = posX;
            _id = id;
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int posX
        {
            get { return _posX; }
            set { _posX = value; }
        }


        public List<EnemySpaceShip> AddEnemies(int scoreCount)
        {

            var rand = new Random();
            var enemiesSpawned = enemySpaceShips.Count + 1;
            if (scoreCount > 1)
            {
                for (int i = 0; i <= enemiesSpawned; i++)
                {
                    var pos = rand.Next(1, 25);
                    var enemy = new EnemySpaceShip(++id, posX = pos);
                    enemySpaceShips.Add(enemy);
                }

            }
            else if (enemySpaceShips.Count == 0)
            {
                var pos = rand.Next(5, 25);
                var enemy = new EnemySpaceShip(id = 1, posX = pos);
                enemySpaceShips.Add(enemy);
            }

            return enemySpaceShips;
        }
        public void SpawnEnemies()
        {
            var enemy = new CanvasImage("images\\enemy.png");
            string enemyPosX = string.Empty;
            List<int> allShipSpacings = new();
            allShipSpacings = enemySpaceShips.Select(s => s.posX).ToList();
            var totalShipSpacing = allShipSpacings.Take(allShipSpacings.Count).Sum();
            if (totalShipSpacing >= (23 - enemySpaceShips.Count))
            {
                
                var maxSpace =  (totalShipSpacing * enemySpaceShips.Count) / totalShipSpacing;
                var rand = new Random();
                var newPosFirst = rand.Next(8, maxSpace + 8);
                var newPosTrailing = rand.Next(1, maxSpace);
                foreach (var enemyShip in enemySpaceShips)
                {
                    if (totalShipSpacing > (23 - enemySpaceShips.Count))
                    {
                        if (enemyShip.Equals(enemySpaceShips.First()))
                        {
                            enemySpaceShips.Select(x => x).First().posX = newPosFirst;
                        }
                        else
                        {
                            enemySpaceShips.Find(x => x.id == enemyShip.id).posX = -8 + newPosTrailing;
                        }
                    }
                    else
                    {
                        if (enemyShip.Equals(enemySpaceShips.First()))
                        {
                            enemySpaceShips.Select(x => x).First().posX = 5;
                        }
                        else
                        {
                            enemyShip.posX = enemyShip.posX;
                        }
                    }
                }
                allShipSpacings.Clear();
                allShipSpacings = enemySpaceShips.Select(s => s.posX).ToList();
                totalShipSpacing = allShipSpacings.Take(allShipSpacings.Count).Sum();
            }
            var posCursor = 0;
            foreach (var enemyShip in enemySpaceShips)
            {
                for (int i = 0; i <= enemyShip.posX; i++)
                {
                    enemyPosX += "  ";
                }
                posCursor += enemyPosX.Length + 2;
                Console.Write($"{enemyPosX}");
                AnsiConsole.Write(enemy);
                int top = Console.CursorTop;
                Console.SetCursorPosition(posCursor, Console.CursorTop - top + 1);
            }
        }
        public int EnemyKilled(string shotPosX, int scoreCount)
        {
            foreach (var ship in enemySpaceShips)
            {
                var shotPos = shotPosX.Length - ship.posX - 2;
                if (ship.posX == shotPos)
                {
                    enemySpaceShips.Remove(ship);
                    scoreCount++;
                    AddEnemies(scoreCount);
                    return scoreCount;
                }
            }
            return scoreCount;
        }
    }
}
