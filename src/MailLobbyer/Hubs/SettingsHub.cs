using MailLobbyer.SettingsProfilesClass;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.IO;
using System;

namespace MailLobbyer.Server.Hubs;

public class SettingsHub : Hub
{
    private static List<SettingsProfiles> settingsprofilesinmemory = new List<SettingsProfiles>();
    private static string directorypath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MailLobbyer", "Profiles");
    private static string[] filepaths;
    private static List<SettingsProfiles> profiles = new List<SettingsProfiles>();

    public async Task SettingsProfilesSeeker()
    {
        
        if (!Directory.Exists(directorypath))
        {
            Directory.CreateDirectory(directorypath);
        }

        // Later on change to use config values set by the user
        filepaths = Directory.GetFiles(directorypath);

        foreach (string filepath in filepaths)
        {
           SettingsProfiles newsettingsprofile = DeserialiseSettingsProfile(filepath);
           settingsprofilesinmemory.Add(newsettingsprofile);
        }
            
        
    }

    public async Task SerialiseSettingsProfile(SettingsProfiles settingsprofile)
    {
        string jsonstring = JsonSerializer.Serialize(settingsprofile);
        File.WriteAllText(Path.Combine(directorypath, $"{settingsprofile.Id}.json"), jsonstring);
    }

    public static SettingsProfiles DeserialiseSettingsProfile(string filepath)
    {
        try
        {
            string jsonstring = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<SettingsProfiles>(jsonstring);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Error in deserialisation: {ex.Message}");
            return null;
        }
    }

    public async Task AddSettingProfile(string profilename,string sendername, string senderemail, string username, string password, string host, int port)
    {
        SettingsProfiles newprofile = new SettingsProfiles(Guid.NewGuid(),profilename,sendername, senderemail, username, password, host, port);
        await SerialiseSettingsProfile(newprofile);

        settingsprofilesinmemory.Add(newprofile);
    }

    public async Task RemoveSettingsProfile(Guid ident)
    {
        foreach (SettingsProfiles profile in profiles)
        {
            if(profile.Id == ident)
            {
                settingsprofilesinmemory.Remove(profile);
            }
            else
            {
                System.Console.WriteLine("Profile not found on server");
            }
        }
    }

    public async Task<List<SettingsProfiles>> GetProfiles()
    {
        return settingsprofilesinmemory;
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        settingsprofilesinmemory.Clear();
        await base.OnDisconnectedAsync(exception);
    }
}