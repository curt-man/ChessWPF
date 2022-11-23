using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Player : INotifyPropertyChanged
    {

        private PlayerColor color;
        public PlayerColor Color
        {
            get { return color; }
            set { color = value; }
        }

        private int timer;
        public int Timer
        {
            get { return timer; }
            set
            {
                timer = value;
                OnPropertyChanged("Timer");
            }
        }

        

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
