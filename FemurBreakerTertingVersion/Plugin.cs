namespace FemurBreakerTertingVersion
{
    using Exiled.API.Features;
    using Merr = ProjectMER.Events.Handlers.Schematic;
    using ExiledhandlerS = Exiled.Events.Handlers.Server;
    using System.IO;
    using FemurBreaker;
    using System;

    public class Plugin : Plugin<Config, Translations>
    {
        public override string Name => "FemurBreaker";
        public override string Author => "davilone32";
        public override Version Version => new Version(1, 4, 2);

        private EventHandlers Handlers;
        public readonly string AudioPath = Path.Combine(Paths.Plugins, "audio");
        public override void OnEnabled()
        {

            if (!Directory.Exists(AudioPath))
            {
                Directory.CreateDirectory(AudioPath);
            }

            Handlers = new EventHandlers(this);
            ExiledhandlerS.RoundEnded += Handlers.OnRestart;
            Merr.ButtonInteracted += Handlers.OnTrap;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            ExiledhandlerS.RoundEnded -= Handlers.OnRestart;
            Merr.ButtonInteracted -= Handlers.OnTrap;
            Handlers = null;
            base.OnDisabled();
        }
    }
}
