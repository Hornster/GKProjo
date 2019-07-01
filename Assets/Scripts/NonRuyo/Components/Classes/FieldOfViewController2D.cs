using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components
{
	/// <summary>
	/// Klasa która ma z założenia odpowiadać za wykrywanie obiektów
	/// przez komputerwo sterowane jednostki. Pole wykrywania zazeży od 
	/// komponentu collidera. Wyrkrywa obiekty na wskazanych warstwach ze wskazanym tagiem.
	/// </summary>
	class FieldOfViewController2D : MonoBehaviour
	{
		/// <summary>
		/// Stores references to obejcts in detection zone
		/// </summary>
		public List<GameObject> TrackedObjects
		{
			get;
			private set;
		}

		/// <summary>
		/// Checks whether specified object is in detection range
		/// </summary>
		/// <param name="objectToSearch"></param>
		/// <returns>True if object found, false otherwise</returns>
		public bool IsDetected(GameObject objectToSearch)
		{
			if (LayerFilter[objectToSearch.layer]) //Dont search if layer is not tracked
			{
				foreach(var gameObject in TrackedObjects)
				{
					if (gameObject == objectToSearch) //TODO poprawić
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool IsDetected()
		{
			if (TrackedObjects.Count > 0) return true;
			else return false;
		}

		/// <summary>
		/// Tags of objects to track
		/// </summary>
		private List<string> _tags = new List<string>();

		/// <summary>
		/// Layers on which objects will be tracked
		/// </summary>
		public bool[] LayerFilter { get; set; } 

		private void Awake()
		{
			LayerFilter = new bool[32]; //32 for 32 layers
			TrackedObjects = new List<GameObject>();
		}

		/// <summary>
		/// Will track objects with given tag that are on available layers 
		/// </summary>
		/// <param name="tag">Tag of objects that will be tracked </param>
		public void TrackObjectsWithTag(string tag)
		{
			if(!_tags.Exists(str => str == tag))
			{
				_tags.Add(tag);
			}
		}

		/// <summary>
		/// Stops tracking objects with this tag
		/// </summary>
		public void UnregisterTag()
		{
			//TODO usuwanie z listy tych które tam już są
			_tags.Remove(tag);
		}

		/// <summary>
		/// If provided object should be tracked, return true
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		private bool Filter(GameObject gameObject)
		{
			return (LayerFilter[gameObject.layer]  && _tags.Exists(str => str == gameObject.tag));
		}

		/// <summary>
		/// Happens when seeked objects enetrs FOV collider. They are registered and followed since then
		/// </summary>
		/// <param name="collision"></param>
		public void OnTriggerEnter2D(Collider2D collision)
		{
			if (Filter(collision.gameObject) && !TrackedObjects.Exists(obj => obj == collision.gameObject))
			{
				TrackedObjects.Add(collision.gameObject);
			}
		}

		/// <summary>
		/// Originally this method updates objects' positions, but since references are used to teack them this is no longer neccesary
		/// </summary>
		/// <param name="collision"></param>
		/*public void OnTriggerStay2D(Collider2D collision)
		{
			
		}*/

		/// <summary>
		/// Stops tracking object that leaves detection zone
		/// </summary>
		/// <param name="collision"></param>
		public void OnTriggerExit2D(Collider2D collision)
		{
			TrackedObjects.Remove(collision.gameObject);
		}
	}
}
