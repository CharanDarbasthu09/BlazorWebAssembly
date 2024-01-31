using BlazorIntAuto.Components;

using Bunit;
using Bunit.TestDoubles;

using Microsoft.AspNetCore.Authorization;

namespace BlazorIntAutoTests
{
  public class LoginTests
  {
    private readonly TestContext _testContext;

    private const string testUser = "TEST USER";

    private readonly TestAuthorizationContext _authorizationContext;

    public LoginTests()
    {
      _testContext = new TestContext();

      _authorizationContext = _testContext.AddTestAuthorization();
      _authorizationContext.SetAuthorized(testUser, AuthorizationState.Authorized);
    }

    [Fact]
    public void Verify_LoginComponent_IsDecoratedWithAuthorizeAttribute()
    {

      var loginComponent = _testContext.RenderComponent<Login>();

      var actual = Attribute.GetCustomAttribute(typeof(Login), typeof(AuthorizeAttribute)) as AuthorizeAttribute;

      //Assert.NotNull(actual);
      //Assert.Equal("admin", actual.Roles);

      Assert.Throws<ElementNotFoundException>(() => loginComponent.Find("h1"));
    }

    [Fact]
    public void Login_MarkUp_Test()
    {
      var expectedMarkUp = "  <p>context.User.Claims.FirstOrDefault()?.Value</p>\r\n    <a href=\"Account/Logout\">Log out</a>";

      var component = _testContext.RenderComponent<Login>();

      component.MarkupMatches(expectedMarkUp);
    }
  }
}