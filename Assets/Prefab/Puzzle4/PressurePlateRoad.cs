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
            if (!PhotonNetwork.connected) // si on est offline
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
            if (!PhotonNetwork.connected) // si on est offline 
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
