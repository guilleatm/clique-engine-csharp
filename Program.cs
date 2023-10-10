using System.Diagnostics;
using System.Runtime.InteropServices;
using CliqueEngine;

Console.WriteLine("Entry point");

// Engine clique = new Engine();

// clique.Start();

int N = 100_000;


Stopwatch sw = new Stopwatch();
sw.Start();

List<Random> l = new List<Random>();
for (int i = 0; i < N; i++)
{
	l.Add( new Random() );
}


for (int i = 0; i < l.Count; i++)
{
	l[i].Next();
}

sw.Stop();

Console.WriteLine(sw.ElapsedMilliseconds);


try
{

	unsafe
	{

		int data_size = Marshal.SizeOf<TestData>();

		nint memory = Marshal.AllocHGlobal(data_size);

		TestData data = new TestData()
		{
			position = new Vector2f(12, 43),
			size = new Vector2f(88, 51)
		};

		Marshal.StructureToPtr<TestData>(data, memory, false);

		TestData* ptr = (TestData*) memory;

		Test r = new Test(ptr);

		Console.WriteLine(r.data->position);



	}
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}