using ChessWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChessWPF.MVVM.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        // It was a stupid idea to make tile contain a chess piece in itself, I'm gonna separate them soon.
        public ObservableCollection<Tile> Board { get; set; }

        PlayerColor playerTurn = PlayerColor.White;

        private int selectedTile = -1;
        private SolidColorBrush temp;
        
        public int SelectedTile
        {
            get
            {
                return selectedTile;
            }
            set
            {
                if(selectedTile != -1)
                {

                    if (Board[value].isOccupied())
                    {
                        if (Board[value].ChessPiece.PlayerColor == playerTurn)
                        {
                            deselectTile();
                            MessageBox.Show("It's your piece!");
                        }
                        else
                        {
                            makeMove(value);
                            nextTurn();
                        }
                    }
                    else
                    {
                        makeMove(value);
                        nextTurn();
                    }
                    
                }
                else if(Board[value].isOccupied())
                {
                    if (Board[value].ChessPiece.PlayerColor == playerTurn)
                        selectTile(value);
                    else
                        MessageBox.Show("It's not your turn!");
                }


                OnPropertyChanged("SelectedTile");

            }
        }

        private void nextTurn()
        {
            if (playerTurn == PlayerColor.White)
                playerTurn = PlayerColor.Black;
            else
                playerTurn = PlayerColor.White;
        }
        private void makeMove(int value)
        {
            Board[value].ChessPiece = Board[selectedTile].ChessPiece;
            Board[selectedTile].ChessPiece = null;
            deselectTile();
        }
        private void deselectTile()
        {
            Board[selectedTile].TileColor = temp;
            selectedTile = -1;
        }
        private void selectTile(int value)
        {
            temp = Board[value].TileColor;
            selectedTile = value;
            Board[selectedTile].TileColor = selectedTileColor;
        }


        private SolidColorBrush whiteTile = (SolidColorBrush)new BrushConverter().ConvertFrom("#696969");
        private SolidColorBrush blackTile = (SolidColorBrush)new BrushConverter().ConvertFrom("#494949");
        private SolidColorBrush selectedTileColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFAFD8");

        private SolidColorBrush whitePiece = new SolidColorBrush(Colors.White);
        private SolidColorBrush blackPiece = new SolidColorBrush(Colors.Black);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            Board = new ObservableCollection<Tile>();
            
            //Board.CollectionChanged += Board_CollectionChanged;
            CreateNewBoard();
            CreateChessPieces(PlayerColor.Black, whitePiece, blackPiece);
        }
        void CreateNewBoard()
        {
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (((i + j) % 2) == 0)
                        Board.Add(new Tile(whiteTile, null));
                    else
                        Board.Add(new Tile(blackTile, null));
                }
            }
        }

        void CreateChessPieces(PlayerColor playerColor, SolidColorBrush whitePiece, SolidColorBrush blackPiece)
        {

            if (playerColor == PlayerColor.White)
            {
                Board[0].ChessPiece = new Rook(whitePiece, PlayerColor.White);
                Board[1].ChessPiece = new Knight(whitePiece, PlayerColor.White);
                Board[2].ChessPiece = new Bishop(whitePiece, PlayerColor.White);
                Board[3].ChessPiece = new Queen(whitePiece, PlayerColor.White);
                Board[4].ChessPiece = new King(whitePiece, PlayerColor.White);
                Board[5].ChessPiece = new Bishop(whitePiece, PlayerColor.White);
                Board[6].ChessPiece = new Knight(whitePiece, PlayerColor.White);
                Board[7].ChessPiece = new Rook(whitePiece, PlayerColor.White);
                Board[8].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[9].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[10].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[11].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[12].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[13].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[14].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[15].ChessPiece = new Pawn(whitePiece, PlayerColor.White);

                Board[48].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[49].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[50].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[51].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[52].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[53].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[54].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[55].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[56].ChessPiece = new Rook(blackPiece, PlayerColor.Black);
                Board[57].ChessPiece = new Knight(blackPiece, PlayerColor.Black);
                Board[58].ChessPiece = new Bishop(blackPiece, PlayerColor.Black);
                Board[59].ChessPiece = new Queen(blackPiece, PlayerColor.Black);
                Board[60].ChessPiece = new King(blackPiece, PlayerColor.Black);
                Board[61].ChessPiece = new Bishop(blackPiece, PlayerColor.Black);
                Board[62].ChessPiece = new Knight(blackPiece, PlayerColor.Black);
                Board[63].ChessPiece = new Rook(blackPiece, PlayerColor.Black);
            }
            else
            {
                Board[0].ChessPiece = new Rook(blackPiece, PlayerColor.Black);
                Board[1].ChessPiece = new Knight(blackPiece, PlayerColor.Black);
                Board[2].ChessPiece = new Bishop(blackPiece, PlayerColor.Black);
                Board[3].ChessPiece = new King(blackPiece, PlayerColor.Black);
                Board[4].ChessPiece = new Queen(blackPiece, PlayerColor.Black);
                Board[5].ChessPiece = new Bishop(blackPiece, PlayerColor.Black);
                Board[6].ChessPiece = new Knight(blackPiece, PlayerColor.Black);
                Board[7].ChessPiece = new Rook(blackPiece, PlayerColor.Black);
                Board[8].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[9].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[10].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[11].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[12].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[13].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[14].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);
                Board[15].ChessPiece = new Pawn(blackPiece, PlayerColor.Black);

                Board[48].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[49].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[50].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[51].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[52].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[53].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[54].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[55].ChessPiece = new Pawn(whitePiece, PlayerColor.White);
                Board[56].ChessPiece = new Rook(whitePiece, PlayerColor.White);
                Board[57].ChessPiece = new Knight(whitePiece, PlayerColor.White);
                Board[58].ChessPiece = new Bishop(whitePiece, PlayerColor.White);
                Board[59].ChessPiece = new King(whitePiece, PlayerColor.White);
                Board[60].ChessPiece = new Queen(whitePiece, PlayerColor.White);
                Board[61].ChessPiece = new Bishop(whitePiece, PlayerColor.White);
                Board[62].ChessPiece = new Knight(whitePiece, PlayerColor.White);
                Board[63].ChessPiece = new Rook(whitePiece, PlayerColor.White);
            }


        }


    }
}
