using UnityEngine;

namespace Prefab.Puzzle4
{
    public class PressurePlateRoad : MonoBehaviour
    {
        [SerializeField] private PhotonView targetObjectView;
        [SerializeField] private string pressedRpcMethode;
        [SerializeField] private string depressedRPCMethode;
        
        
        public void OnTriggerEnter(Collider other)
        {
            Pressed();
        }
    
        public void OnTriggerExit(Collider other)
        {
            Depressed();
        }

        private void Pressed()
        {
            //targetObjectView.RPC(nameRPCMethode, PhotonTargets.All);
            
            if (!PhotonNetwork.connected)
            {
                targetObjectView.GetComponent<MovePlatform>().MovePlatformForward();
            }
            else
            {
                targetObjectView.gameObject.SendMessage(pressedRpcMethode);
            }
        }

        private void Depressed()
        {
            if (!PhotonNetwork.connected)
            {
                targetObjectView.GetComponent<MovePlatform>().MovePlatformBackward();
            }
            else
            {
                targetObjectView.gameObject.SendMessage(depressedRPCMethode);
            }
            
        }
    }
}
