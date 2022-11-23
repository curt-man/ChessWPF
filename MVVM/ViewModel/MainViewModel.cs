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
        public int SelectedTile
        {
            get
            {
                return selectedTile;
            }
            set
            {
                // It happens if you pick two empty tile in a row
                if (selectedTile == -1 && Board[value].ChessPiece == null)
                {
                    
                }
                // It happens if something was 
                else if(selectedTile != -1)
                {
                    if (Board[selectedTile].ChessPiece != null)
                    {
                        if (Board[value].ChessPiece != null)
                        {
                            if (Board[value].ChessPiece.PlayerColor == playerTurn)
                            {
                                selectedTile = -1;
                                MessageBox.Show("It's your piece!");
                            }

                            else
                            {
                                Board[value].ChessPiece = Board[selectedTile].ChessPiece;
                                Board[selectedTile].ChessPiece = null;
                                selectedTile = -1;
                                nextTurn();
                            }
                        }
                        else
                        {
                            Board[value].ChessPiece = Board[selectedTile].ChessPiece;
                            Board[selectedTile].ChessPiece = null;
                            selectedTile = -1;
                            nextTurn();
                        }

                    }
                    else if ((Board[value].ChessPiece.PlayerColor) != playerTurn)
                    {
                        selectedTile = -1;
                        MessageBox.Show("It's not your turn!");
                    }
                }
                else
                {

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

        public SolidColorBrush white;
        public SolidColorBrush black;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            Board = new ObservableCollection<Tile>();
            white = new SolidColorBrush(Colors.White);
            black = new SolidColorBrush(Colors.Black);
            //Board.CollectionChanged += Board_CollectionChanged;
            CreateNewBoard();
            CreateChessPieces(PlayerColor.Black, white, black);
        }
        //private void Board_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add: // если добавление
        //            if (e.NewItems?[0] is Tile newTile)
        //                Console.WriteLine($"Добавлен новый объект: {newTile.TileColor}");
        //            break;
        //        case NotifyCollectionChangedAction.Remove: // если удаление
        //            if (e.OldItems?[0] is Tile oldTile)
        //                Console.WriteLine($"Удален объект: {oldTile.TileColor}");
        //            break;
        //        case NotifyCollectionChangedAction.Replace: // если замена
        //            if ((e.NewItems?[0] is Tile replacingTile) &&
        //                (e.OldItems?[0] is Tile replacedTile))
        //                Console.WriteLine($"Объект {replacedTile.TileColor} заменен объектом {replacingTile.TileColor}");
        //            break;
        //    }
        //}
        void CreateNewBoard()
        {
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (((i + j) % 2) == 0)
                        Board.Add(new Tile(((SolidColorBrush)new BrushConverter().ConvertFrom("#696969")), null));
                    else
                        Board.Add(new Tile(((SolidColorBrush)new BrushConverter().ConvertFrom("#494949")), null));
                }
            }
        }

        void CreateChessPieces(PlayerColor playerColor, SolidColorBrush white, SolidColorBrush black)
        {

            if (playerColor == PlayerColor.White)
            {
                Board[0].ChessPiece = new Rook(white, PlayerColor.White);
                Board[1].ChessPiece = new Knight(white, PlayerColor.White);
                Board[2].ChessPiece = new Bishop(white, PlayerColor.White);
                Board[3].ChessPiece = new Queen(white, PlayerColor.White);
                Board[4].ChessPiece = new King(white, PlayerColor.White);
                Board[5].ChessPiece = new Bishop(white, PlayerColor.White);
                Board[6].ChessPiece = new Knight(white, PlayerColor.White);
                Board[7].ChessPiece = new Rook(white, PlayerColor.White);
                Board[8].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[9].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[10].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[11].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[12].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[13].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[14].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[15].ChessPiece = new Pawn(white, PlayerColor.White);

                Board[48].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[49].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[50].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[51].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[52].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[53].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[54].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[55].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[56].ChessPiece = new Rook(black, PlayerColor.Black);
                Board[57].ChessPiece = new Knight(black, PlayerColor.Black);
                Board[58].ChessPiece = new Bishop(black, PlayerColor.Black);
                Board[59].ChessPiece = new Queen(black, PlayerColor.Black);
                Board[60].ChessPiece = new King(black, PlayerColor.Black);
                Board[61].ChessPiece = new Bishop(black, PlayerColor.Black);
                Board[62].ChessPiece = new Knight(black, PlayerColor.Black);
                Board[63].ChessPiece = new Rook(black, PlayerColor.Black);
            }
            else
            {
                Board[0].ChessPiece = new Rook(black, PlayerColor.Black);
                Board[1].ChessPiece = new Knight(black, PlayerColor.Black);
                Board[2].ChessPiece = new Bishop(black, PlayerColor.Black);
                Board[3].ChessPiece = new King(black, PlayerColor.Black);
                Board[4].ChessPiece = new Queen(black, PlayerColor.Black);
                Board[5].ChessPiece = new Bishop(black, PlayerColor.Black);
                Board[6].ChessPiece = new Knight(black, PlayerColor.Black);
                Board[7].ChessPiece = new Rook(black, PlayerColor.Black);
                Board[8].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[9].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[10].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[11].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[12].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[13].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[14].ChessPiece = new Pawn(black, PlayerColor.Black);
                Board[15].ChessPiece = new Pawn(black, PlayerColor.Black);

                Board[48].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[49].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[50].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[51].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[52].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[53].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[54].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[55].ChessPiece = new Pawn(white, PlayerColor.White);
                Board[56].ChessPiece = new Rook(white, PlayerColor.White);
                Board[57].ChessPiece = new Knight(white, PlayerColor.White);
                Board[58].ChessPiece = new Bishop(white, PlayerColor.White);
                Board[59].ChessPiece = new King(white, PlayerColor.White);
                Board[60].ChessPiece = new Queen(white, PlayerColor.White);
                Board[61].ChessPiece = new Bishop(white, PlayerColor.White);
                Board[62].ChessPiece = new Knight(white, PlayerColor.White);
                Board[63].ChessPiece = new Rook(white, PlayerColor.White);
            }


        }


    }
}
