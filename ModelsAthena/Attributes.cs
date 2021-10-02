using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenProfileFN.Models
{
    public class Attributes
    {
        [JsonProperty("season_match_boost")]
        public int SeasonMatchBoost = 999;

        [JsonProperty("loadouts")]
        public string[] loadouts = new string[] {       "sandbox_loadout",
                "loadout_1"};

        [JsonProperty("rested_xp_overflow")]
        public int RestedXpOverflow = 10000;

        [JsonProperty("mfa_reward_claimed")]
        public bool MfaRewardClaimed = true;

        [JsonProperty("quest_manager")]

        public QuestManager questmanager = new QuestManager();

        [JsonProperty("book_level")]
        public int BookLevel = 1000;

        [JsonProperty("season_num")]

        public int SeasonNum = 15;

        [JsonProperty("season_update")]

        public int SeasonUpdate = 0;  
        
        [JsonProperty("book_xp")]

        public int BookXp = 1000;   
        
        [JsonProperty("permissions")]

        public Array Permissions = Array.Empty<object>();

        [JsonProperty("book_purchased")]
        public bool BookPurchased = true;

        [JsonProperty("lifetime_wins")]
        public int LifetimeWins = 999;

        [JsonProperty("party_assist_quest")]
        public string PartyAssistQuest = "";

        [JsonProperty("purchased_battle_pass_tier_offers")]
        public Array PurchasedBattlePassTierOffers = Array.Empty<object>();

        [JsonProperty("rested_xp_exchange")]
        public double RestedXpExchange = 5.333;

        [JsonProperty("level")]
        public int Level = 1000;

        [JsonProperty("xp_overflow")]
        public int XpOverflow = 10200;

        [JsonProperty("rested_xp")]
        public int RestedXp = 204000;

        [JsonProperty("rested_xp_mult")]
        public double RestedXpMult = 13;

        [JsonProperty("accountLevel")]
        public int AccountLevel = 1000;

        [JsonProperty("competitive_identity")]
        public CompetitiveIdentity CompetitiveIdentity = new CompetitiveIdentity();

        [JsonProperty("inventory_limit_bonus")]
        public int InventoryLimitBonus = 0;

        [JsonProperty("last_applied_loadout")]
        public string LastAppliedLoadout = "louadout_1";

        [JsonProperty("daily_rewards")]
        public DailyRewards DailyRewards = new DailyRewards();

        [JsonProperty("xp")]
        public int Xp = 100000;

        [JsonProperty("season_friend_match_boost")]
        public int SeasonFriendMatchBoost = 0;

        [JsonProperty("active_loadout_index")]
        public int ActiveLoadoutIndex = 0;





    }
}
