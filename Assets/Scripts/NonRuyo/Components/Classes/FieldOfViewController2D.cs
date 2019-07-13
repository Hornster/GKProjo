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
		/// Zawiera referencje do wykrytych obiektów
		/// </summary>
		public List<GameObject> TrackedObjects
		{
			get;
			private set;
		}

		/// <summary>
		/// Sprawdza, czy dany obiekt jest w polu wykrywania (porównanie referencji)
		/// </summary>
		/// <param name="objectToSearch"></param>
		/// <returns>true jeżeli okiekt w polu, false w przeciwnym wypadku</returns>
		public bool IsDetected(GameObject objectToSearch)
		{
			if (LayerFilter[objectToSearch.layer]) //Dont search if layer is not tracked
			{
				foreach(var gameObject in TrackedObjects)
				{
					if (gameObject == objectToSearch) 
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Zwraca true jeżeli w polu wyrywania znajdują się jakieś obiekty
		/// </summary>
		/// <returns></returns>
		public bool IsDetected()
		{
			if (TrackedObjects.Count > 0) return true;
			else return false;
		}

		/// <summary>
		/// Zawiera listę tagów które nalezy śledzić
		/// </summary>
		private List<string> _tags = new List<string>();

		/// <summary>
		/// Lista z informacjami, które warstwy należy śledzić
		/// </summary>
		public bool[] LayerFilter { get; set; } 

		private void Awake()
		{
			LayerFilter = new bool[32]; //32 for 32 layers
			TrackedObjects = new List<GameObject>();
		}

		/// <summary>
		/// Zapisuje tag do listy tagów do śledzenia
		/// </summary>
		/// <param name="tag"></param>
		public void TrackObjectsWithTag(string tag)
		{
			if(!_tags.Exists(str => str == tag))
			{
				_tags.Add(tag);
			}
		}

		/// <summary>
		/// Usuwa tag z listy tagów do śledzenia
		/// </summary>
		public void UnregisterTag()
		{
			_tags.Remove(tag);
		}

		/// <summary>
		/// Sprawdzenie, czy dany obiekt spełnia warunku do klasyfikacji jako śledzony (tag i warstwa)
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		private bool Filter(GameObject gameObject)
		{
			return (LayerFilter[gameObject.layer]  && _tags.Exists(str => str == gameObject.tag));
		}

		/// <summary>
		/// Zdarzenie wywoływane gdy obiekt wedźie w pole wykrywania
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
		/// Przestaje śledzić obiekt kótry wyszedł poza pole wykrywania
		/// </summary>
		/// <param name="collision"></param>
		public void OnTriggerExit2D(Collider2D collision)
		{
			TrackedObjects.Remove(collision.gameObject);
		}
	}
}
