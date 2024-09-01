using Microsoft.Data.SqlClient;

namespace kolos1.DrugStore;

public interface IPrescriptionRepository
{
    Task<Prescription> GetPrescriptionData(int id, SqlConnection con); 
    Task<Prescription> AdPrescriptionData(Prescription prescription, SqlConnection con);
}