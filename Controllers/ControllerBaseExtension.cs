using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Helper methods for the claims
namespace BackEnd.Controllers
{
    [Authorize]
    public static class ControllerBaseExtension 
    {
        /*
            Action: returns the id from the claim of user 
            Param: claim
            Return: user ID int
        */
        public static int GetIdentifier(this ClaimsPrincipal claim){

            return int.Parse(claim.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}