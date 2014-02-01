using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TestNet : MonoBehaviour {

	void OnGUI() {
		if (GUILayout.Button("Start Server"))
		{
			string strIp = "192.168.0.28";
			int port = 19855;
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			
			IPAddress ip = IPAddress.Parse(strIp);
			IPEndPoint endPoint = new IPEndPoint(ip, port);
			
			//socket.Connect(endPoint);
			
			byte[] sBuffer = Encoding.UTF8.GetBytes("can you see me?");
			
			//socket.Send(sBuffer, sBuffer.Length, SocketFlags.None);
			
			socket.SendTo(sBuffer, endPoint);
			socket.Close();
		}
	}
}
