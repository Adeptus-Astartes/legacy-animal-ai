
using System.Collections;
public static class NavMeshExtend
{
//	public float range = 10.0f;
//	bool RandomPoint(Vector3 center, float range, out Vector3 result) {
//		for (int i = 0; i < 30; i++) {
//			Vector3 randomPoint = center + Random.insideUnitSphere * range;
//			NavMeshHit hit;
//			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
//			{
//				result = hit.position;
//				print (hit.mask);
//				return true;
//			}
//		}
//		result = Vector3.zero;
//		return false;
//	}
//	void Update() {
//		Vector3 point;
//		if (RandomPoint(transform.position, range, out point)) {
//			Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
//		}
//	}

	public static int GetNavMeshAreaIndex(this UnityEngine.AI.NavMeshAgent agent)
	{
		int layer = -1;
		UnityEngine.AI.NavMeshHit hit;
		if (UnityEngine.AI.NavMesh.SamplePosition(agent.transform.position, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas)) 
		{
			layer = hit.mask;
		}
		return layer;
	}
}
