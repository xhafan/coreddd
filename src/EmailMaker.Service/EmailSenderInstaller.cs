using System.Configuration;
using Castle.Core.Smtp;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EmailMaker.Service
{
    public class EmailSenderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IEmailSender>()
                    .ImplementedBy<DefaultSmtpSender>()
                    .DependsOn(new { hostname = ConfigurationManager.AppSettings["SmtpServer"]  })
                    .LifeStyle.Transient);
        }
    }
}