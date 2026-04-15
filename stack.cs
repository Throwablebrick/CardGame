using System;
using System.Collections.Generic;

public class Stack<T>
{
	private List<T> _list;

	public Stack()
	{
		_list = new List<T>();
	}

	public void Push(T ob)
	{
		_list.Add(ob);
	}
	public T Peek()
	{
		return _list[_list.Count()-1];
	}
	public void Pop()
	{
		_list.RemoveAt(_list.Count()-1);
	}
	public void Clear()
	{
		_list.Clear();
	}
	public int Count()
	{
		return _list.Count();
	}
}
