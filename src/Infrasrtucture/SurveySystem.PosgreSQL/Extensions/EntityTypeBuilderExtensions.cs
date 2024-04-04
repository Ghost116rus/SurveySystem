using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.PosgreSQL.Extensions
{
    /// <summary>
    /// Расширения <see cref="EntityTypeBuilder"/>
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Установить доступ EF к navigation property по полю
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="builder">Строитель</param>
        /// <param name="navigation">Navigation property</param>
        /// <param name="fieldName">Название поля</param>
        /// <returns>Строитель</returns>
        public static EntityTypeBuilder<T> SetPropertyAccessModeField<T>(
            this EntityTypeBuilder<T> builder,
            Expression<Func<T, object?>> navigation,
            string fieldName)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(navigation);

            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException($"'{nameof(fieldName)}' cannot be null or empty.", nameof(fieldName));

            var navigationValue = builder.Metadata.FindNavigation(GetName(navigation));
            navigationValue?.SetField(fieldName);
            navigationValue?.SetPropertyAccessMode(PropertyAccessMode.Field);
            return builder;
        }

        private static string GetName<TSource, TField>(Expression<Func<TSource, TField>> field)
        {
            ArgumentNullException.ThrowIfNull(field);

            MemberExpression expr;
            if (field.Body is MemberExpression expression)
                expr = expression;
            else if (field.Body is UnaryExpression expression1)
                expr = (MemberExpression)expression1.Operand;
            else
                throw new ArgumentException($"Expression '{field}' not supported.", nameof(field));

            return expr.Member.Name;
        }
    }
}
