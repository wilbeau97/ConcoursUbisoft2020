using UnityEngine;

namespace Prefab.Puzzle4
{
    public class RoadDoorOpen : MonoBehaviour
    {
        [SerializeField] private GameObject[] boolListGameObject;
        [SerializeField] private bool endMaze = true;
        private bool listResultBool = false;
        public PhotonView targetRpcView;

        public void FixedUpdate()
        {
            CheckBoolValidate();
            if (listResultBool && endMaze)
            {
                
                targetRpcView.RPC("OpenDoorRPC",PhotonTargets.All);
                endMaze = false;
            }
        }

        private void CheckBoolValidate()
        {
            foreach (GameObject gameObject in boolListGameObject)
            {
                if (!gameObject.activeInHierarchy)
                {
                    listResultBool = false;
                    break;
                }
                listResultBool = true;
            }
        }
    }
}