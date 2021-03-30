using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    class Queen : ChessPiece
    {
        public Queen(string name, (int, int) position, string color)
        {
            Name = name;
            Position = position;
            Color = color;
         }

        public override bool HasPathFree(int row, int column)
        {
            if(Position.Item1 == row || Position.Item2 == column)
            {
                if (Position.Item1 == row)
                {
                    int direction = column > Position.Item2 ? 1 : -1;
                    for (int i = 1; i < Math.Abs(column - Position.Item2); i++)
                    {
                        if (ChessBoard.Table[Position.Item1, Position.Item2 + i * direction] != null)
                            return false;
                    }
                }
                else
                {
                    int direction = row > Position.Item1 ? 1 : -1;
                    for (int i = 1; i < Math.Abs(row - Position.Item1); i++)
                    {
                        if (ChessBoard.Table[Position.Item1 + i * direction, Position.Item2] != null)
                            return false;
                    }
                }
            }
            else 
            {
                var directionX = row > Position.Item1 ? 1 : -1;
                var directionY = column > Position.Item2 ? 1 : -1;
                for (int i = 1; i < Math.Abs(row - Position.Item1); i++)
                {
                    if (ChessBoard.Table[Position.Item1 + i * directionX, Position.Item2 + i * directionY] != null)
                        return false;
                }
            }
            return true;

        }

        public override bool IsAValidMovement(int row, int column)
        {
            if (Position.Item1 != row && Position.Item2 != column && Math.Abs(row - Position.Item1) != Math.Abs(column - Position.Item2))
                return false;
            if (!HasPathFree(row, column) || !HasAPieceToKillOrFieldIsEmpty(row, column))
                return false;
            return true;

        }
    }
}
