using System.Runtime.Serialization;

namespace DollarInfo.Utils.Extensions
{
    public static class EnumExtensions
    {
        public static T GetValueMember<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);

            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string ToEnumString<T>(this T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();

            return enumMemberAttribute.Value;
        }

        public static IEnumerable<(string key, int value)> GetValues(this Type enumType)
        {
            return Enum.GetValues(enumType)
              .Cast<int>()
              .Select(e => (Enum.GetName(enumType, e), e));
        }

        public static IEnumerable<T> GetEnumValues<T>(this Enum enumObj)
        {
            return enumObj is null ? throw new ArgumentNullException(nameof(enumObj)) : Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
