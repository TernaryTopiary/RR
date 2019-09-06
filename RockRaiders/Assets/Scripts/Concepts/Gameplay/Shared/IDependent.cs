using System;
using System.Collections.Generic;

namespace Assets.Scripts.Concepts.Gameplay.Shared
{
    class IDependent
    {
        private Dictionary<Type, int> Requirements { get; set; }
    }
}
