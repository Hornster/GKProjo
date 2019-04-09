using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Emitters
{
    public interface IParticleEmitter
    {
        /// <summary>
        /// Assigns parent object to the emitter. The particles will be emitted at the parent's position.
        /// </summary>
        /// <param name="parent"></param>
        void AssignParent(Transform parent);
    }
}
