﻿//-----------------------------------------------------------------------
// <copyright file="DataParser.cs" company="Stefan Meyre>
//     Copyright (c) 2016 Stefan Meyre. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Data;

namespace VisualControlV3
{
    /// <summary>
    /// Extracts from the received raw data the values
    /// </summary>
    /// <param name="incommingData">byte array with the data bytes from the serial com buffer</param>
    public class DataParser
    {
        /// <summary>
        /// Extracts from the received raw data the values  
        /// </summary>
        /// <param name="incommingData">byte array with the data bytes from the serial com buffer</param>
        /// <returns>no return value</returns>
        public static ReceivedRawData ParseIncommingData(byte[] incommingData)
        {
            var receivedRawData = new ReceivedRawData();
            var data = incommingData;
            byte[] nextRxByte = new byte[1];
            byte[] dataBoxByte = new byte[11];
            int commandIsReady = 0;
            int dataIsReady = 0;
            int receivedBytesCounter = 0;


            foreach (var abyte in data)
            {
                nextRxByte[0] = abyte;
                dataBoxByte[receivedBytesCounter] = nextRxByte[0];
                receivedBytesCounter++;

                if (receivedBytesCounter > 10)
                {
                    receivedBytesCounter = 1;
                }

                if (dataBoxByte[receivedBytesCounter - 1] == '#')
                {
                    receivedBytesCounter = 1;
                    commandIsReady = 0;
                    dataIsReady = 0;
                }

                if (commandIsReady == 1)
                {
                    if ((dataBoxByte[4] == '1') && (receivedBytesCounter == 7))
                    {
                        dataIsReady = 1; // only needed if a one byte sensor data value is used
                    }

                    if ((dataBoxByte[4] == '2') && (receivedBytesCounter == 8))
                    {
                        dataIsReady = 1;
                    }

                    if ((dataBoxByte[4] == '4') && (receivedBytesCounter == 10))
                    {
                        dataIsReady = 1;
                    }
                }

                if (dataBoxByte[receivedBytesCounter - 1] == '/')
                {
                    commandIsReady = 1;
                }

                if ((commandIsReady & dataIsReady) == 1) //used to be only if dataIsReady
                {
                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'a')
                        {
                            if (dataBoxByte[2] == 'l')
                            {
                                if (dataBoxByte[3] == 'p')
                                {
                                    receivedRawData.AlpMsb = dataBoxByte[7];
                                    receivedRawData.AlpLsb = dataBoxByte[6];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'a')
                        {
                            if (dataBoxByte[2] == 'l')
                            {
                                if (dataBoxByte[3] == 'g')
                                {
                                    receivedRawData.AlgB1 = dataBoxByte[6];
                                    receivedRawData.AlgB2 = dataBoxByte[7];
                                    receivedRawData.AlgB3 = dataBoxByte[8];
                                    receivedRawData.AlgB4 = dataBoxByte[9];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'c')
                        {
                            if (dataBoxByte[2] == 'v')
                            {
                                if (dataBoxByte[3] == 'g')
                                {
                                    receivedRawData.CvgB1 = dataBoxByte[6];
                                    receivedRawData.CvgB2 = dataBoxByte[7];
                                    receivedRawData.CvgB3 = dataBoxByte[8];
                                    receivedRawData.CvgB4 = dataBoxByte[9];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'v')
                        {
                            if (dataBoxByte[2] == 'l')
                            {
                                if (dataBoxByte[3] == 't')
                                {
                                    receivedRawData.VltMsb = dataBoxByte[7];
                                    receivedRawData.VltLsb = dataBoxByte[6];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'y')
                        {
                            if (dataBoxByte[2] == 'a')
                            {
                                if (dataBoxByte[3] == 'n')
                                {
                                    receivedRawData.YanMsb = dataBoxByte[7];
                                    receivedRawData.YanLsb = dataBoxByte[6];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'r')
                        {
                            if (dataBoxByte[2] == 'a')
                            {
                                if (dataBoxByte[3] == 'n')
                                {
                                    receivedRawData.RanMsb = dataBoxByte[7];
                                    receivedRawData.RanLsb = dataBoxByte[6];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'p')
                        {
                            if (dataBoxByte[2] == 'a')
                            {
                                if (dataBoxByte[3] == 'n')
                                {
                                    receivedRawData.PanMsb = dataBoxByte[7];
                                    receivedRawData.PanLsb = dataBoxByte[6];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'l')
                        {
                            if (dataBoxByte[2] == 'a')
                            {
                                if (dataBoxByte[3] == 't')
                                {
                                    receivedRawData.LatB1 = dataBoxByte[6];
                                    receivedRawData.LatB2 = dataBoxByte[7];
                                    receivedRawData.LatB3 = dataBoxByte[8];
                                    receivedRawData.LatB4 = dataBoxByte[9];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'l')
                        {
                            if (dataBoxByte[2] == 'o')
                            {
                                if (dataBoxByte[3] == 'n')
                                {
                                    receivedRawData.LonB1 = dataBoxByte[6];
                                    receivedRawData.LonB2 = dataBoxByte[7];
                                    receivedRawData.LonB3 = dataBoxByte[8];
                                    receivedRawData.LonB4 = dataBoxByte[9];
                                }
                            }
                        }
                    }

                    if (dataBoxByte[0] == '#')
                    {
                        if (dataBoxByte[1] == 'h')
                        {
                            if (dataBoxByte[2] == 'a')
                            {
                                if (dataBoxByte[3] == 'g')
                                {
                                    receivedRawData.HagB1 = dataBoxByte[6];
                                    receivedRawData.HagB2 = dataBoxByte[7];
                                    receivedRawData.HagB3 = dataBoxByte[8];
                                    receivedRawData.HagB4 = dataBoxByte[9];
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 11; i++)
                   {
                         dataBoxByte[i] = 0;
                   }
            commandIsReady = dataIsReady = receivedBytesCounter = 0;
              
            return receivedRawData;
        }
    }
 }
 
