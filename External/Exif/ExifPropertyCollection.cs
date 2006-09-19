using System;
using System.Collections;
using System.Collections.Generic;

namespace PhotoLib.Model.Exif
{
	[Serializable]
	public class ExifPropertyCollection : System.Collections.Generic.ICollection<ExifProperty>, System.Collections.ICollection
	{
		#region Fields

		private SortedDictionary<Int32, ExifProperty> items = new SortedDictionary<int, ExifProperty>();

		#endregion Fields

		#region Properties

		public ExifProperty this[ExifTag tagID]
		{
			get
			{
				if (!this.items.ContainsKey((int)tagID))
				{
					ExifProperty property = new ExifProperty();
					property.Tag = tagID;
					property.Type = ExifDataTypeAttribute.GetExifType(tagID);
					this.items[(int)tagID] = property;
				}
				return this.items[(int)tagID];
			}
			set { this.items[(int)tagID] = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		/// <remarks>
		/// Warning: inefficient, used only for serialization
		/// </remarks>
		public ExifProperty this[int index]
		{
			get
			{
				int[] keys = new int[this.items.Keys.Count];
				this.items.Keys.CopyTo(keys, 0);
				return this.items[keys[index]];
			}
			set { throw new NotSupportedException("This operation is not supported."); }
		}

		#endregion Properties

		#region Methods

		public bool Remove(ExifTag tag)
		{
			if (!this.items.ContainsKey((int)tag))
				return false;

			this.items.Remove((int)tag);
			return true;
		}

		public bool Contains(ExifTag tag)
		{
			return this.items.ContainsKey((int)tag);
		}

		#endregion Methods

		#region ICollection Members

		public void CopyTo(Array array, int index)
		{
			((ICollection)this.items).CopyTo(array, index);
		}

		public int Count
		{
			get { return ((ICollection)this.items).Count; }
		}

		bool ICollection.IsSynchronized
		{
			get { return ((ICollection)this.items).IsSynchronized; }
		}

		object ICollection.SyncRoot
		{
			get { return ((ICollection)this.items).SyncRoot; }
		}

		#endregion ICollection Members

		#region IEnumerable Members

		System.Collections.IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.items.Values).GetEnumerator();
		}

		#endregion IEnumerable Members

		#region ICollection<ExifProperty> Members

		public void Add(ExifProperty item)
		{
			if (item == null)
				return;

			if (item.Value == null)
			{
				if (this.Contains(item.Tag))
					this.Remove(item.Tag);
				return;
			}

			this.items[item.ID] = item;
		}

		public void Clear()
		{
			this.items.Clear();
		}

		public bool Contains(ExifProperty item)
		{
			return this.items.ContainsValue(item);
		}

		public void CopyTo(ExifProperty[] array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		public bool IsReadOnly
		{
			get { return ((ICollection<KeyValuePair<int,ExifProperty>>)this.items).IsReadOnly; }
		}

		public bool Remove(ExifProperty item)
		{
			if (!this.items.ContainsKey(item.ID))
				return false;

			this.items.Remove(item.ID);
			return true;
		}

		#endregion ICollection<ExifProperty> Members

		#region IEnumerable<ExifProperty> Members

		public IEnumerator<ExifProperty> GetEnumerator()
		{
			return ((IEnumerable<ExifProperty>)this.items.Values).GetEnumerator();
		}

		#endregion IEnumerable<ExifProperty> Members
	}
}