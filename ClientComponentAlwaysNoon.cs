namespace CryoFall.Noon
{
    using AtomicTorch.CBND.CoreMod.Systems.TimeOfDaySystem;
    using AtomicTorch.CBND.GameApi.Scripting.ClientComponents;
    using System;

    public class ClientComponentAlwaysNoon : ClientComponent
    {
        private double originalOffset = 0.0;
        private DateTime lastUpdateTime = DateTime.Now;
        public void Setup(double originalOffset)
        {
            this.originalOffset = originalOffset;
        }
        public override void Update(double deltaTime)
        {
            var difference = DateTime.Now - lastUpdateTime;

            if(difference.TotalSeconds > 5)
            {
                var currentOffset = TimeOfDaySystem.ServerTimeOfDayOffsetSeconds;
                var currentTimeOfDayseconds = TimeOfDaySystem.CurrentTimeOfDaySeconds / 60;
                var newOffset = originalOffset - (currentTimeOfDayseconds - currentOffset);
                var obj = new object[1] { newOffset };
                var sut = TimeOfDaySystem.Instance;
                typeof(TimeOfDaySystem)
                    .GetMethod("ClientRemote_ServerTimeOfDayOffsetUpdate",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(sut, obj);
                lastUpdateTime = DateTime.Now;
            }

            if(Client.CurrentGame.ConnectionState != AtomicTorch.CBND.GameApi.ServicesClient.ConnectionState.Connected)
            {
                this.Destroy();
            }
        }
    }
}
