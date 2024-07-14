namespace AllIn1SpringsToolkit
{
	public interface IVector
	{
		public int GetSize();

		float this[int index]
		{
			get;
			set;
		}
	}
}