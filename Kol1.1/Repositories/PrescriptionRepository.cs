using System.Data.SqlClient;
using Kol1._1.Models;

namespace Kol1._1.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly IConfiguration _configuration;

    public PrescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<PrescriptionINFO> GetPrescriptions(string doctorLastName)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;

                command.Parameters.AddWithValue("@DoctorLastName", doctorLastName);
                

                command.CommandText = "SELECT Pr.IdPrescription, Pr.Date, Pr.DueDate, D.LastName AS DoctorLastName, P.LastName" +
                                      " FROM Prescription Pr" +
                                      " JOIN Patient P ON P.IdPatient = Pr.IdPatient" +
                                      " JOIN Doctor D ON D.IdDoctor = Pr.IdDoctor" +
                                      " WHERE D.LastName LIKE @DoctorLastName";
                

                var reader = command.ExecuteReader();

                var listOfPrescription = new List<PrescriptionINFO>();
                
                while (reader.Read())
                {
                    listOfPrescription.Add(new PrescriptionINFO()
                    {
                      IdPrescription = (int)reader["IdPrescription"],
                      Date = DateTime.Parse(reader["Date"].ToString()),
                      DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                      DoctorLastName = reader["DoctorLastName"].ToString(),
                      PatientLastName = reader["LastName"].ToString()
                    }
                    );
                }

                return listOfPrescription;
            }
        }
    }
}