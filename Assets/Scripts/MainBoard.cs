using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainBoard : MonoBehaviour
{
	public GameObject staff;
	public GameObject objSeletion;	
	// Material mặc định của các object
	public Material objMat;
	//Khi obj vào của hàng cái chúng ta cho nó nháy phát để nhìn thấy
	public Material objInResMat;
	public Transform Parent;
	List<BaseObject> objectSelection = new List<BaseObject> ();
	List<Staff> staffs = new List<Staff> ();
	List<BaseObject> objInResList = new List<BaseObject> ();
	AreaMap areaMap;
	float areaLength = 50f;
	int subAreaLength = 10;
	int numberObject = 150;
	int numberStaff = 20;
	public static int idChecking = -1;
	
	void Start ()
	{
		areaMap = new AreaMap ((int)areaLength, subAreaLength);
		for (int i = 0; i < numberObject; i++) {
			Vector3 ranPos = new Vector3 (Random.Range (0f, areaLength), 0.5f, Random.Range (0f, areaLength));
			GameObject obj = Instantiate (objSeletion, ranPos, Quaternion.identity) as GameObject;
			BaseObject baseObj = new ObjectSelection (obj, areaLength, areaMap);
			baseObj.id = i;
			objectSelection.Add (baseObj);
			obj.transform.parent = Parent;								
		}

		for (int i = 0; i < numberStaff; i++) {
			Vector3 ranPos = new Vector3 (Random.Range (0f, areaLength), 0.5f, Random.Range (0f, areaLength));
			GameObject staffIns = Instantiate (staff, ranPos, Quaternion.identity) as GameObject;
			staffs.Add (new Staff (staffIns, areaLength));
			staffIns.transform.parent = Parent;							
		}
	}
	
	// Mặc định các obj sẽ không có một vị trí cố định nào cả.
	void Update ()
	{
		if (timeCount <= 0)
			return;
		for (int i = 0; i < objectSelection.Count; i++) {
			objectSelection [i].GoSomewhere ();
		}

		for (int i = 0; i < objInResList.Count; i++) {
			objInResList [i].meshRenderer.material = objMat;
		}
		objInResList.Clear ();

		for (int i = 0; i < staffs.Count; i++) {
			BaseObject objInRest = areaMap.SetInRestaurent (staffs [i]);
			if (objInRest != null) {
				objInRest.meshRenderer.material = objInResMat;				
				objInResList.Add (objInRest);				
				if (idChecking != objInRest.id) { 
					idChecking = objInRest.id;
					objInRest.Mark (true);
					// Phát hiện có tính xấu
					int ran = Random.Range (0, 6);
					if (ran >= 5.0f) {
						objInRest.Mark (false);
					}
				}
			}
		}
	}

	private string ui_Notifi = "Time: ";
	private float timeCount = 365f;
	private int loveComp = 0;
	private BaseObject finalObj;
	void OnGUI() {
		timeCount -= Time.deltaTime;
		if (timeCount > 0) {
			ui_Notifi = "Time: " + Mathf.Round(timeCount);
		} else {
			for (int i = 0; i < objectSelection.Count; i++) {
				if(objectSelection[i].love > loveComp){
					loveComp = objectSelection[i].love;
					finalObj = objectSelection[i];
				}
			}

			ui_Notifi = "Final Object(ID): "+ finalObj.id + " with love: "+ finalObj.love;

		}

		GUI.Button (new Rect (10, 10, 250, 100), ui_Notifi);		
	}
}
