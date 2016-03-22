/*
 This C# class gives simple extension access to checking if an Renderer is rendered by a specific Camera.
 SOURCE: Unity Community Wiki
 http://wiki.unity3d.com/index.php?title=IsVisibleFrom
*/

using UnityEngine;

public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}