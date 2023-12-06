using Data;
using Microsoft.EntityFrameworkCore;
using Service;
using static shared.Util;

namespace ordination_test;

using shared.Model;

[TestClass]
public class DagligSkævTest
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
    //TC11-12
    public void OpretDosis()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DagligSkæv dagligSkæv = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(3), lm);
        dagligSkæv.opretDosis(CreateTimeOnly(12, 0, 0), 5);

        Assert.ThrowsException<IndexOutOfRangeException>(() =>
        {
            dagligSkæv.opretDosis(CreateTimeOnly(12, 0, 0), -5);
        });
        
        dagligSkæv.opretDosis(CreateTimeOnly(12, 0, 0), 5);
    }
    
    [TestMethod]
    //TC13
    public void SamletDosis()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DagligSkæv dagligSkæv = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(3), lm);
        dagligSkæv.opretDosis(CreateTimeOnly(12, 0, 0), 5);
        dagligSkæv.opretDosis(CreateTimeOnly(14, 0, 0), 2);
        
        Assert.AreEqual(28, dagligSkæv.samletDosis());
    }
    
    [TestMethod]
    //TC14
    public void DoegnDosis()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DagligSkæv dagligSkæv = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(3), lm);
        dagligSkæv.opretDosis(CreateTimeOnly(12, 0, 0), 4);
        dagligSkæv.opretDosis(CreateTimeOnly(14, 0, 0), 5);
        
        Assert.AreEqual(9, dagligSkæv.doegnDosis());
    }
}