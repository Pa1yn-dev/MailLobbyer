using MailLobbyer.SettingsProfilesClass;
using Microsoft.AspNetCore.SignalR;

namespace MailLobbyer.Server.Hubs;

public class SettingsHub : Hub
{
    private static List<SettingsProfiles> profiles = new List<SettingsProfiles>();

    public async Task AddSettingProfile(string profilename,string sendername, string senderemail, string username, string password, string host, int port)
    {
        SettingsProfiles profile = new SettingsProfiles(Guid.NewGuid(),profilename,sendername, senderemail, username, password, host, port);
        profiles.Add(profile);
    }

    public async Task RemoveSettingsProfile(Guid ident)
    {
        foreach (SettingsProfiles profile in profiles)
        {
            if(profile.Id == ident)
            {
                profiles.Remove(profile);
            }
            else
            {
                System.Console.WriteLine("Profile not found on server");
            }
        }
    }

    public async Task<List<SettingsProfiles>> GetProfiles()
    {
        return profiles;
    }
}