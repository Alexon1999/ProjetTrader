using System;
using System.Collections.Generic;
using System.Text;

namespace MetierTrader
{
    public class ActionPerso
    {
        private Action monAction;
        private double prixAchat;
        private int quantite;
        private double total;

        public ActionPerso(Action unAction, double unPrixAchat , int uneQuantite , double unTotal)
        {
            MonAction = unAction;
            PrixAchat = unPrixAchat;
            Quantite = uneQuantite;
            Total = unTotal;
        }

        public double PrixAchat { get => prixAchat; set => prixAchat = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public Action MonAction { get => monAction; set => monAction = value; }
        public double Total { get => total; set => total = value; }
    }
}
