using System;
using System.Collections.Generic;

public class Stack<T>
{
	private List<T> _stack;

	public Stack()
	{
		_stack = new List<T>();
	}

	public void Push(T ob)
	{
		_stack.Add(ob);
	}
	public T Peek()
	{
		return _stack[_stack.Count()-1];
	}
	public void Pop()
	{
		_stack.RemoveAt(_stack.Count()-1);
	}
	public void Clear()
	{
		_stack.Clear();
	}
	public int Count()
	{
		return _stack.Count();
	}
}
