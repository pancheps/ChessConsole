using System;

namespace ChessGame
{
    class Bishop : ChessPiece
    {
        public Bishop(string name, (int, int) position, string color)
        {
            Name = name;
            Position = position;
            Color = color;
        }

        public override bool HasPathFree(int row, int column)
        {
            var directionX = row > Position.Item1 ? 1 : -1;
            var directionY = column > Position.Item2 ? 1 : -1;
            for(int i = 1; i < Math.Abs(row - Position.Item1); i++)
            {
                if (ChessBoard.Table[Position.Item1+i*directionX,Position.Item2+i*directionY] != null)
                    return false;
            }
            return true;
        }

        public override bool IsAValidMovement(int row, int column)
        {
            if(Math.Abs(row - Position.Item1) != Math.Abs(column - Position.Item2))
                return false;
            if(!HasPathFree(row,column) || !HasAPieceToKillOrFieldIsEmpty(row,column))
                return false;
            return true;
        }
    }
}
