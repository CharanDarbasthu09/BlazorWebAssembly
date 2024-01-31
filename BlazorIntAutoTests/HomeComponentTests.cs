using BlazorIntAuto.Components.Pages;

using Bunit;
using Bunit.TestDoubles;

namespace BlazorIntAutoTests
{
  public class HomeComponentTests
  {
    private readonly TestContext _testContext;

    private const string testUser = "TEST USER";

    private readonly TestAuthorizationContext _authorizationContext;

    public HomeComponentTests()
    {
      _testContext = new TestContext();

      _authorizationContext = _testContext.AddTestAuthorization();
      _authorizationContext.SetAuthorized(testUser, AuthorizationState.Authorized);
    }

    [Fact]
    public void Home_MarkUp_Test()
    {
      var expectedMarkUp = " <table class=\"table\">\r\n      <tbody>\r\n        <tr>\r\n          <td>http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name - TEST USER</td>\r\n        </tr>\r\n      </tbody>\r\n    </table>";

      var component = _testContext.RenderComponent<Home>();

      component.MarkupMatches(expectedMarkUp);
    }
  }
}
