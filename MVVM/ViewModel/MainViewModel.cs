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

        public int kingIndex;
        int[] possibleMoves;

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

                    if (Board[value].TileColor == possibleToMoveTileColor)
                    {
                        // Creating a temporary object to hold a piece inside a tile that we clicked
                        // and if it's not empty, assign it to temp. 
                        // Trading pieces on selected and clicked tiles.
                        ChessPiece temp = (Board[value].IsOccupied()) ? Board[value].ChessPiece.Clone() as ChessPiece : null;

                        Board[value].ChessPiece = Board[selectedTile].ChessPiece;
                        Board[selectedTile].ChessPiece = null;

                        // Just in case if king was moved getting his new position
                        FindTheKing();
                        
                        // If after trading king is still in danger, cancel all changes and deselect clicked tile.
                        if ((Board[kingIndex].ChessPiece as King).IsInDanger(kingIndex, Board))
                        {
                            Board[selectedTile].ChessPiece = Board[value].ChessPiece;
                            Board[value].ChessPiece = temp;
                            // Just in case if king was moved getting his new position
                            FindTheKing();
                            DeselectTile(true);
                        }
                        // Else we keep the changes
                        else
                        {
                            Board[value].ChessPiece.hasMoved = true;
                            // Just in case if king was moved getting his new position
                            FindTheKing();
                            DeselectTile(false);
                            NextTurn();
                            FindTheKing();
                            if ((Board[kingIndex].ChessPiece as King).IsInDanger(kingIndex, Board))
                            {
                                Board[kingIndex].TileColor = checkTileColor;
                            }
                        }

                    }
                    else
                    {
                        DeselectTile((Board[kingIndex].ChessPiece as King).IsInDanger(kingIndex, Board));
                    }
                    
                }
                else if(Board[value].IsOccupied())
                {
                    if (Board[value].ChessPiece.isSameColor(playerTurn))
                    {
                        SelectTile(value);
                        ShowPossibleMoves();
                    }
                        
                    //else
                    //    MessageBox.Show("It's not your turn!");
                }


                OnPropertyChanged("SelectedTile");

            }
        }
        void NextTurn()
        {
            if (PlayerTurn == PlayerColor.White)
                PlayerTurn = PlayerColor.Black;
            else
                PlayerTurn = PlayerColor.White;
        }
        void DeselectTile(bool kingIsInDanger)
        {
            ReturnTileColors(kingIsInDanger);
            selectedTile = -1;
        }
        void SelectTile(int value)
        {
            selectedTile = value;
            Board[selectedTile].TileColor = selectedTileColor;
        }
        void ShowPossibleMoves()
        {
            possibleMoves = Board[selectedTile].ChessPiece.CalculatePossibleMoves(selectedTile, Board);
            foreach(int move in possibleMoves)
            {
                Board[move].TileColor = possibleToMoveTileColor;
            }
        }
        void ReturnTileColors(bool kingIsInDanger)
        {
            Board[selectedTile].TileColor = baseTileColors[selectedTile];
            for (int i = 0; i < possibleMoves.Length; i++)
            {
                Board[possibleMoves[i]].TileColor = baseTileColors[possibleMoves[i]];
            }
            if(kingIsInDanger)
                Board[kingIndex].TileColor = checkTileColor;
            else
                Board[kingIndex].TileColor = baseTileColors[kingIndex];


        }
        void FindTheKing()
        {
            kingIndex = Board.ToList().FindIndex(x => (x.ChessPiece is King && x.ChessPiece.PlayerColor == playerTurn));
        }

        #region "Colors"
        // Colors of tiles
        private SolidColorBrush whiteTile = (SolidColorBrush)new BrushConverter().ConvertFrom("#239CB9");

        public SolidColorBrush WhiteTile
        {
            get { return whiteTile; }
            set
            {
                whiteTile = value;
                OnPropertyChanged("WhiteTile");
            }
        }
        private SolidColorBrush blackTile = (SolidColorBrush)new BrushConverter().ConvertFrom("#137188");

        public SolidColorBrush BlackTile
        {
            get { return blackTile; }
            set
            {
                blackTile = value;
                OnPropertyChanged("WhiteTile");
            }
        }

        private SolidColorBrush selectedTileColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#008065");
        private SolidColorBrush possibleToMoveTileColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#008040");
        private SolidColorBrush checkTileColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#AC0F4B");

        // Colors of pieces
        private SolidColorBrush whitePiece = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
        private SolidColorBrush blackPiece = (SolidColorBrush)new BrushConverter().ConvertFrom("#000000");
        #endregion

        SolidColorBrush[] baseTileColors = new SolidColorBrush[64];
        
        public static void Populate<T>(T[] arr, T value1, T value2)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (((i + j) % 2) == 0)
                        arr[i * 8 + j] = value1;
                    else
                        arr[i * 8 + j] = value2;
                }
            }
        }

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
            
            Populate<SolidColorBrush>(baseTileColors, whiteTile, blackTile);
            CreateNewBoard();
            CreateChessPieces(whitePiece, blackPiece);
            FindTheKing();
        }


        void CreateNewBoard()
        {
            for(int i = 0; i<64; ++i)
                Board.Add(new Tile(baseTileColors[i], null));
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
