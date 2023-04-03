using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeDTO employeeDTO, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = employeeDTO.email, Email = employeeDTO.email };

        var result = userManager.CreateAsync(user, employeeDTO.password).Result;

        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var userClaims = new List<Claim> { 
            new Claim("Name", employeeDTO.name),
            new Claim("EmployeeCode", employeeDTO.EmployeeCode)
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;
     

        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(result.Errors.First());
        }

        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}
