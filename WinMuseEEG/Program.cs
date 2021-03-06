﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOSC;
using WinMuseEEG.Models;

namespace WinMuseEEG
{
	
	class Program
	{
		public event EventHandler<EegEventArgs> OnEeg;
		static void Main(string[] args)
		{
			// Callback function for received OSC messages. 
			// Prints EEG and Relative Alpha data only.
			HandleOscPacket callback = delegate (OscPacket packet)
			{
				var messageReceived = (OscMessage)packet;
				var addr = messageReceived.Address;
				if (addr == "/muse/eeg")
				{
					//					Console.Write("EEG values: ");
					var eeg = new Eeg();
//					foreach (var arg in messageReceived.Arguments)
//					{
//						Console.Write(arg + " ");
						

//					}
					OnEeg(this, new EegEventArgs(){Eeg = messageReceived.Arguments });


				}
//				if (addr == "/muse/elements/alpha_relative")
//				{
//					Console.Write("Relative Alpha power values: ");
//					foreach (var arg in messageReceived.Arguments)
//					{
//						Console.Write(arg + " ");
//					}
//				}
			};

			// Create an OSC server.
			var listener = new UDPListener(5000, callback);

			Console.WriteLine("Press enter to stop");
			Console.ReadLine();
			listener.Close();
		}
	}
}
