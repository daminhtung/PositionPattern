using UnityEngine;
using System.Collections;

public class BaseObject
{
	public int id = -1;
	//Thay đổi Mesh cho đối tượng. Nếu đối tượng nào không đạt tiêu chuẩn sẽ đổi thành màu đen luôn
	public MeshRenderer meshRenderer;
	public virtual void GoSomewhere(){}
	public Transform objTrans;

	// Việc lưu trữ các object theo kiểu liên kết với các object thế này sẽ giúp chúng ta tránh được việc lưu trữ chúng trong mảng-> tránh được việc phải duyệt nhiều for
	public BaseObject preObj;
	public BaseObject nextObj;

	//Cái này để cho các các đối tượng và nhân viên có thể di chuyển được
	public virtual void GoSomewhere(BaseObject obj)
	{ }

	public int love = 365; // điểm tình yêu trung bình của mỗi obj :D 

	// Chấm điểm
	public void Mark(bool isAdd)
	{
		love = isAdd == true ? love+1 : love-1;
	}
}