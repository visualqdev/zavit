using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace zavit.Infrastructure.Orm.Conventions
{
    public class ForeignKeyConventionn : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";

            return property.Name + "Id";
        }
    }
}