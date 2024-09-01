namespace kolos1.DrugStore;

public interface IPrescriptionService
{
    Task<Prescription> GetPrescription(int id);
}