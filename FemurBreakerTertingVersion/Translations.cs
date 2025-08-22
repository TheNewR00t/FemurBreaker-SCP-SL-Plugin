using Exiled.API.Interfaces;
using System.ComponentModel;

namespace FemurBreaker
{
    public class Translations : ITranslation
    {
        [Description("Onfailure")]
        public string OnFailure { get; set; } = "¡The recontainment of scp 106 has failed! Find another person for recontainment!";
        [Description("OnDeath")]
        public string OnDeath { get; set; } = "SCP-106 has been successfully recontained!";
        [Description("OnRecontainmentRepeat")]
        public string OnRecontainmentRepeat { get; set; } = "The old bastard has been recontained!";
        [Description("OnRequirements")]
        public string OnRequirements { get; set; } = "Someone still needs to sacrifice themselves!";
        [Description("OnRequerimentsComplete")]
        public string OnRequerimentsComplete { get; set; } = "One has already sacrificed themselves!";
        [Description("OnSacrificeDeathReason")]
        public string OnSacrificeDeathReason { get; set; } = "You're a hero!";
        [Description("Cassie sound when 106 holds back (You will need to have \"UseCassie\" disabled)")]
        public string Cassie { get; set; } = "SCP 1 0 6 HAS BEEN SUCCEFULY RECONTAINED BY FEMUR BREAKER";
    }
}
