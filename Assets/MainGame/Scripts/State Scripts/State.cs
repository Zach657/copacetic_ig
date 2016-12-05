using UnityEngine;
using System.Collections;

public interface State {

	void PlayerSeen(bool isSeen);

	void PerformAction();

	void PlayerClose();

}
