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
    public class OurStoryManager : IOurStoryService
    {
        IOurStoryDal _ourStory;

        public OurStoryManager(IOurStoryDal ourStory)
        {
            _ourStory = ourStory;
        }

        public OurStory GetById(int id)
        {
            return _ourStory.GetById(id);
        }

        public List<OurStory> GetList()
        {
            return _ourStory.GetList();
        }

        public void TAdd(OurStory t)
        {
             _ourStory.Insert(t);
        }

        public void TDelete(OurStory t)
        {
            _ourStory.Delete(t);
        }

        public void TUpdate(OurStory t)
        {
            _ourStory.Update(t);
        }
    }
}
