using Merchandising.IntegrationTests.Builders;
using Merchandising.IntegrationTests.Extensions;
using Merchandising.IntegrationTests.Models;

namespace Merchandising.IntegrationTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Configure()
        {
            var container = _scenarioContext.ScenarioContainer;

            var configuration = ConfigurationProvider.GetConfiguration();
            
            container.RegisterInstanceAs(configuration);
            
            var sharedContext = new SharedContext();
            
            container.RegisterFactoryAs(x => sharedContext);
            container.RegisterTypeAs<ProductBuilder, IProductBuilder>();

        }
    }
    
}