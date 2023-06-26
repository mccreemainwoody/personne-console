using System;
using System.Text;

using PersonneLibrary;

namespace PersonneConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Afficher les accents et autres caractères
            Console.OutputEncoding = Encoding.UTF8;

            Service production = new Service(1, "Production");
            Service compta = new Service(1, "Comptabilité");
            Service direction = new Service(1, "Direction");

            Entreprise entreprise = new Entreprise();
            entreprise.AjouterPersonne(new Employe(1, "DUPONT", "Charles", new DateTime(1998, 04, 12), 1500, production, 14));
            entreprise.AjouterPersonne(new Employe(2, "KERBAN", "Henry", new DateTime(1981, 09, 24), 1500, production, 6));
            entreprise.AjouterPersonne(new Employe(3, "CHAMPOT", "Paul", new DateTime(1981, 09, 17), 1700, compta, 0));
            entreprise.AjouterPersonne(new Cadre(4, "JOULIE", "Alexandre", new DateTime(1987, 11, 21), 3500, direction, 1000, Cadre.TypeStatut.Financier));
            entreprise.AjouterPersonne(new Cadre(5, "CARMARIE", "Ambre", new DateTime(1991, 4, 15), 6500, direction, 2500, Cadre.TypeStatut.Administratif));

            entreprise.AfficherPersonnes();
            entreprise.AfficherMasseSalariale();
            entreprise.AfficherMasseSalarialeParService();
            entreprise.AfficherInfoMasseSalariale();
        }
    }
}
