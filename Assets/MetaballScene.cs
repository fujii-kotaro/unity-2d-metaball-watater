using UnityEngine;
using System.Collections.Generic;

public class MetaballScene : MonoBehaviour
{
	[SerializeField] Transform _ballsParent;
	[SerializeField] Rigidbody2D _ballPrefab;
	List<Rigidbody2D> _activeBalls = new List<Rigidbody2D>();
	List<Rigidbody2D> _inactiveBalls = new List<Rigidbody2D>();
	bool _dropWater = true;

	public void Start()
	{
		_ballPrefab.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_dropWater = !_dropWater;
		}

		if (_dropWater)
		{
			Rigidbody2D activeBall = null;
			if (_inactiveBalls.Count > 0)
			{
				activeBall = _inactiveBalls[0];
				_inactiveBalls.RemoveAt(0);
				_activeBalls.Add(activeBall);
			}
			else
			{
				activeBall = Instantiate(_ballPrefab, _ballsParent, false);
				_activeBalls.Add(activeBall);
			}
			activeBall.gameObject.SetActive(true);
			var x = Random.Range(-5, 5);
			activeBall.transform.localPosition = new Vector3(x, 0, 0);
		}

		for (int i = 0; i < _activeBalls.Count; i++)
		{
			var ball = _activeBalls[i];
			if (ball.transform.localPosition.y < -1200)
			{
				ball.gameObject.SetActive(false);
				_activeBalls.RemoveAt(i);
				_inactiveBalls.Add(ball);
			}
		}
	}
}
