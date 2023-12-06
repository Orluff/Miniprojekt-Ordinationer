namespace ordination_test;

using shared.Model;

[TestClass]
public class PatientTest
{

    [TestMethod]
    public void PatientHasName()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;
        
        Patient patient = new Patient(cpr, navn, vægt);
        Assert.AreEqual(navn, patient.navn);
    }
    
    [TestMethod]
    //TC5
    public void NameAndCprToString()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;

        string expectedValue = navn + " " + cpr;
        
        Patient patient = new Patient(cpr, navn, vægt);
        Assert.AreEqual(expectedValue, patient.ToString());
    }
}