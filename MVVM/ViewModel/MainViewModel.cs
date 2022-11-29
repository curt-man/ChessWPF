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
        public ObservableCollection<Tile> Board { get; set; }

        private PlayerColor playerTurn = PlayerColor.White;

        public PlayerColor PlayerTurn
        {
            get { return playerTurn; }
            set
            {
                playerTurn = value;
                OnPropertyChanged("PlayerTurn");
            }
        }

        int selectedTile = -1;
        
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
                        if (Board[value].ChessPiece.isSameColor(playerTurn))
                        {
                            deselectTile();
                            //MessageBox.Show("It's your piece!");
                        }
                        else
                        {
                            if(Board[value].TileColor == possibleToMoveTileColor)
                            {
                                makeMove(value);
                                nextTurn();
                            }
                            else
                            {
                                deselectTile();
                            }
                        }
                    }
                    else
                    {
                        if (Board[value].TileColor == possibleToMoveTileColor)
                        {
                            makeMove(value);
                            nextTurn();
                        }
                        else
                        {
                            deselectTile();
                        }
                    }
                    
                }
                else if(Board[value].isOccupied())
                {
                    if (Board[value].ChessPiece.isSameColor(playerTurn))
                    {
                        selectTile(value);
                        showPossibleMoves();
                    }
                        
                    //else
                    //    MessageBox.Show("It's not your turn!");
                }


                OnPropertyChanged("SelectedTile");

            }
        }
        void nextTurn()
        {
            if (PlayerTurn == PlayerColor.White)
                PlayerTurn = PlayerColor.Black;
            else
                PlayerTurn = PlayerColor.White;
        }
        void makeMove(int value)
        {
            Board[value].ChessPiece = Board[selectedTile].ChessPiece;
            Board[selectedTile].ChessPiece.hasMoved = true;
            Board[selectedTile].ChessPiece = null;
            deselectTile();
        }
        void deselectTile()
        {
            returnTileColors();
            selectedTile = -1;
        }
        void selectTile(int value)
        {
            selectedTile = value;
            Board[selectedTile].TileColor = selectedTileColor;
        }
        void showPossibleMoves()
        {
            int[] possibleMoves = Board[selectedTile].ChessPiece.CalculatePossibleMoves(selectedTile);
            foreach(int move in possibleMoves)
            {
                
                //if (!Board[move].isOccupied())
                    Board[move].TileColor = possibleToMoveTileColor;
            }
        }
        void returnTileColors()
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (((i + j) % 2) == 0)
                        Board[i * 8 + j].TileColor = whiteTile;
                    else
                        Board[i * 8 + j].TileColor = blackTile;
                }
            }
        }

        #region "Colors"
        private SolidColorBrush whiteTile = (SolidColorBrush)new BrushConverter().ConvertFrom("#47C0DD");

        public SolidColorBrush WhiteTile
        {
            get { return whiteTile; }
            set
            {
                whiteTile = value;
                OnPropertyChanged("WhiteTile");
            }
        }
        private SolidColorBrush blackTile = (SolidColorBrush)new BrushConverter().ConvertFrom("#198DA9");

        public SolidColorBrush BlackTile
        {
            get { return blackTile; }
            set
            {
                blackTile = value;
                OnPropertyChanged("WhiteTile");
            }
        }

        private SolidColorBrush selectedTileColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#008080");
        private SolidColorBrush possibleToMoveTileColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#008061");


        // Colors of pieces
        private SolidColorBrush whitePiece = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
        private SolidColorBrush blackPiece = (SolidColorBrush)new BrushConverter().ConvertFrom("#000000");
        #endregion

        // Which color is player
        public PlayerColor firstBoardPlayerColor = PlayerColor.Black;
        public PlayerColor secondBoardPlayerColor = PlayerColor.White;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainViewModel()
        {
            Board = new ObservableCollection<Tile>();
            
            CreateNewBoard();
            CreateChessPieces(whitePiece, blackPiece);
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

        void CreateChessPieces(SolidColorBrush whitePiece, SolidColorBrush blackPiece)
        {

            Board[0].ChessPiece =  new Rook(blackPiece, PlayerColor.Black);
            Board[1].ChessPiece =  new Knight(blackPiece, PlayerColor.Black);
            Board[2].ChessPiece =  new Bishop(blackPiece, PlayerColor.Black);
            Board[3].ChessPiece =  new King(blackPiece, PlayerColor.Black);
            Board[4].ChessPiece =  new Queen(blackPiece, PlayerColor.Black);
            Board[5].ChessPiece =  new Bishop(blackPiece, PlayerColor.Black);
            Board[6].ChessPiece =  new Knight(blackPiece, PlayerColor.Black);
            Board[7].ChessPiece =  new Rook(blackPiece, PlayerColor.Black);
            Board[8].ChessPiece =  new Pawn(blackPiece, PlayerColor.Black);
            Board[9].ChessPiece =  new Pawn(blackPiece, PlayerColor.Black);
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
