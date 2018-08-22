#if !NETCOREAPP
using System;
using System.ComponentModel;
using System.Web;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetTests
{
    public class FakeHttpApplication : HttpApplication
    {
        public void FireBeginRequest()
        {
            FireHttpApplicationEvent(this, "EventBeginRequest", this, EventArgs.Empty);
        }

        public void FireEndRequest()
        {
            FireHttpApplicationEvent(this, "EventEndRequest", this, EventArgs.Empty);
        }

        public void FireError()
        {
            FireHttpApplicationEvent(this, "EventErrorRecorded", this, EventArgs.Empty);
        }

        // inspired by https://stackoverflow.com/a/24117425/379279
        private static void FireHttpApplicationEvent(HttpApplication httpApplication, string invokeMe, params object[] args)
        {
            var httpApplicationType = typeof(HttpApplication);

            var eventIndex = httpApplicationType
                .GetField(invokeMe, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(httpApplication);
            var events = (EventHandlerList)httpApplicationType
                .GetField("_events", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(httpApplication);

            var handler = (EventHandler)events[eventIndex];

            var delegates = handler.GetInvocationList();

            foreach (var dlg in delegates)
            {
                dlg.Method.Invoke(dlg.Target, args);
            }
        }
    }
}
#endif