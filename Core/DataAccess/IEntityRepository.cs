using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess;
                                            //class olan,referansı olan her şey gönderilebilir.
public interface IEntityRepository<T> where T:class,IEntity,new()
{
    T? Get(Expression<Func<T, bool>> filter);

    IList<T> GetList()
    {
        return GetList(null);
    }

    IList<T> GetList(Expression<Func<T, bool>>? filter=null);
    
    void Add(T entity);
    
    void Update(T entity);
    
    void Delete(T entity);
}