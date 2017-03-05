using UnityEngine;
using System.Collections;

public class AreaMap
{
	//Đơn giản hóa vùng map bởi các vùng con
	int subAreaLength;
	BaseObject[,] subAreas; 
	

	public AreaMap(int areaLength, int subAreaLength)
	{
		this.subAreaLength = subAreaLength;		
		int numberSubArea = areaLength / subAreaLength;		
		subAreas = new BaseObject[numberSubArea, numberSubArea];
	}

	// Trong Unity thì cái trục y nó chọc thẳng đứng lên trên bạn nhé :D Vậy nên cái ta cần là x và trục z của nó thôi
	// Khi thực hiện map object tới map thì cũng đồng thời liên kết nó với các object khác
	public void Add(BaseObject obj)
	{
		int x = (int)(obj.objTrans.position.x / subAreaLength);
		int z = (int)(obj.objTrans.position.z / subAreaLength);
		obj.preObj = null;
		obj.nextObj = subAreas[x, z];
		subAreas[x, z] = obj;		
		if (obj.nextObj != null)
		{
			obj.nextObj.preObj = obj;
		}
	}
	
	// Lấy vùng con mà người nhân viên đang ở, lấy object ở vùng con này 
	// Xác định đối tượng có trong quán hay không
	public BaseObject SetInRestaurent(Staff staff)
	{
		int x = (int)(staff.objTrans.position.x / subAreaLength);
		int y = (int)(staff.objTrans.position.z / subAreaLength);
		BaseObject obj = subAreas[x, y];

		BaseObject inResObj = null;
		while (obj != null)
		{
			float distance = (obj.objTrans.position - staff.objTrans.position).sqrMagnitude;
			if (distance < 1.0f)
			{
				inResObj = obj;
			}					
			obj = obj.nextObj;
		}		
		return inResObj;
	}
	
	
	//Khi mà Object đi đâu đó làm việc gì thì phải cập nhật lại vị trí cho nó. Nếu nó sang subArea mới rồi thì mới cập nhật subArea mới và kết nối obj mới cho nó
	public void GoToNewWhere(BaseObject obj, Vector3 oldPos)
	{
		int oldX = (int)(oldPos.x / subAreaLength);
		int oldZ = (int)(oldPos.z / subAreaLength);
		int curentX = (int)(obj.objTrans.position.x / subAreaLength);
		int curentZ = (int)(obj.objTrans.position.z / subAreaLength);

		if (oldX == curentX && oldZ == curentZ)
		{
			return;
		}

		if (obj.preObj != null)
		{
			obj.preObj.nextObj = obj.nextObj;
		}
		
		if (obj.nextObj != null)
		{
			obj.nextObj.preObj = obj.preObj;
		}

		if (subAreas[oldX, oldZ] == obj)
		{
			subAreas[oldX, oldZ] = obj.nextObj;
		}
		Add(obj);
		MainBoard.idChecking = -1;
	}
}

