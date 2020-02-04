using Mathema.Interfaces;
using Mathema.Models.FlatExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Extensions
{
	public static class ExpressionExtensions
	{
		public static void Substitute(this IExpression thatone, ref IExpression expression, IExpression sub, string key)
		{
			if (expression is FlatAddExpression fae)
			{
				foreach (var kvel in fae.Expressions)
				{
					for (int i = 0; i < kvel.Value.Count; i++)
					{
						var e = kvel.Value[i];
						if (e.DimensionKey.Key == key)
						{
							//kvel.Value[i] = sub.Clone();
							e.Substitute(ref e, sub, key);
							kvel.Value[i] = e;
						}
					}
				}

				fae.UpdateDimensionKey(true);
			}
			else if (expression is FlatMultExpression fme)
			{
				foreach (var kvel in fme.Expressions)
				{
					for (int i = 0; i < kvel.Value.Count; i++)
					{
						var e = kvel.Value[i];
						if (e.DimensionKey.Key == key)
						{
							e.Substitute(ref e, sub, key);
							kvel.Value[i] = e;
						}
					}
				}
			}
			else if (expression is IFunctionExpression fe)
			{
				if (fe.DimensionKey.Key == key)
				{
					Swap(ref expression, sub);
				}
				else
				{
					var a = fe.Argument;
					a.Substitute(ref a, sub, key);
					fe.Argument = a;
				}
			}
			else
			{
				if (expression.DimensionKey.Key == key)
				{
					Swap(ref expression, sub);
				}
			}
		}



		private static void Swap(ref IExpression expression, IExpression sub)
		{
			var tmp = sub.Clone();
			tmp.Count = expression.Count;
			var tmpKey = tmp.DimensionKey.Key;
			tmp.DimensionKey.Value = expression.DimensionKey.Value;
			expression = tmp;
		}
	}
}
