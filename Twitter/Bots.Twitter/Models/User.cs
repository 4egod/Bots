using Newtonsoft.Json;

namespace Bots.Twitter
{
    public class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdAsString { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("profile_location")]
        public string ProfileLocation { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        //[JsonProperty("entities")]
        //public ? Entities { get; set; }

        //[JsonProperty("protected")]
        //public bool Protected { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        //[JsonProperty("friends_count")]
        //public int FriendsCount { get; set; }

        //[JsonProperty("listed_count")]
        //public int ListedCount { get; set; }

        [JsonProperty("created_at")]
        //public DateTime CreatedAt { get; set; }
        public string CreatedAt { get; set; }

        //[JsonProperty("favourites_count")]
        //public int FavouritesCount { get; set; }

        //[JsonProperty("utc_offset")]
        //public ? UtcOffset { get; set; }

        //[JsonProperty("time_zone")]
        //public ? TimeZone { get; set; }

        //[JsonProperty("geo_enabled")]
        //public bool GeoEnabled { get; set; }

        //[JsonProperty("verified")]
        //public bool Verified { get; set; }

        //[JsonProperty("statuses_count")]
        //public int StatusesCount { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        //[JsonProperty("status")]
        //public ? Status { get; set; }

        //[JsonProperty("contributors_enabled")]
        //public bool ContributorsEnabled { get; set; }

        //[JsonProperty("is_translator")]
        //public bool IsTranslator { get; set; }

        //[JsonProperty("is_translation_enabled")]
        //public bool IsTranslationEnabled { get; set; }

        //[JsonProperty("profile_background_color")]
        //public Color ProfileBackgroundColor { get; set; }

        //[JsonProperty("profile_background_image_url")]
        //public string ProfileBackgroundImageUrl { get; set; }

        //[JsonProperty("profile_background_image_url_https")]
        //public string ProfileBackgroundImageUrlHttps { get; set; }

        //[JsonProperty("profile_background_tile")]
        //public bool ProfileBackgroundTile { get; set; }

        //[JsonProperty("profile_image_url")]
        //public string ProfileImageUrl { get; set; }

        //[JsonProperty("profile_image_url_https")]
        //public string ProfileImageUrlHttps { get; set; }

        //[JsonProperty("profile_banner_url")]
        //public string ProfileBannerUrl { get; set; }

        //[JsonProperty("profile_link_color")]
        //public Color ProfileLinkColor { get; set; }

        //[JsonProperty("profile_sidebar_border_color")]
        //public Color ProfileSidebarBorderColor { get; set; }

        //[JsonProperty("profile_sidebar_fill_color")]
        //public Color ProfileSidebarFillColor { get; set; }

        //[JsonProperty("profile_text_color")]
        //public Color ProfileTextColor { get; set; }

        //[JsonProperty("profile_use_background_image")]
        //public bool ProfileUseBackgroundImage { get; set; }

        //[JsonProperty("has_extended_profile")]
        //public bool HasExtendedProfile { get; set; }

        //[JsonProperty("default_profile")]
        //public bool DefaultProfile { get; set; }

        //[JsonProperty("default_profile_image")]
        //public bool DefaultProfileImage { get; set; }

        //[JsonProperty("following")]
        //public bool? Following { get; set; }

        //[JsonProperty("follow_request_sent")]
        //public bool? FollowRequestSent { get; set; }


        //[JsonProperty("notifications")]
        //public bool Notifications { get; set; }

        //[JsonProperty("translator_type")]
        //public ? TranslatorType { get; set; }

        //[JsonProperty("suspended")]
        //public bool Suspended { get; set; }

        //[JsonProperty("needs_phone_verification")]
        //public bool NeedsPhoneVerification { get; set; }
    }
}
