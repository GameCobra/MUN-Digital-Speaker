using Microsoft.EntityFrameworkCore;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;

namespace MUN_Digital_Speaker
{
    public class DelegationLookup
    {
        private readonly MUN_Digital_SpeakerContext _context;

        public DelegationLookup(MUN_Digital_SpeakerContext context)
        {
            _context = context;
        }

        public async Task<String?> GetCountryFromLogin(string login)
        {
            Delegation? selectedDelegation = await _context.Delegation.FirstOrDefaultAsync(x => x.Login.ToString() == login);
            if (selectedDelegation == null)
            {
                return null;
            }
            return selectedDelegation.Country.ToString();
        }

    }
}
