using System;
using System.Collections.Generic;
using System.Text;

namespace Codemash
{
    public class WeatherModel : BaseModel
    {
		private string _condition;

		public string Condition
		{
			get { return _condition; }
			set { _condition = value; OnPropertyChanged(nameof(Condition)); }
		}

		private string _location;

		public string Location
		{
			get { return _location; }
			set { _location = value; OnPropertyChanged(nameof(Location)); }
		}

		private double _temp;

		public double Temp
		{
			get { return _temp; }
			set { _temp = value; OnPropertyChanged(nameof(Temp)); }
		}

	}
}
