using Kol1._1.Models;

namespace Kol1._1.Repositories;

public interface IPrescriptionRepository
{
    public IEnumerable<PrescriptionINFO> GetPrescriptions(string doctorLastName);

}