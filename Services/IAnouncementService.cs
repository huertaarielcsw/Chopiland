using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IAnouncementService
    {
        IEnumerable<Anouncement> GetAnouncements();
        Anouncement GetAnouncement(long id);
        void InsertAnouncement(Anouncement Anouncement);
        void UpdateAnouncement(Anouncement Anouncement);
        void DeleteAnouncement(long id);
    }
}
