using BoardGamesWPF.MVVM.Model;
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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BoardGamesWPF.MVVM.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tile> Board { get; set; }

        #region TurnControl
        private bool playerTurnBool = false;

        public bool PlayerTurnBool
        {
            get
            { return playerTurnBool; }
            set
            {
                playerTurnBool = value;
                playerTurn = (PlayerColor)Convert.ToInt32(playerTurnBool);
                OnPropertyChanged(nameof(PlayerTurnBool));
            }
        }


        private PlayerColor playerTurn = PlayerColor.White;
        public PlayerColor PlayerTurn
        {
            get { return playerTurn; }
            set
            {
                playerTurn = value;
                PlayerTurnBool = Convert.ToBoolean(playerTurn);
                OnPropertyChanged(nameof(PlayerTurn));
            }
        }
        #endregion

        #region Timers
        DispatcherTimer dispatcherTimer = new DispatcherTimer();


        private int firstTimer = 2000;

        public string FirstTimer
        {
            get
            {
                return $"{(firstTimer/60):d2}:{(firstTimer % 60):d2}";
            }
            set
            {
                firstTimer = int.Parse(value);
                OnPropertyChanged(nameof(FirstTimer));
            }
        }


        private int secondTimer = 1200;

        public string SecondTimer
        {
            get
            {
                return $"{(secondTimer / 60):d2}:{(secondTimer % 60):d2}";
            }
            set
            {
                secondTimer = int.Parse(value);
                OnPropertyChanged(nameof(SecondTimer));
            }
        }
        #endregion

        private string gameChoice = "Chess";
        public string GameChoice
        {
            get
            {
                return gameChoice;
            }
            set
            {
                gameChoice = value;
                OnPropertyChanged(nameof(GameChoice));
            }
        }

        public int kingIndex;
        public bool isKingInDanger;

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
                        Piece temp = (Board[value].IsOccupied()) ? Board[value].Piece.Clone() as Piece : null;

                        Board[value].Piece = Board[selectedTile].Piece;
                        Board[selectedTile].Piece = null;

                        // If after trading king is still in danger, cancel all changes and deselect clicked tile.
                        FindTheKing();
                        if (isKingInDanger)
                        {
                            Board[selectedTile].Piece = Board[value].Piece;
                            Board[value].Piece = temp;
                            FindTheKing();
                            DeselectTile();
                        }
                        // Else we keep the changes
                        else
                        {
                            Board[value].Piece.hasMoved = true;

                            FindTheKing();
                            DeselectTile();
                            NextTurn();

                            // If after current player's turn another player's king is checked, try find escape
                            // and If there are not one, announce the winner
                            FindTheKing();
                            if (isKingInDanger)
                            {
                                Board[kingIndex].TileColor = checkTileColor;
                                if(IsCheckmateOrDraw())
                                {
                                    NextTurn();
                                    MessageBox.Show($"{playerTurn} win!");
                                }
                            }
                            else
                            {
                                if(IsCheckmateOrDraw())
                                {
                                    MessageBox.Show($"It's a draw!");
                                }
                            }
                        }
                    }
                    else
                    {
                        FindTheKing();
                        DeselectTile();
                    }
                }
                else if(Board[value].IsOccupied())
                {
                    if (Board[value].Piece.isSameColor(playerTurn))
                    {
                        SelectTile(value);
                        ShowPossibleMoves();
                    }
                }


                OnPropertyChanged(nameof(SelectedTile));
            }
        }

        bool IsCheckmateOrDraw()
        {
            Piece temp;
            List<int> alliesIndexes = new List<int>();
            for (int i = 0; i < 64; i++)
            {
                if (Board[i].IsOccupied() && Board[i].Piece.isSameColor(playerTurn))
                    alliesIndexes.Add(i);
            }
            for (int i = 0; i < alliesIndexes.Count; i++)
            {
                possibleMoves = Board[alliesIndexes[i]].Piece.CalculatePossibleMoves(alliesIndexes[i], Board);
                for (int j = 0; j < possibleMoves.Length; j++)
                {
                    temp = (Board[possibleMoves[j]].IsOccupied()) ? Board[possibleMoves[j]].Piece.Clone() as Piece : null;
                    Board[possibleMoves[j]].Piece = Board[alliesIndexes[i]].Piece;
                    Board[alliesIndexes[i]].Piece = null;

                    FindTheKing();
                    if (isKingInDanger)
                    {
                        Board[alliesIndexes[i]].Piece = Board[possibleMoves[j]].Piece;
                        Board[possibleMoves[j]].Piece = temp;
                    }
                    else
                    {
                        Board[alliesIndexes[i]].Piece = Board[possibleMoves[j]].Piece;
                        Board[possibleMoves[j]].Piece = temp;
                        return false;
                    }

                }

            }
            return true;
        }


        void NextTurn()
        {
            if (PlayerTurn == PlayerColor.White)
                PlayerTurn = PlayerColor.Black;
            else
                PlayerTurn = PlayerColor.White;
        }
        void DeselectTile()
        {
            ReturnTileColors();
            selectedTile = -1;
        }
        void SelectTile(int value)
        {
            selectedTile = value;
            Board[selectedTile].TileColor = selectedTileColor;
        }
        void ShowPossibleMoves()
        {
            possibleMoves = Board[selectedTile].Piece.CalculatePossibleMoves(selectedTile, Board);
            foreach(int move in possibleMoves)
            {
                Board[move].TileColor = possibleToMoveTileColor;
            }
        }
        void ReturnTileColors()
        {
            Board[selectedTile].TileColor = baseTileColors[selectedTile];
            for (int i = 0; i < possibleMoves.Length; i++)
            {
                Board[possibleMoves[i]].TileColor = baseTileColors[possibleMoves[i]];
            }
            if(isKingInDanger)
                Board[kingIndex].TileColor = checkTileColor;
            else
                Board[kingIndex].TileColor = baseTileColors[kingIndex];


        }
        void FindTheKing()
        {
            kingIndex = Board.ToList().FindIndex(x => (x.Piece is King && x.Piece.PlayerColor == playerTurn));
            isKingInDanger = (Board[kingIndex].Piece as King).IsInDanger(kingIndex, Board);
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

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if(playerTurn==PlayerColor.White)
            {
                firstTimer--;
                FirstTimer = firstTimer.ToString();
                if(firstTimer == 0)
                {
                    dispatcherTimer.Stop();
                    MessageBox.Show($"Black won!");
                }
            }
            else
            {
                secondTimer--;
                SecondTimer = secondTimer.ToString();
                if (secondTimer == 0)
                {
                    dispatcherTimer.Stop();
                    MessageBox.Show($"White won!");
                }
            }
                
        }

        void CreateNewBoard()
        {
            for(int i = 0; i<64; ++i)
                Board.Add(new Tile(baseTileColors[i], null));
        }

        void CreateReversiPieces(SolidColorBrush whitePiece, SolidColorBrush blackPiece)
        {
            Board[27].Piece = new Disk(whitePiece, PlayerColor.Black);
            Board[28].Piece = new Disk(blackPiece, PlayerColor.White);
            Board[35].Piece = new Disk(blackPiece, PlayerColor.Black);
            Board[36].Piece = new Disk(whitePiece, PlayerColor.White);
            
        }
        void CreateChessPieces(SolidColorBrush whitePiece, SolidColorBrush blackPiece)
        {
            Board[0].Piece = new Rook(blackPiece, PlayerColor.Black);
            Board[1].Piece = new Knight(blackPiece, PlayerColor.Black);
            Board[2].Piece = new Bishop(blackPiece, PlayerColor.Black);
            Board[3].Piece = new King(blackPiece, PlayerColor.Black);
            Board[4].Piece = new Queen(blackPiece, PlayerColor.Black);
            Board[5].Piece = new Bishop(blackPiece, PlayerColor.Black);
            Board[6].Piece = new Knight(blackPiece, PlayerColor.Black);
            Board[7].Piece = new Rook(blackPiece, PlayerColor.Black);
            Board[8].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[9].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[10].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[11].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[12].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[13].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[14].Piece = new Pawn(blackPiece, PlayerColor.Black);
            Board[15].Piece = new Pawn(blackPiece, PlayerColor.Black);

            Board[48].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[49].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[50].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[51].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[52].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[53].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[54].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[55].Piece = new Pawn(whitePiece, PlayerColor.White);
            Board[56].Piece = new Rook(whitePiece, PlayerColor.White);
            Board[57].Piece = new Knight(whitePiece, PlayerColor.White);
            Board[58].Piece = new Bishop(whitePiece, PlayerColor.White);
            Board[59].Piece = new King(whitePiece, PlayerColor.White);
            Board[60].Piece = new Queen(whitePiece, PlayerColor.White);
            Board[61].Piece = new Bishop(whitePiece, PlayerColor.White);
            Board[62].Piece = new Knight(whitePiece, PlayerColor.White);
            Board[63].Piece = new Rook(whitePiece, PlayerColor.White);
        }


    }
}
