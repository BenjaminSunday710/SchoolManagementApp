using NHibernate.Type;

namespace Shared.Infrastructure.Mappings
{
    public class GenericEnumMapper<TEnum> : EnumStringType
    {
        public GenericEnumMapper() : base(typeof(TEnum)) { }
    }
}
