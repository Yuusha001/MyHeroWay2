using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D.Animation;

namespace MyHeroWay.MapDeco
{
    public class TorchDeco : MonoBehaviour
    {
        public TorchType[] types;
        public Torch[] torches;
        [BoxGroup("Torch Range")]
        public int start;
        [BoxGroup("Torch Range")]
        public int end;
        [BoxGroup("Torch Range")]
        public EETorchType TorchType;
        void Start()
        {
            
        }

        [Button("Get All Torch")]
        public void GetAllTorch()
        {
            var torchesBase = GameObject.FindGameObjectsWithTag("Torch");
            torches = new Torch[torchesBase.Length];
            for (int i = 0; i < torchesBase.Length; i++)
            {
                torches[i] = new Torch
                {
                    transform = torchesBase[i].transform,
                    animator = torchesBase[i].GetComponentInChildren<Animator>(),
                    spriteLibrary = torchesBase[i].GetComponentInChildren<SpriteLibrary>(),
                    torchColor = torchesBase[i].GetComponentInChildren<Light2D>()
                };
                // You might want to assign a name to the torch here if needed
                torches[i].transform.name = "Torch " + i;
            }
        }


        [Button("Torch Settup")]
        public void Setup()
        {
            var type = types.First((e) => e.name == TorchType);
            for (int i = start; i < end; i++)
            {
                int rand = Random.Range(0, type.spriteLibraries.Length);
                torches[i].spriteLibrary.spriteLibraryAsset = type.spriteLibraries[rand];
                torches[i].torchColor.color = type.color;
            }
        }

        

    }

    [System.Serializable]
    public class Torch
    {
        public Transform transform;
        public float animationSpeed = 1;
        public Animator animator;
        public SpriteLibrary spriteLibrary;
        public Light2D torchColor;
       
    }
    [System.Serializable]
    public class TorchType
    {
        public EETorchType name;
        public Color color;
        public SpriteLibraryAsset[] spriteLibraries;
    }
}
