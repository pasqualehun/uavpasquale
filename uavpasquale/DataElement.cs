using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
	public class DataElement
	{
		private String name;

		public double[] fromA;
		public double[] fromB;

		public int faultA;
		public int faultB;

		private double epsilon1;
		private double epsilon2;

		bool flagA = false;
		bool flagB = false;


        int countFlagA = 0;
        int countFlagB = 0;

		private int numberOfSamples;


		public DataElement(String name)
		{
			numberOfSamples = 10;
			fromA = new double[numberOfSamples];
			fromB = new double[numberOfSamples];

			this.name = name;

			epsilon1 = 0.1;
			epsilon2 = 0.1;
		}

		public void Calculate()
		{
            if (flagA == false)
            {
                countFlagA++;
            }
            else
            {
                countFlagA = 0;
            }
            if (flagB == false)
            {
                countFlagB++;
            }
            else
            {
                countFlagB = 0;
            }

            if (flagA && flagB)
            {
                double tempa = 0;
                double tempb = 0;
                for (int i = 0; i < numberOfSamples; i++)
                {
                    tempa += fromA[i];
                    tempb += fromB[i];
                }

                double a = Math.Abs((tempa - fromA[numberOfSamples - 1]) / (numberOfSamples - 1) - fromA[numberOfSamples - 1]);
                double b = tempa * epsilon1;

                double c = Math.Abs((tempa - fromB[numberOfSamples - 1]) / (numberOfSamples - 1) - fromB[numberOfSamples - 1]);

                double d = Math.Abs(fromA[0] - fromA[numberOfSamples - 1]);


                //nagy ugrás
                if (Math.Abs((tempa - fromA[numberOfSamples - 1]) / numberOfSamples - fromA[numberOfSamples - 1]) > Math.Abs(tempa * epsilon1))
                {
                    faultA = faultA + 1;
                }

                if (Math.Abs((tempa - fromB[numberOfSamples - 1]) / numberOfSamples - fromB[numberOfSamples - 1]) > Math.Abs(tempa * epsilon1))
                {
                    faultB = faultB + 1;
                }

                tempa /= numberOfSamples;
                tempb /= numberOfSamples;


                if ((Math.Abs(tempa - tempb) < Math.Abs(GetData() * epsilon2)) && (faultA > 0) && (faultB > 0))
                {
                    faultA = faultA - 2;
                    faultB = faultB - 2;
                }

                if ((Math.Abs(tempa - tempb) > Math.Abs(GetData() * epsilon2)))
                {
                    faultA = faultA + 1;
                    faultB = faultB + 1;
                }

                flagA = flagB = false;
            }
            else
            {
                //beragadás
                if (Math.Abs(fromA[0] - fromA[numberOfSamples - 1]) < 0.0001 || countFlagA >numberOfSamples )// && fromA[numberOfSamples - 1] != 0)
                {
                    faultA = faultA + 15;
                    if (faultB > 0)
                    {
                        faultB = faultB - 10;
                    }
                }

                if (Math.Abs(fromB[0] - fromB[numberOfSamples - 1]) < 0.0001 || countFlagB > numberOfSamples)// && fromB[numberOfSamples - 1] != 0)
                {
                    faultB = faultB + 15;
                    if (faultA > 0)
                    {
                        faultA = faultA - 10;
                    }
                }
            }
		}

		public double GetData()
		{
            if (faultA < faultB)
            {
                if (faultA > 0)
                {
                    faultA--;
                }
                return fromA[numberOfSamples - 1];
            }
            else
            {
                if (faultB > 0)
                {
                    faultB--;
                }
                return fromB[numberOfSamples - 1];
            }
		}

		public string GetName()
		{
			return name;
		}

		public int ErrorCode()
		{
			if (faultA > faultB && faultA > 15)
				return 1;

			if (faultA < faultB && faultB > 15)
				return 2;

			else
				return 0;
		}

		public void SetEpsilon(double e1, double e2)
		{
			epsilon1 = e1;
			epsilon2 = e2;
		}

		public void AddA(double a)
		{
			flagA = true;

			int i = 0;
			for (; i < numberOfSamples-1; i++)
			{
				fromA[i] = fromA[i+1]; 
			}
			fromA[i] = a;
		}

		public void AddB(double b)
		{
			flagB = true;

			int i = 0;
			for (; i < numberOfSamples - 1; i++)
			{
				fromB[i] = fromB[i + 1];
			}
			fromB[i] = b;
		}

        public double getDelta()
        {
            if (faultA < faultB)
            {
                return fromA[numberOfSamples - 1] - fromA[numberOfSamples - 2];
            }
            else
            {
                return fromB[numberOfSamples - 1] - fromB[numberOfSamples - 2];
            }
        }
	}
}
