using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace zavit.Infrastructure.Venues.CustomOrdering
{
    public class CustomOrder : Order
    {
        readonly string _sqlExpression;

        public CustomOrder(string sqlExpression) : base("", true)
        {
            _sqlExpression = sqlExpression;
        }

        public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            return new SqlString(_sqlExpression);
        }
    }
}