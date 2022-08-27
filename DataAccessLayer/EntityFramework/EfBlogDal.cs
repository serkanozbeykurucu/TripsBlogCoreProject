using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).ToList();
            }
        }

        public List<Blog> GetListWithCategory2Filter(Expression<Func<Blog, bool>> filter)
        {
            using (var c = new Context())
            {
                return c.Set<Blog>().Include(x=> x.Category).Where(filter).ToList();
            };
        }

        Blog IBlogDal.GetListWithUser(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.AppUser).Include(x=> x.Category).FirstOrDefault(x=> x.Id == id); 
            }
        }
    }
}
