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


        public List<EnemySpaceShip> AddEnemies(int enemyDifficutly)
        {
            bool setRandomShipPos = true;
            var rand = new Random();
            if (enemyDifficutly >= 5)
            {
                while (setRandomShipPos)
                {
                    var pos = rand.Next(1, 50);
                    if (!enemySpaceShips.Where(s => s.Equals(pos)).Any())
                    {
                        var enemy = new EnemySpaceShip(_enemy = 1, _posX = pos);
                        enemySpaceShips.Add(enemy);
                    }
                }
            }
            else if (enemySpaceShips.Count == 0)
            {
                var pos = rand.Next(1, 50);
                var enemy = new EnemySpaceShip(_enemy = 1, _posX = pos);
                var enemy1 = new EnemySpaceShip(_enemy = 1, _posX = pos);
                enemySpaceShips.Add(enemy);
                enemySpaceShips.Add(enemy1);
            }
            return enemySpaceShips;
        }
        public void SpawnEnemies()
        {
            var enemy = new CanvasImage("images\\enemy.png");
            string enemyPosX = string.Empty;
            bool firstShip = true;
            foreach (var enemyShip in enemySpaceShips)
            {
                if (enemySpaceShips.Count > 1)
                {
                    for (int i = 0; i <= enemyShip.posX / (enemySpaceShips.Count * enemySpaceShips.Count); i++)
                    {
                        enemyPosX += "  ";

                    }
                    if (firstShip == true)
                    {
                        if (enemyPosX.Length <= 10)
                        {
                            int startPos = 5 - enemyPosX.Length;
                            for (int i = 0; i <= startPos; i++)
                            {
                                enemyPosX += "  ";
                            }
                        }
                        firstShip = false;
                    }
                }
                else
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
            Console.SetCursorPosition(default, default);


        }
    }
}
