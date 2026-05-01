using System;
using System.Collections.Generic;

public class Queue<T>
{
	private List<T> things;
	public int Count { get {return things.Count;}}

	public Queue()
	{
		things = new List<T>();
	}
	public void Enqueue(T thing)
	{
		things.Add(thing);
	}
	public void Dequeue()
	{
		things.RemoveAt(0);
	}
	public T Peek()
	{
		return things[0];
	}
	public void Clear()
	{
		things.Clear();
	}
}
