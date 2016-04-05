using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class MeshGenerator : MonoBehaviour {

	public int resolution = 5;
	private int currentResolution;

	private Vector3[] vertices;

	public enum FunctionOption {
		Linear,
		Exponential,
		Parabola,
		Sine
	}

	private delegate float FunctionDelegate (float x);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine
	};

	public FunctionOption function;

	void Start () {
		//StartCoroutine(Generate ());
		Generate();
	}

	void Update () {
		if (currentResolution != resolution) {
			Generate ();
		}
		FunctionDelegate funcOption = functionDelegates [(int)function];
		int m = 0;
		for (int x1 = 0;x1 <= resolution; x1++) {
			for(int y = 0;y <= resolution; y++){
				vertices[m].y = funcOption(vertices[m].x);
				m++;
			}
		}
		connectMesh ();
	}

	private Mesh mesh;

	//private IEnumerator Generate(){
	private void Generate(){
		//WaitForSeconds wait = new WaitForSeconds(0.05f);
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		currentResolution = resolution;
		vertices = new Vector3[(resolution + 1) * (resolution + 1)];
		int n = 0;
		float increment = 1f / (resolution - 1);
		for (int x = 0; x <= resolution; x++) {
			for (int z = 0; z <= resolution; z++) {
				vertices [n] = new Vector3 (x*increment, 0, z*increment);
				n++;
				//yield return wait;
			}
		}
		connectMesh();
	}

	//since meshes only display one side depending on vertex indices, must go over 
	//grid twice. once in a cw direction and again in ccw direction
	private void connectMesh(){
		mesh.vertices = vertices;
		int[] triangles = new int[resolution * resolution * 6];
		for (int ti = 0, vi = 0, z = 0; z < resolution; z++, vi++) {
			for (int x = 0; x < resolution; x++, ti += 6, vi++) {
				triangles [ti] = vi;
				triangles [ti + 3] = triangles [ti + 2] = vi + 1;
				triangles [ti + 4] = triangles [ti + 1] = vi + resolution + 1;
				triangles [ti + 5] = vi + resolution + 2;
			}
		}
		mesh.triangles = triangles;
	}

	private void OnDrawGizmos(){
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere (vertices [i], 0.01f);
		}
	}

	private static float Linear (float x) {
		return x;
	}

	private static float Exponential (float x) {
		return x * x;
	}

	private static float Parabola (float x){
		x = 2f * x - 1f;
		return x * x;
	}

	private static float Sine (float x){
		return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
	}
}

