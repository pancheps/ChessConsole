using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public abstract class ChessPiece
    {
        public string Name { get; set; }
        public (int,int) Position { get; set; }
        public string Color { get; set; }
        public virtual bool IsInCheck()
        {
            return false;
        }
        public override string ToString()
        {
            return Name;
        }
        public virtual bool IsKing()
        {
            return false;
        }
        public void MovePiece(int row, int column)
        {
            if (ChessBoard.Table[row, column] != null && ChessBoard.Table[row, column].Color != Color)
            {
                if (Color == "Black")
                {
                    ChessBoard.PlayerOne.Pieces.Remove(ChessBoard.Table[row, column]);
                    ChessBoard.PlayerOne.NumberOfRemainingPieces-= 1;
                }
                else
                {
                    ChessBoard.PlayerTwo.Pieces.Remove(ChessBoard.Table[row, column]);
                    ChessBoard.PlayerTwo.NumberOfRemainingPieces -= 1;
                }
            }
            Position = (row, column);
        }
        public virtual bool HasAPieceToKillOrFieldIsEmpty(int row, int column)
        {
            return ChessBoard.Table[row, column] == null || (ChessBoard.Table[row, column] != null && ChessBoard.Table[row, column].Color != Color);
        }
        public abstract bool IsAValidMovement(int row, int column);
        public abstract bool HasPathFree(int row, int column);
    }
}
