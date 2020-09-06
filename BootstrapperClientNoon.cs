namespace CryoFall.Noon
{
    using AtomicTorch.CBND.GameApi.Scripting;
    using AtomicTorch.CBND.GameApi.Data.Characters;
    using AtomicTorch.CBND.CoreMod.Bootstrappers;
    using AtomicTorch.CBND.CoreMod.Systems.TimeOfDaySystem;

    class BootstrapperClientNoon : BaseBootstrapper
    {
        public override void ClientInitialize()
        {
            BootstrapperClientGame.InitCallback += BootstrapperClientGame_InitCallback;
        }
        private void BootstrapperClientGame_InitCallback(ICharacter obj)
        {
            Client.Scene
              .CreateSceneObject(nameof(ClientComponentAlwaysNoon))
              .AddComponent<ClientComponentAlwaysNoon>()
              .Setup(TimeOfDaySystem.ServerTimeOfDayOffsetSeconds);
        }
    }
}
