using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public class ChessBoard
    {
        public static Player PlayerOne { get; set; }
        public static Player PlayerTwo { get; set; }
        public int Winner { get; set; }
        public static ChessPiece[,] Table { get; set; }
        public int NumberOfTurnWithoutEatingPieces { get; set; }
        public void ShowChessTable()
        {
            Table = new ChessPiece[8, 8];
            PrintColumnNumeration();
            for (int i = 0; i < 8; i++)
            {
                PrintRowSeparator();
                ShowPiecesInARow(i);
            }
            PrintRowSeparator();
        }
        private static void PrintColumnNumeration()
        {
            Console.WriteLine("     1   2   3   4   5   6   7   8  ");
        }

        private static void PrintRowSeparator()
        {
            Console.WriteLine("   ---------------------------------");
        }

        public void PlayGame()
        {
            bool firstTurn = true;
            Console.WriteLine("Do you want to start first? (1. Yes or 2. No):");
            int numberOfPlayer = int.Parse(Console.ReadLine());
            do
            {   
                if (numberOfPlayer == 1)
                {
                    if (!CheckIfHaveWinner() || firstTurn)
                    {
                        Console.WriteLine("Turns without eating pieces: " + NumberOfTurnWithoutEatingPieces);
                        PlayerMovePiece(PlayerOne);
                        CheckPieces();
                    }
                    if (!CheckIfHaveWinner())
                    {
                        Console.WriteLine("Turns without eating pieces: " + NumberOfTurnWithoutEatingPieces);
                        PlayerMovePiece(PlayerTwo);
                        CheckPieces();
                    }
                }
                else
                {
                    if (!CheckIfHaveWinner() || firstTurn)
                    {
                        Console.WriteLine("Turns without eating pieces: " + NumberOfTurnWithoutEatingPieces);
                        PlayerMovePiece(PlayerTwo);
                        CheckPieces();
                    }
                    if (!CheckIfHaveWinner())
                    {
                        Console.WriteLine("Turns without eating pieces: " + NumberOfTurnWithoutEatingPieces);
                        PlayerMovePiece(PlayerOne);
                        CheckPieces();
                    }
                }
                firstTurn = false;
            } while (Winner == 0);
            if (Winner == 3)
                Console.WriteLine("Tie");
            else
                Console.WriteLine("The Winner is the player " + Winner);
        }
        private bool CheckIfHaveWinner()
        {
            return !CheckIfPlayersHaveKings() || HaveTenTurnsWithoutEatingPieces();
        }
        public bool CheckIfPlayersHaveKings()
        {
            if (PlayerOne.Pieces.Find(piece => piece.Name == "WK ") == null)
            {
                Winner = 2;
                return false;
            }
            if (PlayerTwo.Pieces.Find(piece => piece.Name == "BK ") == null)
            {
                Winner = 1;
                return false;
            }
            return true;
        }
        private bool HaveTenTurnsWithoutEatingPieces()
        {
            if (NumberOfTurnWithoutEatingPieces == 10)
            {
                if (PlayerOne.NumberOfRemainingPieces > PlayerTwo.NumberOfRemainingPieces)
                {
                    Winner = 1;
                    return true;
                }
                else
                {
                    if (PlayerOne.NumberOfRemainingPieces < PlayerTwo.NumberOfRemainingPieces)
                    {
                        Winner = 2;
                        return true;
                    }
                    else
                    {
                        Winner = 3;
                        return true;
                    }
                }
            }
            return false;
        }
        public void CheckPieces()
        { 
            if(PlayerOne.NumberOfRemainingPieces == PlayerTwo.NumberOfRemainingPieces) 
            {
                NumberOfTurnWithoutEatingPieces++;
            }
            else 
            {
                NumberOfTurnWithoutEatingPieces = 0;
            }
        }
        public void PlayerMovePiece(Player player)
        {
            bool nextTurn = false;
            do
            {
                Console.WriteLine("Turn of player with " + player.ColorOfPlayer + " pieces");
                ShowChessTable();
                Console.WriteLine("What piece do you want select:");
                var pieceName = Console.ReadLine();
                if (PieceExists(player, pieceName))
                {
                    Console.WriteLine("To what row do you want to move the piece?");
                    var row = int.Parse(Console.ReadLine());
                    Console.WriteLine("To what column do you want to move the piece?");
                    var column = int.Parse(Console.ReadLine());
                    if (IsInValidRowsAndColumns(row, column))
                    {
                        if (player.IsAValidMovement(pieceName, row - 1, column - 1))
                        {
                            player.MovePiece(pieceName, (row - 1, column - 1));
                            nextTurn = true;
                        }
                        else
                            Console.WriteLine("This is a not valid movement");
                    }
                    else
                        Console.WriteLine("Incorrect position");
                }
                else
                    Console.WriteLine("You don't have this piece");

            } while (nextTurn == false);
        }

        private static bool IsInValidRowsAndColumns(int row, int column)
        {
            return row <= 8 && column <= 8 && row > 0 && column > 0;
        }
        public void WritePiecesInRow(int row)
        {
            var piecesOfPlayersInARow = PlayerOne.GetPiecesOfRow(row);
            piecesOfPlayersInARow.AddRange(PlayerTwo.GetPiecesOfRow(row));
            foreach (var piece in piecesOfPlayersInARow)
                Table[piece.Position.Item1, piece.Position.Item2] = piece;
        }
        public void ShowPiecesInARow(int row)
        {
            WritePiecesInRow(row);
            Console.Write(" " + (row + 1) + " ");
            for (int i = 0; i < 8; i++)
            {
                if (Table[row, i] == null)
                    Console.Write("|   ");
                else
                    Console.Write("|" + Table[row, i]);
            }
            Console.WriteLine("|");
        }
        public bool PieceExists(Player player, string pieceName)
        {
            return (player.Pieces.FindAll(piece => piece.Name == pieceName)).Count > 0; 
        }
        public ChessBoard()
        {
            Winner = 0;
            Table = new ChessPiece[8,8];
            PlayerOne = new Player("White");
            PlayerTwo = new Player("Black");
            NumberOfTurnWithoutEatingPieces = 0;
        }
    }
}
