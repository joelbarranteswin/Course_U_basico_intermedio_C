using System;
using System.Collections.Generic;
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

namespace Dependency_Properties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public int MyProperty
        {
            get { return (int)GetValue(MyDependencyProperty); }
            set { SetValue(MyDependencyProperty, value); }
        }
        public static readonly DependencyProperty MyDependencyProperty =
            DependencyProperty.Register(
                name: "MyProperty", 
                propertyType: typeof(int), 
                ownerType: typeof(MainWindow), 
                typeMetadata: new PropertyMetadata(0));

    }
}
