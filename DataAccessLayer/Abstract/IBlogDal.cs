using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
       public Blog GetListWithUser(int id);
        public List<Blog> GetListWithCategory();
        List<Blog> GetListWithCategory2Filter(Expression<Func<Blog, bool>> filter);
    }
}
