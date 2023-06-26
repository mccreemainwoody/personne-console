using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonneLibrary
{
    public class Entreprise
    {
        #region Attributs
        private List<Personne> personnes = new List<Personne>();
        private List<Service> services = new List<Service>();
        #endregion

        #region Propriétés
        public List<Personne> Personnes
        {
            get { return new List<Personne>(personnes); }
        }
        public List<Service> Services
        {
            get { return new List<Service>(services); }
        }
        #endregion

        #region CRUD Personnes
        public bool AjouterPersonne(Personne personne)
        {
            if (personne == null)
                throw new Exception();

            Personne p = RechercherPersonne(personne.Id);
            if (p != null)
            {
                return false;
            }

            personnes.Add(personne);
            return true;
        }

        public bool SupprimerPersonne(Personne personne)
        {
            if (personne == null)
                throw new Exception();

            return SupprimerPersonne(personne.Id);
        }
        public virtual bool SupprimerPersonne(int id)
        {
            Personne p = RechercherPersonne(id);
            if (p == null)
                return false;

            personnes.Remove(p);
            return true;
        }
        #endregion

        #region Méthodes de recherche
        public Personne RechercherPersonne(int id)
        {
            return personnes.FirstOrDefault(p => p.Id == id);
        }
        public List<Personne> RechercherPersonnesParNom(string nom)
        {
            return personnes
                .Where(p => p.Nom.Equals(nom, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }
        public List<Personne> RechercherPersonnesCommencePar(string nom)
        {
            return personnes
                .Where(p => p.Nom.StartsWith(nom, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }
        public Personne RechercherPersonne_V1(int id)
        {
            foreach (Personne p in personnes)
            {
                if (p.Id == id)
                    return p;
            }
            return null;
        }
        public List<Personne> RechercherPersonnesParNom_V1(string nom)
        {
            List<Personne> list = new List<Personne>();
            foreach (Personne p in personnes)
            {
                if (p.Nom.Equals(nom, StringComparison.InvariantCultureIgnoreCase))
                    list.Add(p);
            }
            return list;
        }
        public List<Personne> RechercherPersonnesCommencePar_V1(string nom)
        {
            List<Personne> list = new List<Personne>();
            foreach (Personne p in personnes)
            {
                if (p.Nom.StartsWith(nom, StringComparison.InvariantCultureIgnoreCase))
                    list.Add(p);
            }
            return list;
        }
        public Personne RechercherPersonne_V2(int id)
        {
            return personnes.Find(delegate (Personne personne)
            {
                return personne.Id == id;
            });
        }
        public List<Personne> RechercherPersonnesParNom_V2(string nom)
        {
            return personnes.FindAll(delegate (Personne personne)
            {
                return personne.Nom.Equals(nom, StringComparison.InvariantCultureIgnoreCase);
            });
        }
        public List<Personne> RechercherPersonnesCommencePar_V2(string nom)
        {
            return personnes.FindAll(delegate (Personne personne)
            {
                return personne.Nom.StartsWith(nom,
                            StringComparison.InvariantCultureIgnoreCase);
            });
        }
        #endregion

        #region Trie
        public void TrierParId()
        {
            personnes.Sort();
        }

        public void TrierParNom()
        {
            personnes = personnes.OrderBy(p => p.Nom).ToList();
            /*
             * ou 
            personnes.Sort(delegate (Personne x, Personne y)
            {
                if (x.Nom == null && y.Nom == null) return 0;
                else if (x.Nom == null) return -1;
                else if (y.Nom == null) return 1;
                else return x.Nom.CompareTo(y.Nom);
            });
            */
        }
        #endregion

        #region CRUD Services
        public bool AjouterService(Service service)
        {
            if (service == null)
                throw new Exception();

            Service s = RechercherService(service.Id);
            if (s != null)
            {
                return false;
            }

            services.Add(service);
            return true;
        }

        public bool SupprimerService(Service service)
        {
            if (service == null)
                throw new Exception();

            return SupprimerService(service.Id);
        }
        public virtual bool SupprimerService(int id)
        {
            Service s = RechercherService(id);
            if (s == null)
                return false;

            services.Remove(s);
            return true;
        }

        public Service RechercherService(int id)
        {
            return services.FirstOrDefault(s => s.Id == id);
        }
        #endregion

        #region Méthodes de debug
        public void AfficherPersonnes()
        {
            Console.WriteLine("Affichage du personnel");
            foreach (Personne p in personnes)
            {
                Console.WriteLine(p.Identite);
            }
        }
        public void AfficherServices()
        {
            Console.WriteLine("Affichage du servicel");
            foreach (Service s in services)
            {
                Console.WriteLine(s.Description);
            }
        }
        #endregion

        #region Un peu de statistiques
        public void AfficherMasseSalariale()
        {
            if (personnes.Count == 0)
            {
                Console.WriteLine("\tAucune presonne dans la liste !!");
                return;
            }
            double total = 0;
            personnes.ForEach(p => total += p.SalaireBrut);
            Console.WriteLine($"Masse salariale brute totale = {total}");
        }
        public void AfficherMasseSalarialeParService()
        {
            Console.WriteLine("Masse salariale brute par service");
            if (personnes.Count == 0)
            {
                Console.WriteLine("\tAucune presonne dans la liste !!");
                return;
            }

            Dictionary<Service, double> salaireParService = new Dictionary<Service, double>();
            foreach (Personne p in personnes)
            {
                if (salaireParService.ContainsKey(p.Service))
                {
                    salaireParService[p.Service] += p.SalaireBrut;
                }
                else
                {
                    salaireParService.Add(p.Service, p.SalaireBrut);
                }
            }
            foreach(KeyValuePair<Service, double> kvp in salaireParService)
            {
                Console.WriteLine($"\tService {kvp.Key.Libelle}, masse salariale={kvp.Value}");
            }
        }
        public void AfficherInfoMasseSalariale()
        {
            Console.WriteLine("Masse salariale brute Infos");
            if (personnes.Count == 0)
            {
                Console.WriteLine("\tAucune presonne dans la liste !!");
                return;
            }
            double total = 0, minimum = 1000000, maximun = 0;
            personnes.ForEach(p =>
            {
                total += p.SalaireBrut;
                if (p.SalaireBrut < minimum)
                    minimum = p.SalaireBrut;
                else if (p.SalaireBrut > maximun)
                    maximun = p.SalaireBrut;
            });
            Console.WriteLine($"\tSalaire moyen = {total / personnes.Count}");
            Console.WriteLine($"\tSalaire minimun = {minimum}");
            Console.WriteLine($"\tSalaire maximum = {maximun}");
        }
        #endregion

    }
}
