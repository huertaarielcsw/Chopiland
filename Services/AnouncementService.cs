using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Repo;

namespace Services
{
    public class AnouncementService : IAnouncementService
    {
        private IRepository<Anouncement> anouncementRepository;

        public AnouncementService(IRepository<Anouncement> anouncementRepository)
        {
            this.anouncementRepository = anouncementRepository;
        }

        public IEnumerable<Anouncement> GetAnouncements()
        {
            return anouncementRepository.GetAll();
        }

        public Anouncement GetAnouncement(long id)
        {
            return anouncementRepository.Get(id);
        }  

        public void InsertAnouncement(Anouncement Anouncement)
        {
            anouncementRepository.Insert(Anouncement);
        }

        public void UpdateAnouncement(Anouncement Anouncement)
        {
            anouncementRepository.Update(Anouncement);
        }

        public void DeleteAnouncement(long id)
        {
            Anouncement anouncement = GetAnouncement(id);
            anouncementRepository.Remove(anouncement);
            anouncementRepository.SaveChanges();
        }
    }
}
