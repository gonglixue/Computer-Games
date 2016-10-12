using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = this.GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //获取击中的对象
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                // 判断是否包含ReactiveTarget组件，如果包含则说明该物体是个可击中的物体
                if (target != null)
                {
                    Debug.Log("target hit");
                    target.ReactToHit();  //调用该组件上的方法
                }
                else
                {
                    //运行协程来响应击中
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {  //协程使用IEnumerator方法
        GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere.transform.position = pos;

        yield return new WaitForSeconds(1);  //yeild关键字告诉协程在何处暂停

        Destroy(Sphere);
    }
    void OnGUI()
    {
        int size = 12;
        //Debug.Log(Screen.width);
        float posX = _camera.pixelWidth / 2.0f - size/4;
        float posY = _camera.pixelHeight / 2.0f - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}

