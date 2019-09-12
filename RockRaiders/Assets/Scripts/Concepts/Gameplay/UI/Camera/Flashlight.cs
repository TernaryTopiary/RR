using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.UI.Camera
{
    public class Flashlight : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            transform.LookAt(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(1000));
        }
    }
}
