using System;

public class CPHInline
{
	string discordUsername = "";
	string avatarURL = "";
	string webhookUrl = "";
	string twitchAvatarURL = "https://i.imgur.com/xGoEvn9.png";
		
	public bool Execute()
	{
		CPH.TryGetArg("webhookUrl", out webhookUrl);
		CPH.TryGetArg("__source", out string source);
		
		switch (source)
		{
			case "TwitchChatMessage":
				{
					CPH.TryGetArg("message", out string message);
					CPH.TryGetArg("user", out string user);
					PostTwitchChatMessage(message, user);
				}
				break;
			case "YouTubeMessage":
				{
					CPH.TryGetArg("message", out string message);
					CPH.TryGetArg("user", out string user);
					CPH.TryGetArg("userId", out string userId);
					CPH.TryGetArg("userProfileUrl", out string userProfileUrl);
					PostYouTubeMessage(message, user, userId, userProfileUrl);
				}
				break;
		}
		
		return true;
	}
	
	public void PostTwitchChatMessage(string message, string user)
	{
		// Include platform in content
		string content = $"[Twitch] {user}: {message}";
		
		// Get Twitch avatar URL
		string avatarURL = CPH.TwitchGetExtendedUserInfoByLogin(user).ProfileImageUrl;
		
		CPH.DiscordPostTextToWebhook(webhookUrl, content, user, avatarURL);
	}
	
	public void PostYouTubeMessage(string message, string user, string userId, string userProfileUrl)
	{
		// Include platform in content
		string content = $"[YouTube] {user}: {message}";
		
		CPH.DiscordPostTextToWebhook(webhookUrl, content, user, userProfileUrl);
	}
	
	// Optional: Keep the timestamp method if you may need it elsewhere
	public string WhenTheThingHappened()
	{
		return $"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}]";
	}
}