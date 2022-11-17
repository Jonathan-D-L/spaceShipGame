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
        private int _enemy { get; set; }
        private int _posX { get; set; }
        public EnemySpaceShip(int enemy, int posX)
        {
            _posX = posX;
            _enemy = enemy;
        }
        public int enemy
        {
            get { return _enemy; }
            set { _enemy = value; }
        }
        public int posX
        {
            get { return _posX; }
            set { _posX = value; }
        }


        public List<EnemySpaceShip> AddEnemies(int scoreCount)
        {
            var rand = new Random();
            if (scoreCount > 1)
            {

                for (int i = 0; i < scoreCount; i++)
                {
                    var pos = rand.Next(1, 50);
                    var enemy = new EnemySpaceShip(_enemy, _posX = pos);
                    enemySpaceShips.Add(enemy);
                }

            }
            else if (enemySpaceShips.Count == 0)
            {
                var pos = rand.Next(1, 50);
                var enemy = new EnemySpaceShip(_enemy, _posX = pos);
                enemySpaceShips.Add(enemy);
            }
            return enemySpaceShips;
        }
        public void SpawnEnemies()
        {
            var enemy = new CanvasImage("images\\enemy.png");
            string enemyPosX = string.Empty;
            var spacing = 0;

            foreach (var enemyShip in enemySpaceShips)
            {
                if (enemySpaceShips.Count > 1)
                {
                    if (spacing > 50 - enemySpaceShips.Count)
                    {

                        while (spacing > 50 - enemySpaceShips.Count)
                        {
                            var enemySpacing = new List<int>();
                            enemySpacing = enemySpaceShips.Select(s => s.posX).ToList();
                            var firstShip = enemySpaceShips[0];
                            foreach (var space in enemySpacing)
                            {
                                spacing += space;
                            }
                            foreach (var enemyPos in enemySpaceShips)
                            {
                                if (firstShip.posX < 5)
                                {
                                    firstShip.posX = 5;
                                }
                                else
                                {
                                    enemyShip.posX /= enemySpaceShips.Count;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= enemyShip.posX; i++)
                        {
                            enemyPosX += "  ";
                        }
                    }
                }
                else if (enemySpaceShips.Count == 1)
                {
                    for (int i = 0; i <= enemyShip.posX; i++)
                    {
                        enemyPosX += "  ";
                    }
                }
                Console.Write($"{enemyPosX}");
                AnsiConsole.Write(enemy);
                int top = Console.CursorTop;
                Console.SetCursorPosition(enemyPosX.Length + 2, Console.CursorTop - top + 1);
            }


        }
        public int EnemyKilled(string shotPosX, int scoreCount)
        {
            foreach (var ship in enemySpaceShips)
            {
                var shotPos = shotPosX.Length - ship.posX -2;
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
