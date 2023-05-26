using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specfication
{
    public class BaseSpecfication<T> : ISpecfication<T>
    {
        public BaseSpecfication(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDecending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnable { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderbyexpression)
        => OrderBy = orderbyexpression;

        protected void AddOrderByDecending(Expression<Func<T, object>> OrderByDecendingExpression)
      => OrderByDecending = OrderByDecendingExpression;

        protected void ApplyPaging(int skip,int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }

    }
}
