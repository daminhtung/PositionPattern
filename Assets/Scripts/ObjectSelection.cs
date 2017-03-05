using UnityEngine;
using System.Collections;

public class ObjectSelection : BaseObject
{
	Vector3 currentTarget;
	// Cái này để sau sẽ tính toán xem là obj này vẫn đang ở ô cũ thì thôi khỏi cần check xem có vào quán hay k. Còn nếu đã chuyển sang ô khác rồi thì mới check
	Vector3 oldPos;
	// Độ lớn của vùng chứa object và quán ăn. Nó được dùng cho mục đích sinh ra các vị trí mà obj sẽ đi tới sao cho không bao giờ đi ra khỏi vùng mà chúng ta đang xét. Việc này cho chúng ta dễ quản lý obj hơn
	float lengthArea;

	AreaMap areaMap;	
	
	//Init enemy
	public ObjectSelection(GameObject obj, float lengthArea, AreaMap areaMap)
	{
		this.objTrans = obj.transform;	
		this.meshRenderer = obj.GetComponent<MeshRenderer>();		
		this.lengthArea = lengthArea;
		
		this.areaMap = areaMap;
		areaMap.Add(this);
		oldPos = objTrans.position;
		GoToNewWhere();
	}

	public override void GoSomewhere()
	{            
		objTrans.Translate(Vector3.forward * Time.deltaTime * 5);
		areaMap.GoToNewWhere(this, oldPos);
		oldPos = objTrans.position;
		if ((objTrans.position - currentTarget).magnitude < 1f)
		{
			GoToNewWhere();
		}
	}
	
	//Đi đâu đó, tới địa điểm nào đó trong Hà Nội chẳng hạn :D 
	void GoToNewWhere()
	{
		currentTarget = new Vector3(Random.Range(0f, lengthArea), 0.5f, Random.Range(0f, lengthArea));
		objTrans.rotation = Quaternion.LookRotation(currentTarget - objTrans.position);
	}
}
