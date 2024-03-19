using System.ComponentModel;

namespace SurveySystem.Domain.Extensions
{
    /// <summary>
    /// Расширение для <see cref="Enum"/>
    /// </summary>
    public static class EnumExtension
    {
        public static string GetDecription(this Enum? value) 
        {
            if (value == null)
                return string.Empty;

            var attribute = GetAttribute<DescriptionAttribute>(value);  
            return attribute == null ? value.ToString() : attribute.Description;
        }



        private static T? GetAttribute<T>(Enum value)
            where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
                ? (T)attributes[0]
                : null;
        }
    }
}
