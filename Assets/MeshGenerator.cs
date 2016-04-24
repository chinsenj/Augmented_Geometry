using UnityEngine;
using System.Collections;

//want to display the mesh
//[ExecuteInEditMode]

//consider switching to double instead of float for resolution
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class MeshGenerator : MonoBehaviour {
	[Range(10,100)]
	public int resolution = 50;
	public bool axesDisplayed = false;
	public GameObject xAxis;
	public GameObject yAxis;
	public GameObject zAxis;
	private int currentResolution;

	private Vector3[] vertices;
	private Color[] colors;
	public enum FunctionOption {
		Linear,
		Exponential,
		Parabola,
		Sine,
		Ripple
	}

	private delegate float FunctionDelegate (float x, float z, float t);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine,
		Ripple
	};

	public FunctionOption function;

	void Start () {
		Generate();
	}

	void Update () {
		if (currentResolution != resolution) {
			Generate ();
		}
		FunctionDelegate funcOption = functionDelegates [(int)function];
		float t = Time.timeSinceLevelLoad;
		int m = 0;
		for (int x1 = 0;x1 <= resolution; x1++) {
			for(int y = 0;y <= resolution; y++){
				vertices[m].y = funcOption(vertices[m].x,vertices[m].z, t)+0.5f;
				colors [m] = new Color (Mathf.Abs(1f*vertices[m].x),Mathf.Abs(1f*vertices[m].z), Mathf.Abs(1f*vertices[m].y), 0f);
				m++;
			}
		}

		if (axesDisplayed) {
			xAxis.SetActive (true);
			yAxis.SetActive (true);
			zAxis.SetActive (true);

		}
		else {
			xAxis.SetActive (false);
			yAxis.SetActive (false);
			zAxis.SetActive (false);
		}

		connectMesh ();
	}

	private Mesh mesh;

	private void Generate(){
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "3D Mesh";

		//ensure resolution is even for axis placement
		resolution = resolution * 2;
		currentResolution = resolution;
		vertices = new Vector3[(resolution + 1) * (resolution + 1)];
		colors = new Color[(resolution + 1) * (resolution + 1)];
		int n = 0;
		float increment = 1f / (resolution - 1);
		for (int x = 0; x <= resolution; x++) {
			for (int z = 0; z <= resolution; z++) {
				vertices [n] = new Vector3 (x*increment-0.5f, 0, z*increment-0.5f);
				n++;
			}
		}
		mesh.MarkDynamic();
		connectMesh();


	}

	//since meshes only display one side depending on vertex indices, must go over 
	//grid twice. once in a cw direction and again in ccw direction
	//note - don't forget original orientation for cw coordinates
	private void connectMesh(){
		mesh.vertices = vertices;
		mesh.colors = colors;
		int[] triangles = new int[resolution * resolution * 6 * 2];
		int ccwIndex = resolution * resolution * 6;

		for (int ti = 0, vi = 0, z = 0; z < resolution; z++, vi++) {
			for (int x = 0; x < resolution; x++, ti += 6, vi++, ccwIndex += 6) {
				triangles [ti] = vi;
				triangles [ti + 3] = triangles [ti + 2] = vi + 1;
				triangles [ti + 4] = triangles [ti + 1] = vi + resolution + 1;
				triangles [ti + 5] = vi + resolution + 2;
				
				triangles [ccwIndex] = vi;
				triangles [ccwIndex + 1] = triangles [ccwIndex + 3] = vi + 1;
				triangles [ccwIndex + 2] = triangles [ccwIndex + 5] = vi + resolution + 1;
				triangles [ccwIndex + 4] = vi + resolution + 2;

			}
		}

		LineRenderer xLine = xAxis.GetComponent<LineRenderer> ();
		Vector3[] xPoints = new Vector3[2];
		xPoints[0] = new Vector3(-1f, 0.5f, 0f);
		xPoints[1] = new Vector3(1f, 0.5f, 0f);
		xLine.SetPositions (xPoints);

		LineRenderer yLine = yAxis.GetComponent<LineRenderer> ();
		Vector3[] yPoints = new Vector3[2];
		yPoints[0] = new Vector3(0f, 0.5f, 0f);
		yPoints[1] = new Vector3(0f, 1.5f, 0f);
		yLine.SetPositions (yPoints);

		LineRenderer zLine = zAxis.GetComponent<LineRenderer> ();
		Vector3[] zPoints = new Vector3[2];
		zPoints[0] = new Vector3(0f, 0.5f, -1f);
		zPoints[1] = new Vector3(0f, 0.5f, 1f);
		zLine.SetPositions (zPoints);
		mesh.triangles = triangles;
	}

	private static float Linear (float x,float z, float t) {
		return x;
	}

	private static float Exponential (float x, float z, float t) {
		return x * x;
	}

	private static float Parabola (float x, float z, float t){
		x += x - 1f;
		z += z - 1f;
		return 1f - x * x * z * z;
	}

	private static float Sine (float x, float z, float t){
		return 0.50f +
			0.25f * Mathf.Sin(4f * Mathf.PI * x + 4f * t) * Mathf.Sin(2f * Mathf.PI * z + t) +
			0.10f * Mathf.Cos(3f * Mathf.PI * x + 5f * t) * Mathf.Cos(5f * Mathf.PI * z + 3f * t) +
			0.15f * Mathf.Sin(Mathf.PI * x + 0.6f * t);
	}

	private static float Ripple (float x, float z, float t){
		x -= 0.5f;
		z -= 0.5f;
		float squareRadius = x * x + z * z;
		return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius);
	}

}

