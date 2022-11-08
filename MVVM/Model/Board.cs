using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Board
    {
        Tile[,] board = new Tile[8, 8];

        public Board(BlackWhite PlayerColor)
        {
            CreateNewBoard();
            CreateNewPieces(PlayerColor);
        }

        // Method creates new array of empty tiles
        void CreateNewBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Tile((BlackWhite)((i + j) % 2), null);
                }
            }
        }


        // Method takes player's color and sets all Chess Pieces on their cells.
        void CreateNewPieces(BlackWhite PlayerColor)
        {
            BlackWhite holder;
            
            // Depending on player's color, method places the king and the queen on different tiles
            // and also sets a different colors to all pieces.
            if(PlayerColor == BlackWhite.White)
            {
                holder = BlackWhite.White;
                for (int i = 0; i < 8; i+=7)
                {
                    board[i, 0].ChessPiece = new Rook(holder);
                    board[i, 1].ChessPiece = new Knight(holder);
                    board[i, 2].ChessPiece = new Bishop(holder);
                    board[i, 3].ChessPiece = new King(holder);
                    board[i, 4].ChessPiece = new Queen(holder);
                    board[i, 5].ChessPiece = new Bishop(holder);
                    board[i, 6].ChessPiece = new Knight(holder);
                    board[i, 7].ChessPiece = new Rook(holder);
                    holder = BlackWhite.Black;
                }
                for(int i = 6; i > 0; i-=5)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        board[i, j].ChessPiece = new Pawn(holder);
                    }
                    holder = BlackWhite.White;
                }
            }
            else
            {
                holder = BlackWhite.Black;
                for (int i = 0; i < 8; i += 7)
                {
                    board[i, 0].ChessPiece = new Rook(holder);
                    board[i, 1].ChessPiece = new Knight(holder);
                    board[i, 2].ChessPiece = new Bishop(holder);
                    board[i, 3].ChessPiece = new King(holder);
                    board[i, 4].ChessPiece = new Queen(holder);
                    board[i, 5].ChessPiece = new Bishop(holder);
                    board[i, 6].ChessPiece = new Knight(holder);
                    board[i, 7].ChessPiece = new Rook(holder);
                    holder = BlackWhite.White;
                }
                for (int i = 6; i > 0; i -= 5)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        board[i, j].ChessPiece = new Pawn(holder);
                    }
                    holder = BlackWhite.Black;
                }
            }
        }
    }
}
