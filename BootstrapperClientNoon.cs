namespace CryoFall.Noon
{
    using System;
    using AtomicTorch.CBND.GameApi.Scripting;
    using AtomicTorch.CBND.GameApi.Data.Characters;
    using AtomicTorch.CBND.CoreMod.Bootstrappers;
    using AtomicTorch.CBND.CoreMod.Systems.TimeOfDaySystem;
    using AtomicTorch.CBND.CoreMod;

    class BootstrapperClientNoon : BaseBootstrapper
    {
        public override void ClientInitialize()
        {
            BootstrapperClientGame.InitCallback += BootstrapperClientGame_InitCallback;
        }
        private void LoadClientComponent()
        {
            Client.Scene
              .CreateSceneObject(nameof(ClientComponentAlwaysNoon))
              .AddComponent<ClientComponentAlwaysNoon>()
              .Setup(TimeOfDaySystem.ServerTimeOfDayOffsetSeconds);
        }
        private void BootstrapperClientGame_InitCallback(ICharacter obj)
        {
            ClientTimersSystem.AddAction(0.5, LoadClientComponent);
        }
    }
}
