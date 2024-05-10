using Kol1._1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kol1._1.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionController(IPrescriptionRepository animalRepository)
    {
        _prescriptionRepository = animalRepository;
    }

    [HttpGet]
    public IActionResult GetPrescriptions(string DoctorLastName = "%")
    {
        var prescriptions = _prescriptionRepository.GetPrescriptions(DoctorLastName);
        return Ok(prescriptions);
    }
}