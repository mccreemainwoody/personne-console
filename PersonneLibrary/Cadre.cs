using System;
using System.Collections.Generic;
using System.Text;

namespace PersonneLibrary
{
    public class Cadre : Personne
    {
        public enum TypeStatut { Aucun, Technique, Administratif, Juridique, Financier }

        private static double tauxImpositionCadre = 0.25;

        private double prime;
        private TypeStatut statut;

        public double Prime
        {
            get { return prime; }
            set
            {
                if (value < 0 || value > 6000)
                {
                    throw new Exception("La prime doit être compris entre 0 et 6000");
                }
                prime = value;
            }
        }

        public override string Identite
        {
            get
            {
                return $"{base.Identite}, prime={Prime:C}, status: {Statut}";
            }
        }
        public override double TauxImposition { get { return tauxImpositionCadre; } }

        public override double SalaireBrut { get { return base.SalaireBrut + Prime; } }

        public TypeStatut Statut { get { return statut; } }



        #region Constructeur
        public Cadre(int id, string nom, string prenom, DateTime dateDeNaissance, double salaireBrut, 
            Service service, double prime, TypeStatut statut)
            : base(id, nom, prenom, dateDeNaissance, salaireBrut, service)
        {
            Prime = prime;
            this.statut = statut;
        }
        #endregion
    }
}
