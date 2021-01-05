using UnityEngine;

public class WaypointTracePresenter : MonoBehaviour
{
	public Color traceColor = Color.white;
	
	
	private void OnDrawGizmos()
	{
		Gizmos.color = traceColor;
		Transform[] waypoints = new Transform[transform.childCount];

		waypoints[0] = transform.GetChild(0);		

		for (int i = 1; i < waypoints.Length; i++)
		{
			waypoints[i] = transform.GetChild(i);
			Gizmos.DrawLine(waypoints[i-1].position, waypoints[i].position);
		}
	}
}
