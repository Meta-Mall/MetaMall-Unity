using System;

[Serializable]
public class Customer : User {

	public string userName;
	public string email;
	public string password;
	public string country;
	public string city;
	public string gender;
	public string photoURL;

	bool isGuest = false;

	public Customer() { 
		isGuest = true;
		userName = "Guest";
	}

	public Customer(string userName, string email, string password, string country, string city, string gender) {
		this.userName = userName;
		this.email = email;
		this.password = password;
		this.country = country;
		this.city = city;
		this.gender = gender;
	}
}
