using Exiled.API.Features;
using System.Collections.Generic;
using PlayerRoles;
using Random = UnityEngine.Random;
using Player = Exiled.API.Features.Player;
using System.Linq;
using Exiled.Events.EventArgs.Server;
using MEC;
using AudioPlayer.API;
using ProjectMER.Events.Arguments;
using AudioPlayer.API.Container;
using Exiled.API.Enums;

namespace FemurBreakerTertingVersion
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;
        float playerAlive = 0;
        public void OnTrap(ButtonInteractedEventArgs ev)
        {

            if (ev.Button.GameObject.name == "Button106")
            {
                if (playerAlive == 0)
                {
                    playerAlive = 1;
                    if (plugin.Config.CassieWithSacrificie)
                    {
                        ev.Player.Kill(plugin.Config.OnSacrificeDeathReason, plugin.Config.CassieAnounceWhitPlayerDead);
                    }
                    else
                    {
                        ev.Player.Kill(plugin.Config.OnSacrificeDeathReason);
                    }
                    
                    return;
                }

                if (playerAlive == 1)
                {
                    if (plugin.Config.UseGenerators)
                    {
                        if (Generator.Get(GeneratorState.Activating).Count() == plugin.Config.Generators)
                        {
                            if (Random.Range(1, 101) > plugin.Config.porcent)
                            {
                                playerAlive = 0;
                                ev.Player.SendBroadcast(plugin.Config.OnFailure, 4);
                            }
                            else
                            {
                                playerAlive = 3;
                                ev.Player.SendBroadcast(plugin.Config.OnDeath, 4);
                                List<Player> scp106 = Player.List.Where(p => p.Role == RoleTypeId.Scp106).ToList();
                                if (scp106 != null)
                                {
                                    foreach (Player player in scp106) {
                                        player.Hurt(9999, DamageType.FemurBreaker);
                                    }
                                    Extension(plugin.Config.npc);
                                }
                            }
                        }
                        else
                        {
                            ev.Player.SendBroadcast($"{plugin.Config.TextGenerators}" + $"{Generator.Get(GeneratorState.Activating).Count()} / {plugin.Config.Generators}", 4);
                        }
                    }
                    else
                    {
                        if (Random.Range(1, 101) > plugin.Config.porcent)
                        {
                            playerAlive = 0;
                            ev.Player.SendBroadcast(plugin.Config.OnFailure, 4);
                        }
                        else
                        {
                            playerAlive = 3;
                            ev.Player.SendBroadcast(plugin.Config.OnDeath, 4);
                            List<Player> scp106 = Player.List.Where(p => p.Role == RoleTypeId.Scp106).ToList();
                            if (scp106 != null)
                            {
                                foreach (Player player in scp106) { player.Kill(plugin.Config.OnRecontainmentDeath); }
                                Extension(plugin.Config.npc);
                            }
                        }
                    }


                }
                else
                {
                    if (playerAlive == 3)
                    {
                        ev.Player.SendBroadcast(plugin.Config.OnRecontainmentRepeat, 4);
                    }
                    else
                    {
                        ev.Player.SendBroadcast(plugin.Config.OnRequirements, 4);
                    }
                }
            }
        }
        
        public void Extension(bool YT)
        {
            if (plugin.Config.SoundOrNotsound) 
            {
                if (YT)
                {
                    var path = System.IO.Path.Combine(Paths.Plugins, "Audio", plugin.Config.FileAudioName + ".ogg");
                    var npc = AudioPlayerBot.SpawnDummy(name: plugin.Config.OnNameBot, id: 99, badgeText: plugin.Config.BotBadgetName, bagdeColor: plugin.Config.BadgetBotColor);
                    npc.Player.ReferenceHub.roleManager.ServerSetRole(RoleTypeId.Overwatch, RoleChangeReason.Died);
                    var audio = npc.AudioPlayerBase;
                    audio.BroadcastChannel = VoiceChat.VoiceChatChannel.Intercom;
                    audio.AudioToPlay.Add(path);
                    audio.Play(0);
                    Timing.CallDelayed(plugin.Config.seconds, () =>
                    {
                        AudioController.DisconnectDummy(npc.ID);
                    });
                }
                else
                {
                    return;
                }
            }
            else
            {
                Cassie.Message(plugin.Config.Cassie);
            }
        }

        public void OnRestart(RoundEndedEventArgs ev)
        {
            playerAlive = 0;
        }
    }
}
