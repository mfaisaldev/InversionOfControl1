using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionOfControlApp
{
	class Program
	{
		static void Main(string[] args)
		{
			/* This is a small application which will explain Inversion of control
			 * Inversion of control means we remove dependencies and higher level module will no longer be dependent on low level module.
			*/

			/* here at the time of instansiation we will decide which class do we need? If we need VISA, we can pass VISA here, if we need MASTER
			 * we will pass MASTER class here using constructor */
			Transaction1 trans = new Transaction1(new VisaCardEntry());
			trans.Add(1234);

			/* here we will use same class for MASTER card option. */
			trans = new Transaction1(new MasterCardEntry());
			trans.Add(1234);

			/* Now if we want to add more card type like American Express or other, we will simply create these 
			 * classes and inherit that interface and that is */
			Console.ReadKey();
		}
	}

	/* this is a transaction class which will validate the card and then will give that card to another class to add. 
	 * this class is built using Inversion of control feature. This class is not longer tightly couple with low level module.
	 * using abstraction we will change the behaviour of class.
	 * For abstraction, we have used an Interface */
	class Transaction1
	{
		public bool IsValid { get; set; }
		private int Cardnumber { get; set; }
		/* this ICard is an interface which will be used to avoid dependencies at this level
		 */
		ICard _iCard;
		public Transaction1(ICard icard)
		{
			/* at the time we will instansiate this class, we will simply pass the required dependency via constructor using abstraction. */
			_iCard = icard;
		}

		public bool IsValidCardNumber(int VisaCardNumber)
		{
			IsValid = true;
			return IsValid;
		}

		public void Add(int VisaCardNumber)
		{
			if (IsValidCardNumber(VisaCardNumber))
			{			
				_iCard.AddCard(1);
			}
		}
	}
	/* This is an interface which will be inheretied by classes to make sure the method define here is implemented */
	interface ICard
	{
		void AddCard(int CardNumber);
	}

	/* this is another transaction class which will also validate the card and then will give that card to another class to add. 
	 * this class is not implementing Inversion of control feature. This class is tightly couple with low level module.*/
	class Transaction2
	{
		public bool IsValid { get; set; }
		private int Cardnumber { get; set; }
		/* VisaCardEntry is a class which is this class thightly couple with */
		VisaCardEntry vCard = new VisaCardEntry();
	
		public bool IsValidCardNumber(int VisaCardNumber)
		{
			IsValid = true;
			return IsValid;
		}

		public void Add(int VisaCardNumber)
		{
			if (IsValidCardNumber(VisaCardNumber))
			{
				vCard.AddCard(1);
			}
		}
		/* this class will also add the Visa Card, but what if we wanted to add Master card? 
		 * If we want this class to add MASTER card then we will add more code to every level of the application. 
		 * This is a problem, for example we will create an instance of Master card class and then we will add it.
		 * this is called tightly couple classes which are dependent on eachother */
	}

	/* this is a class to add VISA card */
	class VisaCardEntry : ICard
	{
		public void AddCard(int CardNumber)
		{
			Console.WriteLine("Visa Card Added!");
		}		
	}
	/* this is a class to add MASTER card */
	class MasterCardEntry : ICard
	{
		public void AddCard(int CardNumber)
		{
			Console.WriteLine("Master Card Added!");
		}
	}
}
