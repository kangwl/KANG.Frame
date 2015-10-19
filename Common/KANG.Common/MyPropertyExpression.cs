#region

using System;
using System.Linq.Expressions;

#endregion

namespace KANG.Common {
    /// <summary>
    ///     用于获取指定属性的属性名
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyPropertyExpression<T> {
        /// <summary>
        ///     根据属性获取对应的属性名称
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public string GetPropertyName(Expression<Func<T, dynamic>> expr) {
            var rtn = "";
            //对象是不是一元运算符
            var body = expr.Body as UnaryExpression;
            if (body != null) {
                rtn = ((MemberExpression) body.Operand).Member.Name;
            }
            //对象是不是访问的字段或属性
            else if (expr.Body is MemberExpression) {
                rtn = ((MemberExpression) expr.Body).Member.Name;
            }
            //对象是不是参数表达式
            else if (expr.Body is ParameterExpression) {
                rtn = ((ParameterExpression) expr.Body).Type.Name;
            }
            return rtn;
        }
    }
}