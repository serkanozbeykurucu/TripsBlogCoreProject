using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AppRoleManager : IAppRoleService
    {
        IAppRoleDal _userRoleDal;

        public AppRoleManager(IAppRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public AppRole GetById(int id)
        {
            return _userRoleDal.GetById(id);
        }

        public List<AppRole> GetByUserRoleFilter(Expression<Func<AppRole,bool>> filter)
        {
            return _userRoleDal.GetListByFilter(filter); 
        }
        public List<AppRole> GetList()
        {
            return _userRoleDal.GetList();
        }

        public void TAdd(AppRole t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(AppRole t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(AppRole t)
        {
            throw new NotImplementedException();
        }
    }
}
