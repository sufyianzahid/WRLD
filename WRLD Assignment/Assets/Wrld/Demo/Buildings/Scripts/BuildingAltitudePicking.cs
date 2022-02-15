using System.Collections;
using Wrld;
using Wrld.Space;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BuildingAltitudePicking : MonoBehaviour
{
    public static BuildingAltitudePicking instance;
    public static double changeVal = 37.795641;
    public static double changeVal2 = 122.404390;

    [SerializeField]
    private GameObject boxPrefab = null;

    private LatLong cameraLocation = LatLong.FromDegrees(37.795641, -122.404173);
    private LatLong boxLocation1 = LatLong.FromDegrees(37.795199, -122.404390);
    private LatLong boxLocation2 = LatLong.FromDegrees(37.795220, -122.404280);


    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {

        InvokeRepeating(nameof(changeVlu),2f,2f);
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
      //  cameraLocation = LatLong.FromDegrees(changeVal, -122.404173);
        boxLocation1 = LatLong.FromDegrees(changeVal, -changeVal2);
        Api.Instance.CameraApi.MoveTo(boxLocation1, distanceFromInterest: 400, headingDegrees: 0, tiltDegrees: 45);
        //Api.Instance.CameraApi.AnimateTo(cameraLocation,cameraLocation,1f,true);
        while (true)
        {
            yield return new WaitForSeconds(4.0f);

            MakeBox(boxLocation1);
          //  MakeBox(boxLocation2);
        }
    }
    public  bool once;
    private void Update()
    {
        if (once)
        {
            changeVlu();
            Debug.LogError(changeVal);
            once = false;
        }
    }
    public void changeVlu()
        {
        changeVal= Random.Range(37.795641f,37.806641f);
        changeVal2 = Random.Range(122.404390f, 122.415390f);
        /*changeVal += Add;

        changeVal2 += Add;
*/
        StartCoroutine(Example());
    }
    public List<GameObject> enemy = new List<GameObject>(); 
    void MakeBox(LatLong latLong)
    {
        var ray = Api.Instance.SpacesApi.LatLongToVerticallyDownRay(latLong);
        LatLongAltitude buildingIntersectionPoint;
        var didIntersectBuilding = Api.Instance.BuildingsApi.TryFindIntersectionWithBuilding(ray, out buildingIntersectionPoint);
        if (didIntersectBuilding)
        {
            var boxAnchor = Instantiate(boxPrefab) as GameObject;
            enemy.Add(boxAnchor.transform.GetChild(0).transform.GetChild(0).gameObject);

            boxAnchor.GetComponent<GeographicTransform>().SetPosition(buildingIntersectionPoint.GetLatLong());

            var box = boxAnchor.transform.GetChild(0);
            box.localPosition = Vector3.up * (float)buildingIntersectionPoint.GetAltitude();
           // Destroy(boxAnchor, 2.0f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
