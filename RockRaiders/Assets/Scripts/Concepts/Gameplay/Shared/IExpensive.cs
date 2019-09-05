using System;
using System.Collections.Generic;

namespace Assets.Scripts.Concepts.Gameplay.Shared
{
    public interface IExpensive
    {
        Dictionary<Type, int> Cost { get; set; }
    }
}
