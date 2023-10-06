namespace CliqueEngine;

public class Server<T> where T : class
{
	static T? _instance = null;
    public static T instance
	{
		get
		{
			if (instance == null)
			{
				throw new NullReferenceException($"No instance of {nameof(T)} has been created.");
			}

			return _instance!;
		}

		private set
		{
			_instance = value;
		}
	}

	public Server(Type serverType)
	{
		if (instance == null)
		{
			instance = (T) Convert.ChangeType(this, serverType);
		}
		else
		{
			throw new Exception($"Only one server of type {GetType().Name} can be instanciated.");
		}
	}

}