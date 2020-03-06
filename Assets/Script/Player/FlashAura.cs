using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAura : MonoBehaviour
{
    public float elapsedTime = 0.0f;

    public bool isMoving = false;
    
    private Material mat;

    private bool startedFlashing = false;

    private float alpha = 0.0f;
    private PhotonView parent;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        parent = transform.parent.gameObject.GetPhotonView();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Si joueur bouge
        if (true)
        {
            elapsedTime = 0;
            StopFlashing();
        }

        if (!startedFlashing)
        {
            if (elapsedTime > 2.0f && parent.isMine)
            {
                StartFlashing();
            }
        }
        else
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
        }
    }

    public void StartFlashing()
    {
        startedFlashing = true;
        StartCoroutine("StartFlashObject");
    }

    public void StopFlashing()
    {
        startedFlashing = false;
        StopCoroutine("StartFlashObject");
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.0f);
    }

    private IEnumerator StartFlashObject()
    {
        while (startedFlashing)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.75f));
            alpha = Random.Range(0.0f, 0.150f);
        }
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isMoving);
        } else if (stream.isReading)
        {
            isMoving = (bool) stream.ReceiveNext();
        }
    }
}
