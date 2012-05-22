using Cassette.Configuration;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Sioen.Layerless.Web
{
    /// <summary>
    /// Configures the Cassette asset modules for the web application.
    /// </summary>
    public class CassetteConfiguration : ICassetteConfiguration
    {
        public void Configure(BundleCollection bundles, CassetteSettings settings)
        {
            bundles.Add<StylesheetBundle>("Content/Styles");
            bundles.Add<ScriptBundle>("Content/Scripts");
        }
    }
}