using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShipGame
{
    internal class PlayGame
    {
        public void PlayingGame()
        {
            var imgSpaceShip = new CanvasImage("images\\spaceship.png");
            imgSpaceShip.MaxWidth(1);
            var enemy = new CanvasImage("images\\enemy.png");
            enemy.MaxWidth(1);
            var enemySpaceShip = new EnemySpaceShip();
            int enemyDifficutly = 0;
            enemySpaceShip.AddEnemies(enemyDifficutly);
            var spaceShip = imgSpaceShip;
            var moveX = "                                                      ";
            var moveY = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            var scoreCount = 0;
            var score = $"score: {scoreCount}";

            Console.CursorVisible = false;

            Console.WriteLine(score);
            enemySpaceShip.SpawnEnemies();
            Console.Write($"{moveY}{moveX}");
            AnsiConsole.Write(spaceShip);
            var action = '0';
            while (true)
            {
                bool wait = true;
                while (wait)
                {
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    action = key.KeyChar;
                    wait = false;
                }
                if (action == 'w' && moveY.Length >= 6)
                {
                    try
                    {
                        moveY = moveY.Remove(moveY.Length - 2);
                    }
                    catch
                    {

                    }
                }
                if (action == 'a' && moveX.Length >= 10)
                {
                    try
                    {
                        moveX = moveX.Remove(moveX.Length - 2);
                    }
                    catch
                    {

                    }
                }
                if (action == 's' && moveY.Length <= 50)
                {
                    moveY += "\r\n";
                }
                if (action == 'd' && moveX.Length <= 100)
                {
                    moveX += "  ";
                }
                if (action == ' ')
                {
                    var shot = "**";
                    var shotPosY = string.Empty;
                    try
                    {
                        shotPosY = moveY.Remove(moveY.Length - 2);
                    }
                    catch
                    {

                    }
                    var shotPosX = moveX;
                    var shipPosY = "\r\n";
                    bool shotMoving = true;
                    while (shotMoving)
                    {
                        System.Threading.Thread.Sleep(10);
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(score);
                            enemySpaceShip.SpawnEnemies();
                            Console.Write($"{shotPosY}{shotPosX}{shot}");
                            Console.Write($"{shipPosY}{moveX}");
                            AnsiConsole.Write(spaceShip);
                            shipPosY += "\r\n\r\n";
                            shotPosY = shotPosY.Remove(shotPosY.Length - 4);
                            scoreCount++;
                            score = $"score: {scoreCount}";
                            scoreCount = enemySpaceShip.EnemyKilled(shotPosX, scoreCount);
                        }
                        catch
                        {
                            shotMoving = false;
                            shotPosX = moveX;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine(score);
                    enemySpaceShip.SpawnEnemies();
                    Console.Write($"{moveY}{moveX}");
                    AnsiConsole.Write(spaceShip);

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(score);
                    enemySpaceShip.SpawnEnemies();
                    Console.Write($"{moveY}{moveX}");
                    AnsiConsole.Write(spaceShip);
                }
            }
        }
    }
}
