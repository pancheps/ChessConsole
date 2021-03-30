using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    class Knight : ChessPiece
    {
        public Knight(string name, (int, int) position, string color)
        {
            Name = name;
            Position = position;
            Color = color;
        }

        public override bool HasPathFree(int row, int column)
        {
            if (Position.Item1 - 2 == row || Position.Item1 + 2 == row)
            {
                if (Position.Item2 + 1 != column && Position.Item2 - 1 != column)
                    return false;
            }
            if (Position.Item1 - 1 == row || Position.Item1 + 1 == row)
            {
                if (Position.Item2 + 2 != column && Position.Item2 - 2 != column)
                    return false;
            }
            return true;
        }

        public override bool IsAValidMovement(int row, int column)
        {
            if (Math.Abs(row - Position.Item1) > 2 || Math.Abs(column - Position.Item2) > 2)
                return false;
            if (!HasPathFree(row, column) || !HasAPieceToKillOrFieldIsEmpty(row, column))
                return false;
            return true;
        }
    }
}
