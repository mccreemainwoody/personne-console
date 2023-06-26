using System;
using System.Collections.Generic;
using System.Text;

namespace PersonneLibrary
{
    public class Employe : Personne
    {
        private static double tauxImpositionEmploye = 0.2;

        private int nbRTT;
        public int NbRTT
        {
            get { return nbRTT; }
            set
            {
                if (value < 0 || value > 26)
                {
                    throw new Exception("Le nombre de RTT doit être compris entre 0 et 26");
                }
                nbRTT = value;
            }
        }

        public override string Identite
        {
            get
            {
                return $"{base.Identite}, RTT={NbRTT}";
            }
        }
        public override double TauxImposition { get { return tauxImpositionEmploye; } }

        #region Constructeur
        public Employe(int id, string nom, string prenom, DateTime dateDeNaissance, double salaireBrut, Service service, int nbRTT)
            : base(id, nom, prenom, dateDeNaissance, salaireBrut, service)
        {
            NbRTT = nbRTT;
        }
        #endregion
    }
}
