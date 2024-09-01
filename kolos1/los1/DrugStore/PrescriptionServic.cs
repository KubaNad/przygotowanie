using Microsoft.Data.SqlClient;

namespace kolos1.DrugStore;


public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _repository;
    private readonly IConfiguration _configuration;

    public PrescriptionService(IPrescriptionRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<Prescription> GetPrescription(int id)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await con.OpenAsync();
        var prescription = await _repository.GetPrescriptionData(id, con);
        return prescription;
    }

    public async Task<Prescription> AdPrescription(Prescription prescription)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await con.OpenAsync();
        var res = await _repository.AdPrescriptionData(prescription, con);
        return res;
    }

    
}
