using AutoMapper;
using AutoMapper.QueryableExtensions;
using example.Data;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace example.Controllers;

[ApiController]
[Route("LoginHistory")]
public class LoginHistoryController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private Guid loggedInUserId;

    public LoginHistoryController(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        SeedData(dbContext);
    }

    /// URL: {{baseUrl}}/LoginHistory/v1/GetLogins
    /// <summary>
    /// Example of getting a LoginHistoryResponse by querying the LoginHistory, and getting
    /// the country name through the Country relationship.
    /// </summary>
    [HttpGet("v1/GetLogins")]
    public async Task<ActionResult<IList<LoginHistoryResponse>>> Get()
    {
        IList<LoginHistoryResponse> user = await _dbContext.Logins
            .AsNoTracking()
            .Where(q => q.UserId == loggedInUserId)
            .OrderByDescending(q => q.Time)
            .Select(q => new LoginHistoryResponse
            {
                CountryName = q.Country.Name,
                Ip = q.Ip,
                Time = q.Time
            })
            .ToListAsync();

        return Ok(user);
    }
    
    /// URL: {{baseUrl}}/LoginHistory/v2/GetLogins
    /// <summary>
    /// Example using .ProjectTo<T> with AutoMapper. Mapping is defined in Program.cs.
    /// </summary>
    [HttpGet("v2/GetLogins")]
    public async Task<ActionResult<IList<LoginHistoryResponse>>> Get_UsingProjectTo()
    {
        IList<LoginHistoryResponse> user = await _dbContext.Logins
            .AsNoTracking()
            .Where(q => q.UserId == loggedInUserId)
            .OrderByDescending(q => q.Time)
            .ProjectTo<LoginHistoryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(user);
    }

    /// URL: {{baseUrl}}/LoginHistory/AddLogin
    /// <summary>
    /// Sample endpoint you can hit to add new logins.
    /// GET request so that it can be run quickly in the browser.
    /// </summary>
    [HttpGet("AddLogin")]
    public async Task<ActionResult> SaveLogin()
    {
        var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

        var countryId = await _dbContext.Countries
            .AsNoTracking()
            .Where(q => q.Name == "Australia")
            .Select(q => q.Id)
            .FirstOrDefaultAsync();

        var login = new LoginHistory
        {
            Time = DateTime.UtcNow,
            Ip = ip,
            UserId = loggedInUserId,
            CountryId = countryId
        };

        _dbContext.Logins.Add(login);
        await _dbContext.SaveChangesAsync();

        return Ok(login);
    }
    
    private void SeedData(ApplicationDbContext dbContext)
    {
        var user = dbContext.Users.FirstOrDefault(q => q.Username == "user");
        if (user == null)
        {
            dbContext.Users.Add(new User
            {
                Username = "user"
            });
        }

        var country = dbContext.Countries.FirstOrDefault(q => q.Name == "Australia");
        if (country == null)
        {
            dbContext.Countries.Add(new Country
            {
                Name = "Australia"
            });
        }

        dbContext.SaveChanges();

        this.loggedInUserId = user.Id;
    }
}
