using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAura : MonoBehaviour, IPunObservable
{
    public float elapsedTime = 0.0f;

    private Material mat;

    private bool startedFlashing = false;

    private float alpha = 0.0f;
    private PhotonView parent;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        parent = transform.parent.gameObject.GetPhotonView();
        if (parent) parent.ObservedComponents.Add(this);
    }

    void Update()
    {
        if (parent.isMine)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 1.5f && !startedFlashing)
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
        alpha = 0;
    }

    private IEnumerator StartFlashObject()
    {
        while (startedFlashing)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.75f));
            alpha = Random.Range(0.2f, 0.4f);
        }
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(alpha);
        } else if (stream.isReading)
        {
            alpha = (float) stream.ReceiveNext();
        }
    }
}
