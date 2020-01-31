﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammingProject.Model;

namespace ProgrammingProject
{
    public class SongGuess
    {
        public static int _guesses { get; set; }
        public static void Start()
        {
            if (_guesses != 2)
            {
                Console.WriteLine("");
                var context = new AppDBContext();
                var currentScore = AuthenticationService.GetScore(AuthenticationService.CurrentUser);


                //Get Random Song
                var song = context.Songs
                    .OrderBy(r => Guid.NewGuid()).Take(5)
                    .FirstOrDefault();


                Console.WriteLine("Artist: " + song.Artist);
                Console.WriteLine("Title: " + song.Name.ToCharArray()[0]);
                Console.WriteLine("What is the song's name?");
                Console.WriteLine("");
                Console.Write("Guess: ");
                var userAnswer = Console.ReadLine();
                if (userAnswer.ToLower() == song.Name.ToLower())
                {
                    Console.WriteLine("Correct! Press ENTER or RETURN to try another. ");
                    AuthenticationService.AddScore(AuthenticationService.CurrentUser, currentScore);
                    var enter = Console.ReadLine();
                    if (enter == "")
                    {
                        Console.Clear();
                        Start();
                    }

                }
                else
                {
                    Console.WriteLine("Incorrect. Press ENTER or RETURN to try again. ");
                    _guesses++;
                    var enter = Console.ReadLine();
                    if (enter == "")
                    {
                        Console.Clear();
                        Start();
                    }
                }
            }
            else
            {
                Console.WriteLine("Game over! Maximum amount of guesses reached. ");
                Console.WriteLine("");
                Console.WriteLine("Pess ENTER or RETURN to continue. ");
                var enter = Console.ReadLine();
                if (enter == "")
                {
                    Console.Clear();
                    AppStart.SelectionScreen();
                }

            }

        }
    }
}
