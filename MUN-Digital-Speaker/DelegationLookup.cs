using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;
using System.Text.Json;

namespace MUN_Digital_Speaker
{
    public class DelegationLookup
    {
        private readonly MUN_Digital_SpeakerContext _context;

        public DelegationLookup(MUN_Digital_SpeakerContext context)
        {
            _context = context;
        }

        public async Task<String?> GetCountryFromLogin(string? login)
        {
            if (login == null)
            {
                return null;
            }

            Delegation? selectedDelegation = await _context.Delegation.FirstOrDefaultAsync(x => x.Login.ToString() == login);
            if (selectedDelegation == null)
            {
                return null;
            }
            return selectedDelegation.Country.ToString();
        }

        [Authorize]
        public async Task<bool> IsAdmin(string? userLogin)
        {
            List<Delegation> delegations = await _context.Delegation.AsNoTracking().ToListAsync();

            if (delegations.Exists(x => x.Login.ToString() == userLogin && x.Country == "Admin"))
            {
                return true;
            }

            return false;
        }

        public async Task<List<Resolution>> LoadResolutionsAsync()
        {
            var path = Path.Combine("wwwroot/Resolutions", "ResolutionJSON.json");

            var json = await System.IO.File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<List<Resolution>>(json);
        }

    }
}
