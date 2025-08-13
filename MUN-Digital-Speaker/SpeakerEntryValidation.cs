using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MUN_Digital_Speaker.Data;
using MUN_Digital_Speaker.Models;

namespace MUN_Digital_Speaker
{
    public class SpeakerEntryValidation
    {
        public async static Task<bool> IsValideEntry(Delegation delegation, MUN_Digital_SpeakerContext speakerDatabase)
        {
            return !await DoseValueExist(x => x.Login == delegation.Login, speakerDatabase);
        }

        public async static Task<bool> DoseValueExist(Func<Delegation, bool> findValueFunction, MUN_Digital_SpeakerContext speakerDatabase)
        {
            List<Delegation> delegations = await speakerDatabase.Delegation.ToListAsync();
            bool isDuplicit = delegations.Any(findValueFunction);
            return isDuplicit;
        }
    }
}
