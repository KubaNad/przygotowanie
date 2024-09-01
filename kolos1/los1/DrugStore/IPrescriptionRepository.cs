using Microsoft.Data.SqlClient;

namespace kolos1.DrugStore;

public interface IPrescriptionRepository
{
    Task<Prescription> GetPrescriptionData(int id, SqlConnection con); 
}