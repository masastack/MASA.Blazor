namespace Masa.Blazor
{
    public class MasaBlazorComponentTypeMapper : IAbstractComponentTypeMapper
    {
        private readonly Dictionary<Type, Type> _mapper = new()
        {
            { typeof(BMenu), typeof(MMenu) },
            { typeof(BTooltip), typeof(MTooltip) },
            { typeof(BResponsive), typeof(MImage) },
        };

        private readonly Dictionary<Type, Type> _genericMapper = new()
        {
            { typeof(BMobilePickerColumn<>), typeof(MMobilePickerColumn<>) }
        };

        public Type Map(Type keyType)
        {
            if (_mapper.ContainsKey(keyType))
            {
                return _mapper[keyType];
            }

            if (keyType.IsGenericType && _genericMapper.ContainsKey(keyType.GetGenericTypeDefinition()))
            {
                var genericType = _genericMapper[keyType.GetGenericTypeDefinition()];
                return genericType.MakeGenericType(keyType.GetGenericArguments());
            }

            return keyType;
        }
    }
}
