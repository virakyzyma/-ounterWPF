using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace СounterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CounterViewModel();
        }
        public class CounterViewModel : INotifyPropertyChanged
        {
            private int count;
            public int Count
            {
                get => count;
                set
                {
                    if (count != value)
                    {
                        count = value;
                        OnPropertyChanged();
                    }
                }
            }
            public ICommand DecrementCommand { get; }
            public ICommand IncrementCommand { get; }

            public CounterViewModel()
            {
                DecrementCommand = new MainCommand(_ =>
                {
                    Count--;
                });
                IncrementCommand = new MainCommand(_ =>
                {
                    Count++;
                });
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
        public class MainCommand : ICommand
        {
            public event EventHandler? CanExecuteChanged;
            private Action<object?> action;

            public MainCommand(Action<object?> action)
            {
                this.action = action;
            }

            public bool CanExecute(object? parameter) => true;
            public void Execute(object? parameter) => action?.Invoke(parameter);
        }
    }
}