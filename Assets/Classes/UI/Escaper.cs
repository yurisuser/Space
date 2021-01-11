using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
	public static class Escaper
	{
		public delegate void function();

		private static Stack<function> stack = new Stack<function>();
		public static void LateUpdate()
		{
			if (stack.Count < 1) return;
			if (Input.GetKeyUp("escape"))
			{
				function f = stack.Pop();
				if (f == null) return;
				f();			}
		}

		public static void Add(function function)
		{
			stack.Push(function);
		}
	}
}
