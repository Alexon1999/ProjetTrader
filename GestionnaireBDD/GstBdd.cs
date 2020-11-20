using MySql.Data.MySqlClient;
using System;
using MetierTrader;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        // Constructeur
        public GstBdd()
        {
            string chaine = "Server=localhost;Database=bourse;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Trader> getAllTraders()
        {
            return null;
        }
        public List<ActionPerso> getAllActionsByTrader(int numTrader)
        {
            return null;
        }

        public List<MetierTrader.Action> getAllActionsNonPossedees(int numTrader)
        {
            return null;
        }

        public void SupprimerActionAcheter(int numAction, int numTrader)
        {
            
        }

        public void UpdateQuantite(int numAction, int numTrader, int quantite)
        {
            
        }

        public double getCoursReel(int numAction)
        {
            return 0;
        }
        public void AcheterAction(int numAction, int numTrader, double prix, int quantite)
        {

        }
        public double getTotalPortefeuille(int numTrader)
        {
            return 0;
        }
    }
}
