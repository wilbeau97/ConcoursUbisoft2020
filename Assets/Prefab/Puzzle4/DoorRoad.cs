using UnityEngine;

namespace Prefab.Puzzle4
{
    public class DoorRoad : MonoBehaviour
    {
        [SerializeField] private bool state;
    
        // Update is called once per frame
        void Update()
        {
            Vector3 position = gameObject.transform.localPosition;
            if (state && position.y < 10.0f)
            {
                gameObject.transform.Translate(0.0f,0.1f,0.0f);
            }
            else if (!state && position.y > 5.0f)
            {
                gameObject.transform.Translate(0.0f,-0.1f,0.0f);
            }
        }
    
        [PunRPC]
        public void StateChange()
        {
            state = !state;
        }
    }
}
