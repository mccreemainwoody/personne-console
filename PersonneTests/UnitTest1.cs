using PersonneLibrary;

namespace PersonneTests;

public class Tests
{
    static Entreprise entreprise = new Entreprise();
    static Service s = new Service(1, "Service quelconque");
    static Employe e = new Employe(1, "Jean", "Dupont", DateTime.Now.AddYears(-50), 2000, s, 10);
    static Cadre c = new Cadre(2, "Pean ", "Bupont", DateTime.Now.AddYears(-32), 2000, s, 1000, Cadre.TypeStatut.Administratif);
    
    [SetUp]
    public void Setup()
    {
        SetupEntreprise();
    }

    [Test]
    public void SetupEntreprise()
    {
        entreprise.AjouterService(s);
        entreprise.AjouterPersonne(e);
        entreprise.AjouterPersonne(c);
    }

    [Test]
    public void Age()
    {
        Assert.That(e.Age, Is.EqualTo(50));
    }

    [Test]
    public void AgeErreur()
    {
        Assert.Throws<ArgumentException>(delegate
        {
            new Employe(1, "Jean", "Dupont", DateTime.Now.AddYears(-12), 2000, s, 10);
        });
    }
    
    [Test]
    public void SalaireEmploye()
    {
        Assert.AreEqual(1600, e.SalaireNet);
    }
    
    [Test]
    public void SalaireCadre()
    {
        Assert.That(c.SalaireNet, Is.EqualTo(2250));
    }
    
    [Test]
    public void EntrepriseCRUD1()
    {
        Service s2 = new Service(2, "Service 2");
        entreprise.AjouterService(s2);
        Assert.That(entreprise.Services.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void EntrepriseCRUD2()
    {
        entreprise.SupprimerService(s);
        Assert.That(entreprise.Services.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void EntrepriseCRUD3()
    {
        Assert.That(entreprise.RechercherPersonne(e.Id), Is.EqualTo(e));
    }
}