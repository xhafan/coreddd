using EmailMaker.DTO.Users;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class UserDtoAutoMap : IAutoMappingOverride<UserDTO>
    {
        public void Override(AutoMapping<UserDTO> mapping)
        {
            mapping.Id(x => x.UserId);
        }
    }
}