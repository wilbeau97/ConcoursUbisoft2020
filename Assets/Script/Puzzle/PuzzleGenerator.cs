using UnityEngine;


    public class PuzzleGenerator : MonoBehaviour
    {
        public PositionSelector[] movableObjectList;
        private GameManager gameManager;
        // Start is called before the first frame update
        // on start : // for loop sur les objet et call "randomSelect"
        void Start()
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            foreach (var positionSelector in movableObjectList)
            {
                positionSelector.SetLocalPlayerName(gameManager.GetLocalPlayerName()); // on vient fetch le playername pour savoir qui va MAJ les positions
                positionSelector.RandomSelectPosition(); // On vient random select une position de la liste des positions possible
                positionSelector.updatePositions(); // Si p1 -> va envoyer a p2 la position de la plateforme
            }
        }
        // recoit un call que c'Est une autre playthrough
        // set isPLaythrouhg = false
        // randomSelect
        
    }

