using ChessWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessWPF.MVVM.ViewModel
{
    internal class MainViewModel
    {

        public ObservableCollection<Tile> board { get; set; }
        public MainViewModel()
        {
            board = new ObservableCollection<Tile>();
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            SolidColorBrush black = new SolidColorBrush(Colors.Black);
            CreateNewBoard();
            CreateChessPieces(PlayerColor.White,white,black);
        }
        void CreateNewBoard()
        {
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (((i + j) % 2) == 0)
                        board.Add(new Tile(new SolidColorBrush(Colors.Gray), null));
                    else
                        board.Add(new Tile(new SolidColorBrush(Colors.DarkGray), null));
                }
            }
        }
        void CreateChessPieces(PlayerColor playerColor, SolidColorBrush white, SolidColorBrush black)
        {

            if (playerColor == PlayerColor.White)
            {
                board[0].ChessPiece = new Rook(white);
                board[1].ChessPiece = new Knight(white);
                board[2].ChessPiece = new Bishop(white);
                board[3].ChessPiece = new Queen(white);
                board[4].ChessPiece = new King(white);
                board[5].ChessPiece = new Bishop(white);
                board[6].ChessPiece = new Knight(white);
                board[7].ChessPiece = new Rook(white);
                board[8].ChessPiece = new Pawn(white);
                board[9].ChessPiece = new Pawn(white);
                board[10].ChessPiece = new Pawn(white);
                board[11].ChessPiece = new Pawn(white);
                board[12].ChessPiece = new Pawn(white);
                board[13].ChessPiece = new Pawn(white);
                board[14].ChessPiece = new Pawn(white);
                board[15].ChessPiece = new Pawn(white);

                board[48].ChessPiece = new Pawn(black);
                board[49].ChessPiece = new Pawn(black);
                board[50].ChessPiece = new Pawn(black);
                board[51].ChessPiece = new Pawn(black);
                board[52].ChessPiece = new Pawn(black);
                board[53].ChessPiece = new Pawn(black);
                board[54].ChessPiece = new Pawn(black);
                board[55].ChessPiece = new Pawn(black);
                board[56].ChessPiece = new Rook(black);
                board[57].ChessPiece = new Knight(black);
                board[58].ChessPiece = new Bishop(black);
                board[59].ChessPiece = new Queen(black);
                board[60].ChessPiece = new King(black);
                board[61].ChessPiece = new Bishop(black);
                board[62].ChessPiece = new Knight(black);
                board[63].ChessPiece = new Rook(black);
            }
            else
            {
                board[0].ChessPiece = new Rook(black);
                board[1].ChessPiece = new Knight(black);
                board[2].ChessPiece = new Bishop(black);
                board[3].ChessPiece = new King(black);
                board[4].ChessPiece = new Queen(black);
                board[5].ChessPiece = new Bishop(black);
                board[6].ChessPiece = new Knight(black);
                board[7].ChessPiece = new Rook(black);
                board[8].ChessPiece = new Pawn(black);
                board[9].ChessPiece = new Pawn(black);
                board[10].ChessPiece = new Pawn(black);
                board[11].ChessPiece = new Pawn(black);
                board[12].ChessPiece = new Pawn(black);
                board[13].ChessPiece = new Pawn(black);
                board[14].ChessPiece = new Pawn(black);
                board[15].ChessPiece = new Pawn(black);

                board[48].ChessPiece = new Pawn(white);
                board[49].ChessPiece = new Pawn(white);
                board[50].ChessPiece = new Pawn(white);
                board[51].ChessPiece = new Pawn(white);
                board[52].ChessPiece = new Pawn(white);
                board[53].ChessPiece = new Pawn(white);
                board[54].ChessPiece = new Pawn(white);
                board[55].ChessPiece = new Pawn(white);
                board[56].ChessPiece = new Rook(white);
                board[57].ChessPiece = new Knight(white);
                board[58].ChessPiece = new Bishop(white);
                board[59].ChessPiece = new King(white);
                board[60].ChessPiece = new Queen(white);
                board[61].ChessPiece = new Bishop(white);
                board[62].ChessPiece = new Knight(white);
                board[63].ChessPiece = new Rook(white);
            }


        }


    }
}
