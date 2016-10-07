using System;

namespace ContextBound
{
	[AttributeUsage (AttributeTargets.Parameter|AttributeTargets.Method)]
	public class CheckAttribute: Attribute
	{
		private int _maxLength;
		private int _maxValue;
		private bool _nonNull;

		public int MaxLength {
			get {
				return _maxLength;
			}
			set {
				_maxLength = value;
			}
		}


		public int MaxValue 
		{
			get 
			{
				return _maxValue;
			}
			set 
			{
				_maxValue = value;
			}
		}

		public bool NonNull 
		{
			get 
			{
				return _nonNull;
			}
			set 
			{
				_nonNull = value;
			}
		}

		public void DoCheck (Object val) 
		{
			// check for NonNull 
			if (_nonNull && val == null) 
			{
				throw new Exception("This value must not be null");
			}

			// check for MaxLength
			if (_maxLength > 0 && val.ToString().Length > _maxLength) 
			{
				throw new Exception("This value must not be longer than " + _maxLength + " characters");
			}

			// check for MaxValue
			if (_maxValue > 0) 
			{
				if ((double) val > _maxValue) 
				{
					throw new Exception("This value must not be higher than " + _maxValue );
				}

			}
		}
	}
}
