using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
	public static class SerialUtil
	{
		public static double[] Decode(byte[] sentByteArray)
		{

			double[] returnArray = new double[29];

			int returnArrayIndex = 0;

			const int SIZE = 75;

			uint calculatedCheckSum = 0;

			//checksum számolás
			for (int j = 2; j < SIZE - 2; j++)
			{
				calculatedCheckSum += sentByteArray[j];
			}


			uint sentCheckSum = 0;
			sentCheckSum = sentCheckSum | (uint)sentByteArray[SIZE - 2] << 8;
			sentCheckSum = sentCheckSum | (uint)sentByteArray[SIZE - 1];

			Console.WriteLine("ch :" + sentCheckSum);
			Console.WriteLine("sum:" + calculatedCheckSum);

			if (sentByteArray[0] == 'U' && sentByteArray[1] == 'U' && sentByteArray[2] == 'T' && sentCheckSum == calculatedCheckSum)
			{

				int firstBytePosition = 3;
				///////////0 u//1 u//2 t//3 ido1//4 ido2//5 ido3//6 ido4

				uint ido = (uint)sentByteArray[firstBytePosition] << 24;
				ido = ido | (uint)sentByteArray[firstBytePosition + 1] << 16;
				ido = ido | (uint)sentByteArray[firstBytePosition + 2] << 8;
				ido = ido | (uint)sentByteArray[firstBytePosition + 3];

				double idod = Convert.ToDouble(ido) / 10000;
				returnArray[returnArrayIndex++] = returnArray[1] = idod;

				Console.WriteLine(idod);


				uint temp = 0;
				double tempd = 0;


				//////////////////////////////////////7,8 magasság parancs
				firstBytePosition = 7;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 10000);
				returnArray[returnArrayIndex++] = tempd;
				Console.WriteLine("mag: " + tempd);

				//////////////////////////////////////9,10 sebesség parancs
				firstBytePosition = 9;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 80);
				returnArray[returnArrayIndex++] = tempd;
				Console.WriteLine("seb: " + tempd);

				//////////////////////////////////////11,12
				firstBytePosition = 11;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 500) - 250;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////13,14
				firstBytePosition = 13;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 500) - 250;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////15,16
				firstBytePosition = 15;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 500) - 250;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////17,18 nyomás alapú magasság
				firstBytePosition = 17;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 8200) - 200;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////19,20
				firstBytePosition = 19;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 80);
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////21,22 euler szögek
				firstBytePosition = 21;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 360) - 180;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////23,24
				firstBytePosition = 23;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 360) - 180;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////25,26
				firstBytePosition = 25;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 360) - 180;
				returnArray[returnArrayIndex++] = tempd;

				//////////////////////////////////////27,28 normalizált kormánykitérítések
				firstBytePosition = 27;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue);
				returnArray[returnArrayIndex++] = tempd;


				////////////////////////////////////////29,30
				firstBytePosition = 29;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue);
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////31,32
				firstBytePosition = 31;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue);
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////33,34 normalizált gázkarállás
				firstBytePosition = 33;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue);
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////35,36 GPS egészség
				firstBytePosition = 35;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				returnArray[returnArrayIndex++] = tempd;


				////////////////////////////////////////37,38,39,40 smart_gps->POSLLH.lon
				firstBytePosition = 37;
				uint lon = 0;
				lon = lon | (uint)sentByteArray[firstBytePosition] << 24;
				lon = lon | (uint)sentByteArray[firstBytePosition + 1] << 16;
				lon = lon | (uint)sentByteArray[firstBytePosition + 2] << 8;
				lon = lon | (uint)sentByteArray[firstBytePosition + 3];

				double lond = Convert.ToDouble(lon);
				lond = lond / (Int32.MaxValue / 180) - 90;
				returnArray[returnArrayIndex++] = lond;
				Console.WriteLine("lon: " + lond);


				////////////////////////////////////////41,42,43,44 smart_gps->POSLLH.lat
				firstBytePosition = 41;
				uint lat = 0;
				lat = lat | (uint)sentByteArray[firstBytePosition] << 24;
				lat = lat | (uint)sentByteArray[firstBytePosition + 1] << 16;
				lat = lat | (uint)sentByteArray[firstBytePosition + 2] << 8;
				lat = lat | (uint)sentByteArray[firstBytePosition + 3];

				double latd = Convert.ToDouble(lat);
				latd = latd / (Int32.MaxValue / 360) - 180;
				returnArray[returnArrayIndex++] = latd;
				Console.WriteLine("lat: " + latd);

				////////////////////////////////////////45,46,47,48 smart_gps->POSLLH.height
				firstBytePosition = 45;
				uint height = 0;
				height = height | (uint)sentByteArray[firstBytePosition] << 24;
				height = height | (uint)sentByteArray[firstBytePosition + 1] << 16;
				height = height | (uint)sentByteArray[firstBytePosition + 2] << 8;
				height = height | (uint)sentByteArray[firstBytePosition + 3];

				double heightd = Convert.ToDouble(height);
				heightd = heightd / (Int32.MaxValue / 10100) - 100;
				returnArray[returnArrayIndex++] = heightd;
				Console.WriteLine("height: " + heightd);

				////////////////////////////////////////49,50 gyorsulás
				firstBytePosition = 49;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 5) - 2.5;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////51,52 gyorsulás
				firstBytePosition = 49;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 5) - 2.5;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////53,54 gyorsulás
				firstBytePosition = 53;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 5) - 2.5;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////55,56 
				firstBytePosition = 55;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 4) - 2;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////57,58
				firstBytePosition = 57;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 4) - 2;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////59,60
				firstBytePosition = 59;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 4) - 2;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////59,60
				firstBytePosition = 59;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				tempd = tempd / (UInt16.MaxValue / 4) - 2;
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////61,62 flightmode
				firstBytePosition = 61;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////63 köv pont
				firstBytePosition = 63;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				returnArray[returnArrayIndex++] = tempd;

				////////////////////////////////////////71 EKF
				firstBytePosition = 71;
				temp = 0;
				temp = temp | (uint)sentByteArray[firstBytePosition] << 8;
				temp = temp | (uint)sentByteArray[firstBytePosition + 1];

				tempd = Convert.ToDouble(temp);
				returnArray[returnArrayIndex++] = tempd;
			}
			return returnArray;
		}


		public static byte[] Code(double[] doubleArray, int n)
		{
			const int SIZE = 80;

			byte[] returnArray = new byte[SIZE];

			for (int i = 0; i < SIZE; i++)
			{
				returnArray[i] =0x01;
			}

			int returnArrayIndex = 0;

			UInt16 calculatedCheckSum = 0;
			returnArray[0] = (byte)'G';
			returnArray[1] = (byte)'P';
			returnArray[2] = (byte)'S';
			returnArray[3] = (byte) n;

			for (int i = 1; i < n+1; i++)
			{

				double a = doubleArray[i];

				a = a * (Int32.MaxValue / 360) + 180;

				uint lon =(uint) Convert.ToUInt32(a);				

				
				returnArray[i*4] = (byte)(lon >> 24);
				returnArray[i*4+1] = (byte)(lon >> 16);
				returnArray[i*4+2] = (byte)(lon >> 8) ;
				returnArray[i*4+3] = (byte)(lon >> 0);

		}

			//checksum számolás
			for (int j = 0; j < SIZE - 2; j++)
			{
				calculatedCheckSum += returnArray[j];
			}
			returnArray[SIZE - 2] = (byte)(calculatedCheckSum >> 8);
			returnArray[SIZE - 1] = (byte)(calculatedCheckSum >> 0);

			return returnArray;
		}
	}
}
