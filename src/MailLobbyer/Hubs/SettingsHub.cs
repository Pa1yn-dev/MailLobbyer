using MailLobbyer.SettingsProfilesClass;
using Microsoft.AspNetCore.SignalR;

namespace MailLobbyer.Server.Hubs;

public class SettingsHub : Hub
{
    private static List<SettingsProfiles> profiles = new List<SettingsProfiles>();

    public async Task AddSettingProfile(string sendername, string senderemail, string username, string password, string host, int port)
    {
        SettingsProfiles profile = new SettingsProfiles(sendername, senderemail, username, password, host, port);
        profiles.Add(profile);
        
    }

    public async Task<List<SettingsProfiles>> GetProfiles()
    {
        return profiles;
    }
}