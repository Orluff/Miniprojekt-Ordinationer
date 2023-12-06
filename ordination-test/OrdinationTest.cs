using Data;
using Microsoft.EntityFrameworkCore;
using Service;

namespace ordination_test;

using shared.Model;

[TestClass]
public class OrdinationTest
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
    //TC6-7
    public void AnvendOrdination()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDato = new DateTime(2023, 12, 7);
        DateTime slutDato = new DateTime(2023, 12, 10);
        
        PN ordination = service.OpretPN(patient.PatientId, lm.LaegemiddelId,
            2, startDato, DateTime.Now.AddDays(3));

        int ordinationID = ordination.OrdinationId;

        Assert.AreEqual("Lykkes", service.AnvendOrdination(ordinationID, new Dato
        {
            dato = new DateTime(2023, 12, 8)
        }));
        
        Assert.AreEqual("Fejl", service.AnvendOrdination(ordinationID, new Dato
        {
            dato = new DateTime(2023, 12, 11)
        }));
    }
}