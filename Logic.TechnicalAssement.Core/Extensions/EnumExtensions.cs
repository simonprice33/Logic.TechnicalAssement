using System.ComponentModel;

namespace Logic.TechnicalAssement.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();

            var fieldInfo = type.GetField(value.ToString());

            if (fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[]
                {
                    Length: > 0
                } attributes)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    }
}
