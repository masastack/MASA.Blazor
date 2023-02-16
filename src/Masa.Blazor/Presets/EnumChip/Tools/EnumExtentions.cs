using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Presets.EnumChip.Tools
{
    public static class EnumExtentions
    {

        /// <summary>
        /// 获取枚举值的显示值[Display(name="")],如果没有就默认值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string DisplayText(this Enum @enum)
        {
            var t_type = @enum.GetType();
            var fieldName = Enum.GetName(t_type, @enum);
            var attributes = t_type.GetField(fieldName).GetCustomAttributes(false);
            var enumDisplayAttribute = attributes.FirstOrDefault(p => p.GetType().Equals(typeof(DisplayAttribute))) as DisplayAttribute;
            return enumDisplayAttribute == null ? fieldName : enumDisplayAttribute.Name;
        }
    }
}
