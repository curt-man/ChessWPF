using BoardGamesWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BoardGamesWPF.MVVM.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {

        #region TurnControl
        private bool playerTurnBool = false;

        public bool PlayerTurnBool
        {
            get
            { return playerTurnBool; }
            set
            {
                if (rotateBoard == false)
                    return;
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

        private int commonTimer;
        public string CommonTimer
        {
            get
            {
                return commonTimer.ToString();
            }
            set
            {
                if(value != "")
                    commonTimer = int.Parse(value)*60;

                OnPropertyChanged(nameof(CommonTimer));
            }
        }

        private int firstTimer;

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


        private int secondTimer;

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
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if(playerTurn==PlayerColor.White)
            {
                firstTimer--;
                FirstTimer = firstTimer.ToString();
            }
            else
            {
                secondTimer--;
                SecondTimer = secondTimer.ToString();
            }
            IsEndOfTheGame(EndOfTheGameReasons.TimeOver);
        }

        #endregion

        #region Colors
        // Colors of tiles
        private SolidColorBrush? whiteTile = (SolidColorBrush?)new BrushConverter().ConvertFrom("#239CB9");

        public SolidColorBrush? WhiteTile
        {
            get { return whiteTile; }
            set
            {
                whiteTile = value;
                OnPropertyChanged(nameof(WhiteTile));
            }
        }

        private SolidColorBrush? blackTile = (SolidColorBrush?)new BrushConverter().ConvertFrom("#137188");

        public SolidColorBrush? BlackTile
        {
            get { return blackTile; }
            set
            {
                blackTile = value;
                OnPropertyChanged(nameof(WhiteTile));
            }
        }

        private SolidColorBrush? selectedTileColor = (SolidColorBrush?)new BrushConverter().ConvertFrom("#008065");
        private SolidColorBrush? possibleToMoveTileColor = (SolidColorBrush?)new BrushConverter().ConvertFrom("#008050");
        private SolidColorBrush? checkTileColor = (SolidColorBrush?)new BrushConverter().ConvertFrom("#AC0F4B");

        // Colors of pieces
        private SolidColorBrush? whitePiece = (SolidColorBrush?)new BrushConverter().ConvertFrom("#FFFFFF");
        private SolidColorBrush? blackPiece = (SolidColorBrush?)new BrushConverter().ConvertFrom("#000000");

        SolidColorBrush[] baseTileColors = new SolidColorBrush[64];

        #endregion

        #region Resign

        private ICommand whiteResignButton;

        public ICommand WhiteResignButton
        {
            get
            {
                if (whiteResignButton == null)
                    whiteResignButton = new RelayCommand(param => Resign(), param => playerTurn == PlayerColor.White && isGameRunning);

                return whiteResignButton;
            }
        }


        private ICommand blackResignButton;

        public ICommand BlackResignButton
        {
            get
            {
                if (blackResignButton == null)
                    blackResignButton = new RelayCommand(param => Resign(), param => playerTurn == PlayerColor.Black && isGameRunning);

                return blackResignButton;
            }
        }


        private void Resign()
        {
            IsEndOfTheGame(EndOfTheGameReasons.Resign);
        }

        #endregion

        #region Start


        private ICommand startButton;

        public ICommand StartButton
        {
            get
            {
                if (startButton == null)
                    startButton = new RelayCommand(param => StartGame(), param => true);

                return startButton;
            }
        }

        void StartGame()
        {
            ClearBoard();

            currentGame = GameChoice;

            switch (currentGame)
            {
                case "Chess":
                    CreateChessPieces(whitePiece, blackPiece);
                    PlayerTurn = PlayerColor.White;
                    FindTheKing();
                    break;

                case "Reversi":
                    CreateReversiPieces(whitePiece, blackPiece);
                    PlayerTurn = PlayerColor.Black;
                    break;

            }

            FirstTimer = CommonTimer;
            SecondTimer = CommonTimer;

            IsGameRunning = true;
            
            WhitePoints = 0;
            BlackPoints = 0;

            dispatcherTimer.Start();
        }


        #endregion

        #region Game

        string currentGame;

        private bool isGameRunning = false;
        public bool IsGameRunning
        {
            get
            {
                return isGameRunning;
            }
            set
            {
                isGameRunning = value;
                OnPropertyChanged(nameof(IsGameRunning));
            }
        }


        private string gameChoice;
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

        #endregion

        #region Points

        private int whitePoints;
        public int WhitePoints
        {
            get
            {
                for (int i = 0; i < 64; i++)
                {
                    if (Board[i].IsOccupied() && Board[i].Piece.isSameColor(PlayerColor.White))
                        whitePoints += Board[i].Piece.Power;
                }
                return whitePoints;
            }
            set
            {
                whitePoints = 0;
                OnPropertyChanged(nameof(WhitePoints));
            }
        }

        private int blackPoints;
        public int BlackPoints
        {
            get
            {
                for (int i = 0; i < 64; i++)
                {
                    if (Board[i].IsOccupied() && Board[i].Piece.isSameColor(PlayerColor.Black))
                        blackPoints += Board[i].Piece.Power;
                }
                return blackPoints;
            }
            set
            {
                blackPoints = 0;
                OnPropertyChanged(nameof(BlackPoints));
            }
        }


        #endregion

        int selectedTile = -1;

        int[] possibleMoves;

        #region CommonMethods

        public int SelectedTile
        {
            get
            {
                return selectedTile;
            }
            set
            {
                if(currentGame=="Reversi")
                {
                    if (!Board[value].IsOccupied())
                    {
                        if(PlayerTurn == PlayerColor.White)
                            Board[value].Piece = new Disk(whitePiece, PlayerTurn);
                        else
                            Board[value].Piece = new Disk(blackPiece, PlayerTurn);

                        possibleMoves = Board[value].Piece.CalculatePossibleMoves(value, Board);

                        if(possibleMoves.Length > 0)
                        {
                            foreach(int move in possibleMoves)
                            {
                                Board[move].Piece.PieceColor = Board[value].Piece.PieceColor;
                                Board[move].Piece.PlayerColor = Board[value].Piece.PlayerColor;
                            }
                            NextTurn();
                            if(!CanMove())
                            {
                                NextTurn();
                                IsEndOfTheGame(EndOfTheGameReasons.GameOver);
                                    
                            }
                        }
                        else
                        {
                            Board[value].Piece = null;
                        }
                    }
                }
                else if(currentGame == "Chess")
                {
                    if (selectedTile != -1)
                    {

                        if (Board[value].TileColor == possibleToMoveTileColor)
                        {
                            // Creating a temporary object to hold a piece inside a tile that we clicked
                            // and if it's not empty, assign it to temp. 
                            // Trading pieces on selected and clicked tiles.
                            Piece? temp = Board[value]?.Piece?.Clone() as Piece;

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
                                if (Board[value].Piece is Pawn promotionPone)
                                {
                                    if(value >= 56 || value <= 7)
                                    {
                                        Board[value].Piece = new Queen(playerTurn==PlayerColor.White ? whitePiece : blackPiece, PlayerTurn);
                                    }
                                }
                                NextTurn();


                                IsEndOfTheGame(EndOfTheGameReasons.GameOver);
                            }
                        }
                        else
                        {
                            FindTheKing();
                            DeselectTile();
                        }
                    }
                    else if (Board[value].IsOccupied())
                    {
                        if (Board[value].Piece.isSameColor(playerTurn))
                        {
                            SelectTile(value);
                            ShowPossibleMoves();
                        }
                    }
                }
                

                WhitePoints = 0;
                BlackPoints = 0;
                OnPropertyChanged(nameof(SelectedTile));
            }
        }

        void IsEndOfTheGame(EndOfTheGameReasons reason)
        {
            switch(reason)
            {
                case EndOfTheGameReasons.TimeOver:
                    if (firstTimer <= 0)
                        MessageBox.Show($"Black won on time!");
                    else if (secondTimer <= 0)
                        MessageBox.Show($"White won on time!");
                    else
                        return;
                    break;

                case EndOfTheGameReasons.GameOver:
                    switch(currentGame)
                    {
                        case "Reversi":
                            if (!CanMove())
                            {
                                WhitePoints = 0;
                                BlackPoints = 0;
                                if (whitePoints > blackPoints)
                                    MessageBox.Show($"White player won with score {whitePoints} to {blackPoints}");
                                else if (whitePoints < blackPoints)
                                    MessageBox.Show($"Black player won with score {blackPoints} to {whitePoints}");
                                else
                                    MessageBox.Show($"It's a draw with score {whitePoints} to {blackPoints}");
                            }
                            else
                            {
                                MessageBox.Show($"There is no possible moves for you. Move transition. Now it's {playerTurn} player turn to move");
                                return;
                            }
                            
                            break;


                        case "Chess":

                            // If after current player's turn another player's king is checked, try find escape
                            // and If there are not one, announce the winner
                            FindTheKing();
                            if (isKingInDanger)
                            {
                                Board[kingIndex].TileColor = checkTileColor;
                                if (IsCheckmateOrDraw())
                                {
                                    NextTurn();
                                    MessageBox.Show($"{playerTurn} won!");
                                }
                                else
                                    return;
                            }
                            else
                            {
                                if (IsCheckmateOrDraw())
                                {
                                    NextTurn();
                                    MessageBox.Show($"It's a draw!");
                                }
                                else
                                    return;
                            }
                            break;
                    }
                    break;


                case EndOfTheGameReasons.Resign:
                    if (PlayerTurn == PlayerColor.White)
                        MessageBox.Show($"Black won by my resignation!");
                    else
                        MessageBox.Show($"White won by my resignation!");
                    break;
            }

            dispatcherTimer.Stop();
            
            IsGameRunning = false;
        }

        void ShowPossibleMoves()
        {
            possibleMoves = Board[selectedTile].Piece.CalculatePossibleMoves(selectedTile, Board);
            foreach(int move in possibleMoves)
            {
                Board[move].TileColor = possibleToMoveTileColor;
            }
        }

        #endregion

        #region ReversiMethods

        bool CanMove()
        {
            List<int> emptyTiles = new List<int>();
            for (int i = 0; i < 64; i++)
            {
                if (!Board[i].IsOccupied())
                    emptyTiles.Add(i);
            }
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                if (PlayerTurn == PlayerColor.White)
                    Board[emptyTiles[i]].Piece = new Disk(whitePiece, PlayerTurn);
                else
                    Board[emptyTiles[i]].Piece = new Disk(blackPiece, PlayerTurn);

                possibleMoves = Board[emptyTiles[i]].Piece.CalculatePossibleMoves(emptyTiles[i], Board);

                if (possibleMoves.Length > 0)
                {
                    Board[emptyTiles[i]].Piece = null;
                    return true;
                }
                Board[emptyTiles[i]].Piece = null;


            }
            return false;
        }

        #endregion

        #region ChessMethods

        int kingIndex;

        bool isKingInDanger;

        bool IsCheckmateOrDraw()
        {
            Piece? temp;
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
                    temp = Board[possibleMoves[j]]?.Piece?.Clone() as Piece;
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

        #endregion

        #region Board

        public ObservableCollection<Tile> Board { get; set; }

        private bool rotateBoard = true;
        public bool RotateBoard
        {
            get
            {
                return rotateBoard;
            }
            set
            {
                rotateBoard = value;
                OnPropertyChanged(nameof(RotateBoard));
            }
        }
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

        void CreateNewBoard()
        {
            Board = new ObservableCollection<Tile>();
            for(int i = 0; i<64; ++i)
                Board.Add(new Tile(baseTileColors[i], null));
        }

        void ClearBoard()
        {
            for (int i = 0; i < 64; ++i)
            {
                Board[i].TileColor = baseTileColors[i];
                Board[i].Piece = null;
            }
        }

        void CreateReversiPieces(SolidColorBrush whitePiece, SolidColorBrush blackPiece)
        {
            Board[27].Piece = new Disk(whitePiece, PlayerColor.White);
            Board[28].Piece = new Disk(blackPiece, PlayerColor.Black);
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

        #endregion

        #region INotifyPropertyChange

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public MainViewModel()
        {

            Populate<SolidColorBrush>(baseTileColors, whiteTile, blackTile);
            CreateNewBoard();

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }
    }
}
