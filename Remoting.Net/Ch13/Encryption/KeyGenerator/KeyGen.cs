using System;
using System.IO;
using System.Security.Cryptography;

class KeyGen
{
	static void Main(string[] args)
	{
		if (args.Length != 1 && args.Length != 3) 
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("KeyGenerator <Algorithm> [<KeySize> <Outputfile>]");
			Console.WriteLine("Algorithm can be: DES, TripleDES, RC2 or Rijndael");
			Console.WriteLine();
			Console.WriteLine("When only <Algorithm> is specified, the program");
			Console.WriteLine("will print a list of valid key sizes.");
			return;
		}

		String algorithmname = args[0];

		SymmetricAlgorithm alg = SymmetricAlgorithm.Create(algorithmname);

		if (alg == null) 
		{
			Console.WriteLine("Invalid algorithm specified.");
			return;
		}

		if (args.Length == 1) 
		{
			// just list the possible key sizes
			Console.WriteLine("Legal key sizes for algorithm {0}:",algorithmname);
			foreach (KeySizes size in alg.LegalKeySizes) 
			{
				if (size.SkipSize != 0) 
				{
					for (int i = size.MinSize;i<=size.MaxSize;i=i+size.SkipSize) 
					{
						Console.WriteLine("{0} bit", i);
					}
				} 
				else 
				{
					if (size.MinSize != size.MaxSize) 
					{
						Console.WriteLine("{0} bit", size.MinSize);
						Console.WriteLine("{0} bit", size.MaxSize);
					} 
					else 
					{
						Console.WriteLine("{0} bit", size.MinSize);
					}
					}
			}
			return;
		}

		// user wants to generate a key
		int keylen = Convert.ToInt32(args[1]);
		String outfile = args[2];
		try 
		{
			alg.KeySize = keylen;
			alg.GenerateKey();
			FileStream fs = new FileStream(outfile,FileMode.CreateNew);			
			fs.Write(alg.Key,0,alg.Key.Length);
			fs.Close();
			Console.WriteLine("{0} bit key written to {1}.",
						alg.Key.Length * 8,
						outfile);

		} 
		catch (Exception e) 
		{
			Console.WriteLine("Exception: {0}" ,e.Message);
			return;
		}
		

	}
}
