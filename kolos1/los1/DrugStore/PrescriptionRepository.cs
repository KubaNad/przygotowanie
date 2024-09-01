using System.Data;
using Microsoft.Data.SqlClient;

namespace kolos1.DrugStore;

public class PrescriptionRepository : IPrescriptionRepository
{
    
    private readonly IConfiguration _configuration;

    public PrescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<Prescription> GetPrescriptionData(int id, SqlConnection con)
    {
        List<Medicament> medicaments = new List<Medicament>();
        string sql1 = "SELECT Prescription_Medicament.IdMedicament, Name, Description, Type, Dose, Details FROM MEDICAMENT " +
                      "INNER JOIN Prescription_Medicament on MEDICAMENT.IdMedicament = Prescription_Medicament.IdMedicament " +
                      "WHERE IdPrescription = @Id;";
        SqlCommand command1 = new SqlCommand(sql1, con);
        command1.Parameters.AddWithValue("@Id", id);
        using (SqlDataReader reader = await command1.ExecuteReaderAsync())
        {
            // await con.OpenAsync();
            while (reader.Read())
            {
                Medicament medicament = new Medicament()
                {
                    IdMedicament = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Type = reader.GetString(3),
                    Dose = reader.GetInt32(4),
                    Details = reader.GetString(5),
                };
                medicaments.Add(medicament);
            }
        }
        
        
        string sql2 = "SELECT IdPrescription, Date, DueDate, IdPatient, IdDoctor FROM Prescription WHERE IdPrescription = @Id;";
        SqlCommand command2 = new SqlCommand(sql2, con);
        command2.Parameters.AddWithValue("@Id", id);
        
        // if (con.State == ConnectionState.Closed)
        // {
        //     await con.OpenAsync();
        // }
        
        
        using (SqlDataReader reader = await command2.ExecuteReaderAsync())
        {
            // await con.OpenAsync();
            if (reader.Read())
            {
                return new Prescription()

                {
                    IdPrescription = reader.GetInt32(0),
                    Date = reader.GetDateTime(1),
                    DueDate = reader.GetDateTime(2),
                    IdPatient = reader.GetInt32(3),
                    IdDoctor = reader.GetInt32(4),
                    Medicaments = medicaments
                };
            }
        }

        throw new ArgumentException();
        // throw new NotImplementedException();
    }

    public async Task<Prescription> AdPrescriptionData(Prescription prescription, SqlConnection con )
    {
        string sql2 = "INSERT INTO Prescription (Date, DueDate, IdPatient, IdDoctor) " +
                      "VALUES (@Date, @DueDate, @IdPatient, @IdDoctor)";
        SqlCommand command2 = new SqlCommand(sql2, con);
        command2.Parameters.AddWithValue("@Date", prescription.Date);
        command2.Parameters.AddWithValue("@DueDate", prescription.DueDate);
        command2.Parameters.AddWithValue("@IdPatient", prescription.IdPatient);
        command2.Parameters.AddWithValue("@IdDoctor", prescription.IdDoctor);
        command2.ExecuteNonQuery();
        
        
        string sql3 = "SELECT IdPrescription, Date, DueDate, IdPatient, IdDoctor FROM Prescription " +
                      "WHERE IdPrescription = (SELECT MAX(IdPrescription) FROM Prescription);";
        SqlCommand command3 = new SqlCommand(sql3, con);
        
        using (SqlDataReader reader = await command3.ExecuteReaderAsync())
        {
            // await con.OpenAsync();
            if (reader.Read())
            {
                return new Prescription()

                {
                    IdPrescription = reader.GetInt32(0),
                    Date = reader.GetDateTime(1),
                    DueDate = reader.GetDateTime(2),
                    IdPatient = reader.GetInt32(3),
                    IdDoctor = reader.GetInt32(4),
                };
            }
        }

        throw new ArgumentException();
    }
}