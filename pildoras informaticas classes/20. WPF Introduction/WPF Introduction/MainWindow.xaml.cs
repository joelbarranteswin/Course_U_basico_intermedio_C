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

namespace WPF_Introduction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Grid miGrid = new Grid();
            this.Content = miGrid;
            Button myButton = new Button();
            myButton.Content = "Hola Mundo";
            myButton.Width = 100;
            myButton.Height = 80;
            myButton.Background = Brushes.Red;

            WrapPanel myWrap = new WrapPanel();
            TextBlock txt1 = new TextBlock();
            txt1.Text = "Click";
            txt1.Background = Brushes.Blue;
            txt1.Foreground = Brushes.Green;
            myWrap.Children.Add(txt1);

            TextBlock txt2 = new TextBlock();
            txt2.Text = "Click";
            myWrap.Children.Add(txt2);

            myButton.Content = myWrap;
            miGrid.Children.Add(myButton);

        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            TxtBlock.Text = "Hello World! two times";
        }
    }
}
