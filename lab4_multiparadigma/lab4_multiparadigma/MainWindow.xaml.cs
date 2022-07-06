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
using model.DobbleGameSpace;

namespace lab4_multiparadigma
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

        //private void prueba_Click(object sender, RoutedEventArgs e)
        //{
        //    DobbleGame dG = new("Juego 1", 1, "Stack Player vs CPU", new List<String>(), 3, 5);
        //    MessageBox.Show(dG.ToString());
        //    dG.register("ManttiuS");
        //    MessageBox.Show(dG.ToString());
        //    dG.start();
        //    MessageBox.Show(dG.ToString());
        //    dG.play(dG.getPlaysOptions()[0]);
        //    MessageBox.Show(dG.ToString());
        //    String[] t = {"1"};
        //    dG.play(dG.getPlaysOptions()[0], t);
        //    MessageBox.Show(dG.ToString());

        //}
    }
}
