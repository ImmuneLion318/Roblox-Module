namespace RobloxClient
{
    public interface RobloxInterface
    {
        #region Client-Version
        public enum Client
        {
            Roblox,
            Studio
        }
        #endregion

        #region User-Data
        public class User
        {
            [JsonProperty("description")]
            public string Description;

            [JsonProperty("created")]
            public DateTime Created;

            [JsonProperty("isBanned")]
            public bool IsBanned;

            [JsonProperty("externalAppDisplayName")]
            public object ExternalAppDisplayName;

            [JsonProperty("hasVerifiedBadge")]
            public bool HasVerifiedBadge;

            [JsonProperty("id")]
            public int Id;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("displayName")]
            public string DisplayName;
        }

        public struct UserInfo
        {
            public string
            UserName,
            DisplayName,
            Description;

            public int
            Id;

            public bool
            IsBanned,
            HasVerifiedBadge;

            public DateTime
            Created;
        }
        #endregion
    }

    public class Roblox : RobloxInterface
    {
        public static async Task<UserInfo> GetUser(int Id)
        {
            User UserData = JsonConvert.DeserializeObject<User>
                (await HttpService.GetContentAsync($"https://users.roblox.com/v1/users/{Id}"));

            return new UserInfo
            {
                UserName = UserData.Name,
                DisplayName = UserData.DisplayName,
                Description = UserData.Description,
                Id = UserData.Id,
                IsBanned = UserData.IsBanned,
                HasVerifiedBadge = UserData.HasVerifiedBadge,
                Created = UserData.Created
            };
        }
        
        public static async Task<string> Version(Client Client)
        {
            switch (Client)
            {
                case Client.Roblox:
                    return await HttpService.GetContentAsync("http://setup.roblox.com/version");
                case Client.Studio:
                    return await HttpService.GetContentAsync("http://setup.roblox.com/versversionStudio");
            }

            return "Failed To Determine Client, Or Get Version.";
        }
    }
}
