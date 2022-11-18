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

            if (enemySpaceShips.Count == 0)
            {
                var pos = rand.Next(5, 50);
                var enemy = new EnemySpaceShip(id = 1, posX = pos);
                enemySpaceShips.Add(enemy);
            }
            else if (enemySpaceShips.Count > 0 && enemySpaceShips.Count < 5)
            {

                var pos = rand.Next(1, 50);
                var enemy = new EnemySpaceShip(++id, posX = pos);
                enemySpaceShips.Add(enemy);
            }
            var renewPos = rand.Next(5, 30);
            enemySpaceShips.First().posX = renewPos;
            if (enemySpaceShips.Count > 1)
            {
                foreach (var ship in enemySpaceShips)
                {
                    renewPos = rand.Next(1, 20);
                    ship.posX = renewPos;
                }
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
            if (totalShipSpacing >= (52 - (enemySpaceShips.Count * 2)))
            {
                var spaceShipTakesSpace = enemySpaceShips.Count * 2;

                var maxSpace = (totalShipSpacing - spaceShipTakesSpace - 5) / enemySpaceShips.Count;
                var rand = new Random();

                foreach (var enemyShip in enemySpaceShips)
                {
                    var maxSpaceTrailing = maxSpace;
                    if (maxSpace < 1)
                    {
                        maxSpaceTrailing = 1;
                    }
                    var newPosTrailing = rand.Next(1, maxSpaceTrailing);
                    if (maxSpace < 5)
                    {
                        maxSpace = 5;
                    }
                    var newPosFirst = rand.Next(5, maxSpace);

                    if (totalShipSpacing > (52 - (enemySpaceShips.Count * 2)))
                    {
                        if (enemyShip.Equals(enemySpaceShips.First()))
                        {
                            enemyShip.posX = newPosFirst;
                        }
                        else
                        {
                            enemyShip.posX = newPosTrailing;
                        }
                    }
                    else
                    {
                        if (enemyShip.Equals(enemySpaceShips.First()))
                        {
                            enemyShip.posX = newPosFirst;
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
                enemyPosX = string.Empty;
                if (enemySpaceShips.First().posX < 5)
                {
                    enemyShip.posX = 5;
                }
                else
                {
                    enemyShip.posX = enemyShip.posX;
                }
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
                    scoreCount++;
                    AddEnemies(scoreCount);
                    return scoreCount;
                }
            }
            return scoreCount;
        }
    }
}
