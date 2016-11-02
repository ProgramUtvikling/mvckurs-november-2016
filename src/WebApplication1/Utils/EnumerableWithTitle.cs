using ImdbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace WebApplication1.Utils
{
	public class EnumerableWithTitle<TItems> : IEnumerable<TItems>
	{
		public string Title { get; private set; }
		private readonly IEnumerable<TItems> _items;

		public EnumerableWithTitle(IEnumerable<TItems> items, string title)
		{
			_items = items;
			Title = title;
		}

		public IEnumerator<TItems> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public static class EnumerableWithTitleExtensions
	{
		public static EnumerableWithTitle<T> WithTitle<T>(this IEnumerable<T> items, string title)
		{
			return new EnumerableWithTitle<T>(items, title);
		}
	}
}
