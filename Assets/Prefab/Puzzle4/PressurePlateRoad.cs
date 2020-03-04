using UnityEngine;

namespace Prefab.Puzzle4
{
    public class PressurePlateRoad : MonoBehaviour
    {
        [SerializeField] private PhotonView targetObjectView;
        [SerializeField] private string nameRpcMethode;

        public void OnTriggerEnter(Collider other)
        {
            Pressed();
        }
    
        public void OnTriggerExit(Collider other)
        {
            Pressed();
        }

        private void Pressed()
        {
            //targetObjectView.RPC(nameRPCMethode, PhotonTargets.All);
            targetObjectView.gameObject.SendMessage(nameRpcMethode);
        }
    }
}
