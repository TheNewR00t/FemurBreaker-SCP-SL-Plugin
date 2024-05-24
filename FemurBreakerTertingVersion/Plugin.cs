﻿namespace FemurBreakerTertingVersion
{
    using Exiled.API.Features;
    using Merr = MapEditorReborn.Events.Handlers.Schematic;
    using ExiledhandlerS = Exiled.Events.Handlers.Server;

    public class Plugin : Plugin<Config>
    {
        private EventHandlers Handlers;
        public override void OnEnabled()
        {
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