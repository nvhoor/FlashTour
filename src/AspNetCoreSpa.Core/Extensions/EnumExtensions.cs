using System;
using System.Collections.Generic;

namespace AspNetCoreSpa.Core
{
    public static class EnumExtensions
    {
        public static Dictionary<int, string> ToDict(this Enum theEnum)
        {
            var enumDict = new Dictionary<int, string>();
            foreach (int enumValue in Enum.GetValues(theEnum.GetType()))
            {
                enumDict.Add(enumValue, enumValue.ToString());
            }

            return enumDict;
        }
        public static int ToRoleInt(this RoleEnum role)
        {
            return ((int) role);
        }
        public static int ToTouristTypeInt(this TouristTypeEnum touristType)
        {
            return ((int) touristType);
        }
    }
}