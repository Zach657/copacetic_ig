using UnityEngine;
using System.Collections;
/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public interface State {

	void PlayerSeen(bool isSeen);

	void PerformAction();

}
