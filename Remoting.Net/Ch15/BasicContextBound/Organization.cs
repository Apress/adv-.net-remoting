using System;

namespace ContextBound
{	
	[Checkable]
	public class Organization: ContextBoundObject
	{
		String _name;
		double _totalDonation;

		public String Name 
		{
			[Check(NonNull=true,MaxLength=30)]
			set 
			{
				_name = value;
			}
			get 
			{
				return _name;
			}
		}

		public void Donate([Check(NonNull=true,MaxValue=100)] double amount) 
		{
			_totalDonation = _totalDonation + amount;
		}
			
	}
}
