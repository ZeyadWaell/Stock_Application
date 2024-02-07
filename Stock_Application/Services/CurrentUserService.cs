using Stock_Application.Services.Interfaces;

namespace Stock_Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId
    {
        get
        {
            if (_httpContextAccessor.HttpContext?.User?.Claims.Count() == 0)
            {
                return null;
            }

            return _httpContextAccessor.HttpContext?.User?.Claims.Single(x => x.Type == "userId").Value;
        }
    }

    public string? UserRole => _httpContextAccessor.HttpContext?.User?.Claims.Single(x => x.Type.Contains("role")).Value;

}