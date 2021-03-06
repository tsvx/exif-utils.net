﻿using System;

namespace XmpUtils.Xmp
{
	public enum XmpQuantity
	{
		/// <summary>
		/// A single value
		/// </summary>
		Single,

		/// <summary>
		/// An unordered array
		/// </summary>
		Bag,

		/// <summary>
		/// An ordered array
		/// </summary>
		Seq,

		/// <summary>
		/// A set of one or more values, one of which should be chosen
		/// </summary>
		Alt
	}
}
