using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    class SchedulerModel:INotifyPropertyChanged
    {
        private bool _isDone;
        private string _text;

        public event PropertyChangedEventHandler PropertyChanged; // Событие вызываемое при изменении строки в DataGrid

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDone
        {
            get { return _isDone; }

            set
            {
               if (_isDone == value)
                    return;

                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = "")  // Обработчик события PropertyChanged
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // Проверка на NULL, если не NULL то вызываем событие и передаем аргументы
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); (Второй вариант реализации)
        }
    }
}
