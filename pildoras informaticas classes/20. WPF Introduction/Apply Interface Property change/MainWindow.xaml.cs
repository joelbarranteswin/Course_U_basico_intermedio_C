using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Apply_Interface_Property_change
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JoinName app;
        public string Surname { get; set; } = "Smith";
        public MainWindow()
        {
            InitializeComponent();

            app = new JoinName
            {
                Name = "joel",
                LastName = "Barrantes",
            };

            //this.DataContext = this;
    
        }
    }

    public class JoinName : INotifyPropertyChanged
    {
        public string name, lastname, fullname;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("FullName");
            }

        }
        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged("FullName");
            }
        }
        public string FullName
        {
            get
            {
                fullname = Name + " " + LastName;
                return fullname;
            }
        }
    }

    public class DataModel
    {
        public string Surname { get; set; } = "Smith";
    }
}
