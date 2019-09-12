using System;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Resource;

namespace Assets.Scripts.Concepts.Gameplay.Shared
{
    public interface IExpensive
    {
        Dictionary<ResourceType, int> Cost { get; set; }
    }

    
}
