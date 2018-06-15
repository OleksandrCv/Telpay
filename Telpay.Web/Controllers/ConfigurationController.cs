using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Telpay.Web.ViewModels;

namespace Telpay.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly HostSettings settings;

        public ConfigurationController(IConfiguration configuration)
        {
            var section = configuration.GetSection("HostSettings");
            settings = new HostSettings()
            {
                ApiHost = section.GetValue<string>("ApiHost"),
                AuthHost = section.GetValue<string>("AuthHost")
            };
        }

        [Route("hostsettings")]
        public HostSettings GetHostSettings()
        {
            return settings;
        }
    }
}