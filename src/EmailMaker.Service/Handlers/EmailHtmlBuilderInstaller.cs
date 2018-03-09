using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EmailMaker.Service.Handlers
{
    public class EmailHtmlBuilderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IEmailHtmlBuilder>()
                    .ImplementedBy<EmailHtmlBuilder>()
                    .LifeStyle.Transient);
        }
    }
}