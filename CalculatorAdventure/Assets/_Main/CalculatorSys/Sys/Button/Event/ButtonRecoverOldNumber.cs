using System.Collections.Generic;
using _Main.CalculatorSys.Manager.Runtime;
using EventSys.Interface;
using UnityEngine;

namespace _Main.CalculatorSys.Sys.Button.Event
{
    public readonly struct ButtonRecoverOldNumber : IEventData
    {
        private readonly List<byte> _lockedButtonIndexes;
        private readonly byte _buttonIndex;
        public List<byte> LockedButtonIndexes => _lockedButtonIndexes;
        public byte ButtonIndexes => _buttonIndex;
        

        public ButtonRecoverOldNumber(byte buttonIndex, List<byte> lockedButtonIndexes)
        {
            _buttonIndex = buttonIndex;
            _lockedButtonIndexes = lockedButtonIndexes;
        }
    }
}