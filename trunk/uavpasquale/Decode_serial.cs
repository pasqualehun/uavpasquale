using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
	public class Decode_serial
	{
		public double[] Decode(byte[] array)
		{

			double[] returnArray = new double[77];
			int i = 0;


			uint checkFor = 0;

			//checksum számolás
			for (int j = 2; j < 156; j++)
			{
				checkFor += array[j];
			}


			uint chsum = 0;
			chsum = chsum | (uint)array[i + 157] << 8;
			chsum = chsum | (uint)array[i + 158];

			Console.WriteLine("ch :" + chsum);
			Console.WriteLine("sum:" + checkFor);


			// for (int i = 0; i < 20; i++)
			{



				if (array[i] == 'U' && array[i + 1] == 'U' && array[i + 2] == 'T' && chsum == checkFor)
				{




					///////////0 u//1 u//2 t//3 ido1//4 ido2//5 ido3//6 ido4
					uint ido = (uint)array[i + 3] << 24 | (uint)array[i + 4] << 16 | (uint)array[i + 5] << 8 | (uint)array[i + 6];
					double idod = Convert.ToDouble(ido) / 1000;
					returnArray[0] = returnArray[1] = idod;
					Console.WriteLine(idod);


					uint temp = 0;
					double tempd = 0;
					
					//////////////////////////////////////7,8
					temp = 0;
					temp = temp | (uint)array[7] << 8;
					temp = temp | (uint)array[8];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 175.975 - 180;		//0xf777/360-180
					returnArray[2] = tempd;

					//////////////////////////////////////9,10
					temp = 0;
					temp = temp | (uint)array[9] << 8;
					temp = temp | (uint)array[10];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 175.975 - 90;		//0xf777/360-90
					returnArray[3] = tempd;

					//////////////////////////////////////11,12
					temp = 0;
					temp = temp | (uint)array[11] << 8;
					temp = temp | (uint)array[12];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 175.975 - 180;		//0xf777/360-180
					returnArray[4] = tempd;

					//////////////////////////////////////13,14
					temp = 0;
					temp = temp | (uint)array[13] << 8;
					temp = temp | (uint)array[14];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 126.702 - 250;		//0xf777/360-250
					returnArray[5] = tempd;

					//////////////////////////////////////15,16
					temp = 0;
					temp = temp | (uint)array[15] << 8;
					temp = temp | (uint)array[16];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 126.702 - 250;		//0xf777/360-250
					returnArray[6] = tempd;

					//////////////////////////////////////17,18
					temp = 0;
					temp = temp | (uint)array[17] << 8;
					temp = temp | (uint)array[18];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 126.702 - 250;		//0xf777/360-250
					returnArray[7] = tempd;

					//////////////////////////////////////19,20
					temp = 0;
					temp = temp | (uint)array[19] << 8;
					temp = temp | (uint)array[20];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 12670.2 - 2.5;		
					returnArray[8] = tempd;
					
					//////////////////////////////////////21,22
					temp = 0;
					temp = temp | (uint)array[21] << 8;
					temp = temp | (uint)array[22];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 12670.2 - 2.5;		
					returnArray[9] = tempd;

					//////////////////////////////////////23,24
					temp = 0;
					temp = temp | (uint)array[23] << 8;
					temp = temp | (uint)array[24];

					tempd = Convert.ToDouble(temp);
					tempd = tempd / 12670.2 - 2.5;		
					returnArray[10] = tempd;

					//////////////////////////////////////25,26
					uint north = 0;
					north = north | (uint)array[i + 25] << 8;
					north = north | (uint)array[i + 26];

					double Ncomp = Convert.ToDouble(north);
					Ncomp = Ncomp / 81.9175 - 200;
					returnArray[11] = Ncomp;

					//////////////////////////////////////27,28
					uint east = 0;
					east = east | (uint)array[i + 27] << 8;
					east = east | (uint)array[i + 28];

					double Ecomp = Convert.ToDouble(east);
					Ecomp = Ecomp / 81.9175 - 200;
					returnArray[12] = Ecomp;
					Console.WriteLine("Y  :" + Ecomp);


					////////////////////////////////////////29,30
					uint speedZ = 0;
					speedZ = speedZ | (uint)array[i + 29] << 8;
					speedZ = speedZ | (uint)array[i + 30];

					double Zcomp = Convert.ToDouble(speedZ);
					Zcomp = Zcomp / 81.9175 - 200;
					Zcomp *= -1;
					returnArray[13] = Zcomp;
					Console.WriteLine("Z  :" + Zcomp);

					double speed = Math.Sqrt(Ncomp * Ncomp + Ecomp * Ecomp) * 3.6;
					Console.WriteLine("Spe:" + speed);
					returnArray[75] = speed;


					//calculate heading
					double heading=0;

					if (Ecomp > 0 && Ncomp > 0)
					{
						heading = Math.Atan(Ecomp * Ncomp);
					}
					else if (Ecomp > 0 && Ncomp < 0)
					{
						heading = Math.Atan(Ecomp * Math.Abs(Ncomp)) + 90;
					}
					else if (Ecomp < 0 && Ncomp < 0)
					{
						heading = Math.Atan(Math.Abs(Ecomp) * Math.Abs(Ncomp)) + 180;
					}
					else if (Ecomp < 0 && Ncomp > 0)
					{
						heading = Math.Atan(Math.Abs(Ecomp) * Ncomp) + 270;
					}
					returnArray[76] = heading;


					Console.WriteLine("HDG:" + heading);




					///////////////31,32
					uint lat = 0;
					lat = lat | (uint)array[i + 31] << 24;
					lat = lat | (uint)array[i + 32] << 16;
					lat = lat | (uint)array[i + 33] << 8;
					lat = lat | (uint)array[i + 34];

					double latd = Convert.ToDouble(lat);
					latd = latd / 11930464.7056 - 90;		//0xf777/180-90
					returnArray[14] = latd;
					Console.WriteLine("Lat: " + latd);


					///////////////33,34
					uint lon = 0;
					lon = lon | (uint)array[i + 35] << 24;
					lon = lon | (uint)array[i + 36] << 16;
					lon = lon | (uint)array[i + 37] << 8;
					lon = lon | (uint)array[i + 38];

					double lond = Convert.ToDouble(lon);
					lond = lond / 11930464.7056 - 90;		//0xf777/360-180
					returnArray[15] = lond;
					Console.WriteLine("Lon: " + lond);


					///////////////35,36
					uint alt = 0;
					alt = alt | (uint)array[i + 39] << 24;
					alt = alt | (uint)array[i + 40] << 16;
					alt = alt | (uint)array[i + 41] << 8;
					alt = alt | (uint)array[i + 42];

					double altd = Convert.ToDouble(alt);
					altd = altd / 212622.143267 - 100; //0xf777/
					returnArray[16] = altd;
					Console.WriteLine("Alt: " + altd + "\n");


					///////////////byte:38-140 sendpacket_index:17-68
					
					
					int arrayIndex = 43;
					for (int j = 17; j < 68; j++)
					{
						temp = temp | (uint)array[arrayIndex++] << 8;
						temp = temp | (uint)array[arrayIndex++];

						tempd = Convert.ToDouble(temp);
						tempd = tempd - 63351; //0xf777/
						returnArray[j] = tempd;
					}

					///////////////139,140,141,142 változás: +6
					temp = (uint)array[145] << 24 | (uint)array[146] << 16 | (uint)array[147] << 8 | (uint)array[148];
					tempd = Convert.ToDouble(temp);
					
					returnArray[68] = returnArray[69] = tempd;

					
					///////////////byte:144-153 sendpacket_index:70-74
					arrayIndex = 149;
					for (int j = 70; j < 75; j++)
					{
						temp = 0;
						temp = temp | (uint)array[arrayIndex++] << 8;
						temp = temp | (uint)array[arrayIndex++];

						tempd = Convert.ToDouble(temp);
						tempd = tempd - 63351; //0xf777/
						returnArray[j] = tempd;
					}
				}
			}
			return returnArray;
		}
	}
}
