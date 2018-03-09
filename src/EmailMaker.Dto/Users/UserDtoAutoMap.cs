using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Dtos.Users
{
    public class UserDtoAutoMap : IAutoMappingOverride<UserDto>
    {
        public void Override(AutoMapping<UserDto> mapping)
        {
            mapping.Id(x => x.UserId);
        }
    }
}