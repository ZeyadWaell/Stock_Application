using System.Security.Claims;

namespace Api.Extentions
{
    public static class ClaimesPreinceplacesExtention
    {
        public static string RetriveEmailPrincepal(this ClaimsPrincipal User)
            => User.FindFirstValue(ClaimTypes.Email);
    }
}
