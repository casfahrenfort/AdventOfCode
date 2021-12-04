using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day4 : Day
    {
        public Day4()
            : base(4, true)
        { }

        public override void Solve()
        {
            int[] calledNumbers = input[0].Split(',').Select(i => int.Parse(i)).ToArray();
            List<Board> boards = new List<Board>();

            for (int i = 2; i < input.Count; i += 6)
            {
                boards.Add(new Board(new string[] { input[i], input[i + 1], input[i + 2], input[i + 3], input[i + 4] }));
            }

            List<Board> playingBoards = new List<Board>(boards);

            for (int i = 0; i < calledNumbers.Length; i++)
            {
                foreach(Board board in playingBoards.ToList())
                {
                    board.MarkNumber(calledNumbers[i]);
                    board.DrawBoard();
                    if (board.HasWon())
                    {
                        if (playingBoards.Count == 1)
                        {
                            Console.WriteLine(board.GetScore(calledNumbers[i]));
                            return;
                        }
                        playingBoards.Remove(board);
                    }
                }
            }

        }
    }

    public class Board
    {
        int[,] rows = new int[5, 5];
        bool[,] marked = new bool[5, 5];
        bool won = false;

        public Board(string[] rows)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                string[] split = rows[i].Split(" ").Where(s => s != "").ToArray();
                for (int j = 0; j < split.Length; j++)
                {
                    this.rows[i, j] = int.Parse(split[j]);
                    this.marked[i, j] = false;
                }
            }
        }

        public void MarkNumber(int number)
        {
            if (won)
                return;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (rows[i,j] == number)
                        marked[i, j] = true;
                }
        }

        public int GetScore(int calledNumber)
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (!marked[i, j])
                        sum += rows[i, j];
                }

            return sum * calledNumber;
        }

        public bool HasWon()
        {
            for(int i = 0; i < 5; i++)
            {
                bool columnWon = true;
                for(int j = 0; j < 5; j++)
                {
                    if (!marked[i, j])
                    {
                        columnWon = false;
                        break;
                    }
                }

                if (columnWon)
                {

                    won = true;
                    return true;
                }
            }

            for (int j = 0; j < 5; j++)
            {
                bool rowWon = true;
                for (int i = 0; i < 5; i++)
                {
                    if (!marked[i, j])
                    {
                        rowWon = false;
                        break;
                    }
                }

                if (rowWon)
                {
                    won = true;
                    return true;
                }
            }

            return false;
        }

        public void DrawBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (rows[i, j] < 10)
                        Console.Write(" ");
                    var color = ConsoleColor.Gray;
                    if (marked[i, j])
                        color = ConsoleColor.Yellow;
                    Console.ForegroundColor = color;
                    Console.Write(rows[i, j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
