using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerComponents
{
    public class Skin : MonoBehaviour
    {
        public void Activate(bool _bool)
        {
            gameObject.SetActive(_bool);
        }
    }
}