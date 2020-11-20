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
            string chaine = "Server=localhost;;port=3309;Database=bourse;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Trader> getAllTraders()
        {
            List<Trader> lesTraders = new List<Trader>();
            cmd = new MySqlCommand("SELECT trader.idTrader , trader.nomTrader from trader", cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Trader unTrader = new Trader(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                lesTraders.Add(unTrader);
            }

            dr.Close();

            return lesTraders;
        }
        public List<ActionPerso> getAllActionsByTrader(int numTrader)
        {
            List<ActionPerso> lesActionsPerso = new List<ActionPerso>();

            cmd = new MySqlCommand("SELECT action.idAction , action.nomAction ,acheter.prixAchat , acheter.quantite from acheter INNER JOIN action ON action.idAction = acheter.numAction WHERE acheter.numTrader = " + numTrader + ";", cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                MetierTrader.Action unAction = new MetierTrader.Action(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                double prixAchat = Convert.ToDouble(dr[2].ToString());
                int quantite = Convert.ToInt16(dr[3].ToString());
                double total = prixAchat * quantite;
                ActionPerso unActionPerso = new ActionPerso(unAction, prixAchat, quantite, total);
                lesActionsPerso.Add(unActionPerso);
            }

            dr.Close();

            return lesActionsPerso;
        }

        public List<MetierTrader.Action> getAllActionsNonPossedees(int numTrader)
        {
            List<MetierTrader.Action> lesActionsNonPossedees = new List<MetierTrader.Action>();

            cmd = new MySqlCommand("SELECT action.idAction,action.nomAction , action.coursReel from acheter INNER JOIN action ON action.idAction = acheter.numAction WHERE acheter.numAction NOT IN (SELECT action.idAction from action) AND acheter.numTrader = " + numTrader, cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                MetierTrader.Action unAction = new MetierTrader.Action(Convert.ToInt16(dr[0].ToString()), dr[1].ToString() , Convert.ToDouble(dr[2].ToString()));
                lesActionsNonPossedees.Add(unAction);
            }

            dr.Close();

            return lesActionsNonPossedees;
        }

        public void SupprimerActionAcheter(int numAction, int numTrader)
        {

            cmd = new MySqlCommand("DELETE FROM acheter WHERE numAction = " + numAction  + " AND " + "numTrader= " + numTrader , cnx);
            cmd.ExecuteNonQuery();
        }

        public void UpdateQuantite(int numAction, int numTrader, int quantite)
        {
            cmd = new MySqlCommand("UPDATE acheter SET quantite = " + quantite + "WHERE numAction = " + numAction + " AND " + "numTrader = " + numTrader + ";", cnx);
            cmd.ExecuteNonQuery();
        }

        public double getCoursReel(int numAction)
        {
            double courReelAction = 0;
            cmd = new MySqlCommand("SELECT ACTION.coursReel from action WHERE action.idAction = " + numAction , cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            courReelAction = Convert.ToDouble(dr[0].ToString());

            dr.Close();

            return courReelAction;
        }
        public void AcheterAction(int numAction, int numTrader, double prix, int quantite)
        {

        }
        public double getTotalPortefeuille(int numTrader)
        {
            double totalPorteFeuille = 0;
            cmd = new MySqlCommand("SELECT SUM(prixAchat*quantite) from acheter WHERE numTrader = " + numTrader, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            totalPorteFeuille = Convert.ToDouble(dr[0].ToString());

            dr.Close();

            return totalPorteFeuille;
        }
    }
}
