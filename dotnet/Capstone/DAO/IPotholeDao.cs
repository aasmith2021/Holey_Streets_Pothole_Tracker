using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPotholeDao
    {
        public bool AddPothole(Pothole pothole);

        public List<Pothole> ListPotholes();

        public bool DeletePothole(int potholeId);

        public bool UpdatePothole(Pothole pothole);
    }
}
