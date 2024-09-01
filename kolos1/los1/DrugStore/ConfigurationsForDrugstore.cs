namespace kolos1.DrugStore;

public static class ConfigurationsForDrugstore
{
    public static void RegisterEndpointsForDrugStore(this IEndpointRouteBuilder app)
    {
        
        
        app.MapPost("/api/prescriptions/{id}", async (int id, IPrescriptionService service) =>
            // await service.GetPrescription(id) is Prescription prescription ? Results.Ok(prescription) : Results.NotFound());
            Results.Ok(await service.GetPrescription(id)));
        
    }
}