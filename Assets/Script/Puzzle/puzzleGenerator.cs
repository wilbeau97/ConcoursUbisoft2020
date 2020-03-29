using UnityEngine;


    public class PuzzleGenerator : MonoBehaviour
    {
        public PositionSelector[] movableObjectList;
    
        // Start is called before the first frame update
        // on start : // for loop sur les objet et call "randomSelect"
        void Start()
        {
            foreach (var positionSelector in movableObjectList)
            {
                positionSelector.RandomSelectPosition();
            }
        }
        // recoit un call que c'Est une autre playthrough
        // set isPLaythrouhg = false
        // randomSelect
        
    }

