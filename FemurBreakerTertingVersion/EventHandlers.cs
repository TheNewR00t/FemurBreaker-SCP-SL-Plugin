using AudioPlayer.API;
using AudioPlayer.API.Container;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using ProjectMER.Events.Arguments;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Player = Exiled.API.Features.Player;
using Random = UnityEngine.Random;

namespace FemurBreakerTertingVersion
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;
        float playerAlive = 0;
        public void OnTrap(ButtonInteractedEventArgs ev)
        {
            if (ev.Button.GameObject.name != "Button106") return;

            switch (playerAlive)
            {
                case 0:
                    HandleFirstInteraction(ev.Player);
                    break;
                case 1:
                    HandleSecondInteraction(ev.Player);
                    break;
                case 3:
                    ev.Player.SendBroadcast(plugin.Config.OnRecontainmentRepeat, 4);
                    break;
                default:
                    ev.Player.SendBroadcast(plugin.Config.OnRequirements, 4);
                    break;
            }
        }

        private void HandleFirstInteraction(Player player)
        {
            playerAlive = 1;
            if (plugin.Config.CassieWithSacrificie)
            {
                player.Kill(plugin.Config.OnSacrificeDeathReason, plugin.Config.CassieAnounceWhitPlayerDead);
            }
            else
            {
                player.Kill(plugin.Config.OnSacrificeDeathReason);
            }
        }

        private void HandleSecondInteraction(Player player)
        {
            bool success = Random.Range(1, 101) <= plugin.Config.porcent;

            if (plugin.Config.UseGenerators)
            {
                int activeGenerators = Generator.Get(GeneratorState.Activating).Count();
                if (activeGenerators != plugin.Config.Generators)
                {
                    player.Broadcast(4, $"{plugin.Config.TextGenerators}{activeGenerators} / {plugin.Config.Generators}");
                    return;
                }
            }

            if (!success)
            {
                playerAlive = 0;
                player.Broadcast(4, plugin.Config.OnFailure);
            }
            else
            {
                playerAlive = 3;
                player.Broadcast(4, plugin.Config.OnDeath);
                AffectScp106();
                Extension(plugin.Config.npc);
            }
        }

        private void AffectScp106()
        {
            List<Player> scp106 = Player.List.Where(p => p.Role == RoleTypeId.Scp106).ToList();
            foreach (Player p in scp106)
            {
                if (plugin.Config.UseGenerators)
                    p.Hurt(9999, DamageType.FemurBreaker);
                else
                    p.Kill(plugin.Config.OnRecontainmentDeath);
            }
        }

        public void Extension(bool YT)
        {
            if (!plugin.Config.SoundOrNotsound)
            {
                Cassie.Message(plugin.Config.Cassie);
                return;
            }

            if (!YT) return;

            PlayAudioBot(plugin.Config.FileAudioName, plugin.Config.OnNameBot, plugin.Config.BotBadgetName, plugin.Config.BadgetBotColor, plugin.Config.seconds);
        }

        private void PlayAudioBot(string fileName, string botName, string badgeText, string badgeColor, float duration)
        {
            string path = Path.Combine(Paths.Plugins, "Audio", fileName + ".ogg");

            var npc = AudioPlayerBot.SpawnDummy(
                name: botName,
                id: 99,
                badgeText: badgeText,
                bagdeColor: badgeColor
            );

            npc.Player.ReferenceHub.roleManager.ServerSetRole(RoleTypeId.Overwatch, RoleChangeReason.Died);

            var audio = npc.AudioPlayerBase;
            audio.BroadcastChannel = VoiceChat.VoiceChatChannel.Intercom;
            audio.AudioToPlay.Add(path);
            audio.Play(0);

            Timing.CallDelayed(duration, () => AudioController.DisconnectDummy(npc.ID));
        }

        public void OnRestart(RoundEndedEventArgs ev)
        {
            playerAlive = 0;
        }
    }
}
