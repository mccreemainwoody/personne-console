using System;
using System.Collections.Generic;
using System.Text;

namespace PersonneLibrary
{
    public abstract class Personne
    {
        #region Attributs
        private int id;
        private string nom;
        private string prenom;
        private DateTime dateDeNaissance;
        private double salaireBrut;
        private Service service;
        #endregion

        #region Propriétés "classiques"
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }
        public string Nom
        {
            get { return nom; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Le nom doit être renseigné");
                }
                nom = value;
            }
        }
        public string Prenom
        {
            get { return prenom; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Le prénom doit être renseigné");
                }
                prenom = value;
            }
        }
        public DateTime DateDeNaissance
        {
            get { return dateDeNaissance; }
            private set { dateDeNaissance = DateTime.Now.Year - value.Year >= 18 ? value : throw new ArgumentException("Individu trop jeune"); }
        }
        public virtual double SalaireBrut
        {
            get { return salaireBrut; }
            set
            {
                if (value < 1500)
                {
                    throw new Exception("Le salaire doit être supérieur ou égal à 1500");
                }
                salaireBrut = value;
            }
        }
        public int Age
        {
            get
            {
                // Le calcul n'est pas très précis mais ça fonctionne ...
                return (int)Math.Floor(DateTime.Now.Subtract(DateDeNaissance).TotalDays / 365);
            }
        }
        public Service Service
        {
            get { return service; }
            set
            {
                if (value == null)
                {
                    throw new Exception("Le service ne doit pas être null");
                }
                service = value; }
        }


        public abstract double TauxImposition { get; }
        public double SalaireNet
        {
            get { return SalaireBrut * (1 - TauxImposition); }
        }

        public virtual string Identite
        {
            get
            {
                return $"{id}, {nom}, {prenom}, {Age} ans, {SalaireNet:C} dans le service {service.Libelle}";
            }
        }

        #endregion

        #region Propriétés automatiques
        public string Adresse { get; set; }
        #endregion

        #region Constructeur
        public Personne(int id, string nom, string prenom, DateTime dateDeNaissance, double salaireBrut, Service service)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
            SalaireBrut = salaireBrut;
            Service = service;
        }
        #endregion

    }
}
