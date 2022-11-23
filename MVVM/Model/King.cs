﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace ChessWPF.MVVM.Model
{
    internal class King : ChessPiece
    {
        public override int Power => 0;
        public King(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M224 0C241.7 0 256 14.3 256 32V48H272C289.701 48 304 62.3 304 80C304 97.7 289.701 112 272 112H256V160H416C426.3 160 435.9 164.9 442 173.3C448.1 181.7 449.7 192.4 446.4 202.1L375.1 416H72.9005L1.60048 202.1C-1.59952 192.4 0.00048399 181.6 6.00048 173.3C12.0005 165 21.7005 160 32.0005 160H192V112H176C158.3 112 144 97.7 144 80C144 62.3 158.3 48 176 48H192V32C192 14.3 206.3 0 224 0ZM32.0005 480C32.0005 462.3 46.3005 448 64.0005 448H83.6005H364.4H384C401.701 448 416 462.3 416 480C416 497.7 401.701 512 384 512H320H128H64.0005C46.3005 512 32.0005 497.7 32.0005 480Z";

        public override int[,] Move(int column, int row)
        {
            //int[,] possibleMoves = new int[3,3];
            //for(int i = 0; i < 3; i++)
            //{
            //    for(int j = 0; j < 3; j++)
            //    {
            //        possibleMoves[row+i,column+j] = 
            //    }
            //}
            return new int[column, row];
        }
    }
}

