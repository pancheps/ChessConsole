using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    class Pawn : ChessPiece
    {
        public Pawn(string name, (int, int) position, string color)
        {
            Name = name;
            Position = position;
            Color = color;
        }

        public override bool HasPathFree(int row, int column)
        {
            if (ChessBoard.Table[row, column] == null && Math.Abs(column - Position.Item2) != 0)
                return false;
            return true;
        }

        public override bool HasAPieceToKillOrFieldIsEmpty(int row, int column)
        {
            if(ChessBoard.Table[row, column] != null)
            {
                if (Math.Abs(column - Position.Item2) == 0)
                    return false;
                if (ChessBoard.Table[row, column].Color == Color)
                    return false;
            }
            return true;
        }

        public override bool IsAValidMovement(int row, int column)
        {
            if (row == Position.Item1 || Math.Abs(column - Position.Item2) > 1 || Math.Abs(row - Position.Item1) != 1)
                return false;
            if (Color == "White" && row > Position.Item1)
                return false;
            if (Color == "Black" && row < Position.Item1)
                return false;
            if (!HasPathFree(row,column) || !HasAPieceToKillOrFieldIsEmpty(row, column))
                return false;
            return true;
        }
    }
}
