using System;
using Infrastructure;
using UnityEngine;

namespace Behaviours
{
    public class BaseTower : MonoBehaviour
    {
        private void Start()
        {
            Bootstrapper.Instance.Services.Resolve<GameLoopController>().Test();
        }
    }
}