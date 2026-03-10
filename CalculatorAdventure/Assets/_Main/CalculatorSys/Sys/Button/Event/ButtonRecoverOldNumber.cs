using System.Collections.Generic;
using EventSys.Interface;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonRecoverOldNumber : IEventData
    {
        public List<byte> LockedButtonIndexes { get; }

        public byte ButtonIndexes { get; }


        public ButtonRecoverOldNumber(byte buttonIndex, List<byte> lockedButtonIndexes)
        {
            ButtonIndexes = buttonIndex;
            LockedButtonIndexes = lockedButtonIndexes;
        }
    }
}