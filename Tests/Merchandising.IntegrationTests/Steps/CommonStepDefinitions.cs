using System.Net;
using System.Net.Http.Json;
using Merchandising.IntegrationTests.Constants;
using Merchandising.IntegrationTests.Models;
using FluentAssertions;

namespace Merchandising.IntegrationTests.Steps;

[Binding]
public class CommonStepDefinitions
{
    private readonly SharedContext _scenarioContext;

    public CommonStepDefinitions(SharedContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [Then(@"the HTTP response is '(.*)'")]
    public void ThenTheHttpResponseIs(HttpStatusCode statusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(SharedContextConstants.Response);
        
        response.StatusCode.Should().Be(statusCode);
    }
    
    [Then(@"the error message is '(.*)'")]
    public async Task ThenTheHttpResponseIs(string errorMessage)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(SharedContextConstants.Response);

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        problemDetails.Detail.Should().Be(errorMessage);
    }
}