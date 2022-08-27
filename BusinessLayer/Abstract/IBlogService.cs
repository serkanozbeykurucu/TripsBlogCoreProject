using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        public List<Blog> GetListByFilter(Expression<Func<Blog, bool>> filter);

        public Blog GetWithUser(int id);
        public List<Blog> GetListWithCategory();
        public List<Blog> GetListWithCategory2Filter(Expression<Func<Blog, bool>> filter);
    }
}
