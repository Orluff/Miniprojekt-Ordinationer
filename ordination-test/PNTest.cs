namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class PNTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }
    
    [TestMethod]
    //TC1
    public void OpretPN()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
        
        service.OpretPN(patient.PatientId, lm.LaegemiddelId,
            2, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(5, service.GetPNs().Count());
    }
    
    [TestMethod]
    //TC2-4
    public void GivDosis()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDato = new DateTime(2023, 11, 15);
        DateTime slutDato = new DateTime(2023, 12, 5);
        
        PN pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId,
            2, startDato, slutDato);

        var dosis1 = pn.givDosis(new Dato
        {
            dato = new DateTime(2023, 11, 20)
        });
        
        var dosis2 = pn.givDosis(new Dato
        {
            dato = new DateTime(2023, 11, 30)
        });
        
        var dosis3 = pn.givDosis(new Dato
        {
            dato = new DateTime(2023, 12, 6)
        });

        Assert.AreEqual(true, dosis1);
        Assert.AreEqual(true, dosis2);
        Assert.AreEqual(false, dosis3);
    }
}