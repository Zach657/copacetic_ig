using UnityEngine;
using System.Collections;
/**
 * @author Nathan Pool
 */
public interface State {

	void PlayerSeen(bool isSeen);

	void PerformAction();

	void PlayerClose();

}
