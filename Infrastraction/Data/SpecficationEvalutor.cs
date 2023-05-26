using Core.Entites;
using Core.Specfication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class SpecficationEvalutor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery, ISpecfication<TEntity> specfication)
        {

            var query = inputquery;

            if(specfication.Criteria != null) 
                query=query.Where(specfication.Criteria);

            if(specfication.OrderBy != null)
            {
                query = query.OrderBy(specfication.OrderBy);
            }
            if (specfication.OrderByDecending != null)
            {
                query = query.OrderByDescending(specfication.OrderByDecending);
            }
            query = specfication.Includes.Aggregate(query, (current, include) => current.Include(include));

            if(specfication.IsPagingEnable)
                query = query.Skip(specfication.Skip).Take(specfication.Take);  
            

            
            
            return query;
        }
    }
}
