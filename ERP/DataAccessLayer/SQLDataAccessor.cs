using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ERP.Models;
using ERP.Utils;

namespace ERP
{
    public class SQLDataAccessor : IDataAccessor
    {
        private ERPEntities context = new ERPEntities(); 

        public IList<T> Get<T>(IList<Condition> conditions) where T : ERPData
        {
            IQueryable<T> data = context.Set<T>();

            if (conditions != null)
            {
                foreach (Condition condition in conditions)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(condition.field);

                    ParameterExpression e = Expression.Parameter(typeof(T), "e");
                    MemberExpression m = Expression.MakeMemberAccess(e, propertyInfo);
                    ConstantExpression c = null;
                    switch (condition.valueType)
                    {
                        case ValType.DateTime:
                            c = Expression.Constant(DateTime.Parse(condition.value), typeof(DateTime));
                            break;
                        case ValType.Int:
                            c = Expression.Constant(int.Parse(condition.value), typeof(int));
                            break;
                        case ValType.String:
                            c = Expression.Constant(condition.value, typeof(string));
                            break;
                    }
                    
                    
                    BinaryExpression b = null;
                    switch (condition.op)
                    {
                        case Operator.EqualTo:
                            b = Expression.Equal(m, c);
                            break;
                        case Operator.GreaterThan:
                            b = Expression.GreaterThan(m, c);
                            break;
                    }
                    
                    Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(b, e);
                    data = data.Where(lambda);
                }
            }
            return data.ToList();
        }


        
        public int Post<T>(IList<T> data) where T : ERPData
        {
            var dataset = context.Set<T>();
            dataset.AddRange(data);

            // return number of updated objects to db
            return context.SaveChanges();
        }

        public int Delete<T>(IList<T> data) where T : ERPData
        {
            var dataset = context.Set<T>();
            dataset.RemoveRange(data);

            // return number of deleted objects to db
            return context.SaveChanges();
        }
    }
}
