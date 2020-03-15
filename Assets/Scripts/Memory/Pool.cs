namespace BulletsPoolNamespace.Memory
{
	public class Pool<T>
	{
		public interface IPoolObjectFactory
		{
			T CreateObject();
		}

		private IPoolObjectFactory _factory;
		private int _size;
		
		public Pool(IPoolObjectFactory factory, int size)
		{
			_factory = factory;
			_size = size;
		}
		
		
	}
}