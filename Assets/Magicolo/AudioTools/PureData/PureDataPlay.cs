using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.AudioTools {
	[AddComponentMenu("Magicolo/Pure Data/Play")]
	public class PureDataPlay : MonoBehaviour {
								
		public void Play(string Alarm) {
			PureData.Play(Alarm);
		}
									
		public void PlayContainer(string Patches) {
			PureData.PlayContainer(Patches);
		}
	}
}