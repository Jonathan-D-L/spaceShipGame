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

            bool newLine = false;
            var spaceShip = imgSpaceShip;
            var moveX = "                                                      ";
            var moveY = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            var enemyPosX = string.Empty;
            var scoreCount = 0;
            var score = $"score: {scoreCount}";
            var rand = new Random();
            int j = rand.Next(1, 50);
            for (int i = 0; i < j; i++)
            {
                enemyPosX += "  ";
            }
            Console.CursorVisible = false;
            Console.WriteLine(score);
            Console.Write($"{enemyPosX}");
            AnsiConsole.Write(enemy);
            Console.Write($"{moveY}{moveX}");
            AnsiConsole.Write(spaceShip);
            while (true)
            {

                var action = Console.ReadKey().KeyChar;
                if (action == 'w')
                {
                    try
                    {
                        moveY = moveY.Remove(moveY.Length - 2);
                    }
                    catch
                    {

                    }
                }
                if (action == 'a')
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
                if (newLine == true)
                {
                    moveY += "\r\n";
                    newLine = false;
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
                            Console.Write($"{enemyPosX}");
                            AnsiConsole.Write(enemy);
                            Console.Write($"{shotPosY}{shotPosX}{shot}");
                            Console.Write($"{shipPosY}{moveX}");
                            AnsiConsole.Write(spaceShip);
                            shipPosY += "\r\n\r\n";
                            shotPosY = shotPosY.Remove(shotPosY.Length - 4);
                        }
                        catch
                        {
                            shotMoving = false;
                            shotPosX = moveX;
                        }
                    }
                    if (shotPosX == enemyPosX)
                    {
                        scoreCount++;
                        score = $"score: {scoreCount}";
                        enemyPosX = string.Empty;
                        j = rand.Next(1, 50);
                        for (int i = 0; i < j; i++)
                        {
                            enemyPosX += "  ";
                        }
                    }
                    Console.Clear();
                    Console.WriteLine(score);
                    Console.Write($"{enemyPosX}");
                    AnsiConsole.Write(enemy);
                    Console.Write($"{moveY}{moveX}");
                    AnsiConsole.Write(spaceShip);

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(score);
                    Console.Write($"{enemyPosX}");
                    AnsiConsole.Write(enemy);
                    Console.Write($"{moveY}{moveX}");
                    AnsiConsole.Write(spaceShip);
                }
            }
        }
    }
}
