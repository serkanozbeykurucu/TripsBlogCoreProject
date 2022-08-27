using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class JourneyManager : IJourneyService
    {
        IJourneyDal _journeyDal;

        public JourneyManager(IJourneyDal journeyDal)
        {
            _journeyDal = journeyDal;
        }

        public Journey GetById(int id)
        {
            return _journeyDal.GetById(id);
        }

        public List<Journey> GetList()
        {
           return _journeyDal.GetList();
        }

        public void TAdd(Journey t)
        {
             _journeyDal.Insert(t);
        }

        public void TDelete(Journey t)
        {
            _journeyDal.Delete(t);
        }

        public void TUpdate(Journey t)
        {
            _journeyDal.Update(t);
        }
    }
}
