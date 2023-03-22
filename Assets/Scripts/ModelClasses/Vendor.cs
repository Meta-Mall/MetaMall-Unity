using System;

[Serializable]
public class Vendor : User {
	
	public string Address { get; private set; }

	public Vendor(string address) {
		Address = address;
	}

}
