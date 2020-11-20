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
using GestionnaireBDD;
using MetierTrader;

namespace WPFTrader
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        public MainWindow()
        {
            InitializeComponent();
        }
        GstBdd unGstBdd;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            unGstBdd = new GstBdd();
            lstTraders.ItemsSource = unGstBdd.getAllTraders();
        }

        private void lstTraders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(lstTraders.SelectedItem != null)
            {
                //List<ActionPerso> lesActionsPerso = unGstBdd.getAllActionsByTrader((lstTraders.SelectedItem as Trader).NumTrader);
                //lstActions.ItemsSource = lesActionsPerso;
                //double totalPrixActions = 0;
                //lesActionsPerso.ForEach(uneActionsPerso =>
                //{
                //    totalPrixActions += uneActionsPerso.Total;
                //});
                lstActions.ItemsSource = unGstBdd.getAllActionsByTrader((lstTraders.SelectedItem as Trader).NumTrader);
                txtTotalPortefeuille.Text = unGstBdd.getTotalPortefeuille((lstTraders.SelectedItem as Trader).NumTrader).ToString();
                lstActionsNonPossedees.ItemsSource = unGstBdd.getAllActionsNonPossedees((lstTraders.SelectedItem as Trader).NumTrader);
            }
        }

        private void lstActions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstActions.SelectedItem != null)
            {
               if(unGstBdd.getCoursReel((lstActions.SelectedItem as ActionPerso).MonAction.NumAction) == (lstActions.SelectedItem as ActionPerso).PrixAchat)
                {
                    imgAction.Source = new BitmapImage(new Uri("Images/Moyen.png" , UriKind.RelativeOrAbsolute));
                }
                else if(unGstBdd.getCoursReel((lstActions.SelectedItem as ActionPerso).MonAction.NumAction) > (lstActions.SelectedItem as ActionPerso).PrixAchat)
                {
                    imgAction.Source = new BitmapImage(new Uri("Images/Haut.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    imgAction.Source = new BitmapImage(new Uri("Images/Bas.png", UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void btnVendre_Click(object sender, RoutedEventArgs e)
        {
            if(lstActions.SelectedItem != null)
            {
                if(txtQuantiteVendue.Text != "")
                {
                    if(Convert.ToInt16(txtQuantiteVendue.Text) > (lstActions.SelectedItem as ActionPerso).Quantite)
                    {
                        MessageBox.Show("Impossible de vendre plus que vous avez", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        unGstBdd.UpdateQuantite((lstActions.SelectedItem as ActionPerso).MonAction.NumAction, (lstTraders.SelectedItem as Trader).NumTrader, Convert.ToInt16(txtQuantiteVendue.Text));
                        lstActions.ItemsSource = null;
                        lstActions.ItemsSource = unGstBdd.getAllActionsByTrader((lstTraders.SelectedItem as Trader).NumTrader);
                    }
                }else
                {
                    MessageBox.Show("Veuillez saisir une quantite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez choisir une action", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAcheter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
