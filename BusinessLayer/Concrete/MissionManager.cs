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
    public class MissionManager : IMissionService
    {
        IMissionDal _missionDal;

        public MissionManager(IMissionDal missionDal)
        {
            _missionDal = missionDal;
        }

        public Mission GetById(int id)
        {
            return _missionDal.GetById(id);
        }

        public List<Mission> GetList()
        {
            return _missionDal.GetList();
        }

        public void TAdd(Mission t)
        {
            _missionDal.Insert(t);
        }

        public void TDelete(Mission t)
        {
            _missionDal.Delete(t);
        }

        public void TUpdate(Mission t)
        {
            _missionDal.Update(t);
        }
    }
}
