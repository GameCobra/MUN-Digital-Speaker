using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;
using System.Security.Claims;

namespace MUN_Digital_Speaker.Controllers
{
    public class GetCountryFromLoginViewComponent : ViewComponent
    {
        private readonly MUN_Digital_SpeakerContext _context;

        public GetCountryFromLoginViewComponent(MUN_Digital_SpeakerContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Delegation delegation = await _context.Delegation.FirstAsync(x => x.Login.ToString() == User.Identity.Name);
            return Content(delegation.Country);
        }
    }
}
