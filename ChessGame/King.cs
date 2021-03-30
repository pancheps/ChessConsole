using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public class King : ChessPiece
    {
        public override bool IsKing()
        {
            return true;
        }
        public override bool IsInCheck()
        {
            return false;
        }
        public override bool IsAValidMovement(int row, int column)
        {
            if (Math.Abs(column - Position.Item2) > 1 || Math.Abs(row - Position.Item1) > 1)
                return false;
            if (!HasAPieceToKillOrFieldIsEmpty(row, column))
                return false;
            return true;
        }

        public override bool HasPathFree(int row, int column)
        {
            return true;
        }

        public King(string name, (int, int) position, string color)
        {
            Name = name;
            Position = position;
            Color = color;
        }
    }
}
