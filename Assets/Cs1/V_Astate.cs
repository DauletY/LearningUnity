using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Cs1
{
   
    public class V_Astate : MonoBehaviour
    {
        public GameObject cube;
        private void Awake()
        {
            // Find game object cube
            cube = FindObjectOfType(typeof(GameObject)) as GameObject;
        }
        private void Start()
        {
            // cube disable
            cube.SetActive(false);
        }
    }
}
