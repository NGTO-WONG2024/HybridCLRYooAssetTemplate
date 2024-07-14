namespace AllIn1SpringsToolkit
{
	public interface IVectorBool
	{
		public int GetSize();

		bool this[int index]
		{
			get;
			set;
		}
	}
}