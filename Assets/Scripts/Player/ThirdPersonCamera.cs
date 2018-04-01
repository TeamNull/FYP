using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	public float smooth = 10f;		// Camera following speed
	Transform standardPos;			// Camera normal position
	Transform jumpPos;			// Jump Camera locater
	
	// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
	bool bQuickSwitch = false;	//Change Camera Position Quickly
	
	
	void Start()
	{
		// 各参照の初期化
        standardPos = GameObject.Find ("MiniMapCamera").transform;

		if(GameObject.Find ("JumpPos"))
			jumpPos = GameObject.Find ("JumpPos").transform;

		//カメラをスタートする
		transform.position = standardPos.position;	
		transform.forward = standardPos.forward;
        StaticVarAndFunction.mainCamRendered = true;
	}

	
	void FixedUpdate ()
	{
        if (bQuickSwitch == false)
        {
            // the camera to standard position and direction
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
        }
        else
        {
            // the camera to standard position and direction / Quick Change
            transform.position = standardPos.position;
            transform.forward = standardPos.forward;
            bQuickSwitch = false;
        }
	}

	void setCameraPositionJumpView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
		transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);	
		transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);		
	}

    public void SetCamPos() {

        // 各参照の初期化
        standardPos = GameObject.Find("CamPos").transform;

        if (GameObject.Find("JumpPos"))
            jumpPos = GameObject.Find("JumpPos").transform;

        //カメラをスタートする
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
    }
}
