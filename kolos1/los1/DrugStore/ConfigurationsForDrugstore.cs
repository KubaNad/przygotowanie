namespace kolos1.DrugStore;

public static class ConfigurationsForDrugstore
{
    public static void RegisterEndpointsForDrugStore(this IEndpointRouteBuilder app)
    {
        
        
        app.MapGet("/api/prescriptions/{id}", async (int id, IPrescriptionService service) =>
            // await service.GetPrescription(id) is Prescription prescription ? Results.Ok(prescription) : Results.NotFound());
            Results.Ok(await service.GetPrescription(id)));
        
        app.MapPost("/api/prescriptions", async (Prescription prescription, IPrescriptionService service) => 
        {
            try
            {
                if (prescription.DueDate<prescription.Date)
                {
                    return Results.BadRequest("błąd daty");
                }
                var newPrescription = await service.AdPrescription(prescription);
                return Results.Ok(newPrescription);
                
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        
        
    }
}