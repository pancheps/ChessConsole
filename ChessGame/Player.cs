using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public class Player
    {
        public List<ChessPiece> Pieces { get; set; }
        public string ColorOfPlayer { get; set; }
        public int NumberOfRemainingPieces { get; set; }
        public bool HaveACheck() 
        {
            var piece = Pieces.Find(x => x.IsKing());
            return piece.IsInCheck();
        }
        public void NamePieces()
        {
            AddKingOfColor(ColorOfPlayer.Substring(0,1));
            AddQueenOfColor(ColorOfPlayer.Substring(0, 1));
            AddKnightsOfColor(ColorOfPlayer.Substring(0, 1));
            AddBishopsOfColor(ColorOfPlayer.Substring(0, 1));
            AddRooksOfColor(ColorOfPlayer.Substring(0, 1));
            AddPawsOfColor(ColorOfPlayer.Substring(0, 1));
        }
        private void AddQueenOfColor(string color)
        {
            Pieces.Add(new Queen(color + "Q ", (color == "B") ? (0, 4) : (7, 4), ColorOfPlayer));
        }
        private void AddKingOfColor(string color)
        {
            Pieces.Add(new King(color + "K ", (color == "B") ? (0, 3) : (7, 3), ColorOfPlayer));
        }
        private void AddPawsOfColor(string color)
        {
            for (int i = 0; i < 8; i++)
                Pieces.Add(new Pawn(color + "P" + (i+1), (color == "B") ? (1, i) : (6, i), ColorOfPlayer));
        }
        private void AddKnightsOfColor(string color)
        {
            Pieces.Add(new Knight(color + "H" + 1, (color == "B") ? (0, 1) : (7, 1), ColorOfPlayer));
            Pieces.Add(new Knight(color + "H" + 2, (color == "B") ? (0, 6) : (7, 6), ColorOfPlayer));
        }
        private void AddBishopsOfColor(string color)
        {
            Pieces.Add(new Bishop(color + "B" + 1, (color == "B") ? (0, 2) : (7, 2), ColorOfPlayer));
            Pieces.Add(new Bishop(color + "B" + 2, (color == "B") ? (0, 5) : (7, 5), ColorOfPlayer));
        }
        private void AddRooksOfColor(string color)
        {
            Pieces.Add(new Rook(color + "R" + 1, (color == "B") ? (0, 0) : (7, 0), ColorOfPlayer));
            Pieces.Add(new Rook(color + "R" + 2, (color == "B") ? (0, 7) : (7, 7), ColorOfPlayer));
        }
        public List<ChessPiece> GetPiecesOfRow(int row)
        {
            var piecesOfRow = Pieces.FindAll(piece => piece.Position.Item1 == row);
            return piecesOfRow;
        }
        public void MovePiece(string nameOfPiece, (int,int) position)
        {
            var piece = Pieces.Find(piece => piece.Name == nameOfPiece);
            piece.MovePiece(position.Item1, position.Item2);
        }
        public bool IsAValidMovement(string nameOfPiece, int row, int column)
        {
            var piece = Pieces.Find(piece => piece.Name == nameOfPiece);
            return piece.IsAValidMovement(row, column);
        }
        public Player(string colorOfPlayer)
        {
            ColorOfPlayer = colorOfPlayer;
            Pieces = new List<ChessPiece>();
            NamePieces();
            NumberOfRemainingPieces = 16;
        }
    }
}
